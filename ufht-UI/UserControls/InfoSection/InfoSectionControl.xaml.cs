using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using untitled_ffxiv_hunt_tracker;
using untitled_ffxiv_hunt_tracker.Entities;
using untitled_ffxiv_hunt_tracker.ViewModels;

namespace ufht_UI.UserControls.InfoSection
{
    /// <summary>
    /// Interaction logic for InfoSectionControl.xaml
    /// </summary>
    public partial class InfoSectionControl : UserControl
    {
        private readonly Session _session;
        private readonly ObservableCollection<Mob> _nearbyMobs;
        internal Mob PriorityMob; //contains single mob, used to display into at top of map image. SS > S > A > B


        public InfoSectionControl(Session session, ObservableCollection<Mob> nearbyMobs)
        {
            _session = session;
            _nearbyMobs = nearbyMobs;
            InitializeComponent();

            Application.Current.Resources["_nearbyMobs"] = _nearbyMobs;
            DataGridInfo.LostFocus += DataGridInfoOnLostFocus;
        }

        private void DataGridInfoOnLostFocus(object sender, RoutedEventArgs e)
        {
            DataGridInfo.UnselectAllCells();
        }

        private void CurrentNearbyMobs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
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

                    //set priority mob
                    if (_nearbyMobs.Count == 0)
                    {
                        PriorityMob = null;
                    }
                    else
                    {
                        foreach (var m in _nearbyMobs)
                        {
                            if (PriorityMob == null ||
                                (HuntRank)Enum.Parse(typeof(HuntRank), m.Rank) >
                                (HuntRank)Enum.Parse(typeof(HuntRank), PriorityMob.Rank))
                            {
                                PriorityMob = m;
                            }
                        }
                    }
                });
            }
        }
    }
}
