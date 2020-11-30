using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF.Data.Entities.Auth
{
    public static class UserMapper
    {
        public static User FromDto(RegisterUserDto dto)
            => new User(dto.FirstName
                , dto.LastName
                , dto.UserName
                , dto.Password
                , dto.Email
                , dto.PhoneNumber
                , dto.Country);

        public static RegisterUserDto FromEntity(User entity)
            => new RegisterUserDto(entity.Id
                , entity.Firstname
                , entity.Lastname
                , entity.UserName
                , entity.PasswordHash
                , entity.Email
                , entity.PhoneNumber
                , null
                , entity.Country?.Id ?? 0
                , entity.Country);
    }
}
