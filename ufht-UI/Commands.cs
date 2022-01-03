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
        public static readonly RoutedUICommand OnTop = new RoutedUICommand
        (
            "On Top",
            "On Top",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.A, ModifierKeys.Control)
            }
        );
        
        public static readonly RoutedUICommand OpacityToggle = new RoutedUICommand
        (
            "Opacity Toggle",
            "Opacity Toggle",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            }
        );
        
        public static readonly RoutedUICommand SidePanelToggle = new RoutedUICommand
        (
            "SidePanel Toggle",
            "SidePanel Toggle",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Tab)
            }
        );

        //Define more commands here, just like the one above
    }
}
