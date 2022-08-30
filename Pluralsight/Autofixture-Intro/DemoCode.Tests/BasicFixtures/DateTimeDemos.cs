using AutoFixture;

namespace DemoCode.Tests.BasicFixtures
{
    public class DateTimeDemos
    {
        [Fact]
        public void DateTimes()
        {
            // arrange manually			
            //DateTime logTime = new DateTime(2020, 1, 21);

            // arrange randomly
            var fixture = new Fixture();
            DateTime logTime = fixture.Create<DateTime>();

            string logMessage = fixture.Create<string>();

            // act
            //LogMessage result = LogMessageCreator.Create("some log message", logTime);
            LogMessage result = LogMessageCreator.Create(logMessage, logTime);

            // assert
            //Assert.Equal(2020, result.Year);
            Assert.Equal(logTime.Year, result.Year);


        }
    }
}
