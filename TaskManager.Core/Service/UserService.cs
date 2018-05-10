using System;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Core.Service
{
    public class UserService : ServiceBase<TB_User>, IUserService
    {

        private readonly IUserRespository _userRepository;

        public UserService(IUserRespository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public TB_User GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }

        public TB_User GetUserByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetUserByEmailAndPassword(email, password);
        }

        public TB_User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public bool ValidateEmailExists(int ID,string email)
        {
            TB_User user = _userRepository.GetUserByEmail(email);

            bool _return = false;

            if (user != null)
                return user.ID != ID ? _return = true :  _return = false;

            return _return;
        }
        
    }
}
