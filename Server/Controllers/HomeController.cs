using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Server.Controllers
{
	/// <summary>
	/// 
	/// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Paint()
        {
            return View();
        }

        public FileResult GetFiles()
        {
            string file_path = Server.MapPath("~/Files/Client.rar");
            string file_type = "application/octet-stream";
            string file_name = "Client.rar";
            return File(file_path, file_type, file_name);
        }
    }
}
