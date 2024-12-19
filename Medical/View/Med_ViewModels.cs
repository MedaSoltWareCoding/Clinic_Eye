using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Medical.Datas;
using Medical.Mod;

namespace Medical.View
{
    public class Med_ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Medecine> Medecines { get; private set; }
        private readonly Data_med databaseHelper;
        private Medecine selectedMedecine;
        public Medecine SelectedMedicne
        {
            get => selectedMedecine;
            set
            {
                selectedMedecine = value;
                OnPropertyChanged(nameof(SelectedMedicne));
            }
        }

        public Med_ViewModel()
        {
            // Initialize the database helper with connection details
            databaseHelper = new Data_med("localhost", "clinics", "root", "");
            Medecines = new ObservableCollection<Medecine>();
        }

        public void LoadMedecines()
        {
           
            Medecines.Clear();

            
            var medecinesFromDb = databaseHelper.GetAllMedecine();

            // Add the patients to the ObservableCollection
            foreach (var medecine in medecinesFromDb)
            {
                Medecines.Add(medecine);
                //MessageBox.Show("sucsses");
            }
        }

        public void AddMedecine(Medecine medecine)
        {
            try
            {
                // Add the new docotrs to the database
                databaseHelper.AddMedecine(medecine.Id_med, medecine.Name_med , medecine.Descreption_med , medecine.dosage_me  );

                // Refresh the doctors list after adding
                LoadMedecines();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Medecine: " + ex.Message, ex);
            }
        }



        public void DeleteMedecine(Medecine medecine)
        {
            if (medecine == null)
                throw new ArgumentNullException(nameof(medecine));

            try
            {
                // Delete the doctors from the database
                databaseHelper.DeleteMedecint(medecine.Id_med);

                // Remove the doctors from the ObservableCollection
                Medecines.Remove(medecine);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Medecine: " + ex.Message, ex);
            }
        }


        public void UpdateMedecinet(Medecine updatemedicent)
        {
            if (updatemedicent == null)
            {
                throw new ArgumentNullException(nameof(updatemedicent));
            }
            try
            {
                // Update the patient in the database
                databaseHelper.UpdateMedicent(updatemedicent.Id_med , updatemedicent.Name_med , updatemedicent.Descreption_med , updatemedicent.dosage_me);

                // Refresh the patients list after updating
                LoadMedecines();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Medecint: " + ex.Message, ex);
            }

        }




















        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }







    }
}
