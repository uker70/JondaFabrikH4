namespace JondaFabrikPrøveH4
{
    class FactorySettings
    {
        #region Factory Time Settings
        //the current time settings means that 24 hours = 2min 24sec
        //calculation is done in seconds
        //x * x = day
        //12 * 12 = 144 which is a day in these settings
        public const int TimeInADay = BodyPlatesCraftingTime*12;

        //the time it takes to create a set of plates for a car
        public const int BodyPlatesCraftingTime = 12000;

        //the time it takes to assembled plates into a car body
        public const int BodyAssembleTime = 8000;

        //the time it takes to paint a body
        public const int BodyPaintingTime = 4000;

        //the time it takes to add wheels to a jonda
        public const int WheelAddingTime = 1500;

        //the times it takes to add engine to a jonda
        public const int EngineAddingTime = 1000;

        //the time that a crew waits, if there is no work to be done
        public const int NoWorkWaitTime = 100;
        #endregion

        #region Factory Teams Settings
        //amount of plate crafting threads
        public const int PlateCraftingTeams = 32;

        //amount of body assembling threads
        public const int BodyAssemblingTeams = 20;

        //amount of body painting threads
        public const int BodyPaintingTeams = 12;

        //amount of wheel adding threads
        public const int WheelAddingTeams = 6;

        //amount of engine adding threads
        public const int EngineAddingTeams = 4;
        #endregion
    }
}
