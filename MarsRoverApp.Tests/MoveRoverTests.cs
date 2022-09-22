using System.Drawing;
using FluentAssertions;
using MarsRoverApp.Models;

namespace MarsRoverApp.Tests;

public class MoveRoverTests
{
    private Rover _RoverA;
    private Rover _RoverB;
    private Plateau _Plateau;

    [SetUp]
    public void Setup()
    {
        _Plateau = new Plateau();
        _Plateau.SetGridMaxSixe(5, 5);

        _RoverA = new Rover();
        _RoverA.SetGridMaxSixe(_Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverB = new Rover();
        _RoverB.SetGridMaxSixe(_Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
    }

    [Test]
    public void Get_Plateau_Grid_Max_Position()
    {
        _Plateau.GridMaxXPosition.Should().Be(5);
        _Plateau.GridMaxYPosition.Should().Be(5);
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_Valid_Input_12N()
    {
        _RoverA.SetRover("12N").Should().Be("12N");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(2);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_Valid_Input_55W()
    {
        _RoverA.SetRover("55W").Should().Be("55W"); ;
        _RoverA.CurrentXCoordinate.Should().Be(5);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('W');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_Direction()
    {
        _RoverA.SetRover("12X").Should().Be("Invalid Start Position for Rover - The Correct Directions are 'N,E,S,W"); ;
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_XCoordinate()
    {
        _RoverA.SetRover("72E").Should().Be("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position");
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_YCoordinate()
    {
        _RoverA.SetRover("27E").Should().Be("Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position");
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Number_Of_Inputs()
    {
        _RoverA.SetRover("27EEA").Should().Be("Invalid Start Position for Rover - The Correct Input Format is 'XXX'");
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverA_From_12N_13N()
    {
        _RoverA.SetRover("12N").Should().Be("12N");
        _RoverA.MoveRover("LMLMLMLMM").Should().Be("Success");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(3);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverB_From_33E_51E()
    {
        _RoverB.SetRover("33E").Should().Be("33E");
        _RoverB.MoveRover("MMRMMRMRRM").Should().Be("Success");
        _RoverB.CurrentXCoordinate.Should().Be(5);
        _RoverB.CurrentYCoordinate.Should().Be(1);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_33E_With_Invalid_Commands()
    {
        _RoverB.SetRover("33E").Should().Be("33E");
        _RoverB.MoveRover("ABCMRMMRMRRM").Should().Be("Move Rover is not Successful. The valid Commands are: L,R and M");
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(3);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_33E_With_InvalidCommands_In_The_Middle()
    {
        _RoverB.SetRover("33E").Should().Be("33E");
        _RoverB.MoveRover("MMRMMRPQRMRRM").Should().Be("Move Rover is not Successful. The valid Commands are: L,R and M");
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(3);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverA_From_13N_15E()
    {
        _RoverA.SetRover("13N").Should().Be("13N");
        _RoverA.MoveRover("LRLRMLRMR").Should().Be("Success");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_From_51E_44E()
    {
        _RoverB.SetRover("51E").Should().Be("51E");
        _RoverB.MoveRover("LLMRMMLMRMRM").Should().Be("Success");
        _RoverB.CurrentXCoordinate.Should().Be(4);
        _RoverB.CurrentYCoordinate.Should().Be(4);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverA_From_13N_Above_Max_Grid_Y_Position()
    {
        _RoverA.SetRover("13N").Should().Be("13N");
        _RoverA.MoveRover("LRLRMLRMRLM").Should().Be("Cannot Move Rover Further As it is Grid's Edge Position");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverB_From_51E_Above_Max_Grid_X_Position()
    {
        _RoverB.SetRover("51E").Should().Be("51E");
        _RoverB.MoveRover("LLMRMMLMRMRMMMM").Should().Be("Cannot Move Rover Further As it is Grid's Edge Position");
        _RoverB.CurrentXCoordinate.Should().Be(5);
        _RoverB.CurrentYCoordinate.Should().Be(4);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverA_From_13N_Less_Than_start_Y_Position()
    {
        _RoverA.SetRover("12N").Should().Be("12N");
        _RoverA.MoveRover("LLMMM").Should().Be("Cannot Move Rover Further As it is Grid's Edge Position");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('S');
    }

    [Test]
    public void Move_RoverB_From_51E_Less_Than_start_X_Position()
    {
        _RoverB.SetRover("51E").Should().Be("51E");
        _RoverB.MoveRover("LLMMMMMM").Should().Be("Cannot Move Rover Further As it is Grid's Edge Position");
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
}