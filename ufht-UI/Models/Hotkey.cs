using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ufht_UI.Models
{
    public class Hotkey
    {
        public int ModifierKeyCode;
        public int KeyCode;

        [JsonIgnore]
        public string Error;
        [JsonIgnore]
        public KeyGesture HotkeyCombo { get; private set; }


        public Hotkey(KeyGesture keyGesture)
        {
            ModifierKeyCode = (int)keyGesture.Modifiers;
            KeyCode = (int)keyGesture.Key;
            HotkeyCombo = keyGesture;
        }

        [JsonConstructor]
        public Hotkey(int keyCode, int modifierKeyCode)
        {
            KeyCode = keyCode;
            ModifierKeyCode = modifierKeyCode;

            try
            {
                HotkeyCombo = new KeyGesture((Key)KeyCode, (ModifierKeys)ModifierKeyCode);
            }
            catch (InvalidEnumArgumentException e)
            {
                Error = e.Message;
                Trace.WriteLine(e.Message);
                HotkeyCombo = null;
            }
        }

        public void CreateGestureOnLoad()
        {
            HotkeyCombo = new KeyGesture((Key)KeyCode, (ModifierKeys)ModifierKeyCode);
        }
    }
}
