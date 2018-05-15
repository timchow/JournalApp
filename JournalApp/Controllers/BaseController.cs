using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace JournalApp.Controllers
{
	public class BaseController : Controller
	{
		public BaseController(IConfiguration appSettings)
		{
			Settings = appSettings;
		}

		public BaseController()
		{
		}

		public IConfiguration Settings { get; set; }
	}
}