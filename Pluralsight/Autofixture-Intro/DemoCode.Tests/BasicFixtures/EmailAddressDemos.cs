using AutoFixture;
using System.Net.Mail;

namespace DemoCode.Tests.BasicFixtures
{
    public class EmailAddressDemos
    {
        [Fact]
        public void EmailAddresses()
        {
            var fixture = new Fixture();

            // Longform...
            //string localPart = fixture.Create<EmailAddressLocalPart>().LocalPart;  //Looks redundant.. but that's how it works.
            //string domain = fixture.Create<DomainName>().Domain;
            //string emailAddress = $"{localPart}@{domain}";

            // Shortform...using System.Net.Mail
            MailAddress emailAddress = fixture.Create<MailAddress>();


            var sut = new EmailMessage(
                                    emailAddress.Address,           // <== Email Address
                                    fixture.Create<string>(),   // <== Message
                                    fixture.Create<bool>()      // <== IsImportant
                                );
            sut.Id = fixture.Create<Guid>();
            sut.MessageType = fixture.Create<EmailMessageType>();

            // assertions not implemented


        }

    }
}
