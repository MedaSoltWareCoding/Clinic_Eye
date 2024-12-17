using Medical.Mod;
using Medical.View;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Medical
{
    public partial class MainWindow : Window
    {
        private string connectionString = "server=localhost;database=clinics;user=root;password=;";
        private readonly PatientViewModel viewModel;
        //public ObservableCollection<Patient> Patients { get; set; }




        public MainWindow()

        {

            InitializeComponent();
            viewModel = new PatientViewModel();
            DataContext = viewModel;
            LoadPatients1();



            //Patients = new ObservableCollection<Patient>();
            //PatientsDataGrid.ItemsSource = Patients; // Bind DataGrid to ObservableCollection
            //LoadPatients();// Bind the ViewModel to the UI



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

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a patient object from input fields
                Patient patient = new Patient
                {
                    
                    Name = NameTextBox.Text,
                    FamilyName = FamilyNameTextBox.Text,
                    Age = int.Parse(AgeTextBox.Text),
                    Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Birthday = DateOfBirthPicker.SelectedDate,
                    City = CityTextBox.Text,
                    Address = AddressTextBox.Text,
                    Phone = PhoneNumberTextBox.Text
                };

                // Add the patient using the ViewModel
                viewModel.AddPatient(patient);


                MessageBox.Show("Patient added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadPatients1();
            ClearInputFields();

        }

        public void LoadPatients1()
        {
            try
            {
                viewModel.LoadPatients();
                MessageBox.Show("Patients loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputFields()
        {
            NameTextBox.Clear();
            FamilyNameTextBox.Clear();
            AgeTextBox.Clear();
            DateOfBirthPicker.SelectedDate = null;
            CityTextBox.Clear();
            AddressTextBox.Clear();
            PhoneNumberTextBox.Clear();
        }

        private void PatientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void LoadPatients()
        //{
        //    Patients.Clear(); // Clear the current list before reloading
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            string query = "SELECT * FROM patient";
        //            MySqlCommand cmd = new MySqlCommand(query, conn);

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {

        //                    Patients.Add(new Patient
        //                    {
                                
        //                        Name = reader.GetString("pat_name"),
        //                        FamilyName = reader.GetString("pat_fname"),
        //                        Age = reader.GetInt32("pat_age"),
        //                        Gender = reader.GetString("pat_gander"),
        //                        Birthday = reader.GetDateTime("pat_birthday"),
        //                        City = reader.GetString("pat_city"),
        //                        Address = reader.GetString("pat_adress"),
        //                        Phone = reader.GetString("pat_phone")
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}







    }
}



