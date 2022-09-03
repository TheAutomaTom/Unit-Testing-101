using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoCode.Tests.CustomFixtures
{
	public class AirportCodeGenerator : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			// See if we're trying to create a value for a property
			var propertyInfo = request as PropertyInfo;

			if( propertyInfo is null )
			{
				// This specimen builder does not apply to current request
				return new NoSpecimen();
			}

			// Now we have confirmed we are creating a property.
			// Is it one we need custom logic for?

			// This applies to DepartureAirportCode, ArrivalAirportCode, and AirportCode
			var isAirportCode = propertyInfo.Name.Contains("AirportCode");

			// and the property type is string... just to be safe.
			var isStringProperty = propertyInfo.PropertyType == typeof(string);

			if (!isAirportCode || !isStringProperty)
			{
				return new NoSpecimen();
			}				
			return RandomAirportCode();

		}

		private string RandomAirportCode()
		{
			if(DateTime.Now.Ticks % 2 == 0)
			{
				return "RDU";
			}
			return "VPS";
		}

	}
}
