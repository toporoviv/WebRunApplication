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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebRunApplication.Controllers
{
    [Authorize]
    public class PdfController : Controller
    {
        private readonly IPdfService _pdfService;
        private readonly ITrainingTemplateService _trainingTemplateService;
        private readonly IUserService _userService;
        private readonly IIndicatorService _indicatorService;

        public PdfController(
            IPdfService pdfService,
            IIndicatorService indicatorService,
            IUserService userService,
            ITrainingTemplateService trainingTemplateService)
        {
            _pdfService = pdfService;
            _indicatorService = indicatorService;
            _userService = userService;
            _trainingTemplateService = trainingTemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.UserIndicators = await GetUserIndicators();
            ViewBag.UserIndicatorsResults = await GetIndicatorResults();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewTraining()
        {
            var trainingTemplatesResponse = await _trainingTemplateService.GetAll();

            if (trainingTemplatesResponse.StatusCode != Enums.StatusCode.OK)
            { 
                ModelState.AddModelError("", trainingTemplatesResponse.Description);
                return RedirectToAction("Index", "Home");
            }

            var trainingTemplates = trainingTemplatesResponse.Data.ToList();

            ViewBag.TrainingTemplateList = new SelectList(trainingTemplates, "Id", "Title");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ViewTraining(int id)
        {
            var response = await _pdfService.GetCurrentTrainingInformationPdf(id, User.Identity.Name, "TrainingInformation.pdf");

            if (response.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", response.Description);
                return RedirectToAction("Index", "Home");
            }

            var information = response.Data;

            return File(information.Data, information.ContentType, information.FileName);
        }

        [HttpGet]
        public async Task<IActionResult> TotalTrainingInformation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TotalTrainingInformation(TimeInterval timeInterval)
        {
            var response = await _pdfService.GetTotalTrainingInformationPdf(User.Identity.Name, timeInterval, "TotalTrainingInformation.pdf");

            if (response.StatusCode != Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", response.Description);
                return RedirectToAction("Index", "Home");
            }

            var result = response.Data;

            return File(result.Data, result.ContentType, result.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> ExportIndicatorsPdf(string ExportData)
        {
            var userIndicators = await GetUserIndicators();
            var indicators = await GetIndicatorResults();

            var indicatorPdfResponse = await _pdfService.GetIndicatorsPdf("ExportData.pdf", userIndicators, indicators);

            if (indicatorPdfResponse.StatusCode != Enums.StatusCode.OK)
            { 
                ModelState.AddModelError("", indicatorPdfResponse.Description);
                return RedirectToAction("Index", "Pdf");
            }

            var indicatorPdf = indicatorPdfResponse.Data;

            return File(indicatorPdf.Data, indicatorPdf.ContentType, indicatorPdf.FileName);
        }

        [NonAction]
        private async Task<List<Indicator>> GetUserIndicators()
        {
            var user = (await _userService.GetAll()).Data.FirstOrDefault(x => x.Login == User.Identity.Name);

            var userIndicators = (await _indicatorService.GetAll())
                .Data
                .Where(x => x.UserId == user.Id)
                .ToList();

            return userIndicators;
        }

        [NonAction]
        private async Task<List<IndicatorViewModel>> GetIndicatorResults()
        {
            var indicators = await GetUserIndicators();

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
