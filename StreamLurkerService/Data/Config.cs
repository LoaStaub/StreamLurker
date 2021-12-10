using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;

namespace StreamLurkerService.Data
{
    public static class Config
    {
        public static IWebDriver? Driver;
        public static Browsers Browser = Browsers.Firefox;
        public static Parameters Parameters = new Parameters();

        public static void ConfigureFirefoxDriver()
        {
            Driver = new FirefoxDriver();
            Driver.Navigate().GoToUrl("https://www.twitch.tv/");
        }

        public static void ConfigureChromeDriver()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://www.twitch.tv/");
        }

        public static void ConfigureOperaDriver()
        {
            Driver = new OperaDriver();
            Driver.Navigate().GoToUrl("https://www.twitch.tv/");
        }

        public static void ConfigureEdgeDriver()
        {
            Driver = new EdgeDriver();
            Driver.Navigate().GoToUrl("https://www.twitch.tv/");
        }
    }
}
