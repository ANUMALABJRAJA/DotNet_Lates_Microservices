using System.Runtime.InteropServices;
using Dapper;
using eCommerece.API.Core;
using eCommerece.API.Core.Entities;
using eCommerece.API.DTO;
using eCommerece.API.Infrastruture.DbContext;

namespace eCommerece.API.Infrastruture.Repositories{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _dapper;

        public UserRepository(DapperDbContext dapper){
            _dapper = dapper;
        }


        public async Task<ApplicationUser?> AddUser(ApplicationUser user)
        {
            user.UserId = Guid.NewGuid();

            string query = "INSERT INTO public.\"Users\"(\"UserId\", \"Email\", \"Password\", \"PersonName\", \"Gender\") VALUES (@UserID, @Email, @Password,@PersonName, @Gender)";
            int rowsCountAffected =  await _dapper.DbConnection.ExecuteAsync(query, user);

            if(rowsCountAffected > 0)
            {
                return user;
            }
            else{
                return null;
            }
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
        {
            string query = "SELECT * FROM public.\"Users\" WHERE \"Email\" =@Email AND \"Password\" = @Password ";
            var parameter = new {Email = email, Password = password};
            ApplicationUser? user = await _dapper.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameter
            );

            // ApplicationUser user = new ApplicationUser()
            // {
            //     UserId = Guid.NewGuid(),
            //     Email = "dummy@gmail.com",
            //     Password = "1234",
            //     Gender = GenderOptions.Male.ToString(),
            //     PersonName = "DummyPerson"
            // };

            return user;
        }
    }
}