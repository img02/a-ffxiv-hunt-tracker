using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ufht_UI.Models
{
    public class Settings
    {
        public double Opacity;
        public double DefaultSizeX;
        public double DefaultSizeY;

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

            OnTopToggleGesture = new KeyGesture(Key.A, ModifierKeys.Control);
            OpacityToggleGesture = new KeyGesture(Key.S, ModifierKeys.Control);
            SidePaneLToggleGesture = new KeyGesture(Key.Tab);
            SSMapToggleGesture = new KeyGesture(Key.F, ModifierKeys.Control);
        }

        [JsonConstructor]
        public Settings(double opacity, double defaultSizeX, double defaultSizeY, 
            Hotkey onTopHotkey, Hotkey opacityHotKey, Hotkey sidePanelHotKey, Hotkey ssMapHotkey)
        {
            Opacity = opacity;
            DefaultSizeX = defaultSizeX;
            DefaultSizeY = defaultSizeY;

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
