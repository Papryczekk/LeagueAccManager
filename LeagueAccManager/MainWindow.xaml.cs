using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Microsoft.Win32;
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
                
            if (!CheckSettingsFile())
            {
                TabControl.SelectedIndex = 2;

                MessageBox.Show(
                    "Due to the fact that you are launching the application for the first time, please select the path for the game.",
                    "First launch", MessageBoxButton.OK);
            }
            
            LoadAccounts();
            InitializeRegions();
            LoadChampionsAsync();
            LoadSettings();
        }
        
        private void InitializeRegions()
        {
            Regions.ItemsSource = LeagueAccManager.Regions.GetRegions();
        }

        private bool CheckSettingsFile()
        {
            if (!File.Exists("settings.json"))
            {
                return false;
            }
            
            string json = File.ReadAllText("settings.json");
            return !string.IsNullOrWhiteSpace(json);
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
        
        private void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                var json = File.ReadAllText("settings.json");
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                PathTextBox.Text = settings?.LeaguePath ?? string.Empty;
            }
            else
            {
                PathTextBox.Text = string.Empty;
            }
        }

        private async void LoadChampionsAsync()
        {
            var championNames = await ChampionDataHandler.GetChampionsNamesAsync();
            
            ChampionsComboBox.ItemsSource = championNames;
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
            if (MiscTabControl.SelectedIndex == 0)
            {
                var selectedAccount = NicknamesComboBox.SelectedItem as Account;
                ProfileLinkHandler.OpenUggProfile(selectedAccount);
            }
            else if (MiscTabControl.SelectedIndex == 1)
            {
                var selectedChampion = ChampionsComboBox.SelectedItem as string;
                ChampionDataHandler.OpenUggChampionProfile(selectedChampion);
            }
            
        }

        private void OpggButton_Click(object sender, RoutedEventArgs e)
        {
            if (MiscTabControl.SelectedIndex == 0)
            {
                var selectedAccount = NicknamesComboBox.SelectedItem as Account;
                ProfileLinkHandler.OpenOpggProfile(selectedAccount);
            }
            else if (MiscTabControl.SelectedIndex == 1)
            {
                var selectedChampion = ChampionsComboBox.SelectedItem as string;
                ChampionDataHandler.OpenOpggChampionProfile(selectedChampion);    
            }
        }

        private void DeeplolButton_Click(object sender, RoutedEventArgs e)
        {
            if (MiscTabControl.SelectedIndex == 0)
            {
                var selectedAccount = NicknamesComboBox.SelectedItem as Account;
                ProfileLinkHandler.OpenDeeplolProfile(selectedAccount);
            }
            else if (MiscTabControl.SelectedIndex == 1)
            {
                var selectedChampion = ChampionsComboBox.SelectedItem as string;
                ChampionDataHandler.OpenDeeplolChampionProfile(selectedChampion);
            }
        }

        private void DpmButton_Click(object sender, RoutedEventArgs e)
        {
            if (MiscTabControl.SelectedIndex == 0)
            {
                var selectedAccount = NicknamesComboBox.SelectedItem as Account;
                ProfileLinkHandler.OpenDpmProfile(selectedAccount);
            }
            else if (MiscTabControl.SelectedIndex == 1)
            {
                var selectedChampion = ChampionsComboBox.SelectedItem as string;
                ChampionDataHandler.OpenDpmChampionProfile(selectedChampion);
            }
        }
        
        private void UggLiveGameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;
            ProfileLinkHandler.OpenUggProfile(selectedAccount, true);
        }

        private void OpggLiveGameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;
            ProfileLinkHandler.OpenOpggProfile(selectedAccount, true);
        }

        private void DeeplolLiveGameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;
            ProfileLinkHandler.OpenDeeplolProfile(selectedAccount, true);
        }

        private void DpmLiveGameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = NicknamesComboBox.SelectedItem as Account;
            ProfileLinkHandler.OpenDpmProfile(selectedAccount, true);
        }
        private void LolalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedChampion = ChampionsComboBox.SelectedItem as string;
            ChampionDataHandler.OpenLolalyticsChampionProfile(selectedChampion);
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.Title = "Select RiotClientServices.exe";

            if (openFileDialog.ShowDialog() == true)
            {
                PathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void SavePathButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new Settings
            {
                LeaguePath = PathTextBox.Text
            };
            
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("settings.json", json);
            
            MessageBox.Show("Path saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void myTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderText.Visibility = string.IsNullOrEmpty(PathTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void KillButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] processesToKill = { "Riot Client", "RiotClientServices", "LeagueClient", "LeagueClientUx", "LeagueCrashHandler64", "League of Legends"  };

                foreach (var processName in processesToKill)
                {
                    Process[] processes = Process.GetProcessesByName(processName);

                    foreach (var process in processes)
                    {
                        try
                        {
                            process.Kill();
                            process.WaitForExit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to kill process {processName}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}    