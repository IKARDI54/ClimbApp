using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model
{
    public class UserClimbingRoutes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClimbingRouteId { get; set; }
        public string Comments { get; set; }
        public int? Pegs { get; set; } // Campo Pegs es nulable
        public int? Cintas { get; set; } // Campo Cintas es nulable
        public DateTime? Date { get; set; }
    }
}
    