
using System.Collections.ObjectModel;
using Medical.Datas;
using Medical.Mod;
using System.ComponentModel;



namespace Medical.View
{
    public class Doctor_ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Doctors> Doctros { get; private set; }
        private readonly Data_doc   databaseHelper;
        private Doctors selectedDoctor;
        public Doctors SelectedDoctor
        {
            get => selectedDoctor;
            set
            {
                selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Doctor_ViewModel()
        {
            // Initialize the database helper with connection details
            databaseHelper = new Data_doc("localhost", "clinics", "root", "");
            Doctros = new ObservableCollection<Doctors>();
        }

        public void LoadDoctors()
        {
            // Clear the existing list of patients
            Doctros.Clear();

            // Get the list of patients from the database
            var doctorsFromDb = databaseHelper.GetAllDoctors();

            // Add the patients to the ObservableCollection
            foreach (var doctor in doctorsFromDb)
            {
                Doctros.Add(doctor);
            }
        }
        public void AddDocotr(Doctors doctor)
        {
            try
            {
                // Add the new docotrs to the database
                databaseHelper.AddDoctor(doctor.Id_doc, doctor.Name_doc, doctor.Familyname_doc, doctor.Age_doc, doctor.Phone_doc, doctor.Adress_doc, doctor.Branch_doc);

                // Refresh the doctors list after adding
                LoadDoctors();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Doctor: " + ex.Message, ex);
            }
        }


        public void DeleteDoctor(Doctors doctros)
        {
            if (doctros == null)
                throw new ArgumentNullException(nameof(doctros));

            try
            {
                // Delete the doctors from the database
                databaseHelper.DeleteDoctor(doctros.Id_doc);

                // Remove the doctors from the ObservableCollection
                Doctros.Remove(doctros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting patient: " + ex.Message, ex);
            }
        }

        public void UpdateDoctor(Doctors updatedoctors)
        {
            if(updatedoctors == null)
            {
                throw new ArgumentNullException(nameof(updatedoctors));
            }
            try
            {
                // Update the patient in the database
                databaseHelper.UpdateDoctor(updatedoctors.Id_doc, updatedoctors.Name_doc, updatedoctors.Familyname_doc, updatedoctors.Age_doc, updatedoctors.Phone_doc, updatedoctors.Adress_doc, updatedoctors.Branch_doc);

                // Refresh the patients list after updating
                LoadDoctors();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Docotr: " + ex.Message, ex);
            }

        }





















        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
