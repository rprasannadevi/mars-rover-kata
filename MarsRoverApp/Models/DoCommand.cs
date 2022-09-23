using System.Drawing;

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
            var sOutput = "";

            var GridMaxXPosition = Rover.GridMaxXPosition;
            var GridMaxYPosition = Rover.GridMaxYPosition;
            var GridStartXPosition = Rover.GridStartXPosition;
            var GridStartYPosition = Rover.GridStartYPosition;

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
                                    if (CurrentPoint.Y > GridMaxYPosition)
                                    {
                                        CurrentPoint.Y = GridMaxYPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo(), CurrentPoint))
                                    {
                                        CurrentPoint.Y -= 1;
                                        sOutput = "Cannot Move Rover Further. Because Obstacle is present over there.";
                                    }
                                    break;
                                }
                            case Directions.E:
                                {
                                    CurrentPoint.X += 1;
                                    if (CurrentPoint.X > GridMaxXPosition)
                                    {
                                        CurrentPoint.X = GridMaxXPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo(), CurrentPoint))
                                    {
                                        CurrentPoint.X -= 1;
                                        sOutput = "Cannot Move Rover Further. Because Obstacle is present over there.";
                                    }
                                    break;
                                }
                            case Directions.S:
                                {
                                    CurrentPoint.Y -= 1;
                                    if (CurrentPoint.Y < GridStartYPosition)
                                    {
                                        CurrentPoint.Y = GridStartYPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo(), CurrentPoint))
                                    {
                                        CurrentPoint.Y += 1;
                                        sOutput = "Cannot Move Rover Further. Because Obstacle is present over there.";
                                    }
                                    break;
                                }
                            case Directions.W:
                                {
                                    CurrentPoint.X -= 1;
                                    if (CurrentPoint.X < GridStartXPosition)
                                    {
                                        CurrentPoint.X = GridStartXPosition;
                                        sOutput = "Cannot Move Rover Further As it is Grid's Edge Position";
                                    }
                                    if (CheckForObstacles(Rover.ObstaclesInfo(), CurrentPoint))
                                    {
                                        CurrentPoint.X += 1;
                                        sOutput = "Cannot Move Rover Further. Because Obstacle is present over there.";
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
                             
    }
}