using System;
using System.Collections.Generic;
using JondaFabrikPrøveH4.Class;

namespace JondaFabrikPrøveH4
{
    class Program
    {
        static void Main(string[] args)
        {
            Logic.FactoryThreadStarter();
            Logic.StartTimers();
            Console.ReadKey(true);
        }

        //writes the time and the progress of each step in the car making progress
        public static void FactoryProgressWriter(int timeTillDone, int plates, int assembled, int painted, int hasWheels, int jondasCompleted)
        {
            Console.Clear();
            Console.WriteLine($"Time: {(timeTillDone / 6).ToString("00")}:{(timeTillDone % 6)}0\n" +
                              $"Plates: {plates}\n" +
                              $"Assembled: {assembled}\n" +
                              $"Painted: {painted}\n" +
                              $"Has Wheels: {hasWheels}\n" +
                              $"Done: {jondasCompleted}");
        }

        //writes the report of the daily activity of the factory, in this case, how many of each car was produced
        public static void FactoryDailyReport(List<Jonda> completedJondas)
        {
            Console.WriteLine();
            int car = 0;
            int mini = 0;
            int maxi = 0;
            int sport = 0;

            foreach (Jonda jonda in completedJondas)
            {
                switch (jonda.GetType().Name.ToUpper())
                {
                    case "CAR":
                        car++;
                        break;

                    case "MINI":
                        mini++;
                        break;

                    case "MAXI":
                        maxi++;
                        break;

                    case "SPORT":
                        sport++;
                        break;
                }
            }
            Console.WriteLine($"Cars: {car}\n" +
                              $"Minis: {mini}\n" +
                              $"Maxis: {maxi}\n" +
                              $"Sports: {sport}\n\n" +
                              $"Press anything to close the program");
        }
    }
}
