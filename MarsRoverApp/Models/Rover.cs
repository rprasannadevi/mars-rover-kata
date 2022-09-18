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

        public Rover() 
        {
            CurrentXCoordinate = 0;
            CurrentYCoordinate = 0;
            CurrentDirection = 'N';
        }

        public DoCommand DoCommand = new DoCommand();

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
            throw new System.NotImplementedException();
        }
    }
}
