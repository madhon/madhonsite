namespace Romulus.Web.Services
{
    using System.Threading.Tasks;
    using Romulus.Web.ViewModels;

    public interface IContactService
    {
        Task SendMessageAsync(ContactViewModel model);
    }
}
