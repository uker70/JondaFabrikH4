using System;
using System.Threading;
using JondaFabrikPrøveH4.Class;

namespace JondaFabrikPrøveH4.Factory
{
    class FactoryThreads
    {
        //bool to show threads are done
        private static bool _workDone = false;
        public static bool WorkDone
        {
            get { return _workDone; }
            set { _workDone = value; }
        }

        //counters to make sure 250 is produced
        private static int _platesCrafted = 0;
        public static int PlatesCrafted
        {
            get { return _platesCrafted; }
            set { _platesCrafted = value; }
        }
        private static object _bodiesCraftedLock = new object();

        private static int _bodiesAssembled = 0;
        public static int BodiesAssembled
        {
            get { return _bodiesAssembled; }
            set { _bodiesAssembled = value; }
        }
        private static object _bodiesAssembledLock = new object();

        private static int _bodiesPainted = 0;
        public static int BodiesPainted
        {
            get { return _bodiesPainted; }
            set { _bodiesPainted = value; }
        }
        private static object _bodiesPaintedLock = new object();

        private static int _jondasWithWheels = 0;
        public static int JondasWithWheels
        {
            get { return _jondasWithWheels; }
            set { _jondasWithWheels = value; }
        }
        private static object _jondasWithWheelsLock = new object();

        private static int _jondasDone = 0;
        public static int JondasDone
        {
            get { return _jondasDone; }
            set { _jondasDone = value; }
        }
        private static object _jondasDoneLock = new object();

        //counters to make sure the correct amount of each jonda is created
        private static int _carsCreated = 0;
        public static int CarsCreated
        {
            get { return _carsCreated; }
            set { _carsCreated = value; }
        }

        private static int _minisCreated = 0;
        public static int MinisCreated
        {
            get { return _minisCreated; }
            set { _minisCreated = value; }
        }

        private static int _maxisCreated = 0;
        public static int MaxisCreated
        {
            get { return _maxisCreated; }
            set { _maxisCreated = value; }
        }

        private static int _sportsCreated = 0;
        public static int SportsCreated
        {
            get { return _sportsCreated; }
            set { _sportsCreated = value; }
        }
        private static object _createCarTypeLock = new object();

        //random to color the bodies :)
        private static Random _rnd = new Random();
        private static ConsoleColor[] _colors = new ConsoleColor[]
        {
            ConsoleColor.Black, ConsoleColor.Blue, ConsoleColor.DarkRed, ConsoleColor.White, ConsoleColor.DarkGreen, ConsoleColor.DarkYellow
        };


        //threads 

        //Crafting bodies thread
        public static void BodyPlatesFactory()
        {
            //loop to run the factory line
            bool run = true;
            while (run == true)
            {
                //locks to make manipulating of data thread safe
                if (Monitor.TryEnter(_bodiesCraftedLock, -1))
                {
                    PlatesCrafted++;
                    Monitor.Exit(_bodiesCraftedLock);
                }

                //time this work takes
                Thread.Sleep(FactorySettings.BodyPlatesCraftingTime);

                //puts the plates onto the next conveyor
                if (Monitor.TryEnter(Conveyers.BodyPlatesConveyerLock, -1))
                {
                    Conveyers.BodyPlatesConveyer = new Body();
                    Monitor.Exit(Conveyers.BodyPlatesConveyerLock);
                }

                //checks if work is done for today
                if (Monitor.TryEnter(_bodiesCraftedLock, -1))
                {
                    if (PlatesCrafted == 250)
                    {
                        run = false;
                    }
                    Monitor.Exit(_bodiesCraftedLock);
                }
            }
        }

        //Assembling bodies thread
        public static void BodyAssemblingFactory()
        {
            //loop to run the factory line
            bool run = true;
            while (run == true)
            {
                //variable to check if factory line has a body to work on later
                Body body = null;

                //locks to make manipulating of data thread safe
                if (Monitor.TryEnter(_bodiesAssembledLock, -1))
                {
                    BodiesAssembled++;
                    Monitor.Exit(_bodiesAssembledLock);
                }

                //times this work takes
                Thread.Sleep(FactorySettings.BodyAssembleTime);

                //gets plates to assemble a body
                while (body == null)
                {
                    if (Monitor.TryEnter(Conveyers.BodyPlatesConveyerLock, -1))
                    {
                        body = Conveyers.BodyPlatesConveyer;
                        Monitor.Exit(Conveyers.BodyPlatesConveyerLock);
                    }

                    if (body == null)
                    {
                        Thread.Sleep(FactorySettings.NoWorkWaitTime);
                    }
                }

                //finds out what kind of jonda to create and creates it
                Jonda jonda = null;
                if (Monitor.TryEnter(_createCarTypeLock, -1))
                {
                    if (CarsCreated != 120)
                    {
                        jonda = new Car();
                        CarsCreated++;
                    }
                    else if (MinisCreated != 40)
                    {
                        jonda = new Mini();
                        MinisCreated++;
                    }
                    else if (MaxisCreated != 50)
                    {
                        jonda = new Maxi();
                        MaxisCreated++;
                    }
                    else if(SportsCreated != 40)
                    {
                        jonda = new Sport();
                        SportsCreated++;
                    }

                    jonda.Body = body;

                    Monitor.Exit(_createCarTypeLock);
                }

                //puts the assembled jonda onto the next conveyor
                if (Monitor.TryEnter(Conveyers.BodiesAssembledConveyerLock, -1))
                {
                    Conveyers.BodiesAssembledConveyer = jonda;
                    Monitor.Exit(Conveyers.BodiesAssembledConveyerLock);
                }

                //checks if work is done for today
                if (Monitor.TryEnter(_bodiesAssembledLock, -1))
                {
                    if (BodiesAssembled == 250)
                    {
                        run = false;
                    }
                    Monitor.Exit(_bodiesAssembledLock);
                }
            }
        }

