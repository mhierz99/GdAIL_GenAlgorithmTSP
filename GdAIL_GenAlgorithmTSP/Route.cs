using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdAIL_GenAlgorithmTSP
{
    public class Route
    {
        public static int PopulationCounter = 0;
        public List<City> Cities;
        private readonly Random _random; 

        public Route(Random random, List<City> cities, bool initRoute = true) 
        {
            _random = random;

            if(initRoute)
            {
                Cities = new List<City>();
                GenerateRandomRoute(cities);
            }
            else
                Cities = cities;

            PopulationCounter++;
        }

        public void GenerateRandomRoute(List<City> cities)
        {
            var citiesCount = cities.Count;
            for (int i = 0; i < citiesCount; i++)
            {
                var cityToTransfer = cities.ElementAt(_random.Next(cities.Count));
                Cities.Add(cityToTransfer);
                cities.Remove(cityToTransfer);
            }
        }

        public void Mutate(float mutateRatio)
        {
            throw new NotImplementedException();
        }

        public Route Recombine(Route secondParent) { throw new NotImplementedException(); }

        public double GetTotalDistance()
        {
            double sum = 0;
            for (int i = 0; i < Cities.Count - 1; i++)
            {
                var actualCity = Cities.ElementAt(i);
                var nextCity = Cities.ElementAt(i +1);

                sum += City.GetDistanceDifference(actualCity, nextCity);
            }

            return sum;
        }
    }
}
