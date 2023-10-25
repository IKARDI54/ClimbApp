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
        public int ClimbingSectorId { get; set; }
        public int ClimbingSchoolId { get; set; }
        public string ImageUrl { get; set; }
    }
}

