using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MarsRoverApp.Models;

namespace MarsRoverApp.Tests
{
    public class PlateauTests
    {
        public Plateau _Plateau;
        private int _MaxX;
        private int _MaxY;

        [SetUp]
        public void Setup()
        {
            _MaxX = 7;
            _MaxY = 5;
            _Plateau = new Plateau(_MaxX, _MaxY);
        }

        [Test]
        public void Get_Plateau_Grid_Max_Position()
        {
            _Plateau.GridMaxXPosition.Should().Be(_MaxX);
            _Plateau.GridMaxYPosition.Should().Be(_MaxY);
        }

    }
}
