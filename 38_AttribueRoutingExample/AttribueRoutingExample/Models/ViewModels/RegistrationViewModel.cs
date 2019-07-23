using System;

namespace AttribueRoutingExample.Models.ViewModels
{
    public class RegistrationViewModel
    {
        public string UserName { get; set; }

        public string EmailID { get; set; }

        public string Password { get; set; }

        public string Adress { get; set; }

        public Nullable<int> RoleID { get; set; }
    }
}