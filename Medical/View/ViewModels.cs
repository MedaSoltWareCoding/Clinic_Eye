using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using Medical.Mod;
using Medical.Datas;
using System.Windows;


namespace Medical.View
{

    public class PatientViewModel : NotFiniteNumberException
    {
        public ObservableCollection<Patient> Patients
        { get; private set; }
        private readonly DatabaseHelper databaseHelper;

        public PatientViewModel()
        {
            // Initialize the database helper with connection details
            databaseHelper = new DatabaseHelper("localhost", "clinics", "root", "");
            Patients = new ObservableCollection<Patient>();

        }
        public void LoadPatients()
        {
           
            Patients.Clear();
            var patientsFromDb = databaseHelper.GetAllPatients();
            
            foreach (var patient in patientsFromDb)
            {
                
                Patients.Add(patient);
            }
        }

       










        public void AddPatient(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name) ||
                string.IsNullOrWhiteSpace(patient.FamilyName) ||
                string.IsNullOrWhiteSpace(patient.Gender))
            {
                throw new ArgumentException("Name, Family Name, and Gender are required.");
            }

            databaseHelper.AddPatient(
                patient.Name,
                patient.FamilyName,
                patient.Age,
                patient.Gender,
                patient.Birthday,
                patient.City,
                patient.Address,
                patient.Phone
            );
        }
    }
}
