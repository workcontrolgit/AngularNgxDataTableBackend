using AngularNgxDataTableBackend.Application.DTOs.Email;
using System.Threading.Tasks;

namespace AngularNgxDataTableBackend.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
