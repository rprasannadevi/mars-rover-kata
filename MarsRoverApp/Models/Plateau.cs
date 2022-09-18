using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.Models
{
    public class Plateau : IPlateau
    {
        public int GridStartXPosition { get; private set; }
        public int GridStartYPosition { get; private set; }

        public int GridMaxYPosition { get; private set; }
        public int GridMaxXPosition { get; private set; }

        public void SetGridStartPosition(int startX, int startY)
        {
            GridStartXPosition = startX;
            GridStartYPosition = startY;
        }

        public void SetGridMaxSixe(int MaxX, int MaxY)
        {
            GridMaxXPosition = MaxX;
            GridMaxYPosition = MaxY;
        }

    }
}
