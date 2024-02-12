using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace BogusDataGenerator.Data
{
    public class StudentModel
    {
        public int Identifier { get; set; }
        public string EqId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Age { get; set; }
        public Gender Gender { get; set; }
        public YearLevel YearLevel { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public string FullName { get; set; }
        public bool HasPhoto { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    } 

   
}
