using Newtonsoft.Json;
using System.ComponentModel;
using ufht_UI.HotkeyCommands;

namespace ufht_UI.UserSettings
{
    public class Settings
    {
        [DefaultValue(60)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int RefreshRate;
        
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

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool OpacityGlobal;

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool OnTopGlobal;

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool SSMapGlobal;

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ClickThruGlobal;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ClickThruWhenOnTop;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool UpdatePriorityMobCoordinates;


        public Hotkey OnTopHotkey;
        public Hotkey OpacityHotKey;
        public Hotkey SidePanelHotKey;
        public Hotkey SSMapHotkey;
        public Hotkey SettingsHotkey;
        public Hotkey ClickThruHotkey;



        public Settings()
        {
            RefreshRate = 60;
            Opacity = 0.8;
            DefaultSizeX = 1024;
            DefaultSizeY = 1024;
            MobIconSize = 64;
            PlayerIconSize = 32;

            LogS = false;
            SRankTTS = true;
            ARankTTS = true;
            BRankTTS = true;

            OpacityGlobal = false;
            OnTopGlobal = false;
            SSMapGlobal = false;
            ClickThruGlobal = false;
            ClickThruWhenOnTop = true;
            UpdatePriorityMobCoordinates = true;

            OnTopHotkey = new Hotkey(Commands.DefaultOnTopKeyGesture);
            OpacityHotKey = new Hotkey(Commands.DefaultOpacityKeyGesture);
            SidePanelHotKey = new Hotkey(Commands.DefaultSidePanelKeyGesture);
            SSMapHotkey = new Hotkey(Commands.DefaultSSMapKeyGesture);
            SettingsHotkey = new Hotkey(Commands.DefaultSettingsWindowKeyGesture);
            ClickThruHotkey = new Hotkey(Commands.DefaultClickThruKeyGesture);
        }

        [JsonConstructor]
        public Settings(int refreshRate, double opacity, double defaultSizeX,
            double defaultSizeY, double mobIconSize, double playerIconSize,
            bool logS, bool sRankTts, bool aRanktts, bool bRanktts,
            bool opacityGlobal, bool onTopGlobal, bool ssMapGlobal,
            bool clickThruGlobal, bool clickThruWhenOnTop, bool updatePriorityMobCoordinates,
            Hotkey onTopHotkey, Hotkey opacityHotKey, Hotkey sidePanelHotKey,
            Hotkey ssMapHotkey, Hotkey settingsHotkey, Hotkey clickThruHotkey)
        {
            RefreshRate = refreshRate;

            Opacity = opacity;
            DefaultSizeX = defaultSizeX;
            DefaultSizeY = defaultSizeY;
            MobIconSize = mobIconSize;
            PlayerIconSize = playerIconSize;

            LogS = logS;
            SRankTTS = sRankTts;
            ARankTTS = aRanktts;
            BRankTTS = bRanktts;

            OpacityGlobal = opacityGlobal;
            OnTopGlobal = onTopGlobal;
            SSMapGlobal = ssMapGlobal;
            ClickThruGlobal = clickThruGlobal;
            ClickThruWhenOnTop = clickThruWhenOnTop;
            UpdatePriorityMobCoordinates = updatePriorityMobCoordinates;

            OnTopHotkey = onTopHotkey ?? new Hotkey(Commands.DefaultOnTopKeyGesture);
            OpacityHotKey = opacityHotKey ?? new Hotkey(Commands.DefaultOpacityKeyGesture);
            SidePanelHotKey = sidePanelHotKey ?? new Hotkey(Commands.DefaultSidePanelKeyGesture);
            SSMapHotkey = ssMapHotkey ?? new Hotkey(Commands.DefaultSSMapKeyGesture);
            SettingsHotkey = settingsHotkey ?? new Hotkey(Commands.DefaultSettingsWindowKeyGesture);
            ClickThruHotkey = clickThruHotkey ?? new Hotkey(Commands.DefaultClickThruKeyGesture);
        }

        public void LoadKeyGestures()
        {
            Commands.ChangeKeyGesture(Commands.OnTop, OnTopHotkey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.OpacityToggle, OpacityHotKey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.SidePanelToggle, SidePanelHotKey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.SSMapToggle, SSMapHotkey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.SettingsWindowToggle, SettingsHotkey.HotkeyCombo);
            Commands.ChangeKeyGesture(Commands.ClickThruToggle, ClickThruHotkey.HotkeyCombo);
        }
    }
}
