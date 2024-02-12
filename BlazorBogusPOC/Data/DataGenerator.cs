using Bogus;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BogusDataGenerator.Data
{
    public class DataGenerator
    {
        Faker<StudentModel> studentFaker;
        Faker<Gender> genderFaker = new Faker<Gender>();        
        Faker<EnrollmentStatus> enrollmentStatusFaker = new Faker<EnrollmentStatus>();        

        public DataGenerator()
        {
            Randomizer.Seed = new Random(112233);

            studentFaker = new Faker<StudentModel>()
                .RuleFor(s => s.Identifier, f => f.Random.Int(1, 10000))
                .RuleFor(s => s.EqId, f => GenerateAlphaNumeric())
                .RuleFor(s => s.ImageId, f => f.Random.Int(10000, 99999))
                .RuleFor(s => s.BirthDate, f => f.Date.Past(20))
                .RuleFor(s => s.Age, (f, s) => $"{DateTime.Now.Year - s.BirthDate.Year} years")
                .RuleFor(s => s.Gender, _ => genderFaker.Generate(1).First().OrNull(_, .1f))
                .RuleFor(s => s.YearLevel, (f, s) => GenerateYearLevel(s.BirthDate))
                .RuleFor(s => s.RollClass, (f, s) => GenerateRollClass(s.YearLevel))
                .RuleFor(s => s.EnrollmentStatus, _ => enrollmentStatusFaker.Generate(1).First().OrNull(_, .1f))
                .RuleFor(s => s.IndigenousStatus, _ => GenerateIndigenousStatus())
                .RuleFor(s => s.FamilyName, f => f.Name.LastName())
                .RuleFor(s => s.GivenNames, f => f.Name.FirstName())
                .RuleFor(s => s.FullName, (f, s) => s.GivenNames + " " + s.FamilyName)
                .RuleFor(s => s.HasPhoto, f => f.Random.Bool())
                .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.FamilyName, s.GivenNames))
                .RuleFor(s => s.PhoneNumber, f => GenerateAustralianPhoneNumber()) //f.Phone.PhoneNumber();
                .RuleFor(s => s.MisId, f => f.Random.AlphaNumeric(5))
                .RuleFor(s => s.Lui, f => f.Random.AlphaNumeric(9))
                .RuleFor(s => s.Usi, f => f.Random.AlphaNumeric(9));

            genderFaker = new Faker<Gender>()
             .RuleFor(g => g.Code, f => f.PickRandom("M", "F"))
             .RuleFor(g => g.Description, (f, g) => g.Code == "M" ? "Male" : "Female");

            enrollmentStatusFaker = new Faker<EnrollmentStatus>()
                .RuleFor(e => e.Code, f => f.PickRandom("Active", "InActive"))
                .RuleFor(g => g.Description, (f, g) => g.Code == "A" ? "Active" : "InActive");
            
        }

        public StudentModel GenerateStudent()
        {
            return studentFaker.Generate();
        }

        public IEnumerable<StudentModel> GenerateStudents()
        {
            return studentFaker.GenerateForever();
        }

        static string GenerateAlphaNumeric()
        {
            var randomNumberPart = new Random().Next(100000000, 999999999).ToString();
            var lastDigit = new Random().Next(0, 9);
            var randomChar = (char)new Random().Next('A', 'Z' + 1);
            return randomNumberPart.ToString() + lastDigit.ToString() + randomChar;
        }

        static string GenerateRollClass(YearLevel yearLevel)
        {
            var randomChar = (char)new Random().Next('A', 'Z' + 1);
            return yearLevel.Code.ToString() + randomChar;   
        }

        static YearLevel GenerateYearLevel(DateTime dob)
        {   
            var age = DateTime.Today.Year - dob.Year;
            
            switch (age)
            {
                case var _ when age <= 6 :
                    return new YearLevel { Code = "1", Description = "Year 1" };
                case 7:
                    return new YearLevel { Code = "2", Description = "Year 2" };
                case 8:
                    return new YearLevel { Code = "3", Description = "Year 3" };
                case 9:
                    return new YearLevel { Code = "4", Description = "Year 4" };
                case 10:
                    return new YearLevel { Code = "5", Description = "Year 5" };
                case 11:
                    return new YearLevel { Code = "6", Description = "Year 6" };
                case 12:
                    return new YearLevel { Code = "7", Description = "Year 7" };
                case 13:
                    return new YearLevel { Code = "8", Description = "Year 8" };
                case 14:
                case 15:
                    return new YearLevel { Code = "9", Description = "Year 9" };
                case 16:
                case 17:
                    return new YearLevel { Code = "10", Description = "Year 10" };
                case 18:
                    return new YearLevel { Code = "11", Description = "Year 11" };
                case 19:
                case 20:
                    return new YearLevel { Code = "12", Description = "Year 12" };
                default: 
                    return new YearLevel { Code = "0", Description = "Check the data"};
                    
            }
        }

        static IndigenousStatus GenerateIndigenousStatus()
        {
            int[] options = { 1, 2, 3, 4, 9 };
            int randomIndex = new Random().Next(options.Length);
            int randomCode = options[randomIndex];

            switch (randomCode)
            {
                case 1:
                    return new IndigenousStatus { Code = 1, Description = "Aboriginal but not Torres Strait Islander origin" };
                case 2:
                    return new IndigenousStatus { Code = 2, Description = "Torres Strait Islander but not Aboriginal origin" };
                case 3:
                    return new IndigenousStatus { Code = 3, Description = "Both Aboriginal and Torres Strait Islander origin" };
                case 4:
                    return new IndigenousStatus { Code = 4, Description = "Neither Aboriginal nor Torres Strait Islander origin" };
                default:
                    return new IndigenousStatus { Code = 9, Description = "Not stated/inadequately described" };
            }


        }

        static string GenerateAustralianPhoneNumber()
        {
            var areaCode = new Random().Next(2, 9).ToString() + new Random().Next(0, 9).ToString(); // Generate a 2-digit area code
            var subscriberNumber = string.Join("", new Random().Next(10000000, 99999999).ToString("D8")); // Generate an 8-digit subscriber number
            return $"0{areaCode}{subscriberNumber}";
        }
    }
}
