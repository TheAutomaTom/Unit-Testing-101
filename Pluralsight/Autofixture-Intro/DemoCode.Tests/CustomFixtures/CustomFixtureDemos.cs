using AutoFixture;

namespace DemoCode.Tests.CustomFixtures
{
	public class CustomFixtureDemos
	{
		[Fact]
		public void InjectedValues()
		{
			var fixture = new Fixture();

			// This fails because there are no data annotations describing an AirportCode,
			// so an error is thrown in the property's setter (which hits in evaluation method).
			// var flight = fixture.Create<FlightDetails>();

			var injectedString = "LHR";

			fixture.Inject(injectedString);
			// This will assign "LHR" for EVERY STRING related to the object, including lists' values
			var flight = fixture.Create<FlightDetails>();

			Assert.Equal(flight.AirlineName, injectedString);
			Assert.Equal(flight.DepartureAirportCode, injectedString);
			Assert.Equal(flight.ArrivalAirportCode, injectedString);
			Assert.All(flight.MealOptions, mo => mo.Equals(injectedString));
		}


		[Fact]
		public void RegistedValues()
		{
			var fixture = new Fixture();
			fixture.Register(() => DateTime.Now.Ticks.ToString());

			var string1 = fixture.Create<string>();
			var string2 = fixture.Create<string>();

			Int64.TryParse(string1, out var x);
			Int64.TryParse(string2, out var y);


			// Each value is assigned the time it was created
			// (as appposed to a static value, like in InjectedValues())
			Assert.True(x < y);
		}







	}
}
