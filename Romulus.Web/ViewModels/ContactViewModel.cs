namespace Romulus.Web.ViewModels
{
    using System;
    using JetBrains.Annotations;
    using MediatR;

    [UsedImplicitly]
    public class ContactViewModel : INotification
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }
    }
}