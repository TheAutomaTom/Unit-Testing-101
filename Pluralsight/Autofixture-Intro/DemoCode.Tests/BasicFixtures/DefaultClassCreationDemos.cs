using AutoFixture;
using System.Net.Mail;

namespace DemoCode.Tests.BasicFixtures
{
    public class DefaultClassCreationDemos
    {
        [Fact]
        public void DefaultObjects()
        {
            // arrange
            var fixture = new Fixture();
            EmailMessage message = fixture.Create<EmailMessage>();

            // act
            // Create() does not know this string is supposed to ba a valid email address.
            bool safelyValidate(string email)
            {
                var valid = true;
                try
                {
                    var emailAddress = new MailAddress(email);
                }
                catch
                {
                    valid = false;
                }
                return valid;
            }
            var sut = safelyValidate(message.ToAddress);

            // assert using safely validation email address
            Assert.True(sut == false);

            // assert by expecting an exception
            Assert.ThrowsAny<FormatException>(() => new MailAddress(message.ToAddress));
        }

        [Fact]
        public void DefaultObjectsWithDataAnnotations()
        {
            // arrange
            var fixture = new Fixture();
            var sut = fixture.Create<PlayerCharacter>();

            // assert the data annotations' contraints were maintained
            Assert.True(sut.RealName.Length <= 20);
            Assert.True(sut.GameCharacterName.Length <= 8);

            Assert.InRange(sut.CurrentHealth, 0, 100);
            Assert.Multiple(
                    () => Assert.IsType<int>(sut.CurrentHealth),
                    () => Assert.True(sut.CurrentHealth >= 0),
                    () => Assert.True(sut.CurrentHealth <= 100)
                );

        }


    }



}
