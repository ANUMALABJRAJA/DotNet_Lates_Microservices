using eCommerece.API.Core.Entities;

namespace eCommerece.API.Core{
    public interface IUserRepository{
        Task<ApplicationUser?> AddUser(ApplicationUser user);

        Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password);
    }
}