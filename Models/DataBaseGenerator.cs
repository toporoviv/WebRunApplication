using WebRunApplication.DataEntity;
using WebRunApplication.DataEntity.Forum;

namespace WebRunApplication.Models
{
    public static class DataBaseGenerator
    {
        public static List<User> GenerateUsers()
        {
            return new List<User>
            {
                new User
                {
                    Login = "степанстоляров1971",
                    Password = "1021971",
                    Fullname = "Степан Столяров",
                    Age = 51,
                    Gender = Enums.Gender.Male,
                    Weight = 61,
                    Height = 129
                },
                new User
                {
                    Login = "нинамитрофанова2002",
                    Password = "402002",
                    Fullname = "Нина Митрофанова",
                    Age = 20,
                    Gender = Enums.Gender.Female,
                    Weight = 110,
                    Height = 152,
                    Email = "gurkinada2003@mail.ru"
                },
                new User
                {
                    Login = "леониддолгов1988",
                    Password = "681988",
                    Fullname = "Леонид Долгов",
                    Age = 34,
                    Gender = Enums.Gender.Male,
                    Weight = 67,
                    Height = 177,
                    Email = "ya.toporow@gmail.com",
                    Role = Enums.Role.Admin
                },
                new User
                {
                    Login = "таисияпозднякова1971",
                    Password = "1021971",
                    Fullname = "Таисия Позднякова",
                    Age = 51,
                    Gender = Enums.Gender.Female,
                    Weight = 101,
                    Height = 112
                },
                new User
                {
                    Login = "евдокияшевелева1968",
                    Password = "1081968",
                    Fullname = "Евдокия Шевелева",
                    Age = 54,
                    Gender = Enums.Gender.Female,
                    Weight = 119,
                    Height = 154
                },
                new User
                {
                    Login = "валерияяшина1986",
                    Password = "721986",
                    Fullname = "Валерия Яшина",
                    Age = 36,
                    Gender = Enums.Gender.Female,
                    Weight = 67,
                    Height = 195
                },
                new User
                {
                    Login = "олегполяков1975",
                    Password = "941975",
                    Fullname = "Олег Поляков",
                    Age = 47,
                    Gender = Enums.Gender.Male,
                    Weight = 93,
                    Height = 142
                },
                new User
                {
                    Login = "сергейстариков1964",
                    Password = "1161964",
                    Fullname = "Сергей Стариков",
                    Age = 58,
                    Gender = Enums.Gender.Male,
                    Weight = 78,
                    Height = 176
                },
                new User
                {
                    Login = "антонбондарев1978",
                    Password = "881978",
                    Fullname = "Антон Бондарев",
                    Age = 44,
                    Gender = Enums.Gender.Male,
                    Weight = 83,
                    Height = 159
                },
                new User
                {
                    Login = "станиславлаврентьев1974",
                    Password = "961974",
                    Fullname = "Станислав Лаврентьев",
                    Age = 48,
                    Gender = Enums.Gender.Male,
                    Weight = 123,
                    Height = 174
                }
            };
        }

