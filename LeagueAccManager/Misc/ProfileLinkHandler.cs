using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace LeagueAccManager
{
    public static class ProfileLinkHandler
    {
        public static void OpenUggProfile(Account selectedAccount, bool isLiveGame = false)
        {
            if (selectedAccount != null)
            {
                var regionMapping = new Dictionary<string, string>
                {
                    { "EUW", "euw1" },
                    { "EUNE", "eun1" },
                    { "NA", "na1" },
                    { "BR", "br1" },
                    { "JP", "jp1" },
                    { "KR", "kr" },
                    { "LAN", "la1" },
                    { "LAS", "la2" },
                    { "OCE", "oc1" },
                    { "TR", "tr1" },
                    { "RU", "ru" }
                };

                if (!regionMapping.TryGetValue(selectedAccount.Region, out var uggRegion))
                {
                    MessageBox.Show("Unsupported region.");
                    return;
                }
                
                string summonerName = selectedAccount.InGameName;

                if (!string.IsNullOrEmpty(selectedAccount.CustomTag))
                {
                    summonerName += $"-{selectedAccount.CustomTag}";
                }
                else
                {
                    summonerName += $"-{selectedAccount.Region}";
                }
                
                string uggUrl = isLiveGame ? $"https://u.gg/lol/profile/{uggRegion}/{summonerName}/live-game" : $"https://u.gg/lol/profile/{uggRegion}/{summonerName}/overview";

                try
                {
                    Process.Start(new ProcessStartInfo(uggUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Please select an account.");
            }
        }

        public static void OpenOpggProfile(Account selectedAccount, bool isLiveGame = false)
        {
            if (selectedAccount != null)
            {
                string summonerName = selectedAccount.InGameName;

                if (!string.IsNullOrEmpty(selectedAccount.CustomTag))
                {
                    summonerName += $"-{selectedAccount.CustomTag}";
                }
                else
                {
                    summonerName += $"-{selectedAccount.Region}";
                }
                
                string opggUrl = isLiveGame ? $"https://op.gg/summoners/{selectedAccount.Region}/{summonerName}/ingame" : $"https://op.gg/summoners/{selectedAccount.Region}/{summonerName}";

                try
                {
                    Process.Start(new ProcessStartInfo(opggUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Please select an account.");
            }
        }

        public static void OpenDeeplolProfile(Account selectedAccount, bool isLiveGame = false)
        {
            if (selectedAccount != null)
            {
                var regionMapping = new Dictionary<string, string>
                {
                    { "EUW", "euw" },
                    { "EUNE", "eun" },
                    { "NA", "na" },
                    { "BR", "br" },
                    { "JP", "jp" },
                    { "KR", "kr" },
                    { "LAN", "lan" },
                    { "LAS", "las" },
                    { "OCE", "oce" },
                    { "TR", "tr" },
                    { "RU", "ru" }
                };

                if (!regionMapping.TryGetValue(selectedAccount.Region, out var deeplolRegion))
                {
                    MessageBox.Show("Unsupported region.");
                    return;
                }
                
                string summonerName = selectedAccount.InGameName;

                if (!string.IsNullOrEmpty(selectedAccount.CustomTag))
                {
                    summonerName += $"-{selectedAccount.CustomTag}";
                }
                else
                {
                    summonerName += $"-{selectedAccount.Region}";
                }
                
                string deeplolUrl = isLiveGame ? $"https://www.deeplol.gg/summoner/{deeplolRegion}/{summonerName}/ingame" : $"https://www.deeplol.gg/summoner/{deeplolRegion}/{summonerName}";

                try
                {
                    Process.Start(new ProcessStartInfo(deeplolUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Please select an account.");
            }
        }

        public static void OpenDpmProfile(Account selectedAccount, bool isLiveGame = false)
        {
            if (selectedAccount != null)
            {
                string summonerName = selectedAccount.InGameName;

                if (!string.IsNullOrEmpty(selectedAccount.CustomTag))
                {
                    summonerName += $"-{selectedAccount.CustomTag}";
                }
                else
                {
                    summonerName += $"-{selectedAccount.Region}";
                }
                
                string dpmUrl = isLiveGame ? $"https://dpm.lol/{summonerName}/live" : $"https://dpm.lol/{summonerName}";

                try
                {
                    Process.Start(new ProcessStartInfo(dpmUrl) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Please select an account.");
            }
        }
    }
}