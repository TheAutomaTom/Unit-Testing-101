using AutoFixture;

namespace DemoCode.Tests.BasicFixtures
{
    public class CreateManyDemos
    {
        [Fact]
        public void CreateManyCountDefault()
        {
            var defaultCount = 3;
            var fixture = new Fixture();

            // This would create a 3 guids, by default
            IEnumerable<string> messages = fixture.CreateMany<string>();

            Assert.True(messages.Count() == defaultCount);
        }

        [Fact]
        public void CreateManyCountSpecified()
        {
            var specifiedCount = 13;
            var fixture = new Fixture();

            // add arg to specify length of sequence
            IEnumerable<int> messages = fixture.CreateMany<int>(specifiedCount);

            Assert.True(messages.Count() == specifiedCount);
        }


        [Fact]
        public void AddManyToSequence()
        {
            // arrange
            var specifiedCount = 130;
            var fixture = new Fixture();
            var sut = new DebugMessageBuffer();


            // act
            fixture.AddManyTo(sut.Messages, specifiedCount);
            sut.WriteMessages();

            // assert
            Assert.Equal(specifiedCount, sut.MessagesWritten);
        }

        [Fact]
        public void AddManyWithFunction()
        {
            // arrange
            var specifiedCount = 1350;
            var fixture = new Fixture();
            var sut = new DebugMessageBuffer();
            var prefix = "Test_";
            var rnd = new Random();


            // act
            fixture.RepeatCount = specifiedCount;
            fixture.AddManyTo(sut.Messages, () => $"{prefix}{rnd.Next()}");
            sut.WriteMessages();

            // assert
            Assert.All<string>(sut.Messages, m => m.StartsWith(prefix));
            Assert.Equal(specifiedCount, sut.MessagesWritten);
        }

    }
}
