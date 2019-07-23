using System.ComponentModel.DataAnnotations;

namespace ServerAndClientSideValidationExample.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage="Kérem adja meg a nevet!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kérem adja meg a címét!")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Kérem adja meg az osztály nevét")]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}