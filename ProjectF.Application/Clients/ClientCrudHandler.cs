using ProjectF.Data.Repositories;
using ProjectF.Data.Entities.Clients;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System.Collections.Generic;
using ProjectF.Data.Entities.Common.ValueObjects;
using System;

namespace ProjectF.Application.Clients
{
    public class ClientCrudHandler
    {
        readonly ClientRepository _clientRepository;
        readonly CountryRepository _countryRepository;

        public ClientCrudHandler(ClientRepository clientRepository, CountryRepository countryRepository)
        {
            _clientRepository = clientRepository;
            _countryRepository = countryRepository;
        }

        public Either<Error, Client> Create(ClientDto clientDto)
            => ValidateCode(clientDto)
            .Bind(ValidateName)
            .Bind(ValidateEmail)
            .Bind(ValidatePhone)
            .Bind(ValidateCountry)
            .Bind(SetCountry)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save);

        public Either<Error, Client> Update(long id, ClientDto clientDto)
            => ValidateIsCorrectUpdate(id, clientDto)
            .Bind(ValidateCode)
            .Bind(ValidateName)
            .Bind(ValidateEmail)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(clientDto, c))
            .Bind(Save);

        public IEnumerable<ClientDto> GetAll()
            => _clientRepository.FindAll().Map(c => (ClientDto)c);

        public Either<Error, Client> Find(long id)
         => _clientRepository.FindByKey(id).Match(Some: t => t,
             None: Left<Error, Client>(Error.New("couldn't find Client type")));

        public Either<Error, Client> Delete(long id)
          => Find(id)
            .Bind(Delete)
            .Bind(Save)
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        Either<Error, ClientDto> ValidateIsCorrectUpdate(long id, ClientDto clientDto)
        {
            if (id == clientDto.Id) return clientDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, ClientDto> ValidateCode(ClientDto clientDto)
            => Code.Of(clientDto.Code)
                .Match<Either<Error, ClientDto>>(
                    Left: err => Error.New(err.Message),
                    Right: c => clientDto);

        Either<Error, ClientDto> ValidateName(ClientDto clientDto)
            => Name.Of(clientDto.Name)
                .Match(Succ: c => clientDto,
                Fail: err => Left<Error, ClientDto>(Error.New(string.Join("; ", err))));

        Either<Error, ClientDto> ValidateEmail(ClientDto clientDto)
            => string.IsNullOrEmpty(clientDto.Email)
            ? clientDto
            : Email.Of(clientDto.Email).Match<Either<Error, ClientDto>>(
                    Left: err => Error.New(err.Message),
                    Right: s => clientDto);

        Either<Error, ClientDto> ValidatePhone(ClientDto clientDto)
            => Phone.Of(clientDto.Phone).Match<Either<Error, ClientDto>>(
                    Left: err => Error.New(err.Message),
                    Right: s => clientDto);

        Either<Error, ClientDto> ValidateCountry(ClientDto clientDto)
        {
            if (clientDto.Country == null && clientDto.SelectedCountry == 0)
                return Error.New("country is required");

            return clientDto;
        }

        Either<Error, ClientDto> SetCountry(ClientDto clientDto)
        {
            var country = _countryRepository.FromCountryId(clientDto.SelectedCountry);
            if (country == null) return Error.New("couldn't find to country");

            var nclient = clientDto.With(country: country);
            return nclient;
        }
        Either<Error, Client> CreateEntity(ClientDto clientDto)
         => Right<Error, Client>(clientDto);

        Either<Error, Client> UpdateEntity(ClientDto clientDto, Client client)
        {
            var code = new Code(clientDto.Code);
            var name = new Name(clientDto.Name);
            var email = new Email(clientDto.Email);
            var phone = new Phone(clientDto.Phone);

            client.EditUserClient(code,
                    name,
                    email,
                    phone,
                    clientDto.Rnc,
                    clientDto.Birthday,
                    clientDto.HomeOrApartment,
                    clientDto.City,
                    clientDto.Street,
                    clientDto.Country);

            return client;
        }

        Either<Error, Client> Add(Client client)
        {
            try
            {
                _clientRepository.Add(client);
                return client;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Client> Save(Client client)
        {
            try
            {
                _clientRepository.Save();
                return client;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Client> Delete(Client client)
        {
            try
            {
                _clientRepository.Delete(client);
                return client;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
