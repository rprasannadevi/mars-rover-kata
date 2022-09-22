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
        protected int _GridStartXPosition { get; set; }
        protected  int _GridStartYPosition { get; set; }

        public int GridMaxYPosition { get; private set; }
        public int GridMaxXPosition { get; private set; }

        public Plateau()
        {
            _GridStartXPosition = 0;
            _GridStartYPosition = 0;
        }
         
        public void SetGridMaxSixe(int MaxX, int MaxY)
        {
            GridMaxXPosition = MaxX;
            GridMaxYPosition = MaxY;
        }

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
    }
}
