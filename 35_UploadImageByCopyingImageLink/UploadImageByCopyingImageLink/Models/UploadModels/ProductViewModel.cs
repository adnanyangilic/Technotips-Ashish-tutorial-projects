using System.Web;

namespace UploadImageByCopyingImageLink.Models.UploadModels
{
    public class ProductViewModel
    {
        public HttpPostedFileWrapper ImageFile { get; set; }

        public string ImageURL { get; set; }
    }
}