using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdAIL_GenAlgorithmTSP
{
    public class Route
    {
        public LinkedList<City> Cities;
        private Random _random; 

        public Route(Random random, LinkedList<City> cities, bool initRoute = true) 
        {
            _random = random;
            Cities = new LinkedList<City>();

            if(initRoute)
            {
                GenerateRandomRoute(cities);
            }
        }

        public void GenerateRandomRoute(LinkedList<City> cities)
        {
            var citiesCount = cities.Count;
            for (int i = 0; i < citiesCount; i++)
            {
                var cityToTransfer = cities.ElementAt(_random.Next(cities.Count));
                Cities.AddLast(cityToTransfer);
                cities.Remove(cityToTransfer);
            }
        }

        public void Mutate()
        {
            throw new NotImplementedException();
        }

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
