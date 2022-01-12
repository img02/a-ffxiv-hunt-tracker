using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Sharlayan.Core.Enums;

namespace ufht_UI.Models
{
    public class Settings
    {
        public double Opacity;
        public double DefaultSizeX;
        public double DefaultSizeY;

        [DefaultValue(64)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double MobIconSize;
        [DefaultValue(32)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double PlayerIconSize;



        public Hotkey OnTopHotkey;
        public Hotkey OpacityHotKey;
        public Hotkey SidePanelHotKey;
        public Hotkey SSMapHotkey;

        [JsonIgnore]
        public KeyGesture OnTopToggleGesture;
        [JsonIgnore]
        public KeyGesture OpacityToggleGesture;
        [JsonIgnore]
        public KeyGesture SidePaneLToggleGesture;
        [JsonIgnore]
        public KeyGesture SSMapToggleGesture;

        public Settings()
        {
            Opacity = 0.8;
            DefaultSizeX = 1024;
            DefaultSizeY = 1024;
            MobIconSize = 64;
            PlayerIconSize = 32;

            OnTopToggleGesture = new KeyGesture(Key.A, ModifierKeys.Control);
            OpacityToggleGesture = new KeyGesture(Key.S, ModifierKeys.Control);
            SidePaneLToggleGesture = new KeyGesture(Key.Tab);
            SSMapToggleGesture = new KeyGesture(Key.F, ModifierKeys.Control);
        }

        [JsonConstructor]
        public Settings(double opacity, double defaultSizeX, double defaultSizeY, double mobIconSize, double playerIconSize,
            Hotkey onTopHotkey, Hotkey opacityHotKey, Hotkey sidePanelHotKey, Hotkey ssMapHotkey)
        {
            Opacity = opacity;
            DefaultSizeX = defaultSizeX;
            DefaultSizeY = defaultSizeY;
            MobIconSize = mobIconSize;
            PlayerIconSize = playerIconSize;

            OnTopHotkey = onTopHotkey ?? new Hotkey(Commands.DefaultOnTopKeyGesture);
            OpacityHotKey = opacityHotKey ?? new Hotkey(Commands.DefaultOpacityKeyGesture);
            SidePanelHotKey = sidePanelHotKey ?? new Hotkey(Commands.DefaultSidePanelKeyGesture);
            SSMapHotkey = ssMapHotkey ?? new Hotkey(Commands.DefaultSSMapKeyGesture);

            OnTopToggleGesture = OnTopHotkey.HotkeyCombo;
            OpacityToggleGesture = OpacityHotKey.HotkeyCombo;
            SidePaneLToggleGesture = SidePanelHotKey.HotkeyCombo;
            SSMapToggleGesture = SSMapHotkey.HotkeyCombo;
        }

        public void LoadKeyGestures()
        {
            Commands.ChangeKeyGesture(Commands.OnTop, OnTopToggleGesture);
            Commands.ChangeKeyGesture(Commands.OpacityToggle, OpacityToggleGesture);
            Commands.ChangeKeyGesture(Commands.SidePanelToggle, SidePaneLToggleGesture);
            Commands.ChangeKeyGesture(Commands.SSMapToggle, SSMapToggleGesture);
        }
    }
}
