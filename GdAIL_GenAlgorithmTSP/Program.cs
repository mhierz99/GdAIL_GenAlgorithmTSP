using GdAIL_GenAlgorithmTSP;
using System;
using System.Diagnostics;

namespace Application
{
    class Program
    {
        private static List<Route> Population = new();
        private static List<City>? BaseCities;

        static void Main(string[] args)
        {
            Random random = new Random();
            BaseCities = City.GetCityFromCSV(random);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Chosen Cities:");
            foreach (var city in BaseCities)
            {
                Console.WriteLine(city.Name);
            }
            Console.WriteLine("=========================================================\n");

            double maxDistance = 0;
            double minDistance = Double.MaxValue;

            //Init
            for (int i = 0; i < Settings.PopulationSize; i++)
                Population.Add(new Route(random, BaseCities, initRoute: true));

            //Learning-Loop
            for (int i = 0; i < Settings.MaxPopulation; i++)
            {
                //calculate fitness
                //order by route quality
                Population = Population.OrderBy(x => x.GetTotalDistance()).ToList();

                var lastElement = Population.ElementAt(Settings.PopulationSize - 1);
                if(lastElement.GetTotalDistance() > maxDistance)
                    maxDistance = lastElement.GetTotalDistance();

                //delete last items
                Population.RemoveRange(Population.Count - Settings.ErrorThreshold, Settings.ErrorThreshold);

                //Recombine // actual: parents are the ones with highest fitness
                int offset = 0;
                while(Population.Count != Settings.PopulationSize)
                {
                    var child = Population.ElementAt(offset).Recombine(Population.ElementAt(++offset));
                    Population.Add(child);
                    offset++;
                }

                var firstElement = Population.ElementAt(0);

                if(firstElement.GetTotalDistance() < minDistance) 
                    minDistance = firstElement.GetTotalDistance();

                Console.WriteLine($"Actual best Route-Distance: {firstElement.GetTotalDistance()} km");
            }

            //Output Result:
            Population = Population.OrderBy(x => x.GetTotalDistance()).ToList();
            foreach (var city in Population.ElementAt(0).Cities)
            {
                Console.WriteLine(city.Name);
            }
            Console.WriteLine($"Total Distance: {Population.ElementAt(0).GetTotalDistance()} km");
            Console.WriteLine($"Min Distance: {minDistance} km");
            Console.WriteLine($"Max Distance: {maxDistance} km");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.ToString());
        }

        static void OutputCities(List<City> cities)
        {
            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }
        }
    }
}