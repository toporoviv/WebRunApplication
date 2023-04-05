using Microsoft.AspNetCore.Mvc;
using WebRunApplication.DataEntity;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;

namespace WebRunApplication.Services.Interfaces
{
    public interface IPdfService
    {
        Task<IBaseResponse<FileResultInformation>> GetIndicatorsPdf(string exportData,
            List<Indicator> userIndicators,
            List<IndicatorViewModel> indicatorsResults);

        Task<IBaseResponse<FileResultInformation>> GetCurrentTrainingInformationPdf(int trainingTemplateId, string userLogin, string fileName);

        Task<IBaseResponse<FileResultInformation>> GetTotalTrainingInformationPdf(string userLogin, TimeInterval timeInterval, string fileName);

        Task<List<IndicatorViewModel>> GetIndicatorResults(string login);

        Task<List<Indicator>> GetUserIndicators(string login);
    }
}
