using BlazorCLIMB.Model.BlazorCRUD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model
{
    public class Notification
    {
        public int Id { get; set; }
        public int ReceiverUserId { get; set; }
        public User ReceiverUser { get; set; }  // Propiedad de navegación
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}





