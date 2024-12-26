using System.Collections.ObjectModel;

namespace Medical.Mod
{
    public class Patient
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string FamilyName { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }

        public override string ToString()
        {
            return this.Name + "  " + this.FamilyName;
        }
        public ObservableCollection<string> Files { get; set; } = new ObservableCollection<string>();
    }

    public class Doctors
    {
        public required int Id_doc { get; set; }
        public required string Name_doc { get; set; }
        public required string Familyname_doc { get; set; }
        public required int Age_doc { get; set; }
        public required string Phone_doc { get; set; }
        public required string Adress_doc { get; set; }
        public required string Branch_doc {get; set;}
        public override string ToString()
        {
            return this.Name_doc + "  " + this.Familyname_doc;
        }

    }

    public class Medecine
    {
        public required int Id_med { get; set; }
        public required string Name_med { get; set; }
        public required string Descreption_med { get; set; }
        public required string dosage_me { get; set; }
       
    }

    public class Appointment
    {
        public required int Id { get; set; }
        public required Patient patient { get; set; }
        public required DateTime date { get; set; }
        public int state { get; set; }
        public String family { get; set; }
    }
    public class Exams
    {
        public required int Id  { get; set; }
        public required string Cylinidrical_OD { get; set; }
        public required string Spherical_OD { get; set; }
        public required string Add_power_OD { get; set; }
        public required string Axis_OD  { get; set; }
        public required string Cylinidrical_OS  { get; set; }
        public required string Spherical_OS { get; set; }
        public required string Add_power_OS { get; set; }
        public required string Axis_OS { get; set; }
        public required string Base_cruve_OD    { get; set; }
        public required string Diameterer_OD { get; set; }
        public required string Power_OD     { get; set; }
        public required string Brand_type_OD    { get; set; }
        public required string Base_cruve_OS { get; set; }
        public required string Diameterer_OS { get; set; }
        public required string Power_OS { get; set; }
        public required string Brand_type_OS { get; set; }
        public required string DV {  get; set; }
        public required string NV { get; set; }
        public required string CV { get; set; }
        public required string SPH { get; set; }
        public required string CYL { get; set; }
        public required string AXIS { get; set; }
        public required string PDP { get; set; }
        public required string NDP { get; set; }
        public required string CTR { get; set; }
        public required string Phorias { get; set; }
        public required string Steropsis{ get; set; }

    }
}