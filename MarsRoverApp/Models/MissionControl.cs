using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace MarsRoverApp.Models
{
    public class MissionControl
    {
        private readonly IPlateau _plateau;
        private Directions CurrentFacingDirection;
        private static Hashtable RoverPresentPoints = new Hashtable();

        public MissionControl(IPlateau plateau)
        {
            _plateau = plateau;
        }


        /// <summary>
        /// Place the Rover in the Position Given after checking all the validations
        /// </summary>
        /// <param name="strCurrentPosition"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetRover(Rover _rover, string? strCurrentPosition)
        {
            if (String.IsNullOrEmpty(strCurrentPosition))
            {
                if (strCurrentPosition == null)
                    throw new ArgumentException("Invalid Start Position for Rover. The Input is NULL");
                else
                    throw new ArgumentException("Invalid Start Position for Rover. The Input is Empty");
            }

            if (strCurrentPosition.Length != 3)
                throw new ArgumentException("Invalid Start Position for Rover - The Correct Input Format is 'XXX'");

            if (!Enum.IsDefined(typeof(Directions), strCurrentPosition.Substring(2, 1)))
                throw new ArgumentException("Invalid Start Position for Rover - The Correct Directions are 'N,E,S,W");

            var xCo = Int32.Parse(strCurrentPosition.Substring(0, 1));
            var yCo = Int32.Parse(strCurrentPosition.Substring(1, 1));

            if (xCo > _plateau.GridMaxXPosition || yCo > _plateau.GridMaxYPosition)
                throw new ArgumentException("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position");

            _rover.CurrentXCoordinate = xCo;
            _rover.CurrentYCoordinate = yCo;
            _rover.CurrentDirection = strCurrentPosition[2];
        }

        /// <summary>
        /// It takes Commands to Move and Rover as inputs and move the Rover to the New Position. It checks for any 
        /// Obstacles present and do the actions according to that
        /// </summary>
        /// <param name="strCommands"></param>
        /// <param name="Rover"></param>
        /// <returns>The New Position of the Rover after Movement</returns>
        public string MoveRover(string strCommands, Rover _rover)
        {
            Point CurrentPoint = new Point();
            var strErrorMessage = "";

            var GridMaxXPosition = _plateau.GridMaxXPosition;
            var GridMaxYPosition = _plateau.GridMaxYPosition;
            var GridStartXPosition = _plateau.GridStartXPosition;
            var GridStartYPosition = _plateau.GridStartYPosition;

            CurrentPoint.X = _rover.CurrentXCoordinate;
            CurrentPoint.Y = _rover.CurrentYCoordinate;
            char CurrentDirection = _rover.CurrentDirection;

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
                                    strRoverName = CheckCollisionWithOtherRovers(RoverPresentPoints, CurrentPoint, _rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.Y -= 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(ObstaclesInfo(), CurrentPoint))
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
                                    strRoverName = CheckCollisionWithOtherRovers(RoverPresentPoints, CurrentPoint, _rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.X -= 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(ObstaclesInfo(), CurrentPoint))
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
                                    strRoverName = CheckCollisionWithOtherRovers(RoverPresentPoints, CurrentPoint, _rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.Y += 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(ObstaclesInfo(), CurrentPoint))
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
                                    strRoverName = CheckCollisionWithOtherRovers(RoverPresentPoints, CurrentPoint, _rover.Name);
                                    if (strRoverName != "")
                                    {
                                        CurrentPoint.X += 1;
                                        strErrorMessage = "Cannot Move Rover Further.Rover " + strRoverName + " is standing over there.";
                                    }
                                    if (CheckForObstacles(ObstaclesInfo(), CurrentPoint))
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
                    CurrentPoint.X = _rover.CurrentXCoordinate;
                    CurrentPoint.Y = _rover.CurrentYCoordinate;
                    CurrentDirection = _rover.CurrentDirection;
                    strErrorMessage = "Move Rover is not Successful. The valid Commands are: L,R and M";
                    break;
                }
            }

            _rover.CurrentXCoordinate = CurrentPoint.X;
            _rover.CurrentYCoordinate = CurrentPoint.Y;
            _rover.CurrentDirection = CurrentDirection;

            //Rover Hashtable - Rover and its co-ordinates will be added/updated here for checking collision.
            if (RoverPresentPoints.ContainsKey(_rover.Name))
                RoverPresentPoints[_rover.Name] = CurrentPoint.X.ToString() + CurrentPoint.Y.ToString();
            else
                RoverPresentPoints.Add(_rover.Name, CurrentPoint.X.ToString() + CurrentPoint.Y.ToString());

            if (strErrorMessage == "")
                return CurrentPoint.X.ToString() + CurrentPoint.Y.ToString() + Char.ToString(CurrentDirection);
            else
                throw new ArgumentException(strErrorMessage);
        }

        /// <summary>
        /// Rotate the Direction to Left of the Rover by 90 degree
        /// </summary>
        /// <param name="CurrentFacingDirection"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Rotate the Direction to Right of the Rover by 90 degree
        /// </summary>
        /// <param name="CurrentFacingDirection"></param>
        /// <returns></returns>
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
        /// The List of Obstacle Points will be added
        /// </summary>
        /// <returns>List<Point></returns>
        public List<Point> ObstaclesInfo()
        {
            var ObstaclePoints = new List<Point>();
            Point ObstaclePoint = new Point();
            ObstaclePoint.X = 4;
            ObstaclePoint.Y = 4;
            ObstaclePoints.Add(ObstaclePoint);
            ObstaclePoint.X = 3;
            ObstaclePoint.Y = 5;
            ObstaclePoints.Add(ObstaclePoint);
            return ObstaclePoints;
        }

        /// <summary>
        /// It checks for any Obstacle is present in the CurrentPoint to stop moving to this point
        /// </summary>
        /// <param name="ObstaclePoints"></param>
        /// <param name="CurrentPoint"></param>
        /// <returns>bool</returns>
        public bool CheckForObstacles(List<Point> ObstaclePoints, Point CurrentPoint)
        {
            //Console.WriteLine("Inside CheckForObstacles");
            foreach (var oPoint in ObstaclePoints)
            {
                //Console.WriteLine($"X {oPoint.X}, Y {oPoint.Y}");
                //Console.WriteLine("Inside CheckForObstacles");
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

        /// <summary>
        /// It just turns on the Camera 
        /// </summary>
        /// <returns>Success</returns>
        public string TakePicture(Rover _rover)
        {
            _rover.IsCameraOn = true;
            return "Success";
        }

        /// <summary>
        /// Gives list of actions to be executed to take sample from Surface.
        /// </summary>
        /// <returns>Success</returns>
        public string TakeSampleFromSurface(Rover _rover)
        {
            _rover.EnableArmActions = true;
            var ArmActions = new List<string>();
            ArmActions.Add(RoverArmActions.Up.ToString());
            ArmActions.Add(RoverArmActions.MoveForward.ToString());
            ArmActions.Add(RoverArmActions.BendForward.ToString());
            ArmActions.Add(RoverArmActions.Take.ToString());
            ArmActions.Add(RoverArmActions.Keep.ToString());
            ArmActions.Add(RoverArmActions.Put.ToString());
            ArmActions.Add(RoverArmActions.Down.ToString());
            return "Success";
        }
    }
}
