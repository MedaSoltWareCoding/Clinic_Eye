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
using Medical.Datas;
using System.Windows.Documents;
namespace Medical
{
    public partial class MainWindow : Window
    {
        public Pescription pescription = null;
        public Appointment? selected = null;
        private readonly Patient_ViewModel pat_viewModel;
        private readonly Doctor_ViewModel  doc_viewModel;
        private readonly Med_ViewModel   med_viewModel;
        private readonly AppointmenViewModel appo_viewmodel;
        private readonly Exm_ViewModel exm_viewModel;
        private readonly string PatientFilesRoot = @"D:\c# project\Medical\Medical\patient_files";
        private Stack<string> navigationHistory = new Stack<string>();
        IdGenerator gen = new IdGenerator();

        public MainWindow()


        {


            InitializeComponent();
            var workingArea = SystemParameters.WorkArea;

            // Set the window size to the maximum available size
            this.Width = workingArea.Width;
            this.Height = workingArea.Height;

            // Position the window at the top-left corner of the screen
            this.Left = workingArea.Left;
            this.Top = workingArea.Top;
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




            //appointmentsDataGrid.DataContext = appo_viewmodel;
            patientComboBox.DataContext = pat_viewModel;
            patientComboBox.SelectedIndex = 0;
            DateOfappointemnt.SelectedDate = DateTime.Now;
            doctorComboBox.DataContext = doc_viewModel;
            doctorComboBox.SelectedIndex = 0;
            //var items = doc_viewModel.Doctros;
            patientpescreptionComboBox.DataContext = pat_viewModel;
            patientpescreptionComboBox.SelectedIndex = 0;
            DateOfpescription.SelectedDate = DateTime.Now;
            doctorpewscriptionComboBox1.DataContext = doc_viewModel;
            doctorpewscriptionComboBox1.SelectedIndex = 0;
            medlist.DataContext = med_viewModel;
            medlist.SelectedIndex = 0;
            patientcertaficatesComboBox.DataContext = pat_viewModel;
            patientcertaficatesComboBox.SelectedIndex = 0;

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
            LoadAppointments1();


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
                FilesListView_File.ItemsSource = null;
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

            FilesListView_File.ItemsSource = files;
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
            FilesListView_File.ItemsSource = files;
        }
        private void FilesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (FilesListView_File.SelectedItem is null)
            {
                MessageBox.Show("No item selected.");
                return;
            }

            // Get the full path of the selected item
            var selectedItem = FilesListView_File.SelectedItem as dynamic;
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
            FilesListView_File.ItemsSource = patientFolders;
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
        public void MouseClick(Appointment app)
        {

            if (app != null)
            {
                int p = 0;
                bool found = false;
                for (int i = 0; i < patientComboBox.Items.Count; i++)
                {
                    if (patientComboBox.Items[i].ToString() == app.patient.ToString())
                    {
                        p = i;
                        found = true; break;
                    }
                }
                if (found)
                    patientComboBox.SelectedIndex = p;

                DateOfappointemnt.SelectedDate = app.date;
                apptime.SelectedTime = app.time;
                selected = app;

            }

        }
        public void update()
        {

        }

