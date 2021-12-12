using System;
using System.Collections.Generic;
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

namespace ufht_UI.UserControls
{
    /// <summary>
    /// Interaction logic for TrainMapControl.xaml
    /// </summary>
    public partial class TrainMapControl : UserControl
    {
        private string _currentMap;

        public TrainMapControl()
        {
            //get the "current train" from session. or from infoSectionTrain's "current train"
            //set current map as first mobs map.

            //draw all mobs whose mapID matches the current map.

            //then change map based on clicked mob from info grid list?
            

            InitializeComponent();
        }
    }
}
