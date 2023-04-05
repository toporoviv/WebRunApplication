using iTextSharp.text;
using iTextSharp.text.pdf;
using Java.Nio.FileNio.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DataEntity;
using WebRunApplication.Enums;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;
using WebRunApplication.Response;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    public class PdfService : IPdfService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Indicator> _indicatorRepository;
        private readonly IBaseRepository<Training> _trainingRepository;
        IBaseRepository<TrainingTemplate> _trainingTemplateRepository;
        private readonly ILogger<PdfService> _logger;

        public PdfService(
            IBaseRepository<User> userRepository,
            IBaseRepository<Indicator> indicatorRepository,
            IBaseRepository<Training> trainingRepository,
            IBaseRepository<TrainingTemplate> trainingTemplateRepository,
            ILogger<PdfService> logger)
        {
            _userRepository = userRepository;
            _indicatorRepository = indicatorRepository;
            _trainingRepository = trainingRepository;
            _trainingTemplateRepository = trainingTemplateRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<FileResultInformation>> GetCurrentTrainingInformationPdf(int trainingTemplateId, string userLogin, string fileName)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == userLogin);

                var title = await _trainingTemplateRepository.GetAll().FirstOrDefaultAsync(x => x.Id == trainingTemplateId);

                var data = _indicatorRepository
                    .GetAll()
                    .Where(x => x.UserId == user.Id)
                    .Join(_trainingRepository.GetAll().Where(x => x.TrainTemplateId == trainingTemplateId),
                        indicator => indicator.Date, training => training.Date, (indicator, training) => training)
                    .Select(x => new TrainingPdfViewModel
                    {
                        Date = x.Date,
                        Title = title.Title
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                Document Doc = new Document(PageSize.LETTER);

                using (var fs = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(Doc, fs);
                    Doc.Open();
                    Doc.NewPage();

                    string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");

                    BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                    Font f = new Font(bf, 12, Font.NORMAL);

                    int columnCount = 3;
                    PdfPTable table = new PdfPTable(columnCount);


                    PdfPTable table1 = new PdfPTable(1);
                    Font font2 = new Font(bf, 20, Font.BOLD);
                    
                    PdfPCell cell1 = new PdfPCell()
                    {
                        BorderWidthBottom = 0f,
                        BorderWidthTop = 0f,
                        BorderWidthLeft = 0f,
                        BorderWidthRight = 0f
                    };
                    cell1.PaddingTop = 5f;
                    Phrase bioPhrase = new Phrase();
                    Chunk bioChunk = new Chunk("RunApp | Список доступных тренировок по выбранному типу", font2);
                    bioPhrase.Add(bioChunk);
                    bioPhrase.Add(new Chunk(Environment.NewLine));

                    bioPhrase.Add(new Chunk(Environment.NewLine));

                    cell1.AddElement(bioPhrase);

                    table1.AddCell(cell1);
                    Doc.Add(table1);

                    PdfPCell cell = new PdfPCell(new Phrase("Список Тренировок", f));
                    cell.Colspan = columnCount;

                    cell.HorizontalAlignment = 1;
                    table.AddCell(cell);
                    table.AddCell(new PdfPCell(new Phrase("№", f)));
                    table.AddCell(new PdfPCell(new Phrase("Тип Тренировки", f)));
                    table.AddCell(new PdfPCell(new Phrase("Дата тренировки", f)));

                    for (int i = 0; i < data.Count; i++)
                    {
                        table.AddCell(new PdfPCell(new Phrase((i + 1).ToString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(data[i].Title, f)));
                        table.AddCell(new PdfPCell(new Phrase(new Phrase(data[i].Date.ToShortDateString(), f))));
                    }

                    Doc.Add(table);
                    Doc.Close();

                    return new BaseResponse<FileResultInformation>
                    {
                        Data = new FileResultInformation
                        {
                            Data = fs.ToArray(),
                            ContentType = "application/pdf",
                            FileName = fileName
                        },
                        StatusCode = StatusCode.OK
                    };
                }
            }
            catch (Exception exception)
            {
                _logger.LogError($"{nameof(PdfService)}{nameof(GetCurrentTrainingInformationPdf)}: {exception.Message}");
                return new BaseResponse<FileResultInformation>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<FileResultInformation>> GetIndicatorsPdf(
            string exportData,
            List<Indicator> userIndicators,
            List<IndicatorViewModel> indicatorsResults)
        {
            try
            {
                await Task.Delay(0);
                Document Doc = new Document(PageSize.A4);

                using (var fs = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(Doc, fs);
                    Doc.Open();
                    Doc.NewPage();

                    string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");

                    BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                    Font f = new Font(bf, 12, Font.NORMAL);

                    int columnCount = typeof(Indicator).GetProperties().Length - 3;
                    PdfPTable table = new PdfPTable(columnCount);
                    PdfPCell cell = new PdfPCell(new Phrase("Показатели тренировок", f));
                    cell.Colspan = columnCount;

                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    table.AddCell(new PdfPCell(new Phrase("Дата", f)));
                    table.AddCell(new PdfPCell(new Phrase("Продолжительность", f)));
                    table.AddCell(new PdfPCell(new Phrase("Мин. пульс", f)));
                    table.AddCell(new PdfPCell(new Phrase("Сред. пульс", f)));
                    table.AddCell(new PdfPCell(new Phrase("Макс. пульс", f)));
                    table.AddCell(new PdfPCell(new Phrase("Шаги", f)));
                    table.AddCell(new PdfPCell(new Phrase("Калории", f)));
                    table.AddCell(new PdfPCell(new Phrase("Сред. скорость", f)));

                    foreach (var indicator in userIndicators)
                    {
                        table.AddCell(new PdfPCell(new Phrase(indicator.Date.ToShortDateString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(indicator.Duration.ToString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(indicator.MinimumPulse.ToString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(indicator.AveragePulse.ToString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(indicator.MaximumPulse.ToString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(indicator.Steps.ToString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(indicator.Calories.ToString(), f)));
                        table.AddCell(new PdfPCell(new Phrase(indicator.AverageSpeed.ToString(), f)));
                    }

                    int i = 0;

                    foreach (var indicator in indicatorsResults)
                    {
                        var newCell = new PdfPCell(new Phrase((i == 0) ? "Min" : (i == 1) ? "Avg" : "Max"));
                        newCell.Colspan = 2;
                        table.AddCell(newCell);
                        table.AddCell(indicator.MinimumPulse.ToString());
                        table.AddCell(indicator.AveragePulse.ToString());
                        table.AddCell(indicator.MaximumPulse.ToString());
                        table.AddCell(indicator.Steps.ToString());
                        table.AddCell(indicator.Calories.ToString());
                        table.AddCell(indicator.AverageSpeed.ToString("0.00"));

                        i++;
                    }

                    Doc.Add(table);
                    Doc.Close();

                    return new BaseResponse<FileResultInformation>
                    {
                        Data = new FileResultInformation
                        {
                            Data = fs.ToArray(),
                            ContentType = "application/pdf",
                            FileName = exportData
                        },
                        StatusCode = StatusCode.OK
                    };
                }
            }
            catch(Exception exception)
            {
                _logger.LogError($"{nameof(PdfService)}{nameof(GetIndicatorsPdf)}: {exception.Message}");
                return new BaseResponse<FileResultInformation>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<FileResultInformation>> GetTotalTrainingInformationPdf(string userLogin, TimeInterval timeInterval, string fileName)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == userLogin);

                var data = _indicatorRepository
                    .GetAll()
                    .Where(x => x.Date >= timeInterval.Begin && x.Date <= timeInterval.End && x.UserId == user.Id)
                    .Join(_trainingRepository.GetAll(), indicator => indicator.Date, training => training.Date, (indicator, training) => training)
                    .Join(_trainingTemplateRepository.GetAll(), training => training.TrainTemplateId, template => template.Id,
                        (training, template) => new { training, template.Title })
                    .GroupBy(x => new { x.training.Date.Month, x.Title })
                    .Select(x => new
                    {
                        Month = x.Key.Month,
                        Title = x.Select(y => y.Title).ToList(),
                        TotalDuration = x.Select(y => y.training).ToList()
                    })
                    .GroupBy(x => x.Month)
                    .ToDictionary(x => x.Key, x => new { Titles = x.Select(y => y.Title).ToList(), TotalDuration = x.Select(y => y.TotalDuration).ToList() });

                Document Doc = new Document(PageSize.LETTER);

                using (var fs = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(Doc, fs);
                    Doc.Open();
                    Doc.NewPage();

                    string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");

                    BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                    Font f = new Font(bf, 12, Font.NORMAL);

                    TimeSpan totalTimeMonth;
                    TimeSpan totalTime = new TimeSpan();

                    PdfPTable table1 = new PdfPTable(1);
                    Font font2 = new Font(bf, 20, Font.BOLD);
                    //Set the biography  
                    PdfPCell cell1 = new PdfPCell()
                    {
                        BorderWidthBottom = 0f,
                        BorderWidthTop = 0f,
                        BorderWidthLeft = 0f,
                        BorderWidthRight = 0f
                    };
                    cell1.PaddingTop = 5f;
                    Phrase bioPhrase = new Phrase();
                    Chunk bioChunk = new Chunk("RunApp | Данные по общему тренировочному времени", font2);
                    bioPhrase.Add(bioChunk);
                    bioPhrase.Add(new Chunk(Environment.NewLine));

                    bioPhrase.Add(new Chunk(Environment.NewLine));

                    cell1.AddElement(bioPhrase);

                    table1.AddCell(cell1);
                    Doc.Add(table1);


                    PdfPTable table = new PdfPTable(2);

                    PdfPCell cell = new PdfPCell(new Phrase("Общее тренировочное время по месяцам", f));
                    cell.Colspan = 2;

                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);
                    table.AddCell(new PdfPCell(new Phrase("Тип тренировки", f)));
                    table.AddCell(new PdfPCell(new Phrase("Общее время", f)));

                    foreach (var trainingTotal in data)
                    {
                        cell = new PdfPCell(new Phrase($"{trainingTotal.Key}-й месяц", f));
                        cell.Colspan = 2;
                        table.AddCell(cell);

                        totalTimeMonth = new TimeSpan();

                        for (int j = 0; j < trainingTotal.Value.Titles.Count; j++)
                        {
                            for (int i = 0; i < trainingTotal.Value.Titles[j].Count(); i++)
                            {
                                table.AddCell(new PdfPCell(new Phrase(trainingTotal.Value.Titles[j][i].ToString(), f)));
                                table.AddCell(new PdfPCell(new Phrase(trainingTotal.Value.TotalDuration[j][i].Duration.ToString(), f)));

                                totalTimeMonth += new TimeSpan(0, 0, trainingTotal.Value.TotalDuration[j].Sum(y => (int)y.Duration.TotalSeconds));
                            }
                        }

                        table.AddCell(new PdfPCell(new Phrase("Итоговое время", f)));
                        table.AddCell(new PdfPCell(new Phrase(totalTimeMonth.ToString(), f)));

                        totalTime += totalTimeMonth;
                    }

                    table.AddCell(new PdfPCell(new Phrase("Итоговое общее время", f)));
                    table.AddCell(new PdfPCell(new Phrase(totalTime.ToString(), f)));

                    Doc.Add(table);
                    Doc.Close();

                    return new BaseResponse<FileResultInformation>
                    {
                        Data = new FileResultInformation
                        {
                            Data = fs.ToArray(),
                            ContentType = "application/pdf",
                            FileName = fileName
                        },
                        StatusCode = StatusCode.OK
                    };
                }
            }
            catch(Exception exception)
            {
                _logger.LogError($"{nameof(PdfService)}{nameof(GetTotalTrainingInformationPdf)}: {exception.Message}");
                return new BaseResponse<FileResultInformation>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<List<Indicator>> GetUserIndicators(string login)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

            var userIndicators = _indicatorRepository
                .GetAll()
                .Where(x => x.UserId == user.Id)
                .ToList();

            return userIndicators;
        }

        public async Task<List<IndicatorViewModel>> GetIndicatorResults(string login)
        {
            var indicators = await GetUserIndicators(login);

            var indicatorMinResults = new IndicatorViewModel
            {
                Calories = indicators.Select(y => y.Calories).Min(),
                Steps = indicators.Select(y => y.Steps).Min(),
                MinimumPulse = indicators.Select(y => y.MinimumPulse).Min(),
                AveragePulse = indicators.Select(y => y.AveragePulse).Min(),
                MaximumPulse = indicators.Select(y => y.MaximumPulse).Min(),
                AverageSpeed = indicators.Select(y => y.AverageSpeed).Min()
            };

            var indicatorAvgResults = new IndicatorViewModel
            {
                Calories = (uint)indicators.Average(x => x.Calories),
                Steps = (uint)indicators.Average(x => x.Steps),
                MinimumPulse = (uint)indicators.Average(x => x.MinimumPulse),
                AveragePulse = (uint)indicators.Average(x => x.AveragePulse),
                MaximumPulse = (uint)indicators.Average(x => x.MaximumPulse),
                AverageSpeed = indicators.Select(y => y.AverageSpeed).Average()
            };

            var indicatorMaxResults = new IndicatorViewModel
            {
                Calories = indicators.Select(y => y.Calories).Max(),
                Steps = indicators.Select(y => y.Steps).Max(),
                MinimumPulse = indicators.Select(y => y.MinimumPulse).Max(),
                AveragePulse = indicators.Select(y => y.AveragePulse).Max(),
                MaximumPulse = indicators.Select(y => y.MaximumPulse).Max(),
                AverageSpeed = indicators.Select(y => y.AverageSpeed).Max()
            };

            var indicatorsResults = new List<IndicatorViewModel>
            {
                indicatorMinResults,
                indicatorAvgResults,
                indicatorMaxResults
            };

            return indicatorsResults;
        }
    }
}
