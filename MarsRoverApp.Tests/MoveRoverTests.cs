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
        _RoverA.SetRover("12N");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(2);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_Valid_Input_55W()
    {
        _RoverA.SetRover("55W");
        _RoverA.CurrentXCoordinate.Should().Be(5);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('W');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_Direction()
    {
        _RoverA.SetRover("12X");
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_XCoordinate()
    {
        _RoverA.SetRover("72E");
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_YCoordinate()
    {
        _RoverA.SetRover("27E");
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Number_Of_Inputs()
    {
        _RoverA.SetRover("27EEA");
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverA_From_12N_13N()
    {
        _RoverA.SetRover("12N");
        _RoverA.MoveRover("LMLMLMLMM");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(3);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Move_RoverB_From_33E_51E()
    {
        _RoverB.SetRover("33E");
        _RoverB.MoveRover("MMRMMRMRRM");
        _RoverB.CurrentXCoordinate.Should().Be(5);
        _RoverB.CurrentYCoordinate.Should().Be(1);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_33E_With_Invalid_Commands()
    {
        _RoverB.SetRover("33E");
        _RoverB.MoveRover("ABCMRMMRMRRM");
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(3);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_33E_With_InvalidCommands_In_The_Middle()
    {
        _RoverB.SetRover("33E");
        _RoverB.MoveRover("MMRMMRPQRMRRM");
        _RoverB.CurrentXCoordinate.Should().Be(3);
        _RoverB.CurrentYCoordinate.Should().Be(3);
        _RoverB.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverA_From_13N_15E()
    {
        _RoverA.SetRover("13N");
        _RoverA.MoveRover("LRLRMLRMR");
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('E');
    }

    [Test]
    public void Move_RoverB_From_51E_44E()
    {
        _RoverB.SetRover("51E");
        _RoverB.MoveRover("LLMRMMLMRMRM");
        _RoverB.CurrentXCoordinate.Should().Be(4);
        _RoverB.CurrentYCoordinate.Should().Be(4);
        _RoverB.CurrentDirection.Should().Be('E');
    }

}