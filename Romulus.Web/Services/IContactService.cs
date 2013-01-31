using System;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Services
{
    public interface IContactService
    {
        void SendMessage(ContactViewModel model);
    }
}
