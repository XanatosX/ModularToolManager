using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager2.Services.IO
{
    public class UrlOpenerService : IUrlOpenerService
    {
        public bool OpenUrl(string url)
        {
            try
            {
                return OpenUrl(new Uri(url));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool OpenUrl(Uri url)
        {
            try
            {

                ProcessStartInfo processStartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = url.OriginalString
                };
                Process process = Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
