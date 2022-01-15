using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using ufht_UI.UserSettings;

namespace ufht_UI.HotkeyCommands
{
    public static class GlobalHotkey
    {

        //////////// Global hotkey  - base code from https://stackoverflow.com/questions/11377977/global-hotkeys-in-wpf-working-from-every-window


        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);

        private static HwndSource _source;

        private const int OPACITY_HOTKEY_ID = 9000;
        private const int ONTOP_HOTKEY_ID = 9001;
        private const int SSMAP_HOTKEY_ID = 9002;
        private const int CLICKTHRU_HOTKEY_ID = 9003;
        private static List<int> REGISTEREDHOTKEYS = new List<int>();

        //https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        //https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-hotkey
        const uint MOD_CTRL = 0x0002;
        const uint MOD_ALT = 0x0001;
        const uint MOD_SHIFT = 0x0004;

        private static Window _w = null;

        private static Settings _settings;

        public static void OnSourceInitialized(Window w, Settings settings)
        {
            _w = w;
            _settings = settings;

            var helper = new WindowInteropHelper(w);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
            RegisterHotKey(w);
        }

        public static void OnClosed(Window w)
        {
            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterAllHotKeys(w);
        }


        public static void RegisterHotKey(Window w)
        {
            var helper = new WindowInteropHelper(w);

            RegisterHotkeyHelper(_settings.OpacityGlobal, _settings.OpacityHotKey.HotkeyCombo, OPACITY_HOTKEY_ID, helper);
            RegisterHotkeyHelper(_settings.OnTopGlobal, _settings.OnTopHotkey.HotkeyCombo, ONTOP_HOTKEY_ID, helper);
            RegisterHotkeyHelper(_settings.SSMapGlobal, _settings.SSMapHotkey.HotkeyCombo, SSMAP_HOTKEY_ID, helper);
            RegisterHotkeyHelper(_settings.ClickThruGlobal, _settings.ClickThruHotkey.HotkeyCombo, CLICKTHRU_HOTKEY_ID, helper);
        }

        //unregister all, then re-register.
        public static void VerifyHotKeys(Window w)
        {
            UnregisterAllHotKeys(w);
            RegisterHotKey(w);
        }

        public static void UnregisterAllHotKeys(Window w)
        {
            var helper = new WindowInteropHelper(w);
            foreach (var i in REGISTEREDHOTKEYS)
            {
                UnregisterHotKey(helper.Handle, i);
            }
            REGISTEREDHOTKEYS.Clear();
        }

        private static IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case OPACITY_HOTKEY_ID:
                            OnOpacityHotKeyPressed();
                            handled = true;
                            break;
                        case ONTOP_HOTKEY_ID:
                            OnOnTopHotKeyPressed();
                            handled = true;
                            break;
                        case SSMAP_HOTKEY_ID:
                            OnSSMapHotKeyPressed();
                            handled = true;
                            break;
                        case CLICKTHRU_HOTKEY_ID:
                            OnClickThruHotKeyPressed();
                            handled = true;
                            break;
                    }

                    break;
            }

            return IntPtr.Zero;
        }


        //on hotkeys
        public static void OnOpacityHotKeyPressed()
        {
            var ww = ((MainWindow)_w);
            ww.OpacityToggle();

        }

        public static void OnOnTopHotKeyPressed()
        {
            var ww = ((MainWindow)_w);
            ww.OnTopToggle();
        }

        public static void OnSSMapHotKeyPressed()
        {
            var ww = ((MainWindow)_w);
            ww.SSMapToggle();
        }

        public static void OnClickThruHotKeyPressed()
        {
            var ww = ((MainWindow)_w);
            ww.ClickThruToggle();
        }


        //helpers

        //convert keys to virtual keycodes 
        public static (uint, uint) ConvertToVirtualKeyCode(KeyGesture keyGesture)
        {

            var k = (uint)(Keys)KeyInterop.VirtualKeyFromKey(keyGesture.Key);
            var m = ConvertModifier(keyGesture);

            return (k, m);
        }

        public static uint ConvertModifier(KeyGesture keyGesture) => keyGesture.Modifiers switch
        {
            ModifierKeys.Alt => MOD_ALT,
            ModifierKeys.Control => MOD_CTRL,
            ModifierKeys.Shift => MOD_SHIFT
        };


        //register helped
        private static void RegisterHotkeyHelper(bool doIt, KeyGesture keyGesture, int hotkeyID, WindowInteropHelper helper)
        {
            if (doIt)
            {
                var (k, m) = ConvertToVirtualKeyCode(keyGesture);


                if (!RegisterHotKey(helper.Handle, hotkeyID, m, k))
                {
                    // handle error
                }
                else
                {
                    REGISTEREDHOTKEYS.Add(hotkeyID);
                }
            }
        }


    }

    public static class ClickThru
    {
        //code based on RainbowMages OverlayPlugin - OverlayForm.cs NativeMethod.cs
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        public static void EnableMouseClickThru(Window w)
        {
            var helper = new WindowInteropHelper(w);

            SetWindowLong(
                helper.Handle,
                GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT);
        }

        public static void DisableMouseClickThru(Window w)
        {
            var helper = new WindowInteropHelper(w);

            SetWindowLong(
                helper.Handle,
                GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) & ~WS_EX_TRANSPARENT);
        }
    }
}

