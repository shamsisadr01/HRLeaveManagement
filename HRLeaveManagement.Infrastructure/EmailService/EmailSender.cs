using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HRLeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailSetting _emailSetting;

    public EmailSender(IOptions<EmailSetting> emailSetting)
    {
        _emailSetting = emailSetting.Value;
    }
    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(_emailSetting.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress(
            _emailSetting.FromAddress,
            _emailSetting.FromName);

        var msg = MailHelper.CreateSingleEmail(from, to, 
            email.Subject, email.Body,
            email.Body);
        var response = await client.SendEmailAsync(msg);

        return response.StatusCode == System.Net.HttpStatusCode.OK ||
               response.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}