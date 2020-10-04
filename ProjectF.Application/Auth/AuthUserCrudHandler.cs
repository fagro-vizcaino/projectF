using ProjectF.Data.Repositories;
using System;
using System.Linq;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.Auth;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.Auth
{
    public class AuthUserCrudHandler
    {
        readonly UserRepository _userRepository;
        readonly CountryRepository _countryRepository;

        public AuthUserCrudHandler(UserRepository userRepository, CountryRepository countryRepository)
        {
            _userRepository = userRepository;
            _countryRepository = countryRepository;
        }

        public Either<Error, Unit> Register(UserDto user)
           => GeneratePasswordHash(user)
               .Bind(ValidateEmail)
               .Bind(ValidateFullname)
               .Bind(ValidateCountry)
               .Bind(SetCountry)
               .Bind(Exits)
               .Bind(CreateEntity)
               .Bind(Add)
               .Bind(Save);

        public Either<Error, UserDto> Login(UserDto user)
          => FindUser(user)
            .Bind(VerifyPasswordHash)
            .Match<Either<Error, UserDto>>(
                Left: err => Error.New(err.Message),
                Right: user => user
            );

        Either<Error, UserDto> ValidateEmail(UserDto user)
          => Email.Of(user.Email)
            .Match<Either<Error, UserDto>>(
              Left: e => Error.New(e.Message),
              Right: em => user
            );

        Either<Error, UserDto> ValidateCountry(UserDto user)
        {
            if (user.Country == null && user.SelectedCountry == 0)
                return Error.New("country is required");

            return user;
        }

        Either<Error, UserDto> SetCountry(UserDto user)
        {
            var country = _countryRepository.FromCountryId(user.SelectedCountry);
            if (country == null) return Error.New("couldn't find to country");

            user.Country = country;
            return user;
        }

        Either<Error, UserDto> ValidateFullname(UserDto user)
        {
            if (string.IsNullOrEmpty(user.Fullname)
             || string.IsNullOrWhiteSpace(user.Fullname)
             || user.Fullname.Length > 200)
                return Error.New("fullname not valid");

            return user;
        }

        Either<Error, UserDto> Exits(UserDto user)
        {
            if (_userRepository.UserExits(user.Email)) return Error.New("Email Already exits");
            return user;
        }

        Either<Error, UserDto> FindUser(UserDto user)
          => _userRepository.FindByUsername(user.Email.ToLower())
                                  .Match<Either<Error, UserDto>>(
                                      Left: e => Error.New(e.Message),
                                      Right: u => new UserDto(id: u.Id
                                      , u.Email
                                      , u.Fullname
                                      , u.Country.Id
                                      , u.Country
                                      , user.Password
                                      , u.PasswordHash
                                      , u.PasswordSalt))
                                  .Bind(SetCountry);

        Either<Error, UserDto> VerifyPasswordHash(UserDto user)
        {
            try
            {
                using var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Password));
                if (computedHash.SequenceEqual(user.PasswordHash)) return user;

                return Error.New("verification fail");
            }
            catch (System.Exception ex)
            {
                return Error.New(ex.Message);
            }
        }

        Either<Error, UserDto> GeneratePasswordHash(UserDto user)
        {
            try
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Password));
                    return user;
                }
            }
            catch (System.Exception ex)
            {
                return Error.New(ex.Message);
            }

        }

        Either<Error, User> CreateEntity(UserDto user)
          => new User(user.Fullname
          , new Email(user.Email)
          , user.Country
          , user.PasswordHash
          , user.PasswordSalt);

        Either<Error, Unit> Add(User user)
        {
            try
            {
                _userRepository.Add(user);
                return Unit.Default;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Unit> Save(Unit unit)
        {
            try
            {
                _userRepository.Save();
                return Unit.Default;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}