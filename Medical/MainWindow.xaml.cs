using MaterialDesignThemes.Wpf.AddOns.Utils.Screen;
using Medical.Mod;
using Medical.View;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing; 
using ZXing;
using Button = System.Windows.Controls.Button;
using System.Windows.Media.Imaging;
using Dynamitey.Internal.Optimization;
using Medical.Datas;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

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


            PatientsDataGrid.DataContext = pat_viewModel;
            DoctorsDataGrid.DataContext = doc_viewModel;
            MedecineDataGrid.DataContext = med_viewModel;

            //appointmentsDataGrid.DataContext = appo_viewmodel;
       
            patientComboBox.DataContext = pat_viewModel;
            patientComboBox.SelectedIndex = 0;
            DateOfappointemnt.SelectedDate = DateTime.Now;
            doctorComboBox.DataContext = doc_viewModel;
            doctorComboBox.SelectedIndex = 0;
            combostate.SelectedIndex = 0;
            med_viewModel.LoadMedecines();
            pat_viewModel.LoadPatients();
            doc_viewModel.LoadDoctors();
            LoadAppointments1();
           // appointmentsDataGrid.IsReadOnly = true;

            //Pescription containers load| ----------------------------- >
            patientpescreptionComboBox.DataContext = pat_viewModel;
            patientpescreptionComboBox.SelectedIndex = 0;
            DateOfpescription.SelectedDate = DateTime.Now;
            doctorpewscriptionComboBox1.DataContext = doc_viewModel ;
            doctorpewscriptionComboBox1 .SelectedIndex = 0;
            medlist.DataContext = med_viewModel;
            medlist .SelectedIndex = 0;
        }
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
                selected = app;

            }

        }

        public void update()
        {

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
                foreach (var row in PatientsDataGrid.Items)
                {
                    DataGridRow dataGridRow = (DataGridRow)PatientsDataGrid.ItemContainerGenerator.ContainerFromItem(row);
                    if (dataGridRow != null)
                    {
                        dataGridRow.Height = 150;  // Set a custom row height
                    }
                }
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
                        mystack_.Children.Add(new UserControl1(app,this));
                    previous = app;
                }
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

        public void DeleteAppointment_Click(Appointment selected , UserControl1 item)
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

                appo_viewmodel.UpdatePatient(selected);
                LoadAppointments1();
                //MessageBox.Show("appointment "+selected.patient.Name+" updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if(appo_viewmodel != null) { 
            List<Appointment> list = new List<Appointment>();
            if (search.Text.Equals(""))
            {
              
                LoadAppointments1();
            }
            else { 
                foreach (Appointment item in appo_viewmodel.appointments)
                {
                    if(item.patient.Name.Contains(search.Text)|| item.patient.FamilyName.Contains(search.Text))
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
            pescription = new Pescription { Id = idint ,
                patient = (Patient)patientpescreptionComboBox.SelectedValue,
                doctor = (Doctors)doctorpewscriptionComboBox1.SelectedValue ,
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
            if(pescription != null)
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
            if(pescription != null && pescription.medcines != null)
            {
                bool exist = false;
                foreach(Session s in pescription.medcines)
                {
                    if(s.medecine.Id_med == ((Medecine)medlist.SelectedValue).Id_med)
                    {
                        exist = true; break;
                    }
                }
                if (exist) {
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

        // Convert the WPF UI to a PDF
        private void ConvertToPdf()
        {
            // Create the PDF document
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage page = pdfDocument.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Capture the visual content of a WPF control (e.g., a Grid or Canvas)
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)pespgrid.ActualWidth, (int)pespgrid.ActualHeight, 96 ,96, PixelFormats.Pbgra32);
            //MessageBox.Show(rtb.Width + "," + rtb.Height, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            rtb.Render(pespgrid); // 'myGrid' is the WPF control to capture (replace with your control)

            // Convert the captured content into an image and save it in the PDF
            MemoryStream ms = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(ms);

            // Create an XImage from the memory stream
            XImage image = XImage.FromStream(ms);
                
            // Draw the image in the PDF page
            gfx.DrawImage(image, 0, 0);

            // Save the PDF to a file
            string filePath = "output.pdf";
            pdfDocument.Save(filePath);

            MessageBox.Show("PDF saved successfully.");
        }

        public void updateThePescriptionPage()
        {
            patientname.Text = pescription.patient.ToString();
            patientage.Text = pescription.patient.Age+" ans";
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
    }
}



