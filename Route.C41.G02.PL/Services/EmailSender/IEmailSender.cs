using System.Threading.Tasks;

namespace Route.C41.G02.PL.Services.EmailSender
{
	public interface IEmailSender
	{

		Task SendAsync(string from , string recipients, string body , string subject  );
	}
}
