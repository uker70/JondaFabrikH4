using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using JondaFabrikPrøveH4.Factory;
using Timer = System.Timers.Timer;

namespace JondaFabrikPrøveH4
{
    class Logic
    {
        //timer that simulates a day
        private static Timer _dayClock;
        private static int _timerRunTime;

        //timer to check if factory is done
        private static Timer _factoryDoneChecker;

        //list of the threads that needs to run, for the factory to work
        private static List<Thread> _factoryThreads = new List<Thread>();

        //starts the factroy threads
        public static void FactoryThreadStarter()
        {
            //creates the necessary threads for the factory
            for (int i = 0; i < FactorySettings.PlateCraftingTeams; i++)
            {
                _factoryThreads.Add(new Thread(FactoryThreads.BodyPlatesFactory));
            }

            for (int i = 0; i < FactorySettings.BodyAssemblingTeams; i++)
            {
                _factoryThreads.Add(new Thread(FactoryThreads.BodyAssemblingFactory));
            }

            for (int i = 0; i < FactorySettings.BodyPaintingTeams; i++)
            {
                _factoryThreads.Add(new Thread(FactoryThreads.BodyPaintingFactory));
            }

            for (int i = 0; i < FactorySettings.WheelAddingTeams; i++)
            {
                _factoryThreads.Add(new Thread(FactoryThreads.AddWheelsFactory));
            }

            for (int i = 0; i < FactorySettings.EngineAddingTeams; i++)
            {
                _factoryThreads.Add(new Thread(FactoryThreads.AddEngineFactory));
            }

            foreach (Thread thread in _factoryThreads)
            {
                thread.Start();
            }
        }

        //starts the 2 timers
        public static void StartTimers()
        {
            //creates a timer to show the factorys progress to the user
            _dayClock = new Timer();
            _dayClock.Elapsed += new ElapsedEventHandler(DayClockTimer);
            _dayClock.Interval = 1000;
            _timerRunTime = 0;
            _dayClock.Start();

            //creates a timer to monitor if the factory is done
            _factoryDoneChecker = new Timer();
            _factoryDoneChecker.Elapsed += new ElapsedEventHandler(IsFactoryDone);
            _factoryDoneChecker.Interval = 5000;
            _factoryDoneChecker.Start();
        }

        //_dayClock timer elapsed method that runs every second
        private static void DayClockTimer(object sender, EventArgs e)
        {
            //runs the timer
            _timerRunTime++;

            //runs the timer to notify the user of the factorys progress
            Program.FactoryProgressWriter(_timerRunTime, FactoryThreads.PlatesCrafted, FactoryThreads.BodiesAssembled,
                FactoryThreads.BodiesPainted, FactoryThreads.JondasWithWheels, FactoryThreads.JondasDone);

            //checks if the day is over or the factory is done
            if (_timerRunTime == FactorySettings.TimeInADay/1000 || FactoryThreads.WorkDone == true)
            {
                //stops the clock
                _dayClock.Stop();

                //adds the jondas to a list of jondas created per day and clears the list, incase you want to implement a "day" system, so you can run it again
                Report.DailyFactoryReport.Add(Conveyers.GetCompletedJondas());
                Conveyers.ClearEmptyJondas();

                //runs the daily report function to tell what was created today
                Program.FactoryDailyReport(Report.DailyFactoryReport[Report.DailyFactoryReport.Count-1]);
            }
        }

        //_factoryDoneChecker timer elapsed method that runs every 5 seconds
        private static void IsFactoryDone(object sender, EventArgs e)
        {
            //checks if all the processes in creating jondas are done
            if (FactoryThreads.PlatesCrafted == 250 && FactoryThreads.BodiesAssembled == 250 &&
                FactoryThreads.BodiesPainted == 250
                && FactoryThreads.JondasWithWheels == 250 && FactoryThreads.JondasDone == 250)
            {
                //tells the clock that the factory is done, if its done early
                FactoryThreads.WorkDone = true;

                //resets the factory incase you want to implement a "day" system, so you can make it run again
                FactoryThreads.PlatesCrafted = 0;
                FactoryThreads.BodiesAssembled = 0;
                FactoryThreads.BodiesPainted = 0;
                FactoryThreads.JondasWithWheels = 0;
                FactoryThreads.JondasDone = 0;
                FactoryThreads.CarsCreated = 0;
                FactoryThreads.MinisCreated = 0;
                FactoryThreads.MaxisCreated = 0;
                FactoryThreads.SportsCreated = 0;
            }
        }
    }
}
