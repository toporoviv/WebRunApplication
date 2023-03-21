using WebRunApplication.DataEntity;

namespace WebRunApplication.Models
{
    public class UserViewModel : User
    {
        public List<TrainingInformation> Trainings { get; set; }

        public User User { get; set; }
    }
}
