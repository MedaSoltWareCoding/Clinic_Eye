using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Mod
{
    public class Appointment
    {
        public required int Id { get; set; }
        public required Patient patient { get; set; }
        public required DateTime date { get; set; }
        public int state { get; set; }
        public String family { get; set; }
    }
}
