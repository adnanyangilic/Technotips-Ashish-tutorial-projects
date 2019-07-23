using System.ComponentModel.DataAnnotations;

namespace DisplayRecordFromDataBaseUsingDataTables.Models.ViewModels
{
    public class LoginViewModel
    {
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Az E-mail cím nem megfelelő")]
        [Required(ErrorMessage = "Kérem adja meg az E-mail címet")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Kérem adja meg a jelszavát")]
        public string Password { get; set; }
    }
}