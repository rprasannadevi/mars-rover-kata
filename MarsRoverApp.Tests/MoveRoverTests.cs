using System.Collections;
using System.Drawing;
using FluentAssertions;
using MarsRoverApp.Models;

namespace MarsRoverApp.Tests;

public class MoveRoverTests
{
    private Rover _RoverA;
    private Rover _RoverB;
    private Rover _RoverC;
    private Plateau _Plateau;
    private int _MaxX;
    private int _MaxY;

    [SetUp]
    public void Setup()
    {
        _Plateau = new Plateau();
        ///Plateau could be Square or Rectangle. Based on Maximum Co-ordinates of X and Y, the validations will be done.
        _MaxX = 7;
        _MaxY = 5;
        _Plateau.SetGridMaxSixe(_MaxX, _MaxY);

        _RoverA = new Rover();
        _RoverA.SetName("A");
        _RoverA.SetGridMaxSixe(_Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverB = new Rover();
        _RoverB.SetName("B");
        _RoverB.SetGridMaxSixe(_Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverC = new Rover();
        _RoverC.SetName("C");
        _RoverC.SetGridMaxSixe(_Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
    }

    [Test]
    public void Get_Plateau_Grid_Max_Position()
    {
        _Plateau.GridMaxXPosition.Should().Be(_MaxX);
        _Plateau.GridMaxYPosition.Should().Be(_MaxY);
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_Valid_Input_12N()
    {
        _RoverA.SetRover("12N");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(2);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_Valid_Input_55W()
    {
        _RoverB.SetRover("33E");
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(3);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_As_Empty()
    {
        var ex = Assert.Throws<ArgumentException>(() => _RoverA.SetRover(""));
        Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover. The Input is Empty"));
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    } 

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_As_Null()
    {
        var ex = Assert.Throws<ArgumentException>(() => _RoverA.SetRover(null));
        Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover. The Input is NULL"));
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_Direction()
    {
        var ex=Assert.Throws<ArgumentException>(() => _RoverA.SetRover("12X"));
        Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Correct Directions are 'N,E,S,W"));
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }
   
    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_XCoordinate()
    {
        var ex = Assert.Throws<ArgumentException>(() => _RoverA.SetRover("82E"));
        Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position"));
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_YCoordinate()
    {
        var ex = Assert.Throws<ArgumentException>(() => _RoverA.SetRover("27E"));
        Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position"));
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Number_Of_Inputs()
    {
        var ex = Assert.Throws<ArgumentException>(() => _RoverA.SetRover("27EEA"));
        Assert.That(ex.Message, Is.EqualTo("Invalid Start Position for Rover - The Correct Input Format is 'XXX'"));
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverA_From_12N_13N()
    {
        _RoverA.SetRover("12N");
        _RoverA.MoveRover("LMLMLMLMM").Should().Be("13N");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(3);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverB_From_33E_51E()
    {
        _RoverB.SetRover("33E");
        _RoverB.MoveRover("MMRMMRMRRM").Should().Be("51E");
        _RoverB.CurrentXCoordinate.Should().Be(5);
        _RoverB.CurrentYCoordinate.Should().Be(1);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_33E_With_Invalid_Commands()
    {
        _RoverB.SetRover("33E");
        var ex = Assert.Throws<ArgumentException>(() => _RoverB.MoveRover("ABCMRMMRMRRM"));
        Assert.That(ex.Message, Is.EqualTo("Move Rover is not Successful. The valid Commands are: L,R and M"));
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(3);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_33E_With_InvalidCommands_In_The_Middle()
    {
        _RoverB.SetRover("33E");
         var ex = Assert.Throws<ArgumentException>(() => _RoverB.MoveRover("MMRMMRPQRMRRM"));
        Assert.That(ex.Message, Is.EqualTo("Move Rover is not Successful. The valid Commands are: L,R and M"));
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(3);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverA_From_13N_15E()
    {
        _RoverA.SetRover("13N");
        _RoverA.MoveRover("LRLRMLRMR").Should().Be("15E");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_From_51E_34E()
    {
        _RoverB.SetRover("51E");
        _RoverB.MoveRover("LLMRMMLMRMR").Should().Be("34E");
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(4);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverA_From_13N_Above_Max_Grid_Y_Position()
    {
        _RoverA.SetRover("13N");
        var ex = Assert.Throws<ArgumentException>(() => _RoverA.MoveRover("LRLRMLRMRLM"));
        Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverB_From_51E_Above_Max_Grid_X_Position_With_Obstacle()
    {
        _RoverB.SetRover("51E");
        var ex = Assert.Throws<ArgumentException>(() => _RoverB.MoveRover("MMM"));
        Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
        _RoverB.CurrentXCoordinate.Should().Be(7);
        _RoverB.CurrentYCoordinate.Should().Be(1);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverA_From_13N_Less_Than_start_Y_Position()
    {
        _RoverA.SetRover("12N");
        var ex = Assert.Throws<ArgumentException>(() => _RoverA.MoveRover("LLMMM"));
        Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('S');
    }

    [Test]
    public void Move_RoverB_From_51E_Less_Than_start_X_Position()
    {
        _RoverB.SetRover("51E");
        var ex = Assert.Throws<ArgumentException>(() => _RoverB.MoveRover("LLMMMMMM"));
        Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further As it is Grid's Edge Position"));
        _RoverB.CurrentXCoordinate.Should().Be(0);
        _RoverB.CurrentYCoordinate.Should().Be(1);
        _RoverB.CurrentDirection.Should().Be('W');
    }

    [Test]
    public void RoverA_Take_Picture()
    {
        _RoverA.TakePicture().Should().Be("Success");
    }

    [Test]
    public void RoverB_Take_Sample_From_Surface()
    {
        _RoverB.TakeSampleFromSurface().Should().Be("Success");
    }

    [Test]
    public void Get_Obstacles_Information_For_Plateau()
    {
        var ObstaclePoints = new List<Point>();
        ObstaclePoints = _Plateau.ObstaclesInfo();
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
        _RoverB.SetRover("51E");
        var ex = Assert.Throws<ArgumentException>(() => _RoverB.MoveRover("LLMRMMLMRMRM"));
        Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further. Because Obstacle is present over there."));
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(4);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverC_From_12E_11S_To_Check_Collision()
    {
        _RoverC.SetRover("12E");
        if (Rover.RoverPresentPoints.Count > 0)
        {
            foreach (DictionaryEntry oPoint in Rover.RoverPresentPoints)
            {
                Console.WriteLine("RoverName: {0}, Co-Ordinates: {1}", oPoint.Key, oPoint.Value);
            }
        }
        var ex = Assert.Throws<ArgumentException>(() => _RoverC.MoveRover("RMM"));
        Assert.That(ex.Message, Is.EqualTo("Cannot Move Rover Further. Another Rover is standing over there."));
        _RoverC.CurrentXCoordinate.Should().Be(1);
        _RoverC.CurrentYCoordinate.Should().Be(1);
        _RoverC.CurrentDirection.Should().Be('S');
    }
}