
using TaskManager.Data.Model;
namespace TaskManager.Core.Service.Interfaces
{
    public interface IUserService : IServiceBase<TB_User>
    {
        TB_User GetUserByName(string name);
        TB_User GetUserByEmailAndPassword(string email, string password);
        TB_User GetUserByEmail(string email);
        bool ValidateEmailExists(int ID,string email);
      
    }
}
