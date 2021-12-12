using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using ufht_UI.UserControls;
using ufht_UI.UserControls.InfoSection;
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
        


        public MainWindow()
        {
            InitializeComponent();

            _ = Task.Run(() =>
            {
                Trace.WriteLine("session initializing");
                _session = new Session();
                Trace.WriteLine("session initialized, calling start()");
                
                
                Dispatcher.Invoke(() =>
                {
                    _listSectionMain = new InfoSectionControl(_session);
                    _listSectionTrainList = new InfoSectionTrainListControl(_session);

                    _mainMap = new MainMapControl(_session);
                    _trainMap = new TrainMapControl();

                    MainGrid.Children.Add(_mainMap);
                    MainGrid.Children.Add(_trainMap);
                    MainGrid.Children[1].Visibility = Visibility.Hidden;

                    ListSection.Children.Add(_listSectionMain);
                    DataContext = _session;

                });

                _session.Start();
            });

        }

        private void TrainButton_OnClick(object sender, RoutedEventArgs e)
        {
            ListSection.Children.Clear();
            ListSection.Children.Add(_listSectionTrainList);

            MainGrid.Children[0].Visibility = Visibility.Hidden;
            MainGrid.Children[1].Visibility = Visibility.Visible;

        }
        private void NeabyButton_OnCLick(object sender, RoutedEventArgs e)
        {
            ListSection.Children.Clear();
            ListSection.Children.Add(_listSectionMain);
            MainGrid.Children[1].Visibility = Visibility.Hidden;
            MainGrid.Children[0].Visibility = Visibility.Visible;
        }
    }
}