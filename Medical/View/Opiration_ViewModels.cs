using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Medical.Datas;
using Medical.Mod;

namespace Medical.View
{
    internal class Opiration_ViewModels
    {


        public ObservableCollection<Opiration> opirations { get; private set; }
        private readonly Data_Opiration databaseHelper;

        public Opiration_ViewModels()
        {
            // Initialize the database helper with connection details
            databaseHelper = new Data_Opiration("localhost", "clinics", "root", "");
            opirations = new ObservableCollection<Opiration>();
        }

        public void LoadOpirations()
        {
            opirations.Clear();
            var OPFromDb = databaseHelper.GetAllOpirations();
            foreach (var opiration in OPFromDb)
            {
                opirations.Add(opiration);
            }
        }

        public void AddOpiration(Opiration opiration)
        {
            try
            {
                databaseHelper.AddOpiration(opiration);
                LoadOpirations();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding opiration: " + ex.Message, ex);
            }
        }

        public void DeleteOpiration(Opiration opiration)
        {
            if (opiration == null)
                throw new ArgumentNullException(nameof(opiration));
            try
            {
                databaseHelper.DeleteOpiration(opiration.Id);
                opirations.Remove(opiration);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting opiration: " + ex.Message, ex);
            }
        }

        public void UpdateOpiration(Opiration opiration)
        {
            if (opiration == null)
            {
                throw new ArgumentNullException(nameof(opiration));
            }
            try
            {
                databaseHelper.UpdateOpiration(opiration);
                LoadOpirations() ;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating opiration: " + ex.Message, ex);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
