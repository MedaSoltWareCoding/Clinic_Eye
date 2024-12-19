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
            return this.Name + "," + this.FamilyName;
        }
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
            return this.Name_doc + "," + this.Familyname_doc;
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
}