        public static List<Indicator> GenerateIndicators()
        {
            return new List<Indicator>
            {
                new Indicator
                {
                    UserId = 1,
                    Date = new DateTime(2022, 6, 1),
                    Pressure = null,
                    Duration = new TimeSpan(1, 30, 0),
                    Calories = 650,
                    AverageSpeed = 5.4,
                    MinimumPulse = 95,
                    AveragePulse = 135,
                    MaximumPulse = 170,
                    Steps = 7656
                },
                new Indicator
                {
                    UserId = 3,
                    Date = new DateTime(2022, 7, 15),
                    Pressure = null,
                    Duration = new TimeSpan(0, 55, 12),
                    Calories = 420,
                    AverageSpeed = 6.3,
                    MinimumPulse = 92,
                    AveragePulse = 132,
                    MaximumPulse = 154,
                    Steps = 4531
                },
                new Indicator
                {
                    UserId = 5,
                    Date = new DateTime(2022, 7, 16),
                    Pressure = null,
                    Duration = new TimeSpan(1, 20, 23),
                    Calories = 543,
                    AverageSpeed = 6.2,
                    MinimumPulse = 99,
                    AveragePulse = 143,
                    MaximumPulse = 157,
                    Steps = 8035
                },
                new Indicator
                {
                    UserId = 7,
                    Date = new DateTime(2022, 7, 22),
                    Pressure = null,
                    Duration = new TimeSpan(0, 45, 23),
                    Calories = 431,
                    AverageSpeed = 7.5,
                    MinimumPulse = 87,
                    AveragePulse = 127,
                    MaximumPulse = 156,
                    Steps = 6735
                },
                new Indicator
                {
                    UserId = 9,
                    Date = new DateTime(2022, 7, 23),
                    Pressure = null,
                    Duration = new TimeSpan(1, 10, 0),
                    Calories = 356,
                    AverageSpeed = 6.9,
                    MinimumPulse = 96,
                    AveragePulse = 132,
                    MaximumPulse = 166,
                    Steps = 7455
                },
                new Indicator
                {
                    UserId = 3,
                    Date = new DateTime(2022, 9, 1),
                    Pressure = null,
                    Duration = new TimeSpan(0, 58, 0),
                    Calories = 301,
                    AverageSpeed = 5.3,
                    MinimumPulse = 103,
                    AveragePulse = 153,
                    MaximumPulse = 178,
                    Steps = 6742
                },
                new Indicator
                {
                    UserId = 3,
                    Date = new DateTime(2022, 9, 10),
                    Pressure = null,
                    Duration = new TimeSpan(1, 10, 0),
                    Calories = 431,
                    AverageSpeed = 5.8,
                    MinimumPulse = 113,
                    AveragePulse = 143,
                    MaximumPulse = 157,
                    Steps = 8146
                },
                new Indicator
                {
                    UserId = 3,
                    Date = new DateTime(2022, 9, 15),
                    Pressure = null,
                    Duration = new TimeSpan(0, 50, 0),
                    Calories = 335,
                    AverageSpeed = 6.8,
                    MinimumPulse = 103,
                    AveragePulse = 135,
                    MaximumPulse = 151,
                    Steps = 5765
                },
                new Indicator
                {
                    UserId = 3,
                    Date = new DateTime(2022, 9, 18),
                    Pressure = null,
                    Duration = new TimeSpan(0, 53, 0),
                    Calories = 421,
                    AverageSpeed = 6.3,
                    MinimumPulse = 98,
                    AveragePulse = 131,
                    MaximumPulse = 149,
                    Steps = 6765
                }
            };
        }

        public static List<Help> GenerateHelps()
        {
            return new List<Help>
            {
                new Help { UserId = 1, Question = "Как выйти из приложения?", Answer = "...", Date = new DateTime(2022, 5, 23) },
                new Help
                {
                    UserId = 3,
                    Question = "А можно ли просмотреть статистику предыдущих тренировок?",
                    Answer = "Да, можно воспользоваться разделом поиск и ввести необходимые параметры запроса",
                    Date = new DateTime(2022, 7, 11)
                },
                new Help
                {
                    UserId = 5,
                    Question = "Рекомендации по тренировке можно назвать хорошими?",
                    Answer = "Нет, конечно",
                    Date = new DateTime(2022, 7, 12)
                },
                new Help { UserId = 7, Question = "Можно ли вводить показатели вручную?", Answer = "Нет", Date = new DateTime(2022, 8, 11) },
                new Help
                {
                    UserId = 9,
                    Question = "До вас дошло мое сообщение, написанное 4 месяца назад?",
                    Answer = null,
                    Date = new DateTime(2022, 8, 5)
                }
            };
        }

        public static List<TemplateType> GenerateTemplateTypes()
        {
            return new List<TemplateType>
            {
                new TemplateType { TemplateId = 1, TrainingTypeId = 8 },
                new TemplateType { TemplateId = 1, TrainingTypeId = 7 },
                new TemplateType { TemplateId = 1, TrainingTypeId = 9 },
                new TemplateType { TemplateId = 2, TrainingTypeId = 8 },
                new TemplateType { TemplateId = 2, TrainingTypeId = 3 },
                new TemplateType { TemplateId = 2, TrainingTypeId = 4 },
                new TemplateType { TemplateId = 2, TrainingTypeId = 9 },
                new TemplateType { TemplateId = 3, TrainingTypeId = 8 },
                new TemplateType { TemplateId = 3, TrainingTypeId = 1 },
                new TemplateType { TemplateId = 4, TrainingTypeId = 8 },
                new TemplateType { TemplateId = 4, TrainingTypeId = 4 },
                new TemplateType { TemplateId = 4, TrainingTypeId = 5 },
                new TemplateType { TemplateId = 4, TrainingTypeId = 9 },
                new TemplateType { TemplateId = 5, TrainingTypeId = 8 },
                new TemplateType { TemplateId = 5, TrainingTypeId = 6 },
                new TemplateType { TemplateId = 5, TrainingTypeId = 9 }
            };
        }

