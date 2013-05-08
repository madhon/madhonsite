using System;
using System.Threading.Tasks;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Services
{
    public interface IContactService
    {
        Task SendMessageAsync(ContactViewModel model);
        void SendMessage(ContactViewModel model);
    }
}
