using WebRunApplication.Domain.Entities;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Services.Interfaces
{
    public interface IPdfService
    {
        Task<IBaseResponse<FileResultInformation>> GetIndicatorsPdfAsync
        (
            string exportData,
            List<Indicator> userIndicators,
            List<IndicatorViewModel> indicatorsResults,
            CancellationToken cancellationToken = default
        );

        Task<IBaseResponse<FileResultInformation>> GetCurrentTrainingInformationPdfAsync
        (
            int trainingTemplateId,
            string userLogin,
            string fileName,
            CancellationToken cancellationToken = default
        );

        Task<IBaseResponse<FileResultInformation>> GetTotalTrainingInformationPdfAsync
        (
            string userLogin,
            TimeInterval timeInterval,
            string fileName,
            CancellationToken cancellationToken = default
        );

        Task<List<IndicatorViewModel>> GetIndicatorResultsAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );

        Task<List<Indicator>> GetUserIndicatorsAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
    }
}
