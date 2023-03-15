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
            //random beetween 0 and 1
            float mutateRand = (float)_random.NextDouble(1);

            if(mutateRand <= mutateRatio)
            {
                
            }
        }

        //Recombinine
        public Route Recombine(Route secondParent) 
        {
              var combinedCities = new List<City>();
              const int firstPartCount = 20;
              const int secondPartCount = 60;
              const int thirdPartCount = 20;
              
              for(int i = 0; i < firstPartCount)
              {
                var c = this.Cities.ElementAt(i);
                combinedCities.Add(c);
                this.Cities.Remove(c);
              }

              for(int i = 0; i < secondPartCount; i++)
              {
                bool insertSuccess = false;
                int offset = 0;
                while(insertSuccess == false)
                {
                    var c = secondParent.Cities.ElementAt(i + firstPartCount + offset);
                    if(!combinedCities.Contains(c))
                    {
                        combinedCities.Add(c);
                        secondParent.Cities.Remove(c);
                        insertSuccess = true;
                    }
                    else offset++;
                }
              }
              for(int i = 0; i < thirdPartCount; i++)
              {
                bool insertSuccess = false;
                int offset = 0;
                while(insertSuccess == false)
                {
                    var c = this.Cities.ElementAt(i + firstPartCount + secondPartCount + offset);
                    if(!combinedCities.Contains(c))
                    {
                        combinedCities.Add(c);
                        this.Cities.Remove(c);
                        insertSuccess = true;
                    }
                    else offset++;
                }
              }

              if(combinedCities.Count != 100)
              {
                throw new Exception("CityCountException");
              }

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
