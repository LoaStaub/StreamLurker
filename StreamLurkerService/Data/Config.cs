using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;

namespace StreamLurkerService.Data
{
    public static class Config
    {
        public static IWebDriver Driver = new FirefoxDriver();
        public static Browsers Browser = Browsers.Firefox;
        public static Parameters Parameters = new Parameters();

        public static void ConfigureFirefoxDriver()
        {
            Driver = new FirefoxDriver();
        }

        public static void ConfigureChromeDriver()
        {
            Driver = new ChromeDriver();
        }

        public static void ConfigureOperaDriver()
        {
            Driver = new OperaDriver();
        }

        public static void ConfigureEdgeDriver()
        {
            Driver = new EdgeDriver();
        }
    }
}
