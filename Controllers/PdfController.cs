using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRunApplication.DataEntity;
using WebRunApplication.Services.Interfaces;
using System.Text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using Microsoft.AspNetCore.Components.Web;
using System.Xml;
using System.IO;
using System.Web;
using Org.Apache.Http.Protocol;
using Android.Content;
using WebRunApplication.Models;
using System.Linq;

namespace WebRunApplication.Controllers
{
    public class PdfController : Controller
    {
        //private readonly IUserService _userService;

        ApplicationDbContext _context;

        public PdfController(ApplicationDbContext userService)
        {
            _context = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.UserIndicators = GetUserIndicators();
            ViewBag.UserIndicatorsResults = GetIndicatorResults();

            return View();
        }

        //[HttpGet]
        //public IActionResult TrainingsList(string trainingType)
        //{
        //    var trainingsTypeList = _context.TrainingTypes.Select(x => x.NameType).ToList();
        //    var selectTrainingList = SelectTrainingList(trainingType);

        //    ViewBag.TrainingsTypeList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(trainingsTypeList, "NameType", "NameType");
        //    ViewBag.SelectTrainingList = selectTrainingList;
        //    return View();
        //}

        //[NonAction]
        //private List<TrainingViewModel> SelectTrainingList(string trainingType)
        //{
        //    var trainingsViewModelList =
        //       _context.Trainings.Select
        //        (x => new TrainingViewModel()
        //        {
        //            Id = x.Id,
        //            NameTraining = x.NameTraining,
        //            NameType = _context.TrainingTypes.FirstOrDefault(y => y.Id == x.IdType).NameType

        //        }).ToList();

        //    var selectTrainingList = trainingsViewModelList
        //        .Select(x => x)
        //        .Where(x => x.NameType == ((trainingType is null) ? x.NameType : trainingType))
        //        .ToList();

        //    return selectTrainingList;
        //}

        [HttpPost]
        public FileResult ExportIndicatorsPdf(string ExportData)
        {
            Document Doc = new Document(PageSize.A4);

            using (var fs = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(Doc, fs);
                Doc.Open();
                Doc.NewPage();

                string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");

                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Font f = new Font(bf, 12, Font.NORMAL);

                var userIndicators = GetUserIndicators();

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
                    table.AddCell(new PdfPCell( new Phrase(indicator.Date.ToShortDateString(), f)));
                    table.AddCell(new PdfPCell( new Phrase(indicator.Duration.ToString(), f)));
                    table.AddCell(new PdfPCell( new Phrase(indicator.MinimumPulse.ToString(), f)));
                    table.AddCell(new PdfPCell( new Phrase(indicator.AveragePulse.ToString(), f)));
                    table.AddCell(new PdfPCell( new Phrase(indicator.MaximumPulse.ToString(), f)));
                    table.AddCell(new PdfPCell( new Phrase(indicator.Steps.ToString(), f)));
                    table.AddCell(new PdfPCell( new Phrase(indicator.Calories.ToString(), f)));
                    table.AddCell(new PdfPCell( new Phrase(indicator.AverageSpeed.ToString(), f)));
                }

                int i = 0;
                var results = GetIndicatorResults();

                foreach (var indicator in results)
                {
                    var newCell = new PdfPCell(new Phrase((i == 0) ? "Min" : (i == 1) ? "Avg" : "Max"));
                    newCell.Colspan = 2;
                    table.AddCell(newCell);
                    table.AddCell("");
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

                return File(fs.ToArray(), "application/pdf", "ExportData.pdf");
            }
        }

        //[HttpPost]
        //public FileResult ExportTrainingListPdf(string trainingType)
        //{
        //    Document Doc = new Document(PageSize.LETTER);

        //    using (var fs = new MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(Doc, fs);
        //        Doc.Open();
        //        Doc.NewPage();

        //        string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");

        //        BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        //        Font f = new Font(bf, 12, Font.NORMAL);

        //        var selectTrainingList = SelectTrainingList(trainingType);

        //        int columnCount = typeof(TrainingViewModel).GetProperties().Length;
        //        PdfPTable table = new PdfPTable(columnCount);
        //        PdfPCell cell = new PdfPCell(new Phrase("Список Тренировок"));
        //        cell.Colspan = columnCount;

        //        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //        table.AddCell(cell);

        //        foreach (var training in selectTrainingList)
        //        {
        //            table.AddCell(training.Id.ToString());
        //            table.AddCell(training.NameType);
        //            table.AddCell(training.NameTraining);
        //        }

        //        Doc.Add(table);
        //        Doc.Close();

        //        return File(fs.ToArray(), "application/pdf", "ExportData.pdf");
        //    }
        //}

        [NonAction]
        private List<Indicator> GetUserIndicators()
        {
            var userIndicators = _context.Indicators
                .Where(x => x.UserId == _context.Users.FirstOrDefault(y => y.Login == User.Identity.Name).Id)
                .ToList();

            return userIndicators;
        }

        [NonAction]
        private List<IndicatorViewModel> GetIndicatorResults()
        {
            var indicators = GetUserIndicators();

            var indicatorMinResults = new IndicatorViewModel
            {
                Calories = indicators.Select(y => y.Calories).Min(),
                Steps = indicators.Select(y => y.Steps).Min(),
                MinimumPulse = indicators.Select(y => y.MinimumPulse).Min(),
                AveragePulse = indicators.Select(y => y.AveragePulse).Min(),
                MaximumPulse = indicators.Select(y => y.MaximumPulse).Min(),
                AverageSpeed = indicators.Select(y => y.AverageSpeed).Min()
            };

            var hrvIndicatorAvgResults = new IndicatorViewModel
            {
                Calories = (uint)indicators.Average(x => x.Calories),
                Steps = (uint)indicators.Average(x => x.Steps),
                MinimumPulse = (uint)indicators.Average(x => x.MinimumPulse),
                AveragePulse = (uint)indicators.Average(x => x.AveragePulse),
                MaximumPulse = (uint)indicators.Average(x => x.MaximumPulse),
                AverageSpeed = indicators.Select(y => y.AverageSpeed).Average()
            };

            var hrvIndicatorMaxResults = new IndicatorViewModel
            {
                Calories = indicators.Select(y => y.Calories).Max(),
                Steps = indicators.Select(y => y.Steps).Max(),
                MinimumPulse = indicators.Select(y => y.MinimumPulse).Max(),
                AveragePulse = indicators.Select(y => y.AveragePulse).Max(),
                MaximumPulse = indicators.Select(y => y.MaximumPulse).Max(),
                AverageSpeed = indicators.Select(y => y.AverageSpeed).Max()
            };

            var hrvIndicatorsResults = new List<IndicatorViewModel>
            {
                indicatorMinResults,
                hrvIndicatorAvgResults,
                hrvIndicatorMaxResults
            };

            return hrvIndicatorsResults;
        }
    }
}
