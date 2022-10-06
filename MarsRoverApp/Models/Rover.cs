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
    public class Rover : IRover
    {
        public int CurrentXCoordinate { get; set; }
        
        public int CurrentYCoordinate { get; set; }

        public char CurrentDirection { get; set; }

        public bool IsCameraOn { get; set; }
        public bool EnableArmActions { get; set; }
        public string Name { get; private set; }
        public void SetName(string sName)
        {
            Name = sName;
        }

        /// <summary>
        /// Constructor - sets all default values
        /// </summary>
        public Rover() : base()
        {
            CurrentXCoordinate = 0;
            CurrentYCoordinate = 0;
            CurrentDirection = 'N';
            IsCameraOn = false;
            EnableArmActions = false;
            Name = "";
        }
    }
}