        //public void LoadAppointments1()
        //{
        //    try
        //    {
        //        appo_viewmodel.LoadAppointemnts();
        //        //MessageBox.Show("Patients loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        public void LoadAppointments1()
        {
            mystack_.Children.Clear();
            try
            {
                Appointment previous = null;
                appo_viewmodel.LoadAppointemnts();
                foreach (Appointment app in appo_viewmodel.appointments)
                {

                    if (previous != null)
                    {
                        if (previous.date != app.date)
                        {
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = app.date.ToString("MMMM dd, yyyy");
                            textBlock.Foreground = new SolidColorBrush(Colors.Gray);
                            textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                            textBlock.Margin = new Thickness(20);
                            mystack_.Children.Add(textBlock);

                        }

                    }
                    else
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = app.date.ToString("MMMM dd, yyyy");
                        textBlock.Foreground = new SolidColorBrush(Colors.Gray);
                        textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                        textBlock.Margin = new Thickness(20);
                        mystack_.Children.Add(textBlock);
                    }
                    mystack_.Children.Add(new UserControl1(app, this));
                    previous = app;
                }
                //MessageBox.Show("Patients loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //private void AddAppointment_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        // Create a appointment object from input fields
        //        Appointment appointment = new Appointment
        //        {
        //            Id = 0,
        //            patient = (Patient)patientComboBox.SelectedItem,
        //            date = (DateTime)DateOfappointemnt.SelectedDate,

        //        };
        //        appo_viewmodel.AddAppointment(appointment);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    LoadAppointments1();
        //    //ClearInputFields();

        //}
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
                    time = (DateTime)apptime.SelectedTime,

                };

