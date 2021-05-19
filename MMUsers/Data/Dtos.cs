using System;

namespace MMUsers.Data
{
    public record UserReadDto(Guid Id, string FirstName, string LastName, string Email, string Address);
    public record UserUpsertDto(string FirstName, string LastName, string Email, string Address);
}