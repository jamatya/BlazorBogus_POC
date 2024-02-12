using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace BogusDataGenerator.Data
{
    public class Gender
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;//  return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
  
}
