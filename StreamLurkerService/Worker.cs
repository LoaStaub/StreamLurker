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
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTime.Now}");
                if (!await Handler.GetParameters())
                {
                    continue;
                }

                var streamers = (await Handler.GetStreamers()).Streamers;
                foreach (var streamer in streamers.Where(streamer => !_oldStreamers.Contains(streamer)))
                {
                    Config.Driver.SwitchTo().NewWindow(WindowType.Window);
                    Config.Driver.Navigate().GoToUrl(streamer.StreamerUrl);
                    //todo: Add Window ID to List to close Streamers not available anymore
                }

                _oldStreamers = streamers;

                foreach (var tab in Config.Driver.WindowHandles)
                {
                    Config.Driver.SwitchTo().Window(tab);
                    try
                    {
                        Config.Driver.FindElement(By.ClassName("ScCoreButton-sc-1qn4ixc-0 ScCoreButtonSuccess-sc-1qn4ixc-5 jGqsfG hERNRa"))?.Click();
                    }
                    catch
                    {
                        Console.WriteLine($"No Points to gather");
                        throw;
                    }
                }
                await Task.Delay(60000, stoppingToken);
            }
            Config.Driver.Quit();
        }
    }
}