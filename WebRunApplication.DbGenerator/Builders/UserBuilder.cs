using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums;

namespace WebRunApplication.DbGenerator.Builders;

internal class UserBuilder
{
    private int? Id { get; set; } 
    private string? Login { get; set; }
    private string? Password { get; set; }
    private string? Email { get; set; }
    private string? Fullname { get; set; }
    private int? Age { get; set; }
    private Gender? Gender { get; set; }
    private uint? Weight { get; set; }
    private uint? Height { get; set; }
    private Role? Role { get; set; }

    public UserBuilder WithId(int id)
    {
        Id = id;
        
        return this;
    }

    public UserBuilder WithLogin(string login)
    {
        Login = login;

        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        Password = password;

        return this;
    }
    
    public UserBuilder WithEmail(string email)
    {
        Email = email;

        return this;
    }
    
    public UserBuilder WithFullname(string fullname)
    {
        Fullname = fullname;

        return this;
    }
    
    public UserBuilder WithAge(int age)
    {
        Age = age;

        return this;
    }
    
    public UserBuilder WithGender(Gender gender)
    {
        Gender = gender;

        return this;
    }
    
    public UserBuilder WithWeight(uint weight)
    {
        Weight = weight;

        return this;
    }
    
    public UserBuilder WithHeight(uint height)
    {
        Height = height;

        return this;
    }
    
    public UserBuilder WithRole(Role role)
    {
        Role = role;

        return this;
    }

    public User Build()
    {
        return new User
        {
            Fullname = Fullname ?? Faker.Name.FullName(),
            Login = Login ?? Faker.Internet.UserName(),
            Password = Password ?? Faker.Internet.UserName(),
            Age = Age ?? Faker.RandomNumber.Next(16, 60),
            Email = Email ?? Faker.Internet.Email(),
            Gender = Gender ?? Domain.Enums.Gender.Female,
            Weight = Weight ?? (uint)Faker.RandomNumber.Next(50, 100),
            Height = Height ?? (uint)Faker.RandomNumber.Next(140, 230)
        };
    }
}