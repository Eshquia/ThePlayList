﻿using OpenQA.Selenium;
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

                if (entries.TryGetValue("Contains", out string elementValue) && entries.TryGetValue("TextValue", out string elementNewValue) && entries.TryGetValue("ElementType", out string elementType) )
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    try
                    {
                        IWebElement element = null;

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

                        element.SendKeys(elementNewValue);
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