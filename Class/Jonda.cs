namespace JondaFabrikPrøveH4.Class
{
    abstract class Jonda
    {
        private Body _body;
        public Body Body
        {
            get { return _body; }
            set { _body = value; }
        }

        //an array of 4 wheels, because there is 4 wheels on a car :)
        private Wheel[] _wheels = new Wheel[4];
        public Wheel[] Wheels
        {
            get { return _wheels; }
            set { _wheels = value; }
        }

        private Motor _motor;
        public Motor Motor
        {
            get { return _motor; }
            set { _motor = value; }
        }
    }
}
