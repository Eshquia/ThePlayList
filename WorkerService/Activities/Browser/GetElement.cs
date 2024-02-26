using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Helpers;
using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.Browser
{
    public class GetElement:IExecutable<bool>
    {
        public Result<bool> Execute(object input)
        {
            var driver = SeleniumHelper.Instance.Driver;
            if (input is Dictionary<string, string> entries)
            {
                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1";
                if (entries.TryGetValue("Contains", out string elementValue) && entries.TryGetValue("ElementType", out string elementType) && entries.TryGetValue("out_1", out string out_1))
                {
                    IWebElement element = null;

                    try
                    {
                        // XPath ile bulma
                        switch (elementType.ToLower())
                        {
                            case "id":
                                element = driver.FindElement(By.Id(elementValue));
                                break;
                            case "xpath":
                                element = driver.FindElement(By.XPath(elementValue));
                                break;
                            case "classname":
                                element = driver.FindElement(By.ClassName(elementValue));
                                break;
                            case "name":
                                element = driver.FindElement(By.Name(elementValue));
                                break;
                            case "tagname":
                                element = driver.FindElement(By.TagName(elementValue));
                                break;
                            default:
                                Console.WriteLine("Geçersiz ElementType: " + elementType);
                                break;
                        }

                   
                        if (element != null)
                        {
                            string innerText = element.GetAttribute("innerText");
                            RedisHelper.Set(out_1, innerText);
                        }
                        else
                        {
                            executionResult.Successful = false;
                            Console.WriteLine("Element bulunamadı.");
                        }
                    }
                    catch (Exception e)
                    {
                        executionResult.Successful = false;
                    }
                }

                return executionResult;
            }
            else
            {
                // Hatalı giriş parametresi
                Result<bool> executionResult = new Result<bool>(false);
                executionResult.Successful = false;
                return executionResult;
            }
        }
    }
}

