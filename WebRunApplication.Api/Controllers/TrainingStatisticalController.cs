using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRunApplication.Services.Models;
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
            var responseMailingCount = await _trainingStatisticalService.GetTotalMailingCountAsync(User.Identity!.Name!);

            if (responseMailingCount.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseMailingCount.Description);
                return View();
            }

            ViewBag.TotalMailingCount = responseMailingCount.Data;

            var responseTrainingDuration = await _trainingStatisticalService.GetTotalTrainingDurationAsync(User.Identity.Name);

            if (responseTrainingDuration.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingDuration.Description);
                return View();
            }

            ViewBag.TotalTrainingDuration = responseTrainingDuration.Data;

            var responseTrainingCount = await _trainingStatisticalService.GetTotalTrainingCountAsync(User.Identity.Name);

            if (responseTrainingCount.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingCount.Description);
                return View();
            }

            ViewBag.TotalTrainingCount = responseTrainingCount.Data;

            var responseTrainingDayDuration = await _trainingStatisticalService.GetTotalTrainingDayDurationAsync(User.Identity.Name);

            if (responseTrainingDayDuration.StatusCode != Domain.Enums.StatusCode.OK)
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
            var responseMailingCount = await _trainingStatisticalService
                .GetTotalMailingCountGroupByYearAndMonthAsync(User.Identity!.Name!);

            if (responseMailingCount.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseMailingCount.Description);
                return View();
            }

            ViewBag.TotalMailingCount = StatisticalCalculator.GetMovingAverages(responseMailingCount.Data);

            var responseTrainingDuration = await _trainingStatisticalService
                .GetTotalTrainingDurationGroupByYearAndMonthAsync(User.Identity.Name);

            if (responseTrainingDuration.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingDuration.Description);
                return View();
            }

            ViewBag.TotalTrainingDuration = StatisticalCalculator.GetMovingAverages(responseTrainingDuration.Data);

            var responseTrainingCount = await _trainingStatisticalService
                .GetTotalTrainingCountGroupByYearAndMonthAsync(User.Identity.Name);

            if (responseTrainingCount.StatusCode != Domain.Enums.StatusCode.OK)
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
            var responseMailingCount = await _trainingStatisticalService
                .GetTotalMailingCountGroupByYearAndMonthAsync(User.Identity!.Name!);

            if (responseMailingCount.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseMailingCount.Description);
                return View();
            }

            var totalMailingCountMovingAverages = StatisticalCalculator
                .GetMovingAveragesWithEdgeValues(responseMailingCount.Data);
            
            ViewBag.TotalMailingCount = totalMailingCountMovingAverages;
            
            ViewBag.PredictiveValueForTotalMailingCount = StatisticalCalculator
                .GetPredictiveValueByMovingAverages(totalMailingCountMovingAverages);

            var responseTrainingDuration = await _trainingStatisticalService
                .GetTotalTrainingDurationGroupByYearAndMonthAsync(User.Identity!.Name!);

            if (responseTrainingDuration.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingDuration.Description);
                return View();
            }

            var totalTrainingDurationMovingAverages = StatisticalCalculator
                .GetMovingAveragesWithEdgeValues(responseTrainingDuration.Data);
            
            ViewBag.TotalTrainingDuration = totalTrainingDurationMovingAverages;
            
            ViewBag.PredictiveValueForTotalTrainingDuration = StatisticalCalculator
                .GetPredictiveValueByMovingAverages(totalTrainingDurationMovingAverages);

            var responseTrainingCount = await _trainingStatisticalService
                .GetTotalTrainingCountGroupByYearAndMonthAsync(User.Identity!.Name!);

            if (responseTrainingCount.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", responseTrainingCount.Description);
                return View();
            }

            var totalTrainingCountMovingAverages = StatisticalCalculator
                .GetMovingAveragesWithEdgeValues(responseTrainingCount.Data);
            
            ViewBag.TotalTrainingCount = totalTrainingCountMovingAverages;
            
            ViewBag.PredictiveValueForTotalTrainingCount = StatisticalCalculator
                .GetPredictiveValueByMovingAverages(totalTrainingCountMovingAverages);

            return View();
        }
    }
}
