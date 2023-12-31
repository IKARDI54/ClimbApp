﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model

{
    namespace BlazorCRUD.Model
    {
        public class User
        {
            public int Id { get; set; }  
            public string? Email { get; set; }
            public string? PasswordHash { get; set; }
            public string? Role { get; set; }
            public string? Name { get; set; }
            public byte[]? Img { get; set; }
        }
    }
}
