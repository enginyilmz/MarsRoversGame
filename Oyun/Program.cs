using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Oyun
{
    class Program
    {
        static void Main(string[] args)
        {
            var marsRover = new MarsRover(5,5);
            marsRover.AddRover(1,2,'N');
            marsRover.SendCommand("LMLMLMLMM");            
            marsRover.AddRover(3, 3, 'E');
            marsRover.SendCommand("MMRMMRMRRM");
            marsRover.GetFinalPositions();
        }
    }

    class MarsRover
    {
        private int X { get; set; }
        private int Y { get; set; }
        private char Direction { get; set; }

        private int XMax { get; set; }
        private int YMax { get; set; }

        private readonly Dictionary<char, int> _directions = new Dictionary<char, int>
        {
            {'N', 1},
            {'E', 2},
            {'S', 3},
            {'W', 4}
        };

        private List<string> Results { get; set; }

        public  MarsRover(int xMax, int yMax)
        {
            XMax = xMax;
            YMax = yMax;
            Results= new List<string>();
        }
        
        public void AddRover(int x, int y, char direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public void SendCommand(string directions)
        {
            char[] array = directions.ToCharArray();

            foreach (var direction in array)
            {
                var oldValue = this._directions.First(_ => _.Key == Direction).Value;
                int newValue = 0;

                switch (direction)
                {
                    case 'L':
                        newValue = oldValue == 1 ? 4 : oldValue - 1;
                        break;
                    case 'R':
                        newValue = oldValue == 4 ? 1 : oldValue + 1;
                        break;
                    case 'M':
                        {
                            switch (this.Direction)
                            {
                                case 'N':
                                    Y = (Y + 1) > YMax ? Y : (Y + 1);
                                    break;
                                case 'E':
                                    X = (X + 1) > XMax ? X : (X + 1);
                                    break;
                                case 'S':
                                    Y = (Y - 1) < 0 ? Y : (Y - 1);
                                    break;
                                case 'W':
                                    X = (X - 1) < 0 ? X : (X - 1);
                                    break;
                            }
                        }
                        break;
                }

                if(newValue!=0)
                Direction = _directions.First(_ => _.Value == newValue).Key;
            }

            var result = string.Format("{0} {1} {2}", X, Y, Direction);
            Results.Add(result);
        }

        public void GetFinalPositions()
        {
            foreach (var result in Results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
