using System;
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

        public void SetRover(string strCurrentPosition)
        {
            _DoCommand.SetGridSize(_GridStartXPosition, _GridStartYPosition, GridMaxXPosition, GridMaxYPosition);
            if (_DoCommand.CheckPosition(strCurrentPosition))
            {
                CurrentXCoordinate = Int32.Parse(strCurrentPosition.Substring(0, 1));
                CurrentYCoordinate = Int32.Parse(strCurrentPosition.Substring(1, 1));
                CurrentDirection = strCurrentPosition[2];
            }
        }

        public void MoveRover(string strCommands)
        {
            string strRoversNewPosition = _DoCommand.MoveRovers(strCommands, this);
            CurrentXCoordinate = Int32.Parse(strRoversNewPosition.Substring(0, 1));
            CurrentYCoordinate = Int32.Parse(strRoversNewPosition.Substring(1, 1));
            CurrentDirection = strRoversNewPosition[2];
        }

        /*public void MoveRover(string strCommands)
        {
            Point CurrentPoint = new Point();

            CurrentPoint.X = CurrentXCoordinate;
            CurrentPoint.Y = CurrentYCoordinate;
            char CurrentFacingDirection = CurrentDirection;

            var strOutput = "";

            foreach (var sCommand in strCommands)
            {
                if (Enum.IsDefined(typeof(Commands), char.ToString(sCommand)))
                {
                    strOutput = DoCommand.MoveRoverByAction(sCommand, CurrentPoint, CurrentFacingDirection);
                    CurrentPoint.X = Int32.Parse(strOutput.Substring(0, 1));
                    CurrentPoint.Y = Int32.Parse(strOutput.Substring(1, 1));
                    CurrentFacingDirection = strOutput[2];
                }
                else
                {
                    CurrentPoint.X = CurrentXCoordinate;
                    CurrentPoint.Y = CurrentYCoordinate;
                    CurrentFacingDirection = CurrentDirection;
                    break;
                }
            }
            CurrentXCoordinate = CurrentPoint.X;
            CurrentYCoordinate = CurrentPoint.Y;
            CurrentDirection = CurrentFacingDirection;
        }*/

        public string RoverPosition()
        {
            return CurrentXCoordinate.ToString() + CurrentYCoordinate.ToString() + Char.ToString(CurrentDirection); ;
        }
    }
}