        public static List<Training> GenerateTrainings()
        {
            return new List<Training>
            {
                new Training { Date = new DateTime(2022, 6, 1), TrainTemplateId = 1, Duration = new TimeSpan(1, 30, 0) },
                new Training { Date = new DateTime(2022, 7, 15), TrainTemplateId = 1, Duration = new TimeSpan(0, 55, 12) },
                new Training { Date = new DateTime(2022, 7, 16), TrainTemplateId = 2, Duration = new TimeSpan(1, 20, 23) },
                new Training { Date = new DateTime(2022, 7, 22), TrainTemplateId = 2, Duration = new TimeSpan(0, 45, 23) },
                new Training { Date = new DateTime(2022, 7, 23), TrainTemplateId = 3, Duration = new TimeSpan(0, 10, 0) },
                new Training { Date = new DateTime(2022, 7, 23), TrainTemplateId = 3, Duration = new TimeSpan(0, 55, 0) },
                new Training { Date = new DateTime(2022, 7, 23), TrainTemplateId = 4, Duration = new TimeSpan(0, 5, 0) },
                new Training { Date = new DateTime(2022, 9, 1), TrainTemplateId = 5, Duration = new TimeSpan(0, 58, 0) },
                new Training { Date = new DateTime(2022, 9, 10), TrainTemplateId = 3, Duration = new TimeSpan(1, 10, 0) },
                new Training { Date = new DateTime(2022, 9, 15), TrainTemplateId = 2, Duration = new TimeSpan(0, 50, 0) },
                new Training { Date = new DateTime(2022, 9, 18), TrainTemplateId = 1, Duration = new TimeSpan(0, 53, 0) }
            };
        }

        public static List<TrainingTemplate> GenerateTrainingTemplate()
        {
            return new List<TrainingTemplate>
            {
                new TrainingTemplate { Title = "Тренировка 1-го типа" },
                new TrainingTemplate { Title = "Тренировка 2-го типа" },
                new TrainingTemplate { Title = "Тренировка 3-го типа" },
                new TrainingTemplate { Title = "Тренировка 4-го типа" },
                new TrainingTemplate { Title = "Тренировка 5-го типа" }
            };
        }

