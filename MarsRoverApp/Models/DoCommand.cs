using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarsRoverApp.Models
{
    public class DoCommand
    {
        public Directions Directions;
        public Commands Commands;
        public Directions CurrentFacingDirection;


        public bool CheckPosition(string strCurrentPosition, int GridMaxXPosition, int GridMaxYPosition)
        {
            if (String.IsNullOrEmpty(strCurrentPosition))
                return false;
            if (strCurrentPosition.Length != 3)
                return false;
            int xCo;
            int yCo;
            xCo = Int32.Parse(strCurrentPosition.Substring(0, 1));
            yCo = Int32.Parse(strCurrentPosition.Substring(1, 1));

            if (xCo > GridMaxXPosition || yCo > GridMaxYPosition)
                return false;
            if (!Enum.IsDefined(typeof(Directions), strCurrentPosition.Substring(2, 1)))
                return false;

            return true;
        }

        public string MoveRoverByAction(char Action, Point CurrentPoint, char CurrentDirection)
        {
            string sOutput = "";

            if (CurrentDirection == 'N')
                CurrentFacingDirection = Directions.N;
            else if (CurrentDirection == 'E')
                CurrentFacingDirection = Directions.E;
            else if (CurrentDirection == 'S')
                CurrentFacingDirection = Directions.S;
            else if (CurrentDirection == 'W')
                CurrentFacingDirection = Directions.W;

            if (Action == 'L')
            {
                switch(CurrentFacingDirection)
                {
                    case Directions.N:
                    {
                            CurrentDirection = 'W';
                            break;
                    }
                    case Directions.E:
                    {
                            CurrentDirection = 'N';
                            break;
                    }
                    case Directions.S:
                    {
                            CurrentDirection = 'E';
                            break;
                    }
                    case Directions.W:
                    {
                            CurrentDirection = 'S';
                            break;
                    }
                }
            }
            else if(Action == 'R')
            {
                switch (CurrentFacingDirection)
                {
                    case Directions.N:
                        {
                            CurrentDirection = 'E';
                            break;
                        }
                    case Directions.E:
                        {
                            CurrentDirection = 'S';
                            break;
                        }
                    case Directions.S:
                        {
                            CurrentDirection = 'W';
                            break;
                        }
                    case Directions.W:
                        {
                            CurrentDirection = 'N';
                            break;
                        }
                }
            }
            else if(Action == 'M')
            {
                switch (CurrentFacingDirection)
                {
                    case Directions.N:
                        {
                            CurrentPoint.Y += 1;
                            break;
                        }
                    case Directions.E:
                        {
                            CurrentPoint.X += 1;
                            break;
                        }
                    case Directions.S:
                        {
                            CurrentPoint.Y -= 1;
                            break;
                        }
                    case Directions.W:
                        {
                            CurrentPoint.X -= 1;
                            break;
                        }
                }
            }
            sOutput = CurrentPoint.X.ToString() + CurrentPoint.Y.ToString() + Char.ToString(CurrentDirection);
            //Console.WriteLine(sOutput);
            return sOutput;
        }


    }
}