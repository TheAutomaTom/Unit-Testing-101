using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCode.Tests.CustomFixtures
{
	public class CustomSpecimenDemos
	{
		[Fact]
		public void BuiltInSpecimenBuilders() {

			var fixture = new Fixture();

			//This is using a built-in ISpecimenBuilder...
			fixture.Customize(new CurrentDateTimeCustomization());
			var date1 = fixture.Create<DateTime>(); //This will be "Now"

			// This is another way to add the same generator...
			fixture.Customizations.Add(new CurrentDateTimeGenerator());

			// Assert...not implemented
		}


		[Fact]
		public void CustomSpecimenBuilders()
		{
			var fixture = new Fixture();
			fixture.Customizations.Add(new AirportCodeGenerator());

			var flight = fixture.Create<FlightDetails>();

			Assert.NotNull(flight.ArrivalAirportCode);
			Assert.NotNull(flight.DepartureAirportCode);
			Assert.NotNull(flight.AirlineName);

			var airport = fixture.Create<Airport>();
			Assert.NotNull(airport.AirportCode);
		}

	}
}
