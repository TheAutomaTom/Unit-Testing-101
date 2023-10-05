using AutoFixture;

namespace DemoCode.Tests.BasicFixtures
{
    public class NumberDemos
    {
        [Fact]
        public void Ints()
        {
            // arrange
            var sut = new IntCalculator();
            var fixture = new Fixture();
            var anonymousNumber = fixture.Create<int>();

            // act
            sut.Subtract(anonymousNumber);

            // assert
            Assert.True(sut.Value < 0);

        }


        [Fact]
        public void Decimals()
        {
            // arrange
            var sut = new DecimalCalculator();
            var fixture = new Fixture();
            var anonymousNumber = fixture.Create<decimal>();

            // act
            sut.Subtract(anonymousNumber);

            // assert
            Assert.True(sut.Value < 0);

        }


    }
}

