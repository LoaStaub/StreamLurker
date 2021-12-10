using System.Text.Json;

namespace StreamLurkerService.Data
{
    public class Handler
    {
        private static bool _gotParameters = false;

        public static async Task<bool> GetParameters()
        {
            if (_gotParameters)
            {
                return true;
            }
            if(!File.Exists("Config/Parameters.json"))
            {
                SetParameters();
                return false;
            }
            Config.Parameters = (await JsonSerializer.DeserializeAsync<Parameters>(File.OpenRead("Config/Parameters.json")))!;
            _gotParameters = true;
            return true;
        }

        private static void SetParameters()
        {
            var json = JsonSerializer.Serialize(new Parameters()); //todo: Async
            Directory.CreateDirectory("Config");
            File.WriteAllText("Config/Parameters.json", json);
        }

        public static async Task<StreamerList> GetStreamers()
        {
            if (!File.Exists("Config/Streamers.json"))
            {
                SetStreamers();
            }

            using (var json = File.OpenRead("Config/Streamers.json"))
            {
                return (await JsonSerializer.DeserializeAsync<StreamerList>(json))!;
            }
        }

        public static void SetStreamers()
        {
            var list = new List<Streamer>();
            list.Add(new ()
            {
                StreamerUrl = "https://www.twitch.tv/loastaub",
            });
            list.Add(new()
            {
                StreamerUrl = "https://www.twitch.tv/PlayerUnknrw"
            });
            var json = JsonSerializer.Serialize(new StreamerList
            {
                Streamers = list
            });
            File.WriteAllText("Config/Streamers.json", json);
        }
    }
}
