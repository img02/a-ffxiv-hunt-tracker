using Newtonsoft.Json;
using System.ComponentModel;

namespace ufht_UI.Models
{
    public class Settings
    {
        [DefaultValue(0.8)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double Opacity;

        [DefaultValue(1024)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double DefaultSizeX;

        [DefaultValue(1024)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double DefaultSizeY;


        [DefaultValue(64)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double MobIconSize;

        [DefaultValue(32)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double PlayerIconSize;

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool LogS;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool SRankTTS;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ARankTTS;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool BRankTTS;


        public Hotkey OnTopHotkey;
        public Hotkey OpacityHotKey;
        public Hotkey SidePanelHotKey;
        public Hotkey SSMapHotkey;
        public Hotkey SettingsHotkey;



        public Settings()
        {
            Opacity = 0.8;
            DefaultSizeX = 1024;
            DefaultSizeY = 1024;
            MobIconSize = 64;
            PlayerIconSize = 32;

            LogS = true;
            SRankTTS = true;
            ARankTTS = true;
            BRankTTS = true;

            OnTopHotkey = new Hotkey(Commands.DefaultOnTopKeyGesture);
            OpacityHotKey = new Hotkey(Commands.DefaultOpacityKeyGesture);
            SidePanelHotKey = new Hotkey(Commands.DefaultSidePanelKeyGesture);
            SSMapHotkey = new Hotkey(Commands.DefaultSSMapKeyGesture);
            SettingsHotkey = new Hotkey(Commands.DefaultSettingsWindowKeyGesture);
        }

        [JsonConstructor]
        public Settings(double opacity, double defaultSizeX, double defaultSizeY,
            double mobIconSize, double playerIconSize,
            bool logS, bool sRankTts, bool aRanktts, bool bRanktts,
            Hotkey onTopHotkey, Hotkey opacityHotKey, Hotkey sidePanelHotKey,
            Hotkey ssMapHotkey, Hotkey settingsHotkey)
        {
            Opacity = opacity;
            DefaultSizeX = defaultSizeX;
            DefaultSizeY = defaultSizeY;
            MobIconSize = mobIconSize;
            PlayerIconSize = playerIconSize;

            LogS = logS;
            SRankTTS = sRankTts;
            ARankTTS = aRanktts;
            BRankTTS = bRanktts;

            OnTopHotkey = onTopHotkey ?? new Hotkey(Commands.DefaultOnTopKeyGesture);
            OpacityHotKey = opacityHotKey ?? new Hotkey(Commands.DefaultOpacityKeyGesture);
            SidePanelHotKey = sidePanelHotKey ?? new Hotkey(Commands.DefaultSidePanelKeyGesture);
            SSMapHotkey = ssMapHotkey ?? new Hotkey(Commands.DefaultSSMapKeyGesture);
            SettingsHotkey = settingsHotkey ?? new Hotkey(Commands.DefaultSettingsWindowKeyGesture);
        }

        public void LoadKeyGestures()
        {
            Commands.ChangeKeyGesture(Commands.OnTop, OnTopHotkey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.OpacityToggle, OpacityHotKey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.SidePanelToggle, SidePanelHotKey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.SSMapToggle, SSMapHotkey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.SettingsWindowToggle, SettingsHotkey.HotkeyCombo);
        }
    }
}
