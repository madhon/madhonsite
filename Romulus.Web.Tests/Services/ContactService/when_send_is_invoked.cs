using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Romulus.Web.Services;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Tests.Services.ContactService
{
    [Subject(typeof (Web.Services.ContactService))]
    public class when_send_is_invoked
    {
        static IContactService service;
        static ContactViewModel model;

        Establish context = () => service = new Web.Services.ContactService();

        Because of = () =>
                {
                    model = new ContactViewModel
                        {
                            Email = "madhon@madhon.co.uk",
                            Name = "Madhon",
                            Message = "Spec Testing"
                        };
                };

        It should_send_async = () => service.SendMessageAsync(model: model);
        It should_send_sync = () => service.SendMessage(model: model);
    }
}