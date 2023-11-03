using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Model
{
    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }

}
