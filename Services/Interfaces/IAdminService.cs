using WebRunApplication.DataEntity;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;

namespace WebRunApplication.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IBaseResponse<List<HelpViewModel>>> GetQuestions();

        Task<IBaseResponse<bool>> CreateAnswer(uint id, string answer);

        Task<IBaseResponse<Dictionary<string, (int, int, int)>>> GetTopicsInformation();
    }
}
