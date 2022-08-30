using AutoFixture;

namespace DemoCode.Tests.BasicFixtures
{
    public class StringDemos
    {
        [Fact]
        public void BasicString()
        {
            // arrange
            var fixture = new Fixture();
            var sut = new NameJoiner();

            var firstName = fixture.Create("First_"); // The arg will be prefixed* to the randomly generated string.
            var lastName = fixture.Create("Last_");   // *Prefixes are a feature of the SeedExtentions package

            // When Autofixture genereates an entire object, it will prepend the property names without this extention.

            // act
            var result = sut.Join(firstName, lastName);

            // assert
            Assert.Equal($"{firstName} {lastName}", result);

        }



    }
}
