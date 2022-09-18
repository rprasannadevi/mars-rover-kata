using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverApp.Models
{
    public class DoCommand
    {
        /*public char Action;
        public void MoveRoverByAction(char A)
        {
            throw new NotImplementedException();
        }*/

        public Directions Directions;
        public Commands Commands;
        public bool CheckPosition(string strCurrentPosition, int GridMaxXPosition, int GridMaxYPosition)
        {
            if (String.IsNullOrEmpty(strCurrentPosition))
                return false;
            if (strCurrentPosition.Length != 3)
                return false;
            int xCo = Int32.Parse(strCurrentPosition.Substring(0, 1));
            int yCo = Int32.Parse(strCurrentPosition.Substring(1, 1));
            if (xCo > GridMaxXPosition || yCo > GridMaxYPosition)
                return false;
            if (!Enum.IsDefined(typeof(Directions), strCurrentPosition.Substring(2, 1)))
                return false;
            /*Console.WriteLine(strCurrentPosition[0]);
            Console.WriteLine(strCurrentPosition[1]);
            Console.WriteLine(strCurrentPosition[2]);*/
            return true;
        }

        public bool MoveRoverByCommands(string strCommands)
        {
            return true;
        }
        
    }
}