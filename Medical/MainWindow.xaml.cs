using Medical.Mod;
using Medical.View;
using System.Windows;
using System.Windows.Controls;

namespace Medical
{
    public partial class MainWindow : Window
    {
        private Patient selectedPatient;

        private readonly PatientViewModel viewModel;

        public MainWindow()


        {


            InitializeComponent();
            viewModel = new PatientViewModel();
            DataContext = viewModel;
            LoadPatients1();
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
                    Id = 0,
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


                //MessageBox.Show("Patient added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                //MessageBox.Show("Patients loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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


        //public Patient SelectedPatient
        //{
        //    get => selectedPatient;
        //    set
        //    {
        //        selectedPatient = value;
        //        OnPropertyChanged(nameof(SelectedPatient));
        //    }
        //}

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPatient == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this patient?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    viewModel.DeletePatient(viewModel.SelectedPatient);
                    MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPatient == null)
            {
                MessageBox.Show("Please select a patient to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update logic can be tied to input fields or a modal dialog
            try
            {
                Patient updatedPatient = viewModel.SelectedPatient; // Example: Edit in-place
                viewModel.UpdatePatient(updatedPatient);
                MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }










    }
}



