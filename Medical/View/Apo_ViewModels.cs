using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Medical.Datas;
using Medical.Mod;
using System.ComponentModel;

namespace Medical.View
{
    public class AppointmenViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Appointment> appointments { get; private set; }
        private readonly Data_apo databaseHelper;

        private Appointment appointment;
        public Appointment SelectedAppointment
        {
            get => appointment;
            set
            {
                SelectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AppointmenViewModel()
        {
            // Initialize the database helper with connection details
            databaseHelper = new Data_apo("localhost", "clinics", "root", "");
            appointments = new ObservableCollection<Appointment>();
        }

        public void LoadAppointemnts()
        {
            // Clear the existing list of patients
            appointments.Clear();

            // Get the list of patients from the database
            var appointmentFromDb = databaseHelper.GetAllAppointments();

            // Add the patients to the ObservableCollection
            foreach (var app in appointmentFromDb)
            {
                appointments.Add(app);
            }
            Console.WriteLine("appoint,ents : " + appointments);
        }

        public void AddAppointment(Appointment app)
        {
            try
            {
                // Add the new patient to the database
                databaseHelper.AddAppointment(app.Id, app.patient, app.date, app.state);

                // Refresh the patients list after adding
                LoadAppointemnts();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding patient: " + ex.Message, ex);
            }
        }

        public void DeleteAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            try
            {
                // Delete the appointemnt from the database
                databaseHelper.DeleteAppointment(appointment.Id);

                // Remove the patient from the ObservableCollection
                appointments.Remove(appointment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting appointemnt: " + ex.Message, ex);
            }

        }

        public void UpdatePatient(Appointment app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            try
            {
                // Update the appointemnt in the database
                databaseHelper.UpdateAppointment(app.Id, app.patient, app.date, app.state);

                // Refresh the appointemnt list after updating
                LoadAppointemnts();
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