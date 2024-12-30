using Medical.Mod;
using Medical.View;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using Button = System.Windows.Controls.Button;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using System.Reflection.PortableExecutable;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
namespace Medical
{
    public partial class MainWindow : Window
    {
       
        private readonly Patient_ViewModel pat_viewModel;
        private readonly Doctor_ViewModel  doc_viewModel;
        private readonly Med_ViewModel   med_viewModel;
        private readonly AppointmenViewModel appo_viewmodel;
        private readonly Exm_ViewModel exm_viewModel;
        private readonly string PatientFilesRoot = @"D:\c# project\Medical\Medical\patient_files";
        private Stack<string> navigationHistory = new Stack<string>();

        public MainWindow()


        {


            InitializeComponent();
            pat_viewModel = new Patient_ViewModel();
            doc_viewModel = new Doctor_ViewModel();
            med_viewModel = new Med_ViewModel();
            appo_viewmodel = new AppointmenViewModel();
            exm_viewModel = new Exm_ViewModel();


            PatientsDataGrid.DataContext = pat_viewModel;
            DoctorsDataGrid.DataContext = doc_viewModel;
            MedecineDataGrid.DataContext = med_viewModel;
            DocDataGrid.DataContext = doc_viewModel;
            PatDataGrid.DataContext = pat_viewModel;
            ExamDataGrid.DataContext = exm_viewModel;




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
            exm_viewModel.LoadExams();
            if (!Directory.Exists(PatientFilesRoot))
            {
                Directory.CreateDirectory(PatientFilesRoot);
            }

            // Load the patient folders on startup
            LoadPatientFolders();
           


        }
        private void DocDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item
            var selectedItem = DocDataGrid.SelectedItem;

            var nameDocProperty = selectedItem.GetType().GetProperty("Name_doc");
            var fnameDocProprety = selectedItem.GetType().GetProperty("Familyname_doc");
            var ageDocPropreety = selectedItem.GetType().GetProperty("Age_doc");
            var branchProprety = selectedItem.GetType().GetProperty("Branch_doc");

            if (selectedItem != null)
            {
                // Convert the selected item to a string
                //textBlock.Text = selectedItem.ToString();
                NametextBlock.Text = nameDocProperty.GetValue(selectedItem)?.ToString();
                FnametextBlock.Text = fnameDocProprety.GetValue(selectedItem)?.ToString();
                AgetextBlock.Text = ageDocPropreety.GetValue(selectedItem)?.ToString();
                BranchtextBlock.Text = branchProprety.GetValue(selectedItem)?.ToString();

            }
            else
            {
                NametextBlock.Text = "No selection";
                FnametextBlock.Text = "No selecttion";
                AgetextBlock.Text = "No selecttion";
                BranchtextBlock.Text = "No selecttion";
            }
        }



       


        private void PatDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Get the selected item
                var selectedItem = PatDataGrid.SelectedItem;

