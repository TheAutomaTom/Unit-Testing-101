using AutoFixture;

namespace DemoCode.Tests.CustomFixtures
{
	public class SpecifiedPropertyValueDemos
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


		[Fact]
		public void FreezingValues()
		{
			var fixture = new Fixture();

			// This is the long-form...
			var id = fixture.Create<int>();
			fixture.Inject(id);
			// of this:
			var customerName = fixture.Freeze<string>();

			var sut = fixture.Create<Order>();

			Assert.Equal($"{id}-{customerName}", sut.ToString());

		}

		[Fact]
		public void OmitSpecificProperties()
		{
			var fixture = new Fixture();

			var flight = fixture.Build<FlightDetails>()
													.Without(x => x.ArrivalAirportCode)
													.Without(x => x.DepartureAirportCode)
													.Create();

			Assert.Null(flight.ArrivalAirportCode);
			Assert.Null(flight.DepartureAirportCode);
			Assert.NotNull(flight.AirlineName);

		}

		[Fact]
		public void OmitAutoProperties()
		{
			var fixture = new Fixture();

			var flight = fixture.Build<FlightDetails>()
													.OmitAutoProperties()
													.Create();

			Assert.Null(flight.ArrivalAirportCode);
			Assert.Null(flight.DepartureAirportCode);
			Assert.Null(flight.AirlineName);

		}

		[Fact] public void NonAnomymousProperties()
		{
			var fixture = new Fixture();
			var str1 = "LAX";
			var str2 = "RDU";

			var sut = fixture.Build<FlightDetails>()
				.With(x => x.ArrivalAirportCode, str1)
				.With(x => x.DepartureAirportCode, str2)
				.Create();

			Assert.Equal(str1, sut.ArrivalAirportCode);
			Assert.Equal(str2, sut.DepartureAirportCode);
			Assert.NotNull(sut.AirlineName);
		}

		[Fact]
		public void NonAnomymousPropertiesWithActions()
		{
			var fixture = new Fixture();
			var str1 = "LAX";
			var str2 = "RDU";

			var sut = fixture.Build<FlightDetails>()
				.With(x => x.ArrivalAirportCode, str1)
				.With(x => x.DepartureAirportCode, str2)
				.Without(x => x.MealOptions)
				.Do(x => x.MealOptions.Add("Hummus"))
				.Do(x => x.MealOptions.Add("Tabohli"))
				.Create();
			// Create isn't neccesarry here,
			// you can set this up to run create multiple instances,
			// using the same settings.

			Assert.Equal(str1, sut.ArrivalAirportCode);
			Assert.Equal(str2, sut.DepartureAirportCode);
			Assert.NotNull(sut.AirlineName);
			Assert.Equal(2, sut.MealOptions.Count);
			Assert.Equal("Tabohli", sut.MealOptions[1]);
		}

	}
}
