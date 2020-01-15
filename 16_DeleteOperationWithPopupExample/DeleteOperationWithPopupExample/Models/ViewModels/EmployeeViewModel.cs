using System.ComponentModel.DataAnnotations;

namespace DeleteOperationWithPopupExample.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Kérem adja meg a nevet!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kérem adja meg a címét!")]
        public string Adress { get; set; }

        public string DepartmentName { get; set; }
    }
}