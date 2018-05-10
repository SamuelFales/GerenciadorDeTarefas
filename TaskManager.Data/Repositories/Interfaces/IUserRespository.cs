using TaskManager.Data.Model;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface IUserRespository : IRepositoryBase<TB_User>
    {
        TB_User GetUserByName(string name);
        TB_User GetUserByEmailAndPassword(string email, string password);
        TB_User GetUserByEmail(string email);

    }
}
