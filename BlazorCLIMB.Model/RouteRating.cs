using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model
{
    public class RouteRating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClimbingRouteId { get; set; }
        public int Rating { get; set; }

       
    }
}
