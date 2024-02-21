using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Models
{
    public class OpenBrowser
    {
        public string Url { get; private set; }

        // Örnek output parametresi
        public bool Result { get; private set; }

        // Constructor (kurucu) metodu
        public OpenBrowser(string? url)
        {
            Url = url;
        }

        // Metodun çağrılacağı yer
        public Result<bool> Execute()
        {
            try
            {
                return new Result<bool>(true) { Successful = true };
            }
            catch (Exception ex)
            {

                return new Result<bool>(false) { Successful = false };
            }
        }
    }
}
