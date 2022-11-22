namespace BookLand.Application
{
    public interface IMyEmailSender
    {
        Task SendEmailAsync(string userName, string v1, string v2);
    }
}