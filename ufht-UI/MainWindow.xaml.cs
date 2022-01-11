using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ufht_UI.DialogWindow;
using ufht_UI.Models;
using ufht_UI.UserControls;
using ufht_UI.UserControls.InfoSection;
using untitled_ffxiv_hunt_tracker;
using untitled_ffxiv_hunt_tracker.Entities;
using untitled_ffxiv_hunt_tracker.ViewModels;

namespace ufht_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Session _session;

        //side panel section
        private InfoSectionControl _listSectionMain;

        //main map section
        private MainMapControl _mainMap;

        private ObservableCollection<Mob> _nearbyMobs;
        internal Mob priorityMob;


        private SettingsManager _settingsManager;
        private Settings _userSettings;

        public MainWindow()
        {
            _settingsManager = new SettingsManager();
            _userSettings = _settingsManager.UserSettings;


            Application.Current.Resources["ProgramWidth"] = _userSettings.DefaultSizeX;
            Application.Current.Resources["ProgramHeight"] = _userSettings.DefaultSizeY;


            Application.Current.Resources["_sidePanelStartingWidth"] = 0.0;


            Application.Current.Resources["PriorityMobTextVisibility"] = Visibility.Hidden;
            Application.Current.Resources["PriorityMobGridInnerVisibility"] = Visibility.Hidden;

            //Application.Current.Resources["PriorityMobTextColour"] = Brushes.Aquamarine;
            Application.Current.Resources["PriorityMobTextColour"] = Brushes.WhiteSmoke;
            /*Application.Current.Resources["PriorityMobGridBackground"] = new System.Windows.Media.SolidColorBrush(
                (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#444444")); */
            Application.Current.Resources["PriorityMobGridBackground"] = Brushes.Transparent;

            Application.Current.Resources["PriorityMobTextNamFontSize"] = 20.0;
            Application.Current.Resources["PriorityMobTextCoordsFontSize"] = 20.0;
            Application.Current.Resources["PriorityMobTextRankFontSize"] = 35.0;
            Application.Current.Resources["PriorityMobTextHPPFontSize"] = 26.0;
            Application.Current.Resources["PriorityMobTTFontSize"] = 20.0;

            Application.Current.Resources["ProgramOpacity"] = 1.0;
            Application.Current.Resources["ProgramTopMost"] = false;

            InitializeComponent();
            _nearbyMobs = new ObservableCollection<Mob>();
            _ = Task.Run(() =>
            {
                _session = new Session();

                Dispatcher.Invoke(() =>
                {
                    _listSectionMain = new InfoSectionControl(_session, _nearbyMobs);
                    _session.CurrentNearbyMobs.CollectionChanged += CurrentNearbyMobs_CollectionChanged;

                    _mainMap = new MainMapControl(_session);

                    MainGrid2.Children.Add(_mainMap);
                    ListSection.Children.Add(_listSectionMain);

                    DataContext = _session;
                });
                _session.Start();
            });

        }

        //info side panel stuff
        private void CurrentNearbyMobs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            #region brokenModelID
            //BROKEN MODELID VERSION
            /* if (sender is ObservableCollection<Mob> mobCollection)
             {

                 Dispatcher.Invoke(() =>
                 {
                     //Application.Current.Resources["_nearbyMobs"] = _nearbyMobs;
                     var toRemove = new ObservableCollection<Mob>();
                     foreach (var m in _nearbyMobs)
                     {
                         if (mobCollection.FirstOrDefault(m1 => m1.ModelID == m.ModelID) == null)
                         {
                             toRemove.Add(m);
                         }
                     }

                     foreach (var m in toRemove)
                     {
                         _nearbyMobs.Remove(m);
                     }


                     foreach (var m in mobCollection)
                     {
                         if (_nearbyMobs.FirstOrDefault(m1 => m1.ModelID == m.ModelID) == null)
                         {
                             _nearbyMobs.Add(m);
                         }
                     }
                 });
             }*/
            #endregion

            if (sender is ObservableCollection<Mob> mobCollection)
            {

                Dispatcher.Invoke(() =>
                {
                    var toRemove = new ObservableCollection<Mob>();
                    foreach (var m in _nearbyMobs)
                    {
                        if (mobCollection.FirstOrDefault(m1 => m1.Name == m.Name) == null)
                        {
                            toRemove.Add(m);
                        }
                    }

                    foreach (var m in toRemove)
                    {
                        _nearbyMobs.Remove(m);
                    }


                    foreach (var m in mobCollection)
                    {
                        if (_nearbyMobs.FirstOrDefault(m1 => m1.Name == m.Name) == null)
                        {
                            _nearbyMobs.Add(m);
                        }
                    }

                    //setting priority mob
                    if (priorityMob != null)
                    {
                        if (_nearbyMobs.Count == 0 ||
                            _nearbyMobs.FirstOrDefault(m => m.Name == priorityMob.Name) == null)
                        {
                            priorityMob.UnregisterHandlers();
                            priorityMob = null;
                            Application.Current.Resources["PriorityMobTextVisibility"] = Visibility.Hidden;
                            Application.Current.Resources["PriorityMobGridInnerVisibility"] = Visibility.Hidden;
                        }
                    }

                    foreach (var m in _nearbyMobs)
                    {
                        if (priorityMob == null ||
                            (HuntRank)Enum.Parse(typeof(HuntRank), m.Rank) >
                            (HuntRank)Enum.Parse(typeof(HuntRank), priorityMob.Rank))
                        {
                            priorityMob = m;
                            priorityMob.PropertyChanged += PriorityMob_OnPropertyChanged;
                        }
                    }

                });
            }

        }

        #region Event Handlers

        //set current into for priority mob
        private void PriorityMob_OnPropertyChanged(object o, PropertyChangedEventArgs e)
        {
            Application.Current.Resources["PriorityMobTextRank"] = priorityMob.Rank;
            Application.Current.Resources["PriorityMobTextName"] = priorityMob.Name;
            Application.Current.Resources["PriorityMobTTText"] = priorityMob.Name;
            Application.Current.Resources["PriorityMobTextCoords"] = priorityMob.GetCoords();
            Application.Current.Resources["PriorityMobTextHPP"] = $"{priorityMob.HPPercentAsPercentage,0:0}%";

            Application.Current.Resources["PriorityMobTextVisibility"] = Visibility.Visible;
            Application.Current.Resources["PriorityMobGridInnerVisibility"] = Visibility.Visible;
        }


        //priority mob tool tip
        private void PriorityMobText_OnMouseMove(object sender, MouseEventArgs e)
        {
            PriorityMobTT.IsOpen = true;
            PriorityMobTT.VerticalOffset = PriorityMobGridInner.ActualHeight * .8;
        }

        private void PriorityMobText_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PriorityMobTT.IsOpen = false;
        }

        //priority mob row - make top black bar close program on double click.
        private void PriorityMobTopBar_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                SystemCommands.CloseWindow(this);
            }
        }

        //make the whole window draggable
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #region BUTTON event handlers

        //side panel toggle -- not needed if using command
        private void SidePanelToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (InfoGrid.Width == 0)
            {
                InfoGrid.Width = 300;
                MainWindow1.Width += 300;


            }
            else
            {
                InfoGrid.Width = 0;
                MainWindow1.Width -= 300;

            }
        }

        /*  -- no longer used, using commands instead
                private void OnTop_OnClick(object sender, RoutedEventArgs e)
                {
                    if (!MainWindow1.Topmost)
                    {
                        Application.Current.Resources["ProgramTopMost"] = true;
                    }
                    else
                    {
                        Application.Current.Resources["ProgramTopMost"] = false;
                    }
                }

                private void Opacity_OnClick(object sender, RoutedEventArgs e)
                {
                    if (MainWindow1.Opacity == 1.0)
                    {
                        Application.Current.Resources["ProgramOpacity"] = 0.7;
                    }
                    else
                    {
                        Application.Current.Resources["ProgramOpacity"] = 1.0;
                    }
                }*/

        //exit button
        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        #endregion


        #endregion


        #region Commands

        private void OnTop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnTop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!MainWindow1.Topmost)
            {
                Application.Current.Resources["ProgramTopMost"] = true;
            }
            else
            {
                Application.Current.Resources["ProgramTopMost"] = false;
            }
        }

        private void OpacityToggle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpacityToggle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MainWindow1.Opacity == 1.0)
            {
                Application.Current.Resources["ProgramOpacity"] = _userSettings.Opacity;
            }
            else
            {
                Application.Current.Resources["ProgramOpacity"] = 1.0;
            }
        }

        private void SidePanelToggle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SidePanelToggle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (InfoGrid.Width == 0)
            {
                InfoGrid.Width = 300;
                MainWindow1.Width += 300;
            }
            else
            {
                InfoGrid.Width = 0;
                MainWindow1.Width -= 300;
            }
        }

        private void SSMapToggle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SSMapToggle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _session.ToggleSSMap();
        }
        
        private void SettingsWindowToggle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SettingsWindowToggle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new SettingsWindow(_settingsManager).ShowDialog();
            if (InfoGrid.Width > 0)
            {
                SidePanelToggle_Executed(null, null); //prob bad and should separate into own method but.., turn off side panel
            }

            this.Width = _userSettings.DefaultSizeX;
            this.Height = _userSettings.DefaultSizeY;
        }

        #endregion
    }
}