using Bogus;
using ProjectsCore.Models;
using System;
using System.Text;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests
{
    public class SomePersonRepresentaion : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid MemberIdentifier { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal Credits { get; set; }

        public static SomePersonRepresentaion CreateOne()
        {
            var faker = new Faker<SomePersonRepresentaion>()
                .StrictMode(true)
                .RuleFor(x => x.Id, y => 0)
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
            builder.AppendLine($"{nameof(SomePersonRepresentaion.FirstName)} : {FirstName}");
            builder.AppendLine($"{nameof(SomePersonRepresentaion.LastName)} : {LastName}");
            builder.AppendLine($"{nameof(SomePersonRepresentaion.MemberIdentifier)} : {MemberIdentifier}");
            builder.AppendLine($"{nameof(SomePersonRepresentaion.Email)} : {Email}");
            builder.AppendLine($"{nameof(SomePersonRepresentaion.LoyaltyPoints)} : {LoyaltyPoints}");
            builder.AppendLine($"{nameof(SomePersonRepresentaion.Credits)} : {Credits}");
            builder.AppendLine($"{nameof(SomePersonRepresentaion.Phone)} : {Phone}");

            return builder.ToString();
        }
    }
}
