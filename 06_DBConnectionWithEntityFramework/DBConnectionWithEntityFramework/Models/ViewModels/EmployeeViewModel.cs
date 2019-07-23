namespace DBConnectionWithEntityFramework.Models.ViewModels
{
    /// <summary>
    ///     Az Employee típusú objektumok megjelenítéséhez szükséges osztály. 
    ///     (Azaz csak az Employee táblából kérdezünk le adatokat)
    /// </summary>
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public string Adress { get; set; }
    }
}