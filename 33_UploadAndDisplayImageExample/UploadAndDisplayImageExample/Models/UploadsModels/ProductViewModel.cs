using System.Web;

namespace UploadAndDisplayImageExample.Models.UploadsModels
{
    public class ProductViewModel
    {
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}