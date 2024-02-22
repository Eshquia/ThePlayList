using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

public sealed class SeleniumHelper
{
    private static readonly SeleniumHelper instance = new SeleniumHelper();
    private IWebDriver driver;

    private SeleniumHelper()
    {
        // ChromeDriver'ı başlat
        driver = new ChromeDriver();
    }

    public static SeleniumHelper Instance
    {
        get { return instance; }
    }

    public IWebDriver Driver
    {
        get { return driver; }
    }


}