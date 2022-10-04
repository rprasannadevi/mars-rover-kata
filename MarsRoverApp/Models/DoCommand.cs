using System.Collections;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace MarsRoverApp.Models
{
    public class DoCommand
    {
        public Directions Directions;
        public Commands Commands;
        public Directions CurrentFacingDirection;

        /// <summary>
        /// It takes Commands to Move and Rover as inputs and move the Rover to the New Position. It checks for any 
        /// Obstacles present and do the actions according to that
        /// </summary>
        /// <param name="strCommands"></param>
        /// <param name="Rover"></param>
        /// <returns>The New Position of the Rover after Movement</returns>
        public string DoMoveRover(string strCommands, Rover Rover)
        {
            Point CurrentPoint = new Point();
            var strErrorMessage = "";

            var GridMaxXPosition = Rover.GridMaxXPosition;
            var GridMaxYPosition = Rover.GridMaxYPosition;
            var GridStartXPosition = Rover.GridStartXPosition;
            var GridStartYPosition = Rover.GridStartYPosition;

            CurrentPoint.X = Rover.CurrentXCoordinate;
            CurrentPoint.Y = Rover.CurrentYCoordinate;
            char CurrentDirection = Rover.CurrentDirection;

            var strRoverName = "";

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

                    if (Action == 'L')   //Rotate 90 degree Left 
                        CurrentDirection = SpinLeft(CurrentFacingDirection);
                    else if (Action == 'R')    //Rotate 90 degree Right
                        CurrentDirection = SpinRight(CurrentFacingDirection);
                    else   //Move Based on the Command and do Validation
                    {
                        switch (CurrentFacingDirection)
                        {
                            case Directions.N:
                                {
                                    CurrentPoint.Y += 1;
                                    
                                    if (CurrentPoint.Y > GridMaxYPosition)
                                    {
                                        CurrentPoint.Y = GridMaxYPosition;
                                        strErrorMessage = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    strRoverName = CheckCollisionWithOtherRovers(Rover.RoverPresentPoints, CurrentPoint, Rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.Y -= 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo, CurrentPoint))
                                    {
                                        CurrentPoint.Y -= 1;
                                        strErrorMessage = "Cannot Move Rover Further. Because Obstacle is present over there.";
                                    }
                                    break;
                                }
                            case Directions.E:
                                {
                                    CurrentPoint.X += 1;
                                    
                                    if (CurrentPoint.X > GridMaxXPosition)
                                    {
                                        CurrentPoint.X = GridMaxXPosition;
                                        strErrorMessage = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    strRoverName = CheckCollisionWithOtherRovers(Rover.RoverPresentPoints, CurrentPoint, Rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.X -= 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo, CurrentPoint))
                                    {
                                        CurrentPoint.X -= 1;
                                        strErrorMessage = "Cannot Move Rover Further. Because Obstacle is present over there.";
                                    }
                                    break;
                                }
                            case Directions.S:
                                {
                                    CurrentPoint.Y -= 1;
                                    
                                    if (CurrentPoint.Y < GridStartYPosition)
                                    {
                                        CurrentPoint.Y = GridStartYPosition;
                                        strErrorMessage = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    strRoverName = CheckCollisionWithOtherRovers(Rover.RoverPresentPoints, CurrentPoint, Rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.Y += 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo, CurrentPoint))
                                    {
                                        CurrentPoint.Y += 1;
                                        strErrorMessage = "Cannot Move Rover Further. Because Obstacle is present over there.";
                                    }
                                    break;
                                }
                            case Directions.W:
                                {
                                    CurrentPoint.X -= 1;
                                    
                                    if (CurrentPoint.X < GridStartXPosition)
                                    {
                                        CurrentPoint.X = GridStartXPosition;
                                        strErrorMessage = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    strRoverName = CheckCollisionWithOtherRovers(Rover.RoverPresentPoints, CurrentPoint, Rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.X += 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo, CurrentPoint))
                                    {
                                        CurrentPoint.X += 1;
                                        strErrorMessage = "Cannot Move Rover Further. Because Obstacle is present over there.";
                                    }
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    CurrentPoint.X = Rover.CurrentXCoordinate;
                    CurrentPoint.Y = Rover.CurrentYCoordinate;
                    CurrentDirection = Rover.CurrentDirection;
                    strErrorMessage = "Move Rover is not Successful. The valid Commands are: L,R and M";
                    break;
                }
            }

            //Rover Hashtable - Rover and its co-ordinates will be added/updated here for checking collision.
            if (Rover.RoverPresentPoints.ContainsKey(Rover.Name))
                Rover.RoverPresentPoints[Rover.Name] = CurrentPoint.X.ToString() + CurrentPoint.Y.ToString();
            else
                Rover.RoverPresentPoints.Add(Rover.Name, CurrentPoint.X.ToString() + CurrentPoint.Y.ToString());

            if (strErrorMessage == "")
                return CurrentPoint.X.ToString() + CurrentPoint.Y.ToString() + Char.ToString(CurrentDirection);
            else
            {
                strErrorMessage = CurrentPoint.X.ToString() + CurrentPoint.Y.ToString() + Char.ToString(CurrentDirection) + strErrorMessage;
                return strErrorMessage;
            }
        }

        public char SpinLeft(Directions CurrentFacingDirection)
        {
            char CurrentDirection = ' ';
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
            return CurrentDirection;
        }

        public char SpinRight(Directions CurrentFacingDirection)
        {
            char CurrentDirection = ' ';
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
            return CurrentDirection;
        }

        /// <summary>
        /// It checks for any Obstacle is present in the CurrentPoint to stop moving to this point
        /// </summary>
        /// <param name="ObstaclePoints"></param>
        /// <param name="CurrentPoint"></param>
        /// <returns>bool</returns>
        public bool CheckForObstacles(List<Point> ObstaclePoints,Point CurrentPoint)
        {
            foreach (var oPoint in ObstaclePoints)
            {
                if (oPoint == CurrentPoint)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// It Checks is there any entry for Rovers in the Hashtable with name as Key and returns true if it finds match 
        /// </summary>
        /// <param name="RoverStandingPoints"></param>
        /// <param name="CurrentPoint"></param>
        /// <param name="RoverName"></param>
        /// <returns></returns>
        public string? CheckCollisionWithOtherRovers(Hashtable RoverStandingPoints, Point CurrentPoint, string RoverName)
        {
            string strCurrentPoint = CurrentPoint.X.ToString() + CurrentPoint.Y.ToString();
            foreach (DictionaryEntry oPoint in RoverStandingPoints)
            {
                if (oPoint.Key.ToString() != RoverName)
                {
                    if (oPoint.Value!.ToString() == strCurrentPoint)
                        return oPoint.Key.ToString();
                }
            }
            return "";
        }

}
}