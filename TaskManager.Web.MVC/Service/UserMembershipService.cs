using System;
using System.Web.Security;
using TaskManager.Web.MVC.Service.Interfaces;

namespace TaskManager.Web.MVC.Service 
{

    public class UserMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public UserMembershipService()
            : this(null)
        {
        }

        public UserMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
           
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }
        
    }

}
