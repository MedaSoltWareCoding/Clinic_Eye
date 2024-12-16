using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;


namespace Medical
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            
        }
        private void OpenTab(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string tabName = button.Tag.ToString();
                foreach (TabItem tabItem in MainTabControl.Items)
                {
                    if (tabItem.Header.ToString() == tabName)
                    {
                        MainTabControl.SelectedItem = tabItem;
                        break;
                    }
                }
            }
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void PatientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    }

