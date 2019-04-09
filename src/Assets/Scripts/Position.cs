using System;

namespace Assets.Scripts
{
        public class Position
        {
                public int X { get; private set; }
                public int Y { get; private set; }

                public Position(int x, int y)
                {
                        X = x;
                        Y = y;
                }

                public override string ToString()
                {
                        return string.Format("X= {0} ; Y= {1} ;", X, Y); 
                }
        }
}