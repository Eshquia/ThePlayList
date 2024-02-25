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
                if (entries.TryGetValue("Contains", out string partialLinkText) && entries.TryGetValue("ButtonType", out string elementType))
                {
                    IWebElement element = null;

                    try
                    {
                        // XPath ile bulma
                        switch (elementType.ToLower())
                        {
                            case "button":
                                element = driver.FindElement(By.XPath($"//button[contains(text(), '{partialLinkText}')]"));
                                break;
                            case "a":
                                element = driver.FindElement(By.XPath($"//a[contains(text(), '{partialLinkText}')]"));
                                break;
                            default:
                                Console.WriteLine("Geçersiz ElementType: " + elementType);
                                break;
                        }
                        element.Click();
                    }
                    catch (NoSuchElementException)
                    {
                        // Eğer element bulunamazsa, diğer arama yöntemlerini dene
                        try
                        {
                            // Id ile bulma
                            element = driver.FindElement(By.Id(partialLinkText));
                            element.Click();
                        }
                        catch (NoSuchElementException)
                        {
                            // Eğer Id ile bulunamazsa, diğer arama yöntemlerini dene
                            try
                            {
                                // Name ile bulma
                                element = driver.FindElement(By.Name(partialLinkText));
                                element.Click();
                            }
                            catch (NoSuchElementException e)
                            {
                                try
                                {
                                    // Css class ile bulma                                 
                                    string dynamicXPath = $"//button[@class='{partialLinkText}']";
                                    element = driver.FindElement(By.XPath(dynamicXPath));
                                    element.Click();
                                }
                                catch (NoSuchElementException ex)
                                {
                                    // Eğer element bulunamazsa gerekli hata işlemlerini gerçekleştirin
                                    Console.WriteLine("Element bulunamadı: " + e.Message);
                                }
                            }
                        }
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