        //Painting bodies thread
        public static void BodyPaintingFactory()
        {
            //loop to run the factory line
            bool run = true;
            while (run == true)
            {
                //variable to check if factory line has a body to work on later
                Jonda jonda = null;

                //locks to make manipulating of data thread safe
                if (Monitor.TryEnter(_bodiesPaintedLock, -1))
                {
                    BodiesPainted++;
                    Monitor.Exit(_bodiesPaintedLock);
                }

                //times this work takes
                Thread.Sleep(FactorySettings.BodyPaintingTime);

                //gets body to paint
                while (jonda == null)
                {
                    if (Monitor.TryEnter(Conveyers.BodiesAssembledConveyerLock, -1))
                    {
                        jonda = Conveyers.BodiesAssembledConveyer;
                        Monitor.Exit(Conveyers.BodiesAssembledConveyerLock);
                    }

                    if (jonda == null)
                    {
                        Thread.Sleep(FactorySettings.NoWorkWaitTime);
                    }
                }

                //random color for the car
                jonda.Body.Color = _colors[_rnd.Next(0, _colors.Length)];

                //puts the painted body on next conveyor
                if (Monitor.TryEnter(Conveyers.BodiesPaintedConveyerLock, -1))
                {
                    Conveyers.BodiesPaintedConveyer = jonda;
                    Monitor.Exit(Conveyers.BodiesPaintedConveyerLock);
                }

                //checks if work is done for today
                if (Monitor.TryEnter(_bodiesPaintedLock, -1))
                {
                    if (BodiesPainted == 250)
                    {
                        run = false;
                    }
                    Monitor.Exit(_bodiesPaintedLock);
                }
            }
        }

        //Adding wheels to jondas thread
        public static void AddWheelsFactory()
        {
            //loop to run the factory line
            bool run = true;
            while (run == true)
            {
                //variable to check if factory line has a jonda to work on later
                Jonda jonda = null;

                //locks to make manipulating of data thread safe
                if (Monitor.TryEnter(_jondasWithWheelsLock, -1))
                {
                    JondasWithWheels++;
                    Monitor.Exit(_jondasWithWheelsLock);
                }

                //times this work takes
                Thread.Sleep(FactorySettings.WheelAddingTime);

                //gets the jonda with body to put wheels on
                while (jonda == null)
                {
                    if (Monitor.TryEnter(Conveyers.BodiesPaintedConveyerLock, -1))
                    {
                        jonda = Conveyers.BodiesPaintedConveyer;
                        Monitor.Exit(Conveyers.BodiesPaintedConveyerLock);
                    }

                    if (jonda == null)
                    {
                        Thread.Sleep(FactorySettings.NoWorkWaitTime);
                    }
                }

                //puts the wheels on the jonda
                for (int i = 0; i < 4; i++)
                {
                    jonda.Wheels[i] = new Wheel();
                }

                //puts the jonda with body and wheels onto the next conveyor
                if (Monitor.TryEnter(Conveyers.MissingEngineJondaLock, -1))
                {
                    Conveyers.MissingEngineJondas = jonda;
                    Monitor.Exit(Conveyers.MissingEngineJondaLock);
                }

                //checks if work is done for today
                if (Monitor.TryEnter(_jondasWithWheelsLock, -1))
                {
                    if (JondasWithWheels == 250)
                    {
                        run = false;
                    }
                    Monitor.Exit(_jondasWithWheelsLock);
                }
            }
        }

        //adding engine to jondas thread
        //
        public static void AddEngineFactory()
        {
            bool run = true;
            while (run == true)
            {
                //variable to check if factory line has a jonda to work on later
                Jonda jonda = null;

                //locks to make manipulating of data thread safe
                if (Monitor.TryEnter(_jondasDoneLock , -1))
                {
                    JondasDone++;
                    Monitor.Exit(_jondasDoneLock);
                }

                //times this work takes
                Thread.Sleep(FactorySettings.EngineAddingTime);

                //gets the jonda with body to add engine to it
                while (jonda == null)
                {
                    if (Monitor.TryEnter(Conveyers.MissingEngineJondaLock, -1))
                    {
                        jonda = Conveyers.MissingEngineJondas;
                        Monitor.Exit(Conveyers.MissingEngineJondaLock);
                    }

                    if (jonda == null)
                    {
                        Thread.Sleep(FactorySettings.NoWorkWaitTime);
                    }
                }

                //puts the engine in the jonda
                jonda.Motor = new Motor();

                //puts the jonda with body, whells and engine finished conveyor
                if (Monitor.TryEnter(Conveyers.JondasDoneLock, -1))
                {
                    Conveyers.JondasDone = jonda;
                    Monitor.Exit(Conveyers.JondasDoneLock);
                }

                //checks if work is done for today
                if (Monitor.TryEnter(_jondasDoneLock, -1))
                {
                    if (JondasDone == 250)
                    {
                        run = false;
                    }
                    Monitor.Exit(_jondasDoneLock);
                }
            }
        }
    }
}
