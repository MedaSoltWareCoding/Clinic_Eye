using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Medical.Datas;
using Medical.Mod;

namespace Medical.View
{
    public class Exm_ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Exams> Exams { get; private set; }
        private readonly Data_Exam databaseHelper;
        private Exams selectedExams;
        public Exams SelectedExams
        {
            get => selectedExams;
            set
            {
                selectedExams = value;
                OnPropertyChanged(nameof(SelectedExams));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Exm_ViewModel()
        {
            // Initialize the database helper with connection details
            databaseHelper = new Data_Exam("localhost", "clinics", "root", "");
            Exams = new ObservableCollection<Exams>();
        }

        public void LoadExams()
        {
            // Clear the existing list of patients
            Exams.Clear();

            // Get the list of patients from the database
            var examsFromDb = databaseHelper.GetAllExams();


            // Add the patients to the ObservableCollection
            foreach (var exam in examsFromDb)
            {
                Exams.Add(exam);
            }
        }

        public void AddExams(Exams exams)
        {
            try
            {
                // Add the new docotrs to the database
                databaseHelper.AddExams(exams.Id, exams.Cylinidrical_OD, exams.Spherical_OD, exams.Add_power_OD, exams.Axis_OD, exams.Cylinidrical_OS, exams.Spherical_OS, exams.Add_power_OS, exams.Axis_OS,
                    exams.Base_cruve_OD, exams.Diameterer_OD, exams.Power_OD, exams.Brand_type_OD, exams.Base_cruve_OS, exams.Diameterer_OS, exams.Power_OS, exams.Brand_type_OS, exams.DV, exams.NV, exams.CV, exams.SPH, exams.CYL, exams.AXIS, exams.PDP, exams.NDP, exams.CTR, exams.Phorias, exams.Steropsis);

                // Refresh the doctors list after adding
                LoadExams();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Exams: " + ex.Message, ex);
            }
        }
        public void DeleteExams(Exams exams)
        {
            if (exams == null)
                throw new ArgumentNullException(nameof(exams));

            try
            {
                // Delete the doctors from the database
                databaseHelper.DeleteExams(exams.Id);

                // Remove the doctors from the ObservableCollection
                Exams.Remove(exams);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting exams: " + ex.Message, ex);
            }
        }






























        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }








    }
}
