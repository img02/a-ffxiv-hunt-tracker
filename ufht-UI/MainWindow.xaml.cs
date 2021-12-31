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
using System.Windows.Media;
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
        private InfoSectionControl _listSectionMain;
        private InfoSectionTrainListControl _listSectionTrainList;

        private MainMapControl _mainMap;
        private TrainMapControl _trainMap;

        private ObservableCollection<Mob> _nearbyMobs;
        internal Mob priorityMob;



        public MainWindow()
        {
            Application.Current.Resources["_sidePanelStartingWidth"] = 0.0;

            Application.Current.Resources["PriorityMobTextVisibility"] = Visibility.Hidden;

            //Application.Current.Resources["PriorityMobTextColour"] = Brushes.Aquamarine;
            Application.Current.Resources["PriorityMobTextColour"] = Brushes.WhiteSmoke;
            //Application.Current.Resources["PriorityMobGridBackground"] = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#444444")); 
            

            Application.Current.Resources["PriorityMobTextFontSize"] = 20.0;
            Application.Current.Resources["PriorityMobTextRankFontSize"] = 35.0;
            Application.Current.Resources["PriorityMobTextHPPFontSize"] = 26.0;


            InitializeComponent();
            _nearbyMobs = new ObservableCollection<Mob>();
            _ = Task.Run(() =>
            {
                Trace.WriteLine("session initializing");
                _session = new Session();
                Trace.WriteLine("session initialized, calling start()");

                
                Dispatcher.Invoke(() =>
                {
                    _listSectionMain = new InfoSectionControl(_session, _nearbyMobs);
                    _session.CurrentNearbyMobs.CollectionChanged += CurrentNearbyMobs_CollectionChanged;

                    _listSectionTrainList = new InfoSectionTrainListControl(_session);

                    _mainMap = new MainMapControl(_session);
                    _trainMap = new TrainMapControl();

                    MainGrid2.Children.Add(_mainMap);

                    ListSection.Children.Add(_listSectionMain);
                    DataContext = _session;

                });
                
                _session.Start();
            });

        }

        private void SidePanelToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (InfoGrid.Width == 0)
            {
                //ListSection.Children.Add(_listSectionMain);
                MainWindow1.Width += 300;
                InfoGrid.Width = 300;

            }
            else
            {
                //ListSection.Children.Clear();
                MainWindow1.Width -= 300;
                InfoGrid.Width = 0;
            }

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
                            //Application.Current.Resources["buttontext"] = "";
                            Application.Current.Resources["PriorityMobTextVisibility"] = Visibility.Hidden;
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
                           // Application.Current.Resources["buttontext"] = $"{priorityMob.Rank} {priorityMob.ToString()}";
                        }
                    }

                });
            }

        }

        private void PriorityMob_OnPropertyChanged(object o, PropertyChangedEventArgs e)
        {
            /*Application.Current.Resources["buttontext"] = $"{priorityMob.Rank}\t{priorityMob.Name} \n" +
                                                          $"\t{priorityMob.GetCoords()}      {priorityMob.HPPercent*100, 0:0.00}%"*/;

            Application.Current.Resources["PriorityMobTextRank"] = priorityMob.Rank;
            Application.Current.Resources["PriorityMobTextName"] = priorityMob.Name;
            Application.Current.Resources["PriorityMobTextCoords"] = priorityMob.GetCoords();
            Application.Current.Resources["PriorityMobTextHPP"] = $"{priorityMob.HPPercentAsPercentage,0:0}%";

            Application.Current.Resources["PriorityMobTextVisibility"] = Visibility.Visible;
        }

    }
}