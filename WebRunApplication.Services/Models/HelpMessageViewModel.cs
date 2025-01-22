using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Services.Models;

public class HelpMessageViewModel : HelpMessage
{
    public string UserFIO { get; set; }
}