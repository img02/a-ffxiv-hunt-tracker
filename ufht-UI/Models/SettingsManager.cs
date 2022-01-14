using Newtonsoft.Json;
using System;
using System.IO;

namespace ufht_UI.Models
{
    public class SettingsManager
    {
        private readonly string ConfigFileLocation = "./data/config.json";

        public Settings UserSettings;

        public event EventHandler<Settings> PropertyChanged;


        public SettingsManager()
        {
            LoadSettings();
        }



        public void LoadSettings()
        {
            if (File.Exists(ConfigFileLocation))
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                UserSettings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(ConfigFileLocation), jsonSettings);
                UserSettings?.LoadKeyGestures();
            }

            else if (!File.Exists(ConfigFileLocation) || UserSettings == null)
            {
                CreateDefaultSettings();
            }
            SaveSettings();
        }

        public void SaveSettings()
        {
            File.WriteAllText(ConfigFileLocation, JsonConvert.SerializeObject(UserSettings, Formatting.Indented));
            PropertyChanged?.Invoke(this, UserSettings);
        }

        private void CreateDefaultSettings()
        {
            File.Create(ConfigFileLocation).Close();
            UserSettings = new Settings();
            UserSettings.LoadKeyGestures();
        }
    }
}
