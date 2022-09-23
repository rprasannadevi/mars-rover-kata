using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.Models
{
    public class Rover : Plateau, IRover
    {
        public int CurrentXCoordinate { get; private set; }
        
        public int CurrentYCoordinate { get; private set; }
 
        public char CurrentDirection { get; private set; }

        public bool isCameraOn { get; private set; }

        public RoverArmActions RoverArmActions;
        private DoCommand _DoCommand = new();

        /// <summary>
        /// Constructor - sets all default values
        /// </summary>
        public Rover() : base()
        {
            CurrentXCoordinate = 0;
            CurrentYCoordinate = 0;
            CurrentDirection = 'N';
            isCameraOn = false;
        }

        /// <summary>
        /// Place the Rover in the Position Given after checking all the validations
        /// </summary>
        /// <param name="strCurrentPosition"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetRover(string strCurrentPosition)
        {
            if (String.IsNullOrEmpty(strCurrentPosition))
            {
                if(strCurrentPosition == null)
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

            if (xCo > GridMaxXPosition || yCo > GridMaxYPosition)
                throw new ArgumentException("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position");

            CurrentXCoordinate = xCo;
            CurrentYCoordinate = yCo;
            CurrentDirection = strCurrentPosition[2];
        }

        /// <summary>
        /// Move Rover - According to the Commands after checking all the validations.
        /// </summary>
        /// <param name="strCommands"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string MoveRover(string strCommands)
        {
            string strRoversNewPositionMessage = _DoCommand.DoMoveRover(strCommands, this);
            CurrentXCoordinate = Int32.Parse(strRoversNewPositionMessage.Substring(0, 1));
            CurrentYCoordinate = Int32.Parse(strRoversNewPositionMessage.Substring(1, 1));
            CurrentDirection = strRoversNewPositionMessage[2];
            if (strRoversNewPositionMessage.Length == 3)
                return strRoversNewPositionMessage.Substring(0,3);
            else
                throw new ArgumentException(strRoversNewPositionMessage.Substring(3));
        }

        /// <summary>
        /// It just turns on the Camera
        /// </summary>
        /// <returns>Success</returns>
        public string TakePicture()
        {
            isCameraOn = true;
            return "Success";
        }

        /// <summary>
        /// Gives list of actions to be executed to take sample from Surface.
        /// </summary>
        /// <returns>Success</returns>
        public string TakeSampleFromSurface()
        {
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
