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

        private DoCommand _DoCommand = new();

        public Rover() : base()
        {
            CurrentXCoordinate = 0;
            CurrentYCoordinate = 0;
            CurrentDirection = 'N';
            _GridStartXPosition = 0;
            _GridStartYPosition = 0;
        }

        public string SetRover(string strCurrentPosition)
        {
            _DoCommand.SetGridSize(_GridStartXPosition, _GridStartYPosition, GridMaxXPosition, GridMaxYPosition);
            if (String.IsNullOrEmpty(strCurrentPosition))
                return "";

            if (strCurrentPosition.Length != 3)
                return "Invalid Start Position for Rover - The Correct Input Format is 'XXX'";

            if (!Enum.IsDefined(typeof(Directions), strCurrentPosition.Substring(2, 1)))
                return "Invalid Start Position for Rover - The Correct Directions are 'N,E,S,W";

            var xCo = Int32.Parse(strCurrentPosition.Substring(0, 1));
            var yCo = Int32.Parse(strCurrentPosition.Substring(1, 1));

            if (xCo > GridMaxXPosition || yCo > GridMaxYPosition)
                return "Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position";

            CurrentXCoordinate = xCo;
            CurrentYCoordinate = yCo;
            CurrentDirection = strCurrentPosition[2];
            return strCurrentPosition;
        }

        public string MoveRover(string strCommands)
        {
            string strRoversNewPositionMessage = _DoCommand.MoveRovers(strCommands, this);
            CurrentXCoordinate = Int32.Parse(strRoversNewPositionMessage.Substring(0, 1));
            CurrentYCoordinate = Int32.Parse(strRoversNewPositionMessage.Substring(1, 1));
            CurrentDirection = strRoversNewPositionMessage[2];
            if (strRoversNewPositionMessage.Length == 3)
                return "Success";
            else
                 return strRoversNewPositionMessage.Substring(3);
        }

        public string TakePicture()
        {
            return "Success";
        }

        public string TakeSampleFromSurface()
        {
            return "Success";
        }
    }
}
