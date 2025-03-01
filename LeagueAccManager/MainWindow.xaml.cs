using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;

namespace LeagueAccManager
{
    public partial class MainWindow : Window
    {
        private const string CredentialsFilePath = "credentials.json";
        private List<Account> _accounts;

        public MainWindow()
        {
            InitializeComponent();
            LoadAccounts();
            InitializeRegions();
        }
        
        private void InitializeRegions()
        {
            Regions.ItemsSource = LeagueAccManager.Regions.GetRegions();
        }

        private void LoadAccounts()
        {
            if (File.Exists(CredentialsFilePath))
            {
                var json = File.ReadAllText(CredentialsFilePath);
                _accounts = JsonConvert.DeserializeObject<List<Account>>(json) ?? new List<Account>();
            }
            else
            {
                _accounts = new List<Account>();
            }
            AccountsComboBox.ItemsSource = _accounts;
            NicknamesComboBox.ItemsSource = _accounts;
        }

        private void SaveAccounts()
        {
            var json = JsonConvert.SerializeObject(_accounts, Formatting.Indented);
            File.WriteAllText(CredentialsFilePath, json);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            var inGameName = igName.Text;
            var region = Regions.SelectedItem as string;
            var customTag = CustomTagTextBox.Text;
            

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(inGameName) || region == null)
            {
                MessageBox.Show("Please enter username, password, in-game name and select a region.");
                return;
            }

            _accounts.Add(new Account
            {
                Username = username, 
                Password = password, 
                InGameName = inGameName,
                Region = region,
                CustomTag = customTag
            });
            
            SaveAccounts();
            
            AccountsComboBox.ItemsSource = null;
            AccountsComboBox.ItemsSource = _accounts;
            AccountsComboBox.SelectedIndex = AccountsComboBox.Items.Count - 1;
            
            NicknamesComboBox.ItemsSource = null;
            NicknamesComboBox.ItemsSource = _accounts;
            NicknamesComboBox.SelectedIndex = NicknamesComboBox.Items.Count - 1;
            
        }
        
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountsComboBox.SelectedItem as Account;
            if (selectedAccount == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            AccLogin.Login(selectedAccount);
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountsComboBox.SelectedItem as Account;
            var nicknames = NicknamesComboBox.SelectedItem as Account;
            if (selectedAccount == null)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            _accounts.Remove(selectedAccount);
            _accounts.Remove(nicknames);
            SaveAccounts();
            
            AccountsComboBox.ItemsSource = null;
            AccountsComboBox.ItemsSource = _accounts;
            NicknamesComboBox.ItemsSource = null;
            NicknamesComboBox.ItemsSource = _accounts;
        }

        private void UggButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;

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
                
                string uggUrl = $"https://u.gg/lol/profile/{uggRegion}/{summonerName}/overview";

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

        private void OpggButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;

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
                
                string opggUrl = $"https://op.gg/summoners/{selectedAccount.Region}/{summonerName}";

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

        private void DeeplolButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;

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
                
                string deeplolUrl = $"https://www.deeplol.gg/summoner/{deeplolRegion}/{summonerName}";

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

        private void DpmButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;

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
                
                string dpmUrl = $"https://dpm.lol/{summonerName}";

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