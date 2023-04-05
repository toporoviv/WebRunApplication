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

    }
}
