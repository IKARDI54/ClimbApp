using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model
{
    public class ClimbingRoute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public string ClimbingSector { get; set; }
        public int ClimbingSchoolId { get; set; }
        public byte[]? Imag { get; set; }
    }
}

