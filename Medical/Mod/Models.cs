﻿namespace Medical.Mod
{
    public class Patient
    {
        //public required int id { get; set; }
        public required string Name { get; set; }
        public required string FamilyName { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
    }
}