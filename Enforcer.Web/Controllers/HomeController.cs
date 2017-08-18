using Enforcer.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enforcer.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Service1Client client = new Service1Client();
            var result = client.GetData(20);

            //CompositeType type = new CompositeType();
            //type.StringValue = "value";
            //type.BoolValue = true;

            //var com = client.GetDataUsingDataContract(type);

            SampleRequest request = new SampleRequest();
            request.Age = "10";
            request.Name = "Antony";
            client.TestInterfaceClass(request);
            //string va = ContentTypeHelper.GetContentType(@"D:\Antony\sample.log");        
            return View();
        }

    }

    public static class ContentTypeHelper
    {
        /// <summary>
        /// Gets the MIME type corresponding to the extension of the specified file name.
        /// </summary>
        /// <param name="fileName">The file name to determine the MIME type for.</param>
        /// <returns>The MIME type corresponding to the extension of the specified file name, if found; otherwise, null.</returns>
        public static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (String.IsNullOrWhiteSpace(extension))
            {
                return null;
            }

            var registryKey = Registry.ClassesRoot.OpenSubKey(extension);

            if (registryKey == null)
            {
                return null;
            }

            var value = registryKey.GetValue("Content Type") as string;

            return String.IsNullOrWhiteSpace(value) ? null : value;
        }
    }
}
