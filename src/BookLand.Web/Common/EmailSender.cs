using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookLand.Web.Common;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Send Email
        return Task.CompletedTask;
    }
}
