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

            if (initRoute)
            {
                Cities = new List<City>();
                GenerateRandomRoute(cities);
            }
            else
                Cities = cities;

            PopulationCounter++;
        }

        public void GenerateRandomRoute(List<City> citiesBase)
        {
            var cities = new List<City>(citiesBase);
            var citiesCount = cities.Count;
            for (int i = 0; i < citiesCount; i++)
            {
                var cityToTransfer = cities.ElementAt(_random.Next(cities.Count));
                Cities.Add(cityToTransfer);
                cities.Remove(cityToTransfer);
            }
        }

        private void Mutate(float mutateRatio)
        {
            float mutateRand = (float)_random.NextDouble();

            if (mutateRand <= mutateRatio)
                SwapRandomCities();

        }

        private void SwapRandomCities()
        {
            int index1 = _random.Next(Cities.Count);
            int index2 = _random.Next(Cities.Count);

            var tempObj = Cities.ElementAt(index1);
            Cities[index1] = Cities.ElementAt(index2);
            Cities[index2] = tempObj;
        }


        //Recombinine
        public Route Recombine(Route secondParent)
        {
            var combinedCities = new List<City>();
            //var usedCities = new HashSet<City>();

            var cities1 = new List<City>(this.Cities);
            var cities2 = new List<City>(secondParent.Cities);

            while(combinedCities.Count != 20)
            {
                var city = cities1[0];
                
                if(!combinedCities.Contains(city))
                {
                    combinedCities.Add(city);
                }

                cities1.Remove(city);
                cities2.Remove(city);
            }

            int offset = 0;
            while(combinedCities.Count != 80)
            {
                if (offset >= cities2.Count)
                    break;

                var city = cities2[offset];

                if (!combinedCities.Contains(city))
                {
                    combinedCities.Add(city);
                }
                else offset++;

                cities2.Remove(city);
                cities1.Remove(city);
            }

            offset = 0;
            while(combinedCities.Count != 100)
            {
                if(offset >= cities1.Count)  
                    break;

                var city = cities1[offset];

                if (!combinedCities.Contains(city))
                {
                    combinedCities.Add(city);
                }
                else offset++;

                cities2.Remove(city);
                cities1.Remove(city);
            }

            //int counter = 0;
            //while(combinedCities.Count != 100)
            //{
            //    combinedCities.Add(cities1[(counter++)]);
            //}

            if (combinedCities.Count != 100)
            {
                throw new Exception("CityCountException");
            }

            var child = new Route(_random, combinedCities, initRoute: false);
            child.Mutate(Settings.MutationRatio);
            return child;

        }

        public double GetTotalDistance()
        {
            double sum = 0;
            for (int i = 0; i < Cities.Count - 1; i++)
            {
                var actualCity = Cities.ElementAt(i);
                var nextCity = Cities.ElementAt(i + 1);

                sum += City.GetDistanceDifference(actualCity, nextCity);
            }

            return sum;
        }
    }
}
