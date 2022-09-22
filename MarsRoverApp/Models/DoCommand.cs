using System.Drawing;

namespace MarsRoverApp.Models
{
    public class DoCommand
    {
        public Directions Directions;
        public Commands Commands;
        public Directions CurrentFacingDirection;

        private int _GridMaxXPosition { get; set; }
        private int _GridMaxYPosition { get; set; }
        private int _GridStartXPosition { get; set; }
        private int _GridStartYPosition { get; set; }

        public void SetGridSize(int startX, int startY, int MaxX, int MaxY)
        {
            _GridStartXPosition = startX;
            _GridStartYPosition = startY;
            _GridMaxXPosition = MaxX;
            _GridMaxYPosition = MaxY;
        }

        public string MoveRovers(string strCommands, Rover Rover)
        {
            Point CurrentPoint = new Point();
            var sOutput = "";

            CurrentPoint.X = Rover.CurrentXCoordinate;
            CurrentPoint.Y = Rover.CurrentYCoordinate;
            char CurrentDirection = Rover.CurrentDirection;

            foreach (var Action in strCommands)
            {
                if (Enum.IsDefined(typeof(Commands), char.ToString(Action)))
                {
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
                        switch (CurrentFacingDirection)
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
                    else if (Action == 'R')
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
                    else
                    {
                        switch (CurrentFacingDirection)
                        {
                            case Directions.N:
                                {
                                    CurrentPoint.Y += 1;
                                    if (CurrentPoint.Y > _GridMaxYPosition)
                                    {
                                        CurrentPoint.Y = _GridMaxYPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    break;
                                }
                            case Directions.E:
                                {
                                    CurrentPoint.X += 1;
                                    if (CurrentPoint.X > _GridMaxXPosition)
                                    {
                                        CurrentPoint.X = _GridMaxXPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    break;
                                }
                            case Directions.S:
                                {
                                    CurrentPoint.Y -= 1;
                                    if (CurrentPoint.Y < _GridStartYPosition)
                                    {
                                        CurrentPoint.Y = _GridStartYPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    break;
                                }
                            case Directions.W:
                                {
                                    CurrentPoint.X -= 1;
                                    if (CurrentPoint.X < _GridStartXPosition)
                                    {
                                        CurrentPoint.X = _GridStartXPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    break;
                                }
                        }
                        foreach(var oPoint in  Rover.ObstaclesInfo())
                        {
                            if (oPoint == CurrentPoint)
                            {
                                CurrentPoint.X = Rover.CurrentXCoordinate;
                                CurrentPoint.Y = Rover.CurrentYCoordinate;
                                CurrentDirection = Rover.CurrentDirection;
                                sOutput = "Cannot Move Rover. Because Obstacle is present over there.";
                            }
                            break;
                        }
                    }
                }
                else
                {
                    CurrentPoint.X = Rover.CurrentXCoordinate;
                    CurrentPoint.Y = Rover.CurrentYCoordinate;
                    CurrentDirection = Rover.CurrentDirection;
                    sOutput = "Move Rover is not Successful. The valid Commands are: L,R and M";
                    break;
                }
            }

            if (sOutput == "")
                return CurrentPoint.X.ToString() + CurrentPoint.Y.ToString() + Char.ToString(CurrentDirection);
            else
            {
                sOutput = CurrentPoint.X.ToString() + CurrentPoint.Y.ToString() + Char.ToString(CurrentDirection) + sOutput;
                return sOutput;
            }
        }

        public bool CheckAssignedPortsAndObstacles(Point Point)
        {
            
            return true;
        }


        /*public string CheckPosition(string strCurrentPosition)
        {
            if (String.IsNullOrEmpty(strCurrentPosition))
                return "";

            if (strCurrentPosition.Length != 3)
                return "Invalid Start Position for Rover - The Correct Input Format is 'XXX'";

            if (!Enum.IsDefined(typeof(Directions), strCurrentPosition.Substring(2, 1)))
                return "Invalid Start Position for Rover - The Correct Directions are 'N,E,S,W";

            int xCo;
            int yCo;
            xCo = Int32.Parse(strCurrentPosition.Substring(0, 1));
            yCo = Int32.Parse(strCurrentPosition.Substring(1, 1));
            if (xCo > _GridMaxXPosition || yCo > _GridMaxYPosition)
                return "Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position";

            return strCurrentPosition;
        }*/

        /*public string MoveRoverByAction(char Action, Point CurrentPoint, char CurrentDirection)
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
        }*/


    }
}