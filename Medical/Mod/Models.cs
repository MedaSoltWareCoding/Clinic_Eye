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
    }

    public class Medecine
    {
        public required int Id_med { get; set; }
        public required string Name_med { get; set; }
        public required string Descreption_med { get; set; }
        public required string dosage_me { get; set; }
       
    }
}