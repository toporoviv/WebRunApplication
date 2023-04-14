using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebRunApplication.DataEntity;
using WebRunApplication.Models;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Controllers
{
    [Authorize]
    public class TrainingStatisticalController : Controller
    {
        private readonly ITrainingStatisticalService _trainingStatisticalService;

        public TrainingStatisticalController(ITrainingStatisticalService trainingStatisticalService)
        {
            _trainingStatisticalService = trainingStatisticalService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var responseMailingCount = await _trainingStatisticalService.GetTotalMailingCount(User.Identity.Name);

            if (responseMailingCount.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseMailingCount.Description);
                return View();
            }

            ViewBag.TotalMailingCount = responseMailingCount.Data;

            var responseTrainingDuration = await _trainingStatisticalService.GetTotalTrainingDuration(User.Identity.Name);

            if (responseTrainingDuration.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingDuration.Description);
                return View();
            }

            ViewBag.TotalTrainingDuration = responseTrainingDuration.Data;

            var responseTrainingCount = await _trainingStatisticalService.GetTotalTrainingCount(User.Identity.Name);

            if (responseTrainingCount.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingCount.Description);
                return View();
            }

            ViewBag.TotalTrainingCount = responseTrainingCount.Data;

            var responseTrainingDayDuration = await _trainingStatisticalService.GetTotalTrainingDayDuration(User.Identity.Name);

            if (responseTrainingDayDuration.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingDayDuration.Description);
                return View();
            }

            ViewBag.TotalTrainingDayDuration = responseTrainingDayDuration.Data;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Statistical()
        {
            var responseMailingCount = await _trainingStatisticalService.GetTotalMailingCountGroupByYearAndMonth(User.Identity.Name);

            if (responseMailingCount.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseMailingCount.Description);
                return View();
            }

            ViewBag.TotalMailingCount = StatisticalCalculator.GetMovingAverages(responseMailingCount.Data);

            var responseTrainingDuration = await _trainingStatisticalService.GetTotalTrainingDurationGroupByYearAndMonth(User.Identity.Name);

            if (responseTrainingDuration.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingDuration.Description);
                return View();
            }

            ViewBag.TotalTrainingDuration = StatisticalCalculator.GetMovingAverages(responseTrainingDuration.Data);

            var responseTrainingCount = await _trainingStatisticalService.GetTotalTrainingCountGroupByYearAndMonth(User.Identity.Name);

            if (responseTrainingCount.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingCount.Description);
                return View();
            }

            ViewBag.TotalTrainingCount = StatisticalCalculator.GetMovingAverages(responseTrainingCount.Data);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StatisticalWithEdgeValues()
        {
            var responseMailingCount = await _trainingStatisticalService.GetTotalMailingCountGroupByYearAndMonth(User.Identity.Name);

            if (responseMailingCount.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseMailingCount.Description);
                return View();
            }

            var totalMailingCountMovingAverages = StatisticalCalculator.GetMovingAveragesWithEdgeValues(responseMailingCount.Data);
            ViewBag.TotalMailingCount = totalMailingCountMovingAverages;
            ViewBag.PredictiveValueForTotalMailingCount = StatisticalCalculator.GetPredictiveValueByMovingAverages(totalMailingCountMovingAverages);

            var responseTrainingDuration = await _trainingStatisticalService.GetTotalTrainingDurationGroupByYearAndMonth(User.Identity.Name);

            if (responseTrainingDuration.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingDuration.Description);
                return View();
            }

            var totalTrainingDurationMovingAverages = StatisticalCalculator.GetMovingAveragesWithEdgeValues(responseTrainingDuration.Data);
            ViewBag.TotalTrainingDuration = totalTrainingDurationMovingAverages;
            ViewBag.PredictiveValueForTotalTrainingDuration = StatisticalCalculator.GetPredictiveValueByMovingAverages(totalTrainingDurationMovingAverages);

            var responseTrainingCount = await _trainingStatisticalService.GetTotalTrainingCountGroupByYearAndMonth(User.Identity.Name);

            if (responseTrainingCount.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingCount.Description);
                return View();
            }

            var totalTrainingCountMovingAverages = StatisticalCalculator.GetMovingAveragesWithEdgeValues(responseTrainingCount.Data);
            ViewBag.TotalTrainingCount = totalTrainingCountMovingAverages;
            ViewBag.PredictiveValueForTotalTrainingCount = StatisticalCalculator.GetPredictiveValueByMovingAverages(totalTrainingCountMovingAverages);

            return View();
        }
    }
}
