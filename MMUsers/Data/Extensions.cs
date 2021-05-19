using System;

namespace MMUsers.Data
{
    public static class Extensions
    {
        public static UserReadDto AsDto(this User user)
        {
            return new UserReadDto(user.Id, user.FirstName, user.LastName, user.Email, user.Address);
        }

        public static User AsUser(this UserUpsertDto userUpsertDto)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Address = userUpsertDto.Address,
                Email = userUpsertDto.Email,
                FirstName = userUpsertDto.FirstName,
                LastName = userUpsertDto.LastName
            };
        }

        public static User Edit(this User user, UserUpsertDto userUpsertDto)
        {
            user.Address = userUpsertDto.Address;
            user.Email = userUpsertDto.Email;
            user.FirstName = userUpsertDto.FirstName;
            user.LastName = userUpsertDto.LastName;
            return user;
        }
    }
}