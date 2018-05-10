using System;
using System.Linq;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Data.Repositories
{
    public class UserRepository : RepositoryBase<TB_User>, IUserRespository
    {
        public TB_User GetUserByName(string name)
        {
            return Db.TB_User.SingleOrDefault(u => u.name == name);
        }

        public TB_User GetUserByEmailAndPassword(string email, string password)
        {
            return Db.TB_User.SingleOrDefault(u => u.email == email && u.password == password);
        }


        public TB_User GetUserByEmail(string email)
        {
            return Db.TB_User.SingleOrDefault(u => u.email == email);
        }

    }
}
