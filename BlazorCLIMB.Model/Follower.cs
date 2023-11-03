using BlazorCLIMB.Model.BlazorCRUD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model
{
    namespace BlazorCLIMB.Model
    {
        public class Follower
        {
            public int Id { get; set; }
            public int FollowerUserId { get; set; }
            public User FollowerUser { get; set; }  // Propiedad de navegación
            public int FollowedUserId { get; set; }
            public User FollowedUser { get; set; }  // Propiedad de navegación
        }
    }

}
