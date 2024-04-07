using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UriComposer
{
    public interface IUriComposerService
    {
        string ComposeImageUri(string src);
    }

    public class UriComposerService : IUriComposerService
    {
        public string ComposeImageUri(string src)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var client = "";
            if (src == null)
            {
                client = "http://staticfile.mohammadrezasardashti.ir/Resurces/Images/empty.jpg";
            }
            else
            {
                client = "http://staticfile.mohammadrezasardashti.ir/" + src.Replace("\\", "//");
            }

            if (environment == "Development")
            {
                if (src == null)
                {
                    client = "https://localhost:7012/Resurces/Images/empty.jpg";
                }
                else
                {
                    client = "https://localhost:7012/" + src.Replace("\\", "//");
                }
            }

            return client;
        }
    }
}
