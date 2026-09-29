using System.Net;
using System.Net.Mail;

namespace NotificationService.API.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string body)
    {
        var host = _configuration["EmailSettings:Host"];
        var port = int.Parse(
            _configuration["EmailSettings:Port"]!);

        var username =
            _configuration["EmailSettings:Username"];

        var password =
            _configuration["EmailSettings:Password"];

        var from =
            _configuration["EmailSettings:From"];

        using var message = new MailMessage();

        message.From = new MailAddress(from!);
        message.To.Add(to);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;

        using var client = new SmtpClient(host, port);

        client.EnableSsl = true;

        client.Credentials =
            new NetworkCredential(
                username,
                password);

        await client.SendMailAsync(message);
    }
}