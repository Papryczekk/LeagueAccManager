using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;


namespace LeagueAccManager
{
    public static class ChampionDataHandler
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<List<string>> GetChampionsNamesAsync()
        {
            var championNames = new List<string>();

            try
            {
                string url = "https://ddragon.leagueoflegends.com/cdn/15.4.1/data/en_US/champion.json";
                
                var response = await httpClient.GetStringAsync(url);
                
                var json = JObject.Parse(response);
                var champions = json["data"];

                foreach (var champion in champions.Children())
                {
                    var championName = champion.First["name"].ToString();
                    championNames.Add(championName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching champions data: {ex.Message}");
            }
            return championNames;
        }
        
        public static void OpenUggChampionProfile(string selectedChampion)
        {
            if (!string.IsNullOrEmpty(selectedChampion))
            {
                string uggUrl = $"https://u.gg/lol/champions/{selectedChampion.ToLower().Replace(" ", "").Replace("'","")}/build";
                
                try
                {
                    Process.Start(new ProcessStartInfo(uggUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening u.gg: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a champion.");
            }
        }
        
        public static void OpenOpggChampionProfile(string selectedChampion)
        {
            if (!string.IsNullOrEmpty(selectedChampion))
            {
                string opggUrl = $"https://www.op.gg/champions/{selectedChampion.ToLower().Replace(" ", "").Replace("'","").Replace(".","")}/build";
                
                try
                {
                    Process.Start(new ProcessStartInfo(opggUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening op.gg: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a champion.");
            }
        }
        
        public static void OpenDeeplolChampionProfile(string selectedChampion)
        {
            if (!string.IsNullOrEmpty(selectedChampion))
            {
                string deeplolUrl = $"https://deeplol.gg/champions/{selectedChampion.ToLower().Replace(" ", "").Replace("'","").Replace(".","")}/build/";
                
                try
                {
                    Process.Start(new ProcessStartInfo(deeplolUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening deep.lol: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a champion.");
            }
        }
        
        public static void OpenDpmChampionProfile(string selectedChampion)
        {
            if (!string.IsNullOrEmpty(selectedChampion))
            {
                string dpmUrl = $"https://dpm.lol/champions/{selectedChampion.Replace(" ", "").Replace("'","").Replace(".","")}/build";
                
                try
                {
                    Process.Start(new ProcessStartInfo(dpmUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening dpm.lol: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a champion.");
            }
        }
        
        public static void OpenLolalyticsChampionProfile(string selectedChampion)
        {
            if (!string.IsNullOrEmpty(selectedChampion))
            {
                string dpmUrl = $"https://lolalytics.com/lol/{selectedChampion.ToLower().Replace(" ", "").Replace("'","").Replace(".","")}/build";
                
                try
                {
                    Process.Start(new ProcessStartInfo(dpmUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening dpm.lol: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a champion.");
            }
        }
    }
}