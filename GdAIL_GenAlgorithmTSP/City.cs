using System;
using System.Globalization;
using System.IO;

namespace GdAIL_GenAlgorithmTSP
{
    public class City
    {
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static double GetDistanceDifference(City city1, City city2)
        {
            const double r = 637100;

            var theta1 = city1.Latitude * Math.PI / 180;
            var theta2 = city2.Latitude * Math.PI / 180;
            var deltaTheta = (city2.Latitude - city1.Latitude) * Math.PI / 180;
            var deltaLambda = (city2.Longitude - city1.Longitude) * Math.PI / 180;

            var a = Math.Sin(deltaTheta / 2) * Math.Sin(deltaTheta / 2) +
                Math.Cos(theta1) * Math.Cos(theta2) +
                Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = r * c;

            return distance / 1000;
        }

        public static List<City> GetCityFromCSV(Random random)
        {
            var csvItems = new List<City>();
            var cities = new List<City>();
            using var reader = new StreamReader(Settings.CsvPath);

            if(!reader.EndOfStream) { reader.ReadLine(); }

            while(!reader.EndOfStream) 
            {
                var line = reader.ReadLine();
                var columns = line!.Split(',');

                csvItems.Add(new City()
                {
                    Name = columns![0],
                    Latitude = Convert.ToDouble(columns![1], CultureInfo.InvariantCulture),
                    Longitude = Convert.ToDouble(columns![2], CultureInfo.InvariantCulture)
                });
            }

            //for (int i = 0; i < Settings.CitiesCount; i++)
            //{
            //    var citieToTransfer = csvItems[random.Next(csvItems.Count-1)];
            //    cities.Add(citieToTransfer);
            //    csvItems.Remove(citieToTransfer);
            //}

            for(int i= 0; i< Settings.CitiesCount;i++)
            {
                var citiesToTransfer = csvItems[i];
                cities.Add(citiesToTransfer);
            }

            return cities;
        }
    }
}
