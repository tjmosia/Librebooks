namespace Librebooks.Providers.Managers;

public class MailSender : IMailSender
{
	public Task SendAsync (string to, string subject, string body)
	{
		throw new NotImplementedException();
	}
}
