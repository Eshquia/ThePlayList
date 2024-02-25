using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.Browser
{
    public class SendElement : IExecutable<bool>
    {
        public Result<bool> Execute(object input)
        {
            var driver = SeleniumHelper.Instance.Driver;
            if (input is Dictionary<string, string> entries)
            {
                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1";

                if (entries.TryGetValue("TextId", out string textId) && entries.TryGetValue("TextValue", out string textValue))
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    try
                    {
                        IWebElement inputElement = null;

                        // Eğer TextId varsa, bu öğeyi bul
                        if (!string.IsNullOrEmpty(textId))
                        {
                            try
                            {
                                inputElement = driver.FindElement(By.Id(textId));
                            }
                            catch (NoSuchElementException)
                            {
                                // Eğer Id ile bulunamazsa, null'a çekip XPath ile arama yapacak.
                                inputElement = null;
                            }
                        }

                        // Eğer TextId bulunamazsa XPath ile arama yap
                        if (inputElement == null)
                        {
                            try
                            {
                                inputElement = driver.FindElement(By.XPath($"//input[contains(@placeholder, '{textId}')]"));
                            }
                            catch (NoSuchElementException)
                            {
                                // Eğer Id ile bulunamazsa, null'a çekip XPath ile arama yapacak.
                                inputElement = null;
                            }

                        }

                        if (inputElement == null)
                        {
                            try
                            {
                                inputElement = driver.FindElement(By.Name(textId));
                            }
                            catch (NoSuchElementException)
                            {
                                // Eğer Name ile de bulunamazsa, hata işlemlerini gerçekleştirin
                                Console.WriteLine("Element bulunamadı: " + textId);
                                return executionResult;
                            }
                        }

                        // Değer girme işlemini gerçekleştir
                        inputElement.SendKeys(textValue);
                    }
                    catch (NoSuchElementException e)
                    {
                        // Eğer element bulunamazsa gerekli hata işlemlerini gerçekleştirin
                        Console.WriteLine("Element bulunamadı: " + e.Message);
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