using Dapper;
using Microsoft.Extensions.Options;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Infrastructure.Options;

namespace WebRunApplication.Infrastructure.Repositories
{
    internal class UserRepository(IOptions<PostgreOptions> postgreSettings)
        : DbRepository(postgreSettings.Value),
            IUserRepository
    {
        public async Task<User> CreateUserAsync(Models.User user, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(user);
            
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @$"
insert into users (login,
                   password,
                   email,
                   fullname,
                   age,
                   gender,
                   weight,
                   height,
                   role)
            values (@{nameof(user.Login)},
                    @{nameof(user.Password)},
                    @{nameof(user.Email)}),
                    @{nameof(user.Fullname)},
                    @{nameof(user.Age)},
                    @{nameof(user.Gender)},
                    @{nameof(user.Weight)},
                    @{nameof(user.Height)},
                    @{nameof(user.Role)}
        
        returning id, login, password, email, fullname, age, gender, weight, height, role";
            
            return await connection.QueryFirstAsync<User>(sqlQuery, user);
        }

        public async Task<User> UpdateUserAsync(int userId, Models.User user, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(user);
            
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"update users
                    set login = @Login,
                        password = @Password,
                        email = @Email,
                        fullname = @Fullname,
                        age = @Age,
                        gender = @Gender,
                        weight = @Weight,
                        height = @Height,
                        role = @Role
                     where id = @Id
                     returning id, login, password, email, fullname, age, gender, weight, height, role";
            
            var sqlParams = new
            {
                Id = userId,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Fullname = user.Fullname,
                Age = user.Age,
                Gender = user.Gender,
                Weight = user.Weight,
                Height = user.Height,
                Role = user.Role
            };

            return await connection.QuerySingleAsync<User>(sqlQuery, sqlParams);
        }

        public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from users where id = @Id";
            var sqlParams = new
            {
                Id = id
            };

            return await connection.QuerySingleAsync<User>(sqlQuery, sqlParams);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from users";

            return await connection.QueryAsync<User>(sqlQuery, cancellationToken);
        }

        public async Task<User?> DeleteUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"delete from users where id = @Id 
                returning id, login, password, email, fullname, age, gender, weight, height, role";
            
            var sqlParams = new
            {
                Id = id
            };

            return await connection.QueryFirstOrDefaultAsync<User>(sqlQuery, sqlParams);
        }
    }
}
