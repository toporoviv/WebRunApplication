using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Services.Models
{
    // todo: сомнительно выглядит
    public class UserViewModel // : User
    {
        public List<TrainingInformation> Trainings { get; init; } = new();

        public required User User { get; init; }
    }
}
