using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Models;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Extensions;

public static class ModelExtensions
{
    public static User ToUserWithoutId(this RegisterModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new User
        {
            Age = model.Age,
            Email = model.Email,
            Fullname = model.Fullname,
            Gender = model.Gender,
            Height = model.Height,
            Login = model.Login,
            Password = model.Password,
            Weight = model.Weight,
            Role = Role.User
        };
    }
}