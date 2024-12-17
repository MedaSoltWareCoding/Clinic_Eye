using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Medical.Datas;
using Medical.Mod;
using System.ComponentModel;

namespace Medical.View
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Patient> Patients { get; private set; }
        private readonly DatabaseHelper databaseHelper;

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PatientViewModel()
        {
            // Initialize the database helper with connection details
            databaseHelper = new DatabaseHelper("localhost", "clinics", "root", "");
            Patients = new ObservableCollection<Patient>();
        }

        public void LoadPatients()
        {
            // Clear the existing list of patients
            Patients.Clear();

            // Get the list of patients from the database
            var patientsFromDb = databaseHelper.GetAllPatients();

            // Add the patients to the ObservableCollection
            foreach (var patient in patientsFromDb)
            {
                Patients.Add(patient);
            }
        }

        public void AddPatient(Patient patient)
        {
            try
            {
                // Add the new patient to the database
                databaseHelper.AddPatient(patient.Id, patient.Name, patient.FamilyName, patient.Age, patient.Gender, patient.Birthday, patient.City, patient.Address, patient.Phone);

                // Refresh the patients list after adding
                LoadPatients();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding patient: " + ex.Message, ex);
            }
        }

        public void DeletePatient(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            try
            {
                // Delete the patient from the database
                databaseHelper.DeletePatient(patient.Id);

                // Remove the patient from the ObservableCollection
                Patients.Remove(patient);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting patient: " + ex.Message, ex);
            }
        }

        public void UpdatePatient(Patient updatedPatient)
        {
            if (updatedPatient == null)
                throw new ArgumentNullException(nameof(updatedPatient));

            try
            {
                // Update the patient in the database
                databaseHelper.UpdatePatient(updatedPatient.Id, updatedPatient.Name, updatedPatient.FamilyName, updatedPatient.Age, updatedPatient.Gender, updatedPatient.Birthday, updatedPatient.City, updatedPatient.Address, updatedPatient.Phone);

                // Refresh the patients list after updating
                LoadPatients();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating patient: " + ex.Message, ex);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
