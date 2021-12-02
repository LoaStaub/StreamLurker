using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLurkerService.Data
{
    public class Parameters
    {
        public string[] PossibleBrowsers => Enum.GetNames<Browsers>();

        public string Browser
        {
            get => Enum.GetName(Config.Browser)!;
            set
            {
                switch (Config.Browser)
                {
                    case Browsers.Firefox:
                        Config.ConfigureFirefoxDriver();
                        break;
                    case Browsers.Chrome:
                        Config.ConfigureChromeDriver();
                        break;
                    case Browsers.Opera:
                        Config.ConfigureOperaDriver();
                        break;
                    case Browsers.Edge:
                        Config.ConfigureEdgeDriver();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                Config.Browser = Enum.Parse<Browsers>(value);
            }
        }
    }
}
