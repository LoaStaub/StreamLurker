using OpenQA.Selenium;
using StreamLurkerService.Data;

namespace StreamLurkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        private static List<Streamer> _oldStreamers = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var firstStart = true;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTime.Now}");
                if (!await Handler.GetParameters())
                {
                    await Task.Delay(60000, stoppingToken);
                    continue;
                }
                await Task.Delay(5000, stoppingToken);
                if (firstStart)
                {
                    Config.Driver.FindElement(By.CssSelector(".ScCoreButtonPrimary-sc-1qn4ixc-1 .Layout-sc-nxg1ff-0 > .Layout-sc-nxg1ff-0"))?.Click();
                    await Task.Delay(5000, stoppingToken);
                    Config.Driver.FindElement(By.CssSelector(".ScCoreButtonSecondary-sc-1qn4ixc-2 .Layout-sc-nxg1ff-0"))?.Click();
                    await Task.Delay(120000, stoppingToken);
                    // todo: Add automated Login
                    //Config.Driver.FindElement(By.Id("login-username")).Click();
                    //Config.Driver.FindElement(By.Id("password-input")).Click();
                    //Config.Driver.FindElement(By.CssSelector(".ibRTKs")).Click();
                }

                var streamers = (await Handler.GetStreamers()).Streamers;
                foreach (var streamer in streamers.Where(streamer => _oldStreamers.All(d => d.StreamerUrl != streamer.StreamerUrl)))
                {
                    if (!firstStart)
                    {
                        Config.Driver.SwitchTo().NewWindow(WindowType.Window);
                    }
                    Config.Driver.Navigate().GoToUrl(streamer.StreamerUrl);
                    firstStart = false;
                    //todo: Add Window ID to List to close Streamers not available anymore
                }

                _oldStreamers = streamers;

                foreach (var tab in Config.Driver.WindowHandles)
                {
                    Config.Driver.SwitchTo().Window(tab);
                    try
                    {
                        Config.Driver.FindElement(By.CssSelector(".jLsLts .ScIconSVG-sc-1bgeryd-1"))?.Click();
                    }
                    catch
                    {
                        Console.WriteLine($"No Points to gather");
                    }
                }
                await Task.Delay(60000, stoppingToken);
            }
            Config.Driver.Quit();
        }
    }
}