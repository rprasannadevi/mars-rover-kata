using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverApp.Models;
using FluentAssertions;
using System.Drawing;

namespace MarsRoverApp.Tests
{
    public class MissionControlTests
    { 
        private MissionControl _MissionControl;
        public Plateau _Plateau;
        private Rover _RoverA;
        private Rover _RoverB;
        private Rover _RoverC;

        [SetUp]
        public void Setup()
        {
            _Plateau = new Plateau(7,5);
            _MissionControl = new MissionControl(_Plateau);

            _RoverA = new Rover();
            _RoverA.SetName("A");

            _RoverB = new Rover();
            _RoverB.SetName("B");

            _RoverC = new Rover();
            _RoverC.SetName("C");
        }
        
        [Test]
        public void Get_RoverA_CurrentPosition_With_Valid_Input_12N()
        {
            _MissionControl.SetRover(_RoverA,"12N");
            _RoverA.CurrentXCoordinate.Should().Be(1);
            _RoverA.CurrentYCoordinate.Should().Be(2);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Get_RoverB_CurrentPosition_With_Valid_Input_55W()
        {
            _MissionControl.SetRover(_RoverB, "33E");
            _RoverB.CurrentXCoordinate.Should().Be(3);
            _RoverB.CurrentYCoordinate.Should().Be(3);
            _RoverB.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Get_RoverA_CurrentPosition_With_InValid_Input_As_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.SetRover(_RoverA, ""));
            Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover. The Input is Empty"));
            _RoverA.CurrentXCoordinate.Should().Be(0);
            _RoverA.CurrentYCoordinate.Should().Be(0);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Get_RoverA_CurrentPosition_With_InValid_Input_As_Null()
        {
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.SetRover(_RoverA, null));
            Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover. The Input is NULL"));
            _RoverA.CurrentXCoordinate.Should().Be(0);
            _RoverA.CurrentYCoordinate.Should().Be(0);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Get_RoverA_CurrentPosition_With_InValid_Input_Direction()
        {
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.SetRover(_RoverA,"12X"));
            Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Correct Directions are 'N,E,S,W"));
            _RoverA.CurrentXCoordinate.Should().Be(0);
            _RoverA.CurrentYCoordinate.Should().Be(0);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Get_RoverA_CurrentPosition_With_InValid_Input_XCoordinate()
        {
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.SetRover(_RoverA,"82E"));
            Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position"));
            _RoverA.CurrentXCoordinate.Should().Be(0);
            _RoverA.CurrentYCoordinate.Should().Be(0);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Get_RoverA_CurrentPosition_With_InValid_Input_YCoordinate()
        {
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.SetRover(_RoverA, "27E"));
            Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position"));
            _RoverA.CurrentXCoordinate.Should().Be(0);
            _RoverA.CurrentYCoordinate.Should().Be(0);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Get_RoverA_CurrentPosition_With_InValid_Number_Of_Inputs()
        {
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.SetRover(_RoverA, "27EEA"));
            Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Correct Input Format is 'XXX'"));
            _RoverA.CurrentXCoordinate.Should().Be(0);
            _RoverA.CurrentYCoordinate.Should().Be(0);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Move_RoverA_From_12N_13N()
        {
            _MissionControl.SetRover(_RoverA, "12N");
            _MissionControl.MoveRover("LMLMLMLMM",_RoverA).Should().Be("13N");
            _RoverA.CurrentXCoordinate.Should().Be(1);
            _RoverA.CurrentYCoordinate.Should().Be(3);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Move_RoverB_From_33E_51E()
        {
            _MissionControl.SetRover(_RoverB,"33E");
            _MissionControl.MoveRover("MMRMMRMRRM", _RoverB).Should().Be("51E");
            _RoverB.CurrentXCoordinate.Should().Be(5);
            _RoverB.CurrentYCoordinate.Should().Be(1);
            _RoverB.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Move_RoverB_33E_With_Invalid_Commands()
        {
            _MissionControl.SetRover(_RoverB, "33E");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("ABCMRMMRMRRM",_RoverB));
            Assert.That(ex.Message, Is.EqualTo("Move Rover is not Successful. The valid Commands are: L,R and M"));
            _RoverB.CurrentXCoordinate.Should().Be(3);
            _RoverB.CurrentYCoordinate.Should().Be(3);
            _RoverB.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Move_RoverB_33E_With_InvalidCommands_In_The_Middle()
        {
            _MissionControl.SetRover(_RoverB, "33E");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("MMRMMRPQRMRRM", _RoverB));
            Assert.That(ex.Message, Is.EqualTo("Move Rover is not Successful. The valid Commands are: L,R and M"));
            _RoverB.CurrentXCoordinate.Should().Be(3);
            _RoverB.CurrentYCoordinate.Should().Be(3);
            _RoverB.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Move_RoverA_From_13N_15E()
        {
            _MissionControl.SetRover(_RoverA, "13N");
            _MissionControl.MoveRover("LRLRMLRMR", _RoverA).Should().Be("15E");
            _RoverA.CurrentXCoordinate.Should().Be(1);
            _RoverA.CurrentYCoordinate.Should().Be(5);
            _RoverA.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Move_RoverB_From_51E_34E()
        {
            _MissionControl.SetRover(_RoverB,"51E");
            _MissionControl.MoveRover("LLMRMMLMRMR", _RoverB).Should().Be("34E");
            _RoverB.CurrentXCoordinate.Should().Be(3);
            _RoverB.CurrentYCoordinate.Should().Be(4);
            _RoverB.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Move_RoverA_From_13N_Above_Max_Grid_Y_Position()
        {
            _MissionControl.SetRover(_RoverA, "13N");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("LRLRMLRMRLM", _RoverA));
            Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
            _RoverA.CurrentXCoordinate.Should().Be(1);
            _RoverA.CurrentYCoordinate.Should().Be(5);
            _RoverA.CurrentDirection.Should().Be('N');
        }

        [Test]
        public void Move_RoverB_From_51E_Above_Max_Grid_X_Position_With_Obstacle()
        {
            _MissionControl.SetRover(_RoverB, "51E");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("MMM", _RoverB));
            Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
            _RoverB.CurrentXCoordinate.Should().Be(7);
            _RoverB.CurrentYCoordinate.Should().Be(1);
            _RoverB.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Move_RoverA_From_13N_Less_Than_start_Y_Position()
        {
            _MissionControl.SetRover(_RoverA, "12N");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("LLMMM", _RoverA));
            Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
            _RoverA.CurrentXCoordinate.Should().Be(1);
            _RoverA.CurrentYCoordinate.Should().Be(0);
            _RoverA.CurrentDirection.Should().Be('S');
        }

        [Test]
        public void Move_RoverB_From_51E_Less_Than_start_X_Position()
        {
            _MissionControl.SetRover(_RoverB, "51E");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("LLMMMMMM", _RoverB));
            Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
            _RoverB.CurrentXCoordinate.Should().Be(0);
            _RoverB.CurrentYCoordinate.Should().Be(1);
            _RoverB.CurrentDirection.Should().Be('W');
        }

        [Test]
        public void RoverA_Take_Picture()
        {
            _MissionControl.TakePicture(_RoverA).Should().Be("Success");
        }

        [Test]
        public void RoverB_Take_Sample_From_Surface()
        {
            _MissionControl.TakeSampleFromSurface(_RoverB).Should().Be("Success");
        }

        [Test]
        public void Get_Obstacles_Information_For_Plateau()
        {
            var ObstaclePoints = new List<Point>();
            _MissionControl.ObstaclesInfo();
            var i = 0;
            foreach (var oPoint in ObstaclePoints)
            {
                ObstaclePoints[i].X.Should().Be(oPoint.X);
                ObstaclePoints[i].Y.Should().Be(oPoint.Y);
                i += 1;
            }
        }

        [Test]
        public void Move_RoverB_From_51E_34E_With_Obstacle()
        {
            _MissionControl.SetRover(_RoverB, "51E");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("LLMRMMLMRMRM", _RoverB));
            Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further. Because Obstacle is present over there."));
            _RoverB.CurrentXCoordinate.Should().Be(3);
            _RoverB.CurrentYCoordinate.Should().Be(4);
            _RoverB.CurrentDirection.Should().Be('E');
        }

        [Test]
        public void Move_RoverC_From_12E_11S_To_Check_Collision()
        {
            _MissionControl.SetRover(_RoverC, "12E");
            var ex = Assert.Throws<ArgumentException>(() => _MissionControl.MoveRover("RMM", _RoverC));
            Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further.Rover A is standing over there."));
            _RoverC.CurrentXCoordinate.Should().Be(1);
            _RoverC.CurrentYCoordinate.Should().Be(1);
            _RoverC.CurrentDirection.Should().Be('S');
             /*if (Rover.RoverPresentPoints.Count > 0)
            {
                foreach (DictionaryEntry oPoint in Rover.RoverPresentPoints)
                {
                    Console.WriteLine("RoverName: {0}, Co-Ordinates: {1}", oPoint.Key, oPoint.Value);
                }
            }*/
        }
    }
}
