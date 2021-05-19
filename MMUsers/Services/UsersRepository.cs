using System;
using System.Collections.Generic;
using System.Linq;
using MMUsers.Data;

namespace MMUsers.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _db;

        public UsersRepository(UsersContext db)
        {
            _db = db;
        }


        public User Add(User entity)
        {
            _db.Users.Add(entity);
            Save();
            return entity;
        }

        public void Delete(User entity)
        {
            _db.Users.Remove(entity);
            Save();
        }

        public User Edit(User entity)
        {
            _db.Users.Update(entity);
            Save();
            return entity;
        }

        public IList<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User GetSingle(Func<User, bool> condition)
        {
            return _db.Users.FirstOrDefault(condition);
        }

        public void Save() =>_db.SaveChanges();
    }
}