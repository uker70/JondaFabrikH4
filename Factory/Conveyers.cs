using System.Collections.Generic;
using JondaFabrikPrøveH4.Class;

namespace JondaFabrikPrøveH4.Factory
{
    class Conveyers
    {
        //this is boilerplate code on how to interact with the conveyers in the factory, so i will be describing one, since they are all the same

        //the private list for a conveyor
        private static List<Body> _bodyPlatesConveyer = new List<Body>();
        //the public conveyor
        public static Body BodyPlatesConveyer
        {
            //when you want to get a part from a conveyor, you can only get the first one, or a null if its empty
            get{
                if (_bodyPlatesConveyer.Count != 0)
                {
                    Body body = _bodyPlatesConveyer[0];
                    _bodyPlatesConveyer.RemoveAt(0);
                    return body;
                }
                else
                {
                    return null;
                }
            }
            //when you want to add something to the conveyor, you can only add 1 at a time
            set{ _bodyPlatesConveyer.Add(value);}
        }
        //the lock to make sure that there is data integrity
        public static object BodyPlatesConveyerLock = new object();


        private static List<Jonda> _bodiesAssembledConveyer = new List<Jonda>();
        public static Jonda BodiesAssembledConveyer
        {
            get
            {
                if (_bodiesAssembledConveyer.Count != 0)
                {
                    Jonda jonda = _bodiesAssembledConveyer[0];
                    _bodiesAssembledConveyer.RemoveAt(0);
                    return jonda;
                }
                else
                {
                    return null;
                }
            }
            set { _bodiesAssembledConveyer.Add(value); }
        }
        public static object BodiesAssembledConveyerLock = new object();


        private static List<Jonda> _bodiesPaintedConveyer = new List<Jonda>();
        public static Jonda BodiesPaintedConveyer
        {
            get
            {
                if (_bodiesPaintedConveyer.Count != 0)
                {
                    Jonda jondaWithBody = _bodiesPaintedConveyer[0];
                    _bodiesPaintedConveyer.RemoveAt(0);
                    return jondaWithBody;
                }
                else
                {
                    return null;
                }
            }
            set { _bodiesPaintedConveyer.Add(value); }
        }
        public static object BodiesPaintedConveyerLock = new object();


        private static List<Jonda> _missingEngineJondas = new List<Jonda>();
        public static Jonda MissingEngineJondas
        {
            get
            {
                if (_missingEngineJondas.Count != 0)
                {
                    Jonda jonda = _missingEngineJondas[0];
                    _missingEngineJondas.RemoveAt(0);
                    return jonda;
                }
                else
                {
                    return null;
                }
            }
            set { _missingEngineJondas.Add(value); }
        }
        public static object MissingEngineJondaLock = new object();


        private static List<Jonda> _jondasDone = new List<Jonda>();
        public static Jonda JondasDone
        {
            get
            {
                if (_jondasDone.Count != 0)
                {
                    Jonda jonda = _jondasDone[0];
                    _jondasDone.RemoveAt(0);
                    return jonda;
                }
                else
                {
                    return null;
                }
            }
            set { _jondasDone.Add(value); }
        }
        public static object JondasDoneLock = new object();

        //gets the complete list of completed jondas, since there is 1 case where this is needed
        public static List<Jonda> GetCompletedJondas()
        {
            return _jondasDone;
        }

        //empties the list of completed jodans, incase you want to implement a "day" system, so the program can run multiple times
        public static void ClearEmptyJondas()
        {
            _jondasDone.Clear();
        }
    }
}
