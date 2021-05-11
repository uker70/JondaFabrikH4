using System.Collections.Generic;
using JondaFabrikPrøveH4.Class;

namespace JondaFabrikPrøveH4.Factory
{
    class Report
    {
        //contains all the jondas created each day, if the program can do multiple days
        private static List<List<Jonda>> _dailyFactoryReport = new List<List<Jonda>>();
        public static List<List<Jonda>> DailyFactoryReport
        {
            get { return _dailyFactoryReport; }
            set { _dailyFactoryReport = value; }
        }
    }
}
