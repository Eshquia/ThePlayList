using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.Browser
{
    public class ClickElement : IExecutable<bool>
    {
        public Result<bool> Execute(object input)
        {
            var driver = SeleniumHelper.Instance.Driver;
            if (input is Dictionary<string, string> entries)
            {
                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1";
                if (entries.TryGetValue("Contains", out string elementValue) && entries.TryGetValue("ButtonType", out string elementType))
                {
                    IWebElement element = null;

                    try
                    {

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
                               element.Click();
                            }
                          else
                            {
                                executionResult.Successful = false;
                                Console.WriteLine("Element bulunamadı.");
                             }

                    }
                    catch (NoSuchElementException)
                    {
                        
                    }
                    finally
                    {
                        // Tarayıcıyı kapat
                    //    driver.Quit();
                    }
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
