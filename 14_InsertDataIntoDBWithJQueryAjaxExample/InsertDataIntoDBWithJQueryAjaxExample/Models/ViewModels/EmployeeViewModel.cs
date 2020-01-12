using System.ComponentModel.DataAnnotations;

namespace InsertDataIntoDBWithJQueryAjaxExample.Models.ViewModels
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "Kérem adja meg a nevet!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kérem adja meg a címét!")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Kérem adja meg az osztály nevét!")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Kérem adja meg az oldala nevét!")]
        public string SiteName { get; set; }
    }
}