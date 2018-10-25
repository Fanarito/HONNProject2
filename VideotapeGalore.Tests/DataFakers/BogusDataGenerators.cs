using Bogus;
using VideotapeGalore.Models.Entities;

namespace VideotapeGalore.Tests.DataFakers
{
    public static class BogusDataGenerators
    {
        private static readonly Faker<Friend> FakeFriendGenerator = new Faker<Friend>()
            .RuleFor(f => f.FirstName, f => f.Name.FirstName())
            .RuleFor(f => f.LastName, f => f.Name.LastName())
            .RuleFor(f => f.Email, (f, x) => f.Internet.Email(x.FirstName, x.LastName))
            .RuleFor(f => f.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(f => f.Address, f => f.Address.FullAddress());

        private static readonly Faker<Tape> FakeTapeGenerator = new Faker<Tape>()
            .RuleFor(t => t.Title, f => f.Hacker.Phrase())
            .RuleFor(t => t.Type, f => f.PickRandom<Tape.TapeType>())
            .RuleFor(t => t.ReleaseDate, f => f.Date.Past())
            .RuleFor(t => t.Eidr, f => f.PickRandom(SomeEidrs.eidrs))
            .RuleFor(t => t.DirectorFirstName, f => f.Name.FirstName())
            .RuleFor(t => t.DirectorLastName, f => f.Name.LastName());
            
            
        public static Friend GetFriend() => FakeFriendGenerator.Generate();

        public static Tape GetTape() => FakeTapeGenerator.Generate();
    }
}