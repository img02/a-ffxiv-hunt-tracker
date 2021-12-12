using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using untitled_ffxiv_hunt_tracker.Entities;
using untitled_ffxiv_hunt_tracker.ViewModels;

namespace ufht_UI.UserControls.InfoSection
{
    /// <summary>
    /// Interaction logic for InfoSectionTrainListControl.xaml
    /// </summary>
    public partial class InfoSectionTrainListControl : UserControl
    {
        private Session _session;
        private ObservableCollection<Mob> _train;

        public InfoSectionTrainListControl(Session session)
        {
            _session = session;

            _train = new ObservableCollection<Mob>();

            foreach (var mob in _session.CurrentTrain)
            {
                _train.Add(mob);
            }

            InitializeComponent();

            Application.Current.Resources["_train"] = _train;
        }
    }
}
