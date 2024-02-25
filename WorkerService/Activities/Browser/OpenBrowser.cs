using OpenQA.Selenium;
using System.Diagnostics;
using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.Browser
{
    public class OpenBrowser : IExecutable<bool>
    {
        public Result<bool> Execute(object input)
        {
            var driver = SeleniumHelper.Instance.Driver;
            if (input is Dictionary<string, string> entries)
            {

                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1";

                if (entries.TryGetValue("Link", out string url))
                {
                    if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    {
                        url = "http://" + url;
                    }
                    driver.Navigate().GoToUrl(url);
                }
                return executionResult;
            }
            else
            {
                // Hatalı giriş parametresi
                Result<bool> executionResult = new Result<bool>(false);
                executionResult.Successful = true;
                return executionResult;
            }
        }

    }
}