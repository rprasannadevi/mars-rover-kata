using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MarsRoverApp
{
    public interface IPlateau
    {
        int GridStartXPosition { get; }
        int GridStartYPosition { get; }
        int GridMaxYPosition { get; }
        int GridMaxXPosition { get; }

        //List<Point> ObstaclesInfo();
    }
}