namespace BogusDataGenerator.Data
{
    public class IndigenousStatus
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;// return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
