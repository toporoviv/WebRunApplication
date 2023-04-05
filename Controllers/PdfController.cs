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
using WebRunApplication.Services.Implementations;

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
            ViewBag.UserIndicators = await _pdfService.GetUserIndicators(User.Identity.Name);
            ViewBag.UserIndicatorsResults = await _pdfService.GetIndicatorResults(User.Identity.Name);

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
            var userIndicators = await _pdfService.GetUserIndicators(User.Identity.Name);
            var indicators = await _pdfService.GetIndicatorResults(User.Identity.Name);

            var indicatorPdfResponse = await _pdfService.GetIndicatorsPdf("ExportData.pdf", userIndicators, indicators);

            if (indicatorPdfResponse.StatusCode != Enums.StatusCode.OK)
            { 
                ModelState.AddModelError("", indicatorPdfResponse.Description);
                return RedirectToAction("Index", "Pdf");
            }

            var indicatorPdf = indicatorPdfResponse.Data;

            return File(indicatorPdf.Data, indicatorPdf.ContentType, indicatorPdf.FileName);
        }
    }
}
