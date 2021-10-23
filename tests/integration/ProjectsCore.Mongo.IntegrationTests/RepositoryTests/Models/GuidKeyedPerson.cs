using Bogus;
using ProjectsCore.Models;
using System;
using System.Text;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models
{
    public class GuidKeyedPerson : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid MemberIdentifier { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal Credits { get; set; }

        public static GuidKeyedPerson CreateOne()
        {
            var faker = new Faker<GuidKeyedPerson>()
                .StrictMode(true)
                .RuleFor(x => x.Id, y => Guid.Empty)
                .RuleFor(x => x.FirstName, y => y.Person.FirstName)
                .RuleFor(x => x.LastName, y => y.Person.LastName)
                .RuleFor(x => x.MemberIdentifier, y => Guid.NewGuid())
                .RuleFor(x => x.Phone, y => y.Phone.PhoneNumber())
                .RuleFor(x => x.Email, y => y.Person.Email)
                .RuleFor(x => x.LoyaltyPoints, y => y.Random.Int(0, int.MaxValue))
                .RuleFor(x => x.Credits, y => y.Finance.Amount(0, decimal.MaxValue));

            return faker.Generate();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"{nameof(IntKeyedPerson.FirstName)} : {FirstName}");
            builder.AppendLine($"{nameof(IntKeyedPerson.LastName)} : {LastName}");
            builder.AppendLine($"{nameof(IntKeyedPerson.MemberIdentifier)} : {MemberIdentifier}");
            builder.AppendLine($"{nameof(IntKeyedPerson.Email)} : {Email}");
            builder.AppendLine($"{nameof(IntKeyedPerson.LoyaltyPoints)} : {LoyaltyPoints}");
            builder.AppendLine($"{nameof(IntKeyedPerson.Credits)} : {Credits}");
            builder.AppendLine($"{nameof(IntKeyedPerson.Phone)} : {Phone}");

            return builder.ToString();
        }
    }
}
