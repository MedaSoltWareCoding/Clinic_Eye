using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Medical.Mod;

namespace Medical
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// 
    
    public partial class UserControl1 : UserControl
    {
        public MainWindow main;
        Appointment appointment;
        public UserControl1(Appointment appointment , MainWindow window)
        {
            InitializeComponent();
            main = window;
            this.appointment = appointment;
            name.Text = this.appointment.patient.ToString();
            datef.Text = this.appointment.date.ToString("d");
            timef.Text = this.appointment.time.ToShortTimeString();
            phonef.Text = this.appointment.patient.Phone;
            address.Text = this.appointment.patient.Address;
            if (this.appointment.state == 0)
            {
                statef.Text = "في الانتظار";
                borderf.Background = new SolidColorBrush(Colors.CadetBlue);
                tail.Background = new SolidColorBrush(Colors.CadetBlue);
            }
            else if (this.appointment.state == 1)
            {
                statef.Text = "اكتمال";
                borderf.Background = new SolidColorBrush(Colors.Green);
                tail.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                statef.Text = "ملغى";
                borderf.Background = new SolidColorBrush(Colors.Red);
                tail.Background = new SolidColorBrush(Colors.Red);
            }
            }

        private void HoverPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Change background color on hover
            grido.Background = System.Windows.Media.Brushes.AntiqueWhite;
        }

        private void HoverPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Revert the background color when the mouse leaves
            
            grido.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1ECE3"));


        }

        private void messageshow(object sender, MouseButtonEventArgs e)
        {
            grido.Background = System.Windows.Media.Brushes.LightBlue;
            main.MouseClick(appointment);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //main.DeleteAppointment_Click(appointment,this);
            this.appointment.state = 2;
            main.selected = this.appointment;
            //MessageBox.Show("appointment " + appointment.patient.Id + " updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            main.UpdateAppointment_state(appointment);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

        }

        private void approved(object sender, RoutedEventArgs e)
        {
           
            this.appointment.state = 1;
            main.selected = this.appointment;
            //MessageBox.Show("appointment " + appointment.patient.Id + " updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            main.UpdateAppointment_state(appointment);
        }
    }
}
