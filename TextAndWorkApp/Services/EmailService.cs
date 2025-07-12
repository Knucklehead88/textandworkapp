using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService
{
    private readonly string _apiKey;
    private readonly string _fromEmail;
    private readonly string _fromName;
    public EmailService(string apiKey, string fromEmail, string fromName = "Support")
    {
        _apiKey = apiKey;
        _fromEmail = fromEmail;
        _fromName = fromName;
    }

    public async Task SendContactEmail(string fromName, string fromEmail, string message, string toEmail)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(toEmail);

        var subject = "New Contact Form Submission";
        var plainTextContent = $"From: {fromName} <{fromEmail}>\n\nMessage:\n{message}";
        var htmlContent = $"<strong>From:</strong> {fromName} ({fromEmail})<br/><br/><p>{message}</p>";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        if ((int)response.StatusCode >= 400)
        {
            throw new Exception($"SendGrid failed with status code {response.StatusCode}");
        }
    }
}
