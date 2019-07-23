using System.Collections.Generic;

namespace HowToReturnMultipleModels.Models.ABCModels
{
    public class CModel
    {
        public List<AModel> AModelsList { get; set; }
        public List<BModel> BModelsList { get; set; }
        public int Age { get; set; }
    }
}