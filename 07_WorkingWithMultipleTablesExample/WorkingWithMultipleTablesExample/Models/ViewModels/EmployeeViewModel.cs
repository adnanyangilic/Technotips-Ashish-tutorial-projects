namespace WorkingWithMultipleTablesExample.Models.ViewModels
{
    /// <summary>
    ///     Az adatok lekérdezéséhez szükséges osztály. Tárolja az összes
    ///     Employee táblához tartozó adatot (Kivéve Employee.DepartmentId)
    ///     és a Department táblához tartozó Department.Name attribútumot.
    ///     (Csak ennyit szeretnénk megjeleníteni)
    /// </summary>
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string DepartmentName { get; set; }
    }
}