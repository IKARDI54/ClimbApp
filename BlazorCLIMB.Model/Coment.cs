using BlazorCLIMB.Model.BlazorCRUD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model
{
    internal class Coment
    {
        public class Comment
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public User User { get; set; }  // Propiedad de navegación
            public int ClimbingRouteId { get; set; }
            public ClimbingRoute ClimbingRoute { get; set; }  // Propiedad de navegación
            public string Text { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
