using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.Models
{
    public interface IRover
    {
        int CurrentXCoordinate { get; }
        int CurrentYCoordinate { get; }
        char CurrentDirection { get; }
        bool isCameraOn { get; }

        void SetRover(string strCurrentPosition);
        string MoveRover(string strCommands);
        string TakePicture();
        string TakeSampleFromSurface();
    }
}
