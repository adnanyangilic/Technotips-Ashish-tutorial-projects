using System.Web;

namespace UploadAndRetrieveImageWithSQLDB.Models.UploadsModels
{
    public class ProductViewModel
    {
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}