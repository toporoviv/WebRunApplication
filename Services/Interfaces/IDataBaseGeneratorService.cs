using WebRunApplication.Interfaces;

namespace WebRunApplication.Services.Interfaces
{
    public interface IDataBaseGeneratorService
    {
        Task<IBaseResponse<bool>> GenerateTrainings();
    }
}