                // Check if selectedItem is null
                if (selectedItem != null)
                {
                    // Use reflection to get properties
                    var idPatProperty = selectedItem.GetType().GetProperty("Id");
                    var namePatProperty = selectedItem.GetType().GetProperty("Name");
                    var fnamePatProperty = selectedItem.GetType().GetProperty("FamilyName");
                    var agePatProperty = selectedItem.GetType().GetProperty("Age");
                    var phonePatProperty = selectedItem.GetType().GetProperty("Phone");

                    // Update UI with the retrieved values
                    Pat_idtextbloc.Text = idPatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_idtextbloc1.Text = idPatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_nametextbloc.Text = namePatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_fnametextblock.Text = fnamePatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_nametextbloc1.Text = namePatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_fnametextblock1.Text = fnamePatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_agetextblock.Text = agePatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_agetextblock1.Text = agePatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                    Pat_phonetextblock.Text = phonePatProperty?.GetValue(selectedItem)?.ToString() ?? "N/A";
                }
                else
                {
                    // Handle case where no item is selected
                    Pat_idtextbloc.Text = "No selection";
                    Pat_idtextbloc.Text = "No selection";
                    Pat_nametextbloc.Text = "No selection";
                    Pat_fnametextblock.Text = "No selection";
                    Pat_nametextbloc1.Text = "No selection";
                    Pat_fnametextblock1.Text = "No selection";
                    Pat_agetextblock.Text = "No selection";
                    Pat_agetextblock1.Text = "No selection";
                    Pat_phonetextblock.Text = "No selection";
                }
            }
            catch (Exception ex)
            {
                // Show error message
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
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

        private void FilePatientUpload_Click(object sender, RoutedEventArgs e)
        {
           

            if (pat_viewModel.SelectedPatient == null)
            {
                MessageBox.Show("Please select a patient first.");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the project directory's root (relative to the .exe startup location)
                string projectDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../");
                string patientFilesFolder = System.IO.Path.Combine(projectDirectory, "patient_files");

                // Ensure the "patient_files" folder exists
                if (!System.IO.Directory.Exists(patientFilesFolder))
                {
                    System.IO.Directory.CreateDirectory(patientFilesFolder);
                }

                // Create a folder for the selected patient using their name
                string patientFolder = System.IO.Path.Combine(patientFilesFolder, $"{pat_viewModel.SelectedPatient.Name}_ملف مرفوع");
                if (!System.IO.Directory.Exists(patientFolder))
                {
                    System.IO.Directory.CreateDirectory(patientFolder);
                }

                // Copy the selected file to the patient's folder
                string fileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                string destinationPath = System.IO.Path.Combine(patientFolder, fileName);

                try
                {
                    System.IO.File.Copy(openFileDialog.FileName, destinationPath, overwrite: true);

                    // Add the destination path to the patient's file list
                    pat_viewModel.SelectedPatient.Files.Add(destinationPath);

                    MessageBox.Show("File uploaded successfully.");
                    LoadPatientFolders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to upload file. Error: {ex.Message}");
                }
            }


        }
        


        private void UpdateFileList()
        {
            if (pat_viewModel.SelectedPatient == null)
            {
                FilesListView.ItemsSource = null;
                return;
            }

            // Get the folder for the selected patient
            string patientFolder = Path.Combine(PatientFilesRoot, pat_viewModel.SelectedPatient.Name);
            if (!Directory.Exists(patientFolder))
            {
                Directory.CreateDirectory(patientFolder);
            }

            // List all files in the patient's folder
            var files = Directory.GetFiles(patientFolder)
                .Select(filePath => new
                {
                    FileName = Path.GetFileName(filePath),
                    FullPath = filePath
                });

            FilesListView.ItemsSource = files;
        }
       
        private void LoadFilesInFolder(string folderPath)
        {
            // Save current path to history stack
            navigationHistory.Push(folderPath);

            // Show the Back button
            BackButton.Visibility = Visibility.Visible;

            // Get all files in the selected folder
            var files = Directory.GetFiles(folderPath)
                .Select(filePath => new
                {
                    FileName = Path.GetFileName(filePath),
                    FullPath = filePath
                });

            // Bind the files to the ListView
            FilesListView.ItemsSource = files;
        }
        private void FilesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (FilesListView.SelectedItem is null)
            {
                MessageBox.Show("No item selected.");
                return;
            }

            // Get the full path of the selected item
            var selectedItem = FilesListView.SelectedItem as dynamic;
            string fullPath = selectedItem.FullPath;

            if (Directory.Exists(fullPath))
            {
                // If the selected item is a folder, display its files
                LoadFilesInFolder(fullPath);
            }
            else if (File.Exists(fullPath))
            {
                // If the selected item is a file, open it
                Process.Start(new ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true
                });
            }
        }
        private void LoadPatientFolders()
        {
            // Clear navigation history and hide the Back button
            navigationHistory.Clear();
            BackButton.Visibility = Visibility.Collapsed;

            // Get all patient folders
            var patientFolders = Directory.GetDirectories(PatientFilesRoot)
                .Select(folderPath => new
                {
                    FileName = Path.GetFileName(folderPath),
                    FullPath = folderPath
                });

            // Bind the patient folders to the ListView
            FilesListView.ItemsSource = patientFolders;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (navigationHistory.Count > 0)
            {
                // Remove the last navigated folder
                navigationHistory.Pop();

                if (navigationHistory.Count == 0)
                {
                    // If no more history, load patient folders
                    LoadPatientFolders();
                }
                else
                {
                    // Load the previous folder
                    string previousFolder = navigationHistory.Peek();
                    LoadFilesInFolder(previousFolder);
                }
            }
        }
        private void ExamDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            
        {
            var selectedExam = ExamDataGrid.SelectedItem as Exams;
            if (selectedExam != null)
            {
                // Set the values of the TextBoxes with the selected exam's properties
                Dv_textbox.Text  = selectedExam.DV.ToString();
                Nv_textbox.Text = selectedExam.NV.ToString();
                Cv_textbox.Text = selectedExam.CV.ToString();
                Sph_textbox.Text = selectedExam.SPH.ToString();
                Cyl_textbox.Text = selectedExam.CYL.ToString();
                Axis_textbox.Text = selectedExam.AXIS.ToString();
                Pdp_textbox.Text = selectedExam.PDP.ToString();
                Ndp_textbox.Text = selectedExam.NDP.ToString();
                Ctr_textbox.Text = selectedExam.CTR.ToString();
                Pho_textbox.Text = selectedExam.Phorias.ToString();
                Stre_textbox.Text = selectedExam.Steropsis.ToString();
                Cy_OS_textbox.Text= selectedExam.Cylinidrical_OS.ToString();
                Cy_OD_textbox.Text = selectedExam.Cylinidrical_OD.ToString();
                Sp_OS_textbox.Text = selectedExam.Spherical_OS.ToString() ;
                Sp_OD_textbox.Text = selectedExam.Spherical_OD.ToString() ;
                Ad_OS_textbox.Text = selectedExam.Add_power_OS.ToString();
                Ad_OD_textbox.Text = selectedExam.Add_power_OD.ToString();
                Ax_OS_textbox.Text = selectedExam.Axis_OS.ToString();
                Ax_OD_textbox.Text = selectedExam.Axis_OD.ToString();
                Bas_OS_textbox.Text = selectedExam.Base_cruve_OS.ToString();
                Bas_OD_textbox.Text = selectedExam.Base_cruve_OD.ToString();
                Di_OS_textbox.Text = selectedExam.Diameterer_OS.ToString();
                Di_OD_textbox.Text = selectedExam.Diameterer_OD.ToString();
                Po_OS_textbox.Text = selectedExam.Power_OS.ToString();
                Po_OD_textbox.Text = selectedExam.Power_OD.ToString();
                Brand_OS_textbox.Text = selectedExam.Brand_type_OS.ToString() ;
                Brand_OD_textbox.Text = selectedExam.Brand_type_OD.ToString();

                // Add other properties as needed
            }

           



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
        //Exams Block********************************************************

        public void AddExams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Exams exams = new Exams
                {
                    Id =  int.Parse(Pat_idtextbloc.Text),
                    Cylinidrical_OD = Cy_OD_textbox.Text,
                    Spherical_OD = Sp_OD_textbox.Text,
                    Add_power_OD = Ad_OD_textbox.Text,
                    Axis_OD = Ax_OD_textbox.Text,
                    Cylinidrical_OS = Cy_OS_textbox.Text,
                    Spherical_OS = Sp_OS_textbox.Text,
                    Add_power_OS = Ad_OS_textbox.Text,
                    Axis_OS = Ax_OS_textbox.Text,
                    Base_cruve_OD = Bas_OD_textbox.Text,
                    Diameterer_OD = Di_OD_textbox.Text,
                    Power_OD = Po_OD_textbox.Text,
                    Brand_type_OD = Brand_OD_textbox.Text,
                    Base_cruve_OS = Bas_OS_textbox.Text,
                    Diameterer_OS = Di_OS_textbox.Text,
                    Power_OS = Po_OS_textbox.Text,
                    Brand_type_OS = Brand_OS_textbox.Text,
                    DV = Dv_textbox.Text,
                    NV = Nv_textbox.Text,
                    CV = Cv_textbox.Text,
                    SPH = Sph_textbox.Text,
                    CYL = Cyl_textbox.Text,
                    AXIS = Axis_textbox.Text,
                    PDP = Pdp_textbox.Text,
                    NDP = Ndp_textbox.Text,
                    CTR = Ctr_textbox.Text,
                    Phorias = Pho_textbox.Text,
                    Steropsis = Stre_textbox.Text
                };
                exm_viewModel.AddExams(exams);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //LoadMedecent1();
            //Med_ClearInputFields();
            Exam_ClearInputFields();
            LoadExam1();
           

        }
        public void DeleteExams_Click(object sender, EventArgs e)
        {
            if (ExamDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this appointemnt?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    exm_viewModel.DeleteExams((Exams)ExamDataGrid.SelectedItem);
                    MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        public void ClearExams_Click(object sander, RoutedEventArgs e)
        {
            Exam_ClearInputFields();
        }
        public void PrintExams_Click(object sander, RoutedEventArgs e)
        {
            //string pdfFilePath = "D:\\c# project\\Medical\\Medical\\patient_files\\Exam.pdf";
            string patientId = Pat_idtextbloc1.Text;
            string patientName = Pat_nametextbloc1.Text;

            // Validate the input for generating a valid file name
            if (string.IsNullOrWhiteSpace(patientId) || string.IsNullOrWhiteSpace(patientName))
            {
                MessageBox.Show("Please ensure both Patient ID and Name fields are filled.");
                return;
            }

            // Construct the file path dynamically
            string folderPath = "D:\\c# project\\Medical\\Medical\\patient_files"; // Update to your desired folder path
            string patientFolderName = $"{patientId}_{patientName}_فحص";
            string patientFolderPath = Path.Combine(folderPath, patientFolderName);
            Directory.CreateDirectory(patientFolderPath); // Create the folder if it doesn't exist
            Random random = new Random();
            double min = 10.0;
            double max = 20.0;
            double id = min + (random.NextDouble() * (max - min));
            // Construct the file path for the PDF within the patient folder
            string pdfFileName = $"{patientId}_{patientName}_{id}_فحص.pdf";
            string pdfFilePath = Path.Combine(patientFolderPath, pdfFileName);

            // Call the Export Method to generate the PDF
            ExportWrapPanelToPdf( ExamPrint, pdfFilePath);

            // Notify the user
            MessageBox.Show("PDF exported successfully to: " + pdfFilePath);
            LoadPatientFolders();

        }
        private void ExportWrapPanelToPdf(WrapPanel wrapPanel, string filePath)
        {
            if (wrapPanel == null)
            {
                MessageBox.Show("The WrapPanel is null. Ensure it's properly initialized.");
                return;
            }

            // Step 1: Create a VisualBrush of the WrapPanel
            var visualBrush = new VisualBrush(wrapPanel);
            var visual = new DrawingVisual();

            using (var drawingContext = visual.RenderOpen())
            {
                // Define the size of the drawing
                drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Size(wrapPanel.ActualWidth, wrapPanel.ActualHeight)));
            }

            // Step 2: Render the visual to a RenderTargetBitmap
            var renderBitmap = new RenderTargetBitmap(
                (int)Math.Ceiling(wrapPanel.ActualWidth),
                (int)Math.Ceiling(wrapPanel.ActualHeight),
                96, // DPI X
                96, // DPI Y
                PixelFormats.Pbgra32);
            renderBitmap.Render(visual);

            // Convert Rendered Bitmap to PNG
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                // Step 3: Create a PDF Document
                PdfDocument pdfDocument = new PdfDocument();
                PdfPage page = pdfDocument.AddPage();

                // Set PDF page size
                double pageWidth = XUnit.FromPoint(page.Width).Point;
                double pageHeight = XUnit.FromPoint(page.Height).Point;

                // Determine the scale to fit the content
                double scaleX = pageWidth / wrapPanel.ActualWidth;
                double scaleY = pageHeight / wrapPanel.ActualHeight;
                double scale = Math.Min(scaleX, scaleY); // Maintain aspect ratio

                // Calculate scaled dimensions
                double scaledWidth = wrapPanel.ActualWidth * scale;
                double scaledHeight = wrapPanel.ActualHeight * scale;

                // Draw the Image on the PDF Page
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    XImage img = XImage.FromStream(stream);
                    gfx.DrawImage(img, 0, 0, scaledWidth, scaledHeight);
                }

                // Step 4: Save the PDF Document
                pdfDocument.Save(filePath);
            }
        }
        






        public void Exam_ClearInputFields()
        {
            Cy_OD_textbox.Clear();
            Sp_OD_textbox.Clear();
            Ad_OD_textbox.Clear();
            Ax_OD_textbox.Clear();
            Cy_OS_textbox.Clear();
            Sp_OS_textbox.Clear();
            Ad_OS_textbox.Clear();
            Ax_OS_textbox.Clear();
            Bas_OD_textbox.Clear();
            Di_OD_textbox.Clear();
            Po_OD_textbox.Clear();
            Brand_OD_textbox.Clear();
            Bas_OS_textbox.Clear();
            Di_OS_textbox.Clear();
            Po_OS_textbox.Clear();
            Brand_OS_textbox.Clear();
            Dv_textbox.Clear();
            Nv_textbox.Clear();
            Cv_textbox.Clear();
            Sph_textbox.Clear();
            Cyl_textbox.Clear();
            Axis_textbox.Clear();
            Pdp_textbox.Clear();
            Ndp_textbox.Clear();
            Ctr_textbox.Clear();
            Pho_textbox.Clear();
            Stre_textbox.Clear();

        }
        public void LoadExam1()
        {
            try
            {
                exm_viewModel.LoadExams();
                //MessageBox.Show("suscc");

            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //textbox Block********************************************************

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void doctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }





}



