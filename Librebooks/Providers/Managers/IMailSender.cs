namespace Librebooks.Providers.Managers
{
	public interface IMailSender
	{
		Task SendAsync (string to, string subject, string body);
	}
}
