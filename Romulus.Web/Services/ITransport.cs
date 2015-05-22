namespace Romulus.Web.Services
{
    using System.Threading.Tasks;
    using MimeKit;

    public interface ITransport
    {
        Task DeliverAsync(MimeMessage message);
    }
}