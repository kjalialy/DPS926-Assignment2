using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class NetworkingManager
    {
        private string players_url = "https://data.nba.com/prod/v1/2021/players.json";
        private string teams_url = "https://data.nba.com/prod/v2/2021/teams.json";

        HttpClient client = new HttpClient();
        public NetworkingManager()
        {
        }


        public async Task<List<Player>> GetPlayers()
        {
            var response = await client.GetAsync(players_url);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<Player>();
            }
            else
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>
                    (jsonString);
                var array = dic.ElementAt(1).Value;
                var newArray = JsonConvert.DeserializeObject<Dictionary<string, object>>(array.ToString());
                var standard = newArray.ElementAt(0).Value;

                var playerList = JsonConvert.DeserializeObject<List<Player>>(standard.ToString());
                return playerList;
            }
        }

        public async Task<List<TeamObj>> GetTeams()
        {
            var response = await client.GetAsync(teams_url);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<TeamObj>();
            }
            else
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>
                    (jsonString);
                var array = dic.ElementAt(0).Value;
                var newArray = JsonConvert.DeserializeObject<Dictionary<string, object>>(array.ToString());
                var standard = newArray.ElementAt(0).Value;

                var teamList = JsonConvert.DeserializeObject<List<TeamObj>>(standard.ToString());
                return teamList;
            }
        }

        public async Task<PlayerStats> GetPlayerStats(string playerId)
        {
            string url = $"https://data.nba.com/prod/v1/2021/players/{playerId}_profile.json";
            var response = await client.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new PlayerStats();
            }
            else
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>
                    (jsonString);
                var array = dic.ElementAt(1).Value;
                var newArray = JsonConvert.DeserializeObject<Dictionary<string, object>>(array.ToString());

                var standard = newArray.ElementAt(0).Value;

                var stats = JsonConvert.DeserializeObject<Dictionary<string, object>>(standard.ToString());

                var careerSummary = stats.ElementAt(1).Value;

                var getData = JsonConvert.DeserializeObject<Dictionary<string, object>>(careerSummary.ToString());

                var data = getData.ElementAt(1).Value;

                return JsonConvert.DeserializeObject<PlayerStats>(data.ToString());
            }
        }

    }
}
