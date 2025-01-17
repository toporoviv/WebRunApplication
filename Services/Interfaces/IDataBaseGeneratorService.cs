using WebRunApplication.Domain.Enums.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Interfaces
{
    public interface IDataBaseGeneratorService
    {
        Task<IBaseResponse<bool>> GenerateTrainings();
    }
}
