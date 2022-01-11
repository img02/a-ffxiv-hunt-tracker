using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ufht_UI
{
    public static class Commands
    {
        public static readonly KeyGesture DefaultOnTopKeyGesture = new KeyGesture(Key.A, ModifierKeys.Control);
        public static readonly KeyGesture DefaultOpacityKeyGesture = new KeyGesture(Key.S, ModifierKeys.Control);
        public static readonly KeyGesture DefaultSidePanelKeyGesture = new KeyGesture(Key.Tab);
        public static readonly KeyGesture DefaultSSMapKeyGesture = new KeyGesture(Key.F, ModifierKeys.Control);
        public static readonly KeyGesture DefaultSettingsWindowKeyGesture = new KeyGesture(Key.Q, ModifierKeys.Control);


        public static readonly RoutedUICommand OnTop = new RoutedUICommand
        (
            "On Top",
            "On Top",
            typeof(Commands),
            new InputGestureCollection()
            {
                DefaultOnTopKeyGesture
            }
        );
        
        public static readonly RoutedUICommand OpacityToggle = new RoutedUICommand
        (
            "Opacity Toggle",
            "Opacity Toggle",
            typeof(Commands),
            new InputGestureCollection()
            {
                DefaultOpacityKeyGesture
            }
        );
        
        public static readonly RoutedUICommand SidePanelToggle = new RoutedUICommand
        (
            "SidePanel Toggle",
            "SidePanel Toggle",
            typeof(Commands),
            new InputGestureCollection()
            {
                DefaultSidePanelKeyGesture
            }
        );
         
        public static readonly RoutedUICommand SSMapToggle = new RoutedUICommand
        (
            "SSMap Toggle",
            "SSMap Toggle",
            typeof(Commands),
            new InputGestureCollection()
            {
                DefaultSSMapKeyGesture
            }
        );
        
        public static readonly RoutedUICommand SettingsWindowToggle = new RoutedUICommand
        (
            "SettingsWindow Toggle",
            "SettingsWindow Toggle",
            typeof(Commands),
            new InputGestureCollection()
            {
                DefaultSettingsWindowKeyGesture
            }
        );

        //Define more commands here, just like the one above




        public static void ChangeKeyGesture(RoutedUICommand command, KeyGesture keyCombo)
        {
            command.InputGestures.Clear();
            command.InputGestures.Add(keyCombo);
        }
    }
}
