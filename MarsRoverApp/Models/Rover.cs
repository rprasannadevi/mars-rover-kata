using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.Models
{
    public class Rover : IRover
    {
        public int CurrentXCoordinate { get; protected set; }
        
        public int CurrentYCoordinate { get; protected set; }
 
        public char CurrentDirection { get; protected set; }

        public List<Point> AssignedPoints = new List<Point>();
        
        public List<Point> GetAssignedPoints()
        {
            var AssignedPointsList = new List<Point>();
            return AssignedPointsList;
        }

        public DoCommand DoCommand = new();
        public Rover() 
        {
            CurrentXCoordinate = 0;
            CurrentYCoordinate = 0;
            CurrentDirection = 'N';
        }

        public void SetRover(string strCurrentPosition, int GridMaxXPosition, int GridMaxYPosition)
        {
            if (DoCommand.CheckPosition(strCurrentPosition, GridMaxXPosition, GridMaxYPosition))
            {
                CurrentXCoordinate = Int32.Parse(strCurrentPosition.Substring(0, 1));
                CurrentYCoordinate = Int32.Parse(strCurrentPosition.Substring(1, 1));
                CurrentDirection = strCurrentPosition[2];
            }
        }
        public void MoveRover(string strCommands)
        {
            Point CurrentPoint = new Point();

            CurrentPoint.X = CurrentXCoordinate;
            CurrentPoint.Y = CurrentYCoordinate;

            var strOutput = "";

            foreach (var sCommand in strCommands)
            {
                if (Enum.IsDefined(typeof(Commands), char.ToString(sCommand)))
                {
                    strOutput = DoCommand.MoveRoverByAction(sCommand, CurrentPoint, CurrentDirection);
                    CurrentPoint.X = Int32.Parse(strOutput.Substring(0, 1));
                    CurrentPoint.Y = Int32.Parse(strOutput.Substring(1, 1));
                    CurrentDirection = strOutput[2];
                }
            }
            CurrentXCoordinate = CurrentPoint.X;
            CurrentYCoordinate = CurrentPoint.Y;
            CurrentDirection = strOutput[2];
        }

        public string RoverPosition()
        {
            return CurrentXCoordinate.ToString() + CurrentYCoordinate.ToString() + Char.ToString(CurrentDirection); ;
        }
    }
}