                appo_viewmodel.AddAppointment(appointment);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            LoadAppointments1();
            // ClearInputFields();

        }
        public void DeleteAppointment_Click(Appointment selected, UserControl1 item)
        {

            if (selected == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this appointemnt?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    appo_viewmodel.DeleteAppointment(selected);
                    mystack_.Children.Remove(item);
                    LoadAppointments1();
                    MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        //private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        //{
        //    if (appointmentsDataGrid.SelectedItem == null)
        //    {
        //        MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    var result = MessageBox.Show("Are you sure you want to delete this appointemnt?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        try
        //        {
        //            appo_viewmodel.DeleteAppointment((Appointment)appointmentsDataGrid.SelectedItem);
        //            MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}
        //private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        //{
        //    if (pat_viewModel.SelectedPatient == null)
        //    {
        //        MessageBox.Show("Please select a patient to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    // Update logic can be tied to input fields or a modal dialog
        //    try
        //    {
        //        Patient updatedPatient = pat_viewModel.SelectedPatient; // Example: Edit in-place
        //        pat_viewModel.UpdatePatient(updatedPatient);
        //        MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        public void UpdateAppointment_state(Appointment app)
        {
            if (app == null)
            {
                MessageBox.Show("Please select a patient to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update logic can be tied to input fields or a modal dialog
            try
            {
                appo_viewmodel.UpdatePatient(app);
                LoadAppointments1();
                //MessageBox.Show("appointment "+selected.patient.Name+" updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a patient to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Update logic can be tied to input fields or a modal dialog
            try
            {
                selected.date = (DateTime)DateOfappointemnt.SelectedDate;
                selected.patient = (Patient)patientComboBox.SelectedValue;
                selected.time = (DateTime)apptime.SelectedTime;

                appo_viewmodel.UpdatePatient(selected);
                LoadAppointments1();
                //MessageBox.Show("appointment "+selected.patient.Name+" updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (appo_viewmodel != null)
            {
                List<Appointment> list = new List<Appointment>();
                if (search.Text.Equals(""))
                {

                    LoadAppointments1();
                }
                else
                {
                    foreach (Appointment item in appo_viewmodel.appointments)
                    {
                        if (item.patient.Name.Contains(search.Text) || item.patient.FamilyName.Contains(search.Text))
                        {
                            list.Add(item);
                        }
                    }
                    LoadSearchedAppo(list);

                }
            }
        }
        public void LoadSearchedAppo(List<Appointment> list)
        {
            mystack_.Children.Clear();
            try
            {
                Appointment previous = null;
                foreach (Appointment app in list)
                {

                    if (previous != null)
                    {
                        if (previous.date != app.date)
                        {
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = app.date.ToString("MMMM dd, yyyy");
                            textBlock.Foreground = new SolidColorBrush(Colors.Gray);
                            textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                            textBlock.Margin = new Thickness(20);
                            mystack_.Children.Add(textBlock);

                        }

                    }
                    else
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = app.date.ToString("MMMM dd, yyyy");
                        textBlock.Foreground = new SolidColorBrush(Colors.Gray);
                        textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                        textBlock.Margin = new Thickness(20);
                        mystack_.Children.Add(textBlock);
                    }
                    mystack_.Children.Add(new UserControl1(app, this));
                    previous = app;
                }
                //MessageBox.Show("Patients loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void combostate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (appo_viewmodel != null && appo_viewmodel.appointments != null)
            {

                List<Appointment> list = new List<Appointment>();
                if (combostate.SelectedIndex == 0)
                {
                    LoadAppointments1();

                }
                else
                {

                    if (combostate.SelectedIndex == 1)
                    {
                        foreach (Appointment app in appo_viewmodel.appointments)
                        {
                            if (app.state == 0)
                            {
                                list.Add(app);
                            }
                        }
                    }
                    else if (combostate.SelectedIndex == 2)
                    {
                        foreach (Appointment app in appo_viewmodel.appointments)
                        {
                            if (app.state == 1)
                            {
                                list.Add(app);
                            }
                        }
                    }
                    else if (combostate.SelectedIndex == 3)
                    {
                        foreach (Appointment app in appo_viewmodel.appointments)
                        {
                            if (app.state == 2)
                            {
                                list.Add(app);
                            }
                        }
                    }

                    LoadSearchedAppo(list);
                }
            }
        }
        private void AddPescription_Click(object sender, RoutedEventArgs e)
        {

            int idint = gen.generateid("pescriptions");
            string id = idint.ToString("D14");
            pescription = new Pescription
            {
                Id = idint,
                patient = (Patient)patientpescreptionComboBox.SelectedValue,
                doctor = (Doctors)doctorpewscriptionComboBox1.SelectedValue,
                date = DateTime.Now,
                medcines = new List<Session>(),
            };
            try
            {
                barcode.Source = gen.GenerateQRCode(pescription.Id.ToString("D14"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            updateThePescriptionPage();

        }
        private void Updatepescription_Click(object sender, RoutedEventArgs e)
        {
            if (pescription != null)
            {
                pescription.patient = (Patient)patientpescreptionComboBox.SelectedValue;
                pescription.doctor = (Doctors)doctorpewscriptionComboBox1.SelectedValue;
                updateThePescriptionPage();
            }
            else
            {
                MessageBox.Show("انشئ وصفة من فضلك", "Error", MessageBoxButton.OK);
            }
        }
        private void Addmedtopescription(object sender, RoutedEventArgs e)
        {
            if (pescription != null && pescription.medcines != null)
            {
                bool exist = false;
                foreach (Session s in pescription.medcines)
                {
                    if (s.medecine.Id_med == ((Medecine)medlist.SelectedValue).Id_med)
                    {
                        exist = true; break;
                    }
                }
                if (exist)
                {
                    MessageBox.Show("لا يمكن تكرار نفس الدواء مرتين ", "Error", MessageBoxButton.OK);
                }
                else
                {
                    Session session = new Session
                    {
                        pescriptionId = pescription.Id,
                        medecine = (Medecine)medlist.SelectedValue,
                        descrition = pescriptionDesc.Text.Length > 0 ? pescriptionDesc.Text : ((Medecine)medlist.SelectedValue).Descreption_med
                    };
                    pescription.medcines.Add(session);
                    updateThePescriptionPage();
                }
            }
            else
            {
                MessageBox.Show("انشئ وصفة من فضلك", "Error", MessageBoxButton.OK);
            }

        }
        private void Savepescription(object sender, RoutedEventArgs e)
        {
            if (pescription != null)
            {
                if (pescription.medcines.Count > 0)
                {
                    Data_pescription data_Pescription = new Data_pescription("localhost", "clinics", "root", "");
                    try
                    {
                        data_Pescription.AddPescription(pescription);
                        ConvertToPdf();
                        //clear the page if done-------------------------

                        pescriptioncontent.Children.Clear();
                        patientname.Text = "";
                        // doctorname.Text = "";
                        datepescriptionlabel.Text = "";
                        barcode.Source = null;

                        pescription = null;

                        //-----------------------------------------
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("الوصفة فارغة تأكد من ملئها قبل الحفظ", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("انشئ وصفة من فضلك", "Error", MessageBoxButton.OK);
            }

        }
        //private void ConvertToPdf()
        //{
        //    // Create the PDF document
        //    PdfDocument pdfDocument = new PdfDocument();
        //    PdfPage page = pdfDocument.AddPage();
        //    XGraphics gfx = XGraphics.FromPdfPage(page);

        //    // Capture the visual content of a WPF control (e.g., a Grid or Canvas)
        //    RenderTargetBitmap rtb = new RenderTargetBitmap((int)pespgrid.ActualWidth, (int)pespgrid.ActualHeight, 96, 96, PixelFormats.Pbgra32);
        //    //MessageBox.Show(rtb.Width + "," + rtb.Height, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    rtb.Render(pespgrid); // 'myGrid' is the WPF control to capture (replace with your control)

        //    // Convert the captured content into an image and save it in the PDF
        //    MemoryStream ms = new MemoryStream();
        //    BitmapEncoder encoder = new PngBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create(rtb));
        //    encoder.Save(ms);

        //    // Create an XImage from the memory stream
        //    XImage image = XImage.FromStream(ms);

        //    // Draw the image in the PDF page
        //    gfx.DrawImage(image, 0, 0);

        //    // Save the PDF to a file
        //    string filePath = "D:\\c# project\\Medical\\Medical\\patient_files\\output.pdf";
        //    pdfDocument.Save(filePath);

        //    MessageBox.Show("PDF saved successfully.");
        //}
        private void ConvertToPdf()
        {
            if (string.IsNullOrWhiteSpace(patientname.Text))
            {
                MessageBox.Show("Please enter a valid patient name.");
                return;
            }

            // Create the PDF document
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage page = pdfDocument.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Capture the visual content of the WPF control (e.g., a Grid or Canvas)
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)pespgrid.ActualWidth,
                (int)pespgrid.ActualHeight,
                96, // DPI X
                96, // DPI Y
                PixelFormats.Pbgra32);
            rtb.Render(pespgrid); // 'pespgrid' is the WPF control to capture

            // Convert the captured content into an image and save it in the PDF
            MemoryStream ms = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(ms);

            // Create an XImage from the memory stream
            XImage image = XImage.FromStream(ms);

            // Draw the image in the PDF page
            gfx.DrawImage(image, 0, 0);

            // Generate a random ID for the folder name
            Random random = new Random();
            int randomId = random.Next(1000, 9999);

            // Construct the folder path
            string baseFolder = "D:\\c# project\\Medical\\Medical\\patient_files";
            string patientFolderName = $"{patientname.Text}_وصفات_{randomId}";
            string patientFolderPath = Path.Combine(baseFolder, patientFolderName);

            // Ensure the folder exists
            Directory.CreateDirectory(patientFolderPath);

            // Save the PDF with the patient name in the created folder
            string pdfFileName = $"{patientname.Text}.pdf";
            string pdfFilePath = Path.Combine(patientFolderPath, pdfFileName);
            pdfDocument.Save(pdfFilePath);

            MessageBox.Show($"PDF saved successfully at: {pdfFilePath}");
        }

        public void updateThePescriptionPage()
        {
            patientname.Text = pescription.patient.ToString();
            patientage.Text = pescription.patient.Age + " ans";
            datepescriptionlabel.Text = pescription.date.ToString();
              

            pescriptioncontent.Children.Clear();
            foreach (Session item in pescription.medcines)
            {
                StackPanel row = new StackPanel();
                row.Margin = new Thickness(15, 15, 15, 15);
                row.HorizontalAlignment = HorizontalAlignment.Center;
                row.Height = 20;
                row.Orientation = Orientation.Horizontal;
                TextBlock medName = new TextBlock();
                medName.Width = 300;
                medName.Text = item.medecine.Name_med;
                TextBlock medDos = new TextBlock();
                medDos.Width = 100;
                medDos.Text = item.medecine.dosage_me;
                TextBlock medDesc = new TextBlock();
                medDesc.Width = 300;
                medDesc.Text = item.descrition;

                row.Children.Add(medName);
                row.Children.Add(medDos);
                row.Children.Add(medDesc);
                pescriptioncontent.Children.Add(row);
                pescriptionDesc.Text = "";
            }

            // Create source

        }
        private void startOver_Click(object sender, RoutedEventArgs e)
        {
            pescriptioncontent.Children.Clear();
            patientname.Text = "";
            // doctorname.Text = "";
            datepescriptionlabel.Text = "";

            pescription = null;

        }
        private void DeleteSession_Click(object sender, RoutedEventArgs e)
        {
            if (pescription != null && pescription.medcines != null)
            {
                foreach (Session s in pescription.medcines)
                {
                    if (s.medecine.Id_med == ((Medecine)medlist.SelectedValue).Id_med)
                    {
                        pescription.medcines.Remove(s);
                        break;
                    }
                }
                updateThePescriptionPage();
            }
            else
            {
                MessageBox.Show("انشئ وصفة من فضلك", "Error", MessageBoxButton.OK);
            }
        }
        private void UpdateSession_Click(object sender, RoutedEventArgs e)
        {
            if (pescription != null && pescription.medcines != null)
            {
                foreach (Session s in pescription.medcines)
                {
                    if (s.medecine.Id_med == ((Medecine)medlist.SelectedValue).Id_med)
                    {

                        s.descrition = pescriptionDesc.Text.Length > 0 ? pescriptionDesc.Text : ((Medecine)medlist.SelectedValue).Descreption_med;

                        break;
                    }
                }
                updateThePescriptionPage();
            }
            else
            {
                MessageBox.Show("انشئ وصفة من فضلك", "Error", MessageBoxButton.OK);
            }

        }
        private void Addcertaficate(object sender, RoutedEventArgs e)
        {
            patientnamecertaficate.Text = ((Patient)patientcertaficatesComboBox.SelectedValue).ToString();
            patientagecertaficate.Text = ((Patient)patientcertaficatesComboBox.SelectedValue).Age + "ans";
            //--------------------------------------------------------------------
            mystack_certaficate.Children.Clear();

            // Get the FlowDocument from the RichTextBox
            FlowDocument flowDocument = certaficateText.Document;

            // Loop through each block (paragraphs) in the FlowDocument
            try
            {
                foreach (var block in flowDocument.Blocks)
                {
                    if (block is Paragraph paragraph)
                    {
                        // Create a new TextBlock for this paragraph
                        TextBlock paragraphTextBlock = new TextBlock
                        {
                            TextWrapping = TextWrapping.Wrap
                        };

                        // Add each Inline element from the paragraph to the TextBlock
                        foreach (Inline inline in ((Paragraph)block).Inlines.ToList())
                        {
                            Inline inl = inline;
                            // You can directly add the Inlines (formatted text) to the TextBlock
                            paragraphTextBlock.Inlines.Add(inl);
                        }
                        MessageBox.Show(paragraphTextBlock.Inlines.Count + "", "Error", MessageBoxButton.OK);

                        // Add the TextBlock to the StackPanel
                        certaficatecontent.Children.Add(paragraphTextBlock);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        private void DeleteApoiment_Click(object sender, RoutedEventArgs e)
        {
            if (selected.patient == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this appointemnt?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    appo_viewmodel.DeleteAppointment((Appointment)selected);
                    MessageBox.Show("Apoo deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        //private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        //{

        //}

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



