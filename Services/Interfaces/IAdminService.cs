using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums.Interfaces;
using WebRunApplication.Domain.Enums.Models;

namespace WebRunApplication.Domain.Enums.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IBaseResponse<List<HelpViewModel>>> GetQuestions();

        Task<IBaseResponse<bool>> CreateAnswer(uint id, string answer);

        Task<IBaseResponse<Dictionary<string, (int, int, int)>>> GetTopicsInformation();
    }
}
