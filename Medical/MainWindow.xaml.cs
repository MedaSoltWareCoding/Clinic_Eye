using Medical.Mod;
using Medical.View;
using System.Windows;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace Medical
{
    public partial class MainWindow : Window
    {
       
        private readonly Patient_ViewModel pat_viewModel;
        private readonly Doctor_ViewModel  doc_viewModel;
        private readonly Med_ViewModel   med_viewModel;
        private readonly AppointmenViewModel appo_viewmodel;


        public MainWindow()


        {


            InitializeComponent();
            pat_viewModel = new Patient_ViewModel();
            doc_viewModel = new Doctor_ViewModel();
            med_viewModel = new Med_ViewModel();
            appo_viewmodel = new AppointmenViewModel();


            PatientsDataGrid.DataContext = pat_viewModel;
            DoctorsDataGrid.DataContext = doc_viewModel;
            MedecineDataGrid.DataContext = med_viewModel;

            appointmentsDataGrid.DataContext = appo_viewmodel;
            patientComboBox.DataContext = pat_viewModel;
            patientComboBox.SelectedIndex = 0;
            DateOfappointemnt.SelectedDate = DateTime.Now;
            doctorComboBox.DataContext = doc_viewModel;
            doctorComboBox.SelectedIndex = 0;
            //var items = doc_viewModel.Doctros;

            //doctorComboBox.ItemsSource = items.Where(item => item.Name_doc != "NewPlaceholder").ToList();

            med_viewModel.LoadMedecines();
            pat_viewModel.LoadPatients();
            doc_viewModel.LoadDoctors();
            appo_viewmodel.LoadAppointemnts();

        }



        private void PatientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        //Patient Block********************************************************
        public void LoadPatients1()
        {
            try
            {
                pat_viewModel.LoadPatients();
                //MessageBox.Show("Patients loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                pat_viewModel.AddPatient(patient);


                //MessageBox.Show("Patient added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadPatients1();
            Pat_ClearInputFields();

        }
        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (pat_viewModel.SelectedPatient == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this patient?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    pat_viewModel.DeletePatient(pat_viewModel.SelectedPatient);
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
            if (pat_viewModel.SelectedPatient == null)
            {
                MessageBox.Show("Please select a patient to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update logic can be tied to input fields or a modal dialog
            try
            {
                Patient updatedPatient = pat_viewModel.SelectedPatient; // Example: Edit in-place
                pat_viewModel.UpdatePatient(updatedPatient);
                MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Pat_ClearInputFields()
        {
            NameTextBox.Clear();
            FamilyNameTextBox.Clear();
            AgeTextBox.Clear();
            DateOfBirthPicker.SelectedDate = null;
            CityTextBox.Clear();
            AddressTextBox.Clear();
            PhoneNumberTextBox.Clear();
        }

        //Doctor Block********************************************************
        public void LoadDoctors1()
        {
            try
            {
                doc_viewModel.LoadDoctors();
                //MessageBox.Show("doctors loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a patient object from input fields
                Doctors doctor = new Doctors
                {
                    Id_doc = 0,
                    Name_doc = NameTextBox1.Text,
                    Familyname_doc = FamilyNameTextBox1.Text,
                    Age_doc = int.Parse(AgeTextBox1.Text),
                    Adress_doc = AddressTextBox1.Text,
                    Phone_doc = PhoneNumberTextBox1.Text,
                    Branch_doc = BranchTextBox1.Text
                };

                // Add the patient using the ViewModel
                doc_viewModel.AddDocotr(doctor);


                MessageBox.Show("Doctor added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadDoctors1();
            Doc_ClearInputFields();

        }
        private void DeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (doc_viewModel.SelectedDoctor == null)
            {
                MessageBox.Show("Please select a doctor to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this patient?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    doc_viewModel.DeleteDoctor(doc_viewModel.SelectedDoctor);
                    MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void UpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (doc_viewModel.SelectedDoctor == null)
            {
                MessageBox.Show("Please select a doctor to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update logic can be tied to input fields or a modal dialog
            try
            {
                Doctors updtaeDoctors = doc_viewModel.SelectedDoctor;
                //MessageBox.Show(" this id  " + updtaeDoctors.Id_doc, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Example: Edit in-place
                doc_viewModel.UpdateDoctor(updtaeDoctors);
                MessageBox.Show("Doctor updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        public void Doc_ClearInputFields()
        {
            NameTextBox1.Clear();
            FamilyNameTextBox1.Clear();
            AgeTextBox1.Clear();
            AddressTextBox1.Clear();
            PhoneNumberTextBox1.Clear();
            BranchTextBox1.Clear();
        }

        //Medeicent Block********************************************************
        public void LoadMedecent1()
        {
            try
            {
                med_viewModel.LoadMedecines();
                //MessageBox.Show("suscc");

            }
            catch(System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void AddMedecine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Medecine medecine = new Medecine
                {
                    Id_med = 0,
                    Name_med = NameTextBox2.Text,
                    Descreption_med = DescriptionTextBox2.Text,
                    dosage_me = DosageBox2.Text

                };
                med_viewModel.AddMedecine(medecine);

            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadMedecent1();
            Med_ClearInputFields();

        }
        private void DeleteMedecine_Click(object sender, RoutedEventArgs e)
        {
            if (med_viewModel.SelectedMedicne == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this patient?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    med_viewModel.DeleteMedecine(med_viewModel.SelectedMedicne);
                    MessageBox.Show("Medecine deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void UpdateMedecine_Click(object sender, RoutedEventArgs e)
        {
            if (med_viewModel.SelectedMedicne == null)
            {
                MessageBox.Show("Please select a medecine to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update logic can be tied to input fields or a modal dialog
            try
            {
                Medecine updateMedecine = med_viewModel.SelectedMedicne;
                //MessageBox.Show(" this id  " + updtaeDoctors.Id_doc, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Example: Edit in-place
                med_viewModel.UpdateMedecinet(updateMedecine);
                MessageBox.Show("Medecine updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        public void Med_ClearInputFields()
        {
            NameTextBox2.Clear();
            DescriptionTextBox2.Clear();
            DosageBox2.Clear();
        }

        //Appoiment Block********************************************************

        public void LoadAppointments1()
        {
            try
            {
                appo_viewmodel.LoadAppointemnts();
                //MessageBox.Show("Patients loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a appointment object from input fields
                Appointment appointment = new Appointment
                {
                    Id = 0,
                    patient = (Patient)patientComboBox.SelectedItem,
                    date = (DateTime)DateOfappointemnt.SelectedDate,

                };
                appo_viewmodel.AddAppointment(appointment);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            LoadAppointments1();
            //ClearInputFields();

        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this appointemnt?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    appo_viewmodel.DeleteAppointment((Appointment)appointmentsDataGrid.SelectedItem);
                    MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (pat_viewModel.SelectedPatient == null)
            {
                MessageBox.Show("Please select a patient to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update logic can be tied to input fields or a modal dialog
            try
            {
                Patient updatedPatient = pat_viewModel.SelectedPatient; // Example: Edit in-place
                pat_viewModel.UpdatePatient(updatedPatient);
                MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
















    }





}



