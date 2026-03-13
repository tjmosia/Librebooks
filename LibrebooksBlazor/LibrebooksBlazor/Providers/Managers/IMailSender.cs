namespace LibrebooksRazor.Providers.Managers
{
	public interface IMailSender
	{
		Task SendAsync (string to, string subject, string body);
	}
}
