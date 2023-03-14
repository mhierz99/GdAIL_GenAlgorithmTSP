using GdAIL_GenAlgorithmTSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdAIL_GenAlgorithmTspTest
{
    public class CityTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ExampleTest()
        {
            Assert.Pass();
        }

        [Test]
        public void CorrectDifference()
        {
            City city1 = new City()
            {
                Name = "Paris",
                Latitude = 48.8566101,
                Longitude = 2.3514992
            };

            City city2 = new City()
            {
                Name = "Berlin",
                Latitude = 52.523403,
                Longitude = 13.4114
            };

            var distance = City.GetDistanceDifference(city1, city2);

            Assert.That((int)distance, Is.EqualTo(874));
        }

        [Test]
        public void ListCorrectFilled()
        {
            Random rand = new Random();
            var cities = City.GetCityFromCSV(rand);

            Assert.That(cities.Count, Is.EqualTo(100));
        }
    }
}