        public static List<TrainingType> GenerateTrainingTypes()
        {
            return new List<TrainingType>
            {
                new TrainingType
                {
                    Title = "Восстановительная тренировка",
                    Description = "это такой вид низкоинтенсивной беговой нагрузки, основной задачей которого является поддержка активного метаболизма и глобального кровообращения, что, несомненно, положительно сказывается на скорости восстановления и приобретения положительных адаптаций у спортсмена.",
                    MinimumPulse = 115,
                    MaximumPulse = 125,
                    Duration = new TimeSpan(0, 50, 0)
                },
                new TrainingType
                {
                    Title = "Продолжительный бег",
                    Description = "Вид беговой тренировки, который способствует повышению выносливости организма. Бег на длительные дистанции (от 10 до 50 км) стимулирует энергетические процессы организма, повышает эффективность сердечно-сосудистой системы, способствует повышению аэробной мощности.",
                    MinimumPulse = 135,
                    MaximumPulse = 155,
                    Duration = new TimeSpan(1, 30, 0)
                },
                new TrainingType
                {
                    Title = "Прогрессивный бег",
                    Description = "Тренировка, во время которой постепенно увеличивают скорость и закончить тренировку на скорости, большей начальной.",
                    MinimumPulse = 150,
                    MaximumPulse = 170,
                    Duration = new TimeSpan(0, 40, 0)
                },
                new TrainingType
                {
                    Title = "Фартлек",
                    Description = "разновидность интервальной циклической тренировки, которая варьирует от анаэробного спринта до аэробной медленной ходьбы или бега трусцой.",
                    MinimumPulse = 145,
                    MaximumPulse = 160,
                    Duration = new TimeSpan(1, 0, 0)
                },
                new TrainingType
                {
                    Title = "Бег в гору",
                    Description = "развивает силу и мощность ног, что позволит тебе не только карабкаться по склону, но и быстрее передвигаться по ровной местности. Кроме того, это упражнение поможет улучшить технику бега.",
                    MinimumPulse = 141,
                    MaximumPulse = 152,
                    Duration = new TimeSpan(1, 0, 0)
                },
                new TrainingType
                {
                    Title = "Темповой бег",
                    Description = "это одна из форм скоростных тренировок, которая включает в себя фартлек, повторения и интервальные тренировки",
                    MinimumPulse = 187,
                    MaximumPulse = 198,
                    Duration = new TimeSpan(1, 20, 0)
                },
                new TrainingType
                {
                    Title = "Интервальный бег",
                    Description = "Интервальный бег предполагает чередование быстрого и медленного бега или шага для восстановления пульса.",
                    MinimumPulse = 135,
                    MaximumPulse = 165,
                    Duration = new TimeSpan(1, 0, 0)
                },
                new TrainingType
                {
                    Title = "Разминка",
                    Description = "вводная часть тренировки. Подготавливает человека к более интенсивным физическим нагрузкам. Выполнение разминки может предохранять спортсмена от травм и является важной частью тренировки.",
                    MinimumPulse = 129,
                    MaximumPulse = 140,
                    Duration = new TimeSpan(0, 30, 0)
                },
                new TrainingType
                {
                    Title = "Заминка",
                    Description = "это продолжение выполнения упражнения после окончания основной части, но с пониженной интенсивностью, традиционно считается неотъемлемой частью тренировочного процесса",
                    MinimumPulse = 60,
                    MaximumPulse = 110,
                    Duration = new TimeSpan(0, 10, 0)
                }
            };
        }

        public static List<MailingTopic> GenerateMailingTopics()
        {
            return new List<MailingTopic>
            {
                new MailingTopic { Title = "Общая информация" },
                new MailingTopic { Title = "Тренировки" },
                new MailingTopic { Title = "Здоровое питание" }
            };
        }

        public static List<Mailing> GenerateMailings()
        {
            return new List<Mailing>
            {
                new Mailing { Date = DateTime.Now, MailingTopicId = 1, Message = "Первая рассылка" }
            };
        }

        public static List<MailingTopicSubscriber> GenerateMailingTopicSubscribers()
        {
            var list = new List<MailingTopicSubscriber>();

            int count = GenerateUsers().Count;

            for (uint i = 1; i <= count; i++)
            {
                list.Add(new MailingTopicSubscriber { MailingTopicId = 1, UserId = i });
            }

            return list;
        }

        public static List<ForumMessage> GenerateForumMessages()
        {
            return new List<ForumMessage>
            {
                new ForumMessage { Date = new DateTime(2023, 1, 1), Message = "1", ParentId = null, UserId = 3 }, // 1
                new ForumMessage { Date = new DateTime(2023, 1, 2), Message = "2", ParentId = null, UserId = 3 }, // 2
                new ForumMessage { Date = new DateTime(2023, 1, 1), Message = "1.1", ParentId = 1, UserId = 4 }, // 3
                new ForumMessage { Date = new DateTime(2023, 1, 2), Message = "1.1.1", ParentId = 3, UserId = 3 }, // 4
                new ForumMessage { Date = new DateTime(2023, 1, 3), Message = "1.2", ParentId = 1, UserId = 1 }, // 5
                new ForumMessage { Date = new DateTime(2023, 1, 4), Message = "1.1.2", ParentId = 3, UserId = 2 }, // 6
                new ForumMessage { Date = new DateTime(2023, 1, 5), Message = "2.1", ParentId = 2, UserId = 7 }, // 7
                new ForumMessage { Date = new DateTime(2023, 1, 5), Message = "2.2", ParentId = 2, UserId = 8 }, // 8
                new ForumMessage { Date = new DateTime(2023, 1, 5), Message = "2.2.1", ParentId = 8, UserId = 5 } // 9
            };
        }
    }
}
