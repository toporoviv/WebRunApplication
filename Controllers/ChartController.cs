using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Packaging;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        private readonly IPersonalAccountService _personalAccountService;
        private readonly ITrainingService _trainingService;
        private readonly ITrainingTemplateService _trainingTemplateService;
        private readonly IChartService _chartService;

        public ChartController(
            IPersonalAccountService personalAccountService,
            IChartService chartService,
            ITrainingService trainingService,
            ITrainingTemplateService trainingTemplateService)
        {
            _personalAccountService = personalAccountService;
            _chartService = chartService;
            _trainingService = trainingService;
            _trainingTemplateService = trainingTemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trainingDictionary = await GetTrainings();
            ViewBag.Trainings = trainingDictionary;

            var trainingsTypes = await _personalAccountService.GetTrainings(User.Identity.Name);

            if (trainingsTypes.StatusCode != Enums.StatusCode.OK) ModelState.AddModelError("", trainingsTypes.Description);
            else ViewBag.TrainingTypes = new SelectList(trainingsTypes.Data, "Id", "Title");

            var dataChart = new object[trainingDictionary.Count()];
            int j = 0;

            foreach (var i in trainingDictionary)
            {
                dataChart[j] = new object[]
                {
                    i.Key.ToString(),
                    i.Value
                };

                j++;
            }

            string dataStr = JsonConvert.SerializeObject(dataChart, Formatting.None);

            ViewBag.dataj = new HtmlString(dataStr);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TrainingPieHistogram()
        {
            var trainingTypes = await GetTrainings();

            ViewBag.TrainingTypes = trainingTypes;

            var dataChart = new object[trainingTypes.Count()];
            int j = 0;

            foreach (var i in trainingTypes)
            {
                dataChart[j] = new object[]
                {
                    i.Key.ToString(),
                    i.Value
                };

                j++;
            }

            string dataStr = JsonConvert.SerializeObject(dataChart, Formatting.None);

            ViewBag.dataj = new HtmlString(dataStr);

            return View();
        }

        [HttpGet]
        public IActionResult TotalTrainingTime()
        {

            var totalTrainingTimeForTrainingCamps = _context.TrainingsSchedules
                .Join(_context.CampsPeriods, x => x.IdCamp, y => y.Id, (x, y) => new { x, y })
                .GroupBy(x => x.y)
                .Select(x => new TotalTrainingCampsDurationView
                {
                    TimeInterval = new TimeInterval
                    {
                        EndIntervalTime = x.Key.End,
                        StartIntervalTime = x.Key.Start
                    },
                    TotalDuration = new TimeSpan(0, (int)x.Sum(y => y.x.Duration), 0)
                });

            ViewBag.TotalTrainingTimeForTrainingCamps = totalTrainingTimeForTrainingCamps;

            var dataChart = new object[totalTrainingTimeForTrainingCamps.Count()];
            int j = 0;

            foreach (var i in totalTrainingTimeForTrainingCamps)
            {
                dataChart[j] = new object[]
                {
                    i.TimeInterval.StartIntervalTime.ToShortDateString() + "-" + i.TimeInterval.StartIntervalTime.ToShortDateString(),
                    i.TotalDuration.TotalMinutes
                };

                j++;
            }

            string dataStr = JsonConvert.SerializeObject(dataChart, Formatting.None);

            ViewBag.dataj = new HtmlString(dataStr);

            return View();
        }

        private async Task<Dictionary<string, int>> GetTrainings()
        {
            var result = await _chartService.GetTrainingCount(User.Identity.Name);

            if (result.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", result.Description);
                return null;
            }

            return result.Data;
        }
    }
}
