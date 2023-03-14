using GdAIL_GenAlgorithmTSP;
using System;

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

            //Init
            for (int i = 0; i < Settings.PopulationSize; i++)
                Population.Add(new Route(random, BaseCities, initRoute: true));

            //Learning-Loop
            for (int i = 0;i < Settings.MaxPopulation; i++)
            {
                //calculate fitness
                //order by route quality
                Population = Population.OrderBy(x => x.GetTotalDistance()).ToList();

                //delete last items
                Population.RemoveRange(Population.Count - Settings.ErrorThreshold, Settings.ErrorThreshold);

                //Recombine // actual: parents are the ones with highest fitness
                for (int x = 0; i < Settings.ErrorThreshold * 2; i++)
                {
                    var child = Population.ElementAt(x).Recombine(Population.ElementAt(++x));
                    Population.Add(child);
                }
            }
        }
    }
}