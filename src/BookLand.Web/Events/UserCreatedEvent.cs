using BookLand.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace BookLand.Web.Events;

public class UserCreatedEvent
{
    public class Notification : INotification
    {
        public ApplicationUser User { get; set; }
        public string ReturnUrl { get; set; }

    }

    public class Handler : INotificationHandler<Notification>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<UserCreatedEvent> _logger;
        public Handler(UserManager<ApplicationUser> userManager, IEmailSender emailSender, LinkGenerator linkGenerator, ILogger<UserCreatedEvent> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            // Send Email

            var userId = await _userManager.GetUserIdAsync(notification.User);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(notification.User);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            var callbackUrl = _linkGenerator.GetPathByPage(
                "/Account/ConfirmEmail",
                handler: null,
                values: new { area = "Identity", userId = userId, code = code, returnUrl = notification.ReturnUrl });

            await _emailSender.SendEmailAsync(notification.User.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            _logger.LogInformation("Email sent");
            await Task.CompletedTask;
        }
    }

}
