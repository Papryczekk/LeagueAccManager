using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace LeagueAccManager
{
    public class Settings
    {
        public string LeaguePath { get; set; }
        
        public static string GetLeaguePathFromSettings()
        {
            try
            {
                if (File.Exists("settings.json"))
                {
                    string json = File.ReadAllText("settings.json");
                    var settings = JsonConvert.DeserializeObject<Settings>(json);
                    return settings.LeaguePath;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error reading settings.json: {e.Message}");
            }
            
            return null;
        }
        
        /*public static string SaveLeaguePathToSettings(string leaguePath)
        {
            if (!File.Exists(GetLeaguePathFromSettings()))
            {
                return null;
            }
            
            string json = File.ReadAllText("settings.json");
            var settings = JsonConvert.DeserializeObject<Settings>(json);
            return settings.LeaguePath;
        }*/
    }
}