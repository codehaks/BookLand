using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookLand.Application;

public class EmailSender : IMyEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine(email);
        Console.WriteLine($"{subject} {htmlMessage}");
        return Task.CompletedTask;
    }
}
