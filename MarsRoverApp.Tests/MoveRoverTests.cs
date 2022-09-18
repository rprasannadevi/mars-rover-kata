using System.Drawing;
using FluentAssertions;
using MarsRoverApp.Models;

namespace MarsRoverApp.Tests;

public class MoveRoverTests
{
    private Rover _RoverA;
    private Plateau _Plateau;
    //private DoCommand _DoCommand;

    [SetUp]
    public void Setup()
    {
        _Plateau = new Plateau();
        _Plateau.SetGridStartPosition(0, 0); ;
        _Plateau.SetGridMaxSixe(5, 5);

        _RoverA = new RoverA();
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
        _RoverA.SetRover("12N", _Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverA.CurrentXCoordinate.Should().Be(1);
        _RoverA.CurrentYCoordinate.Should().Be(2);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_Valid_Input_55W()
    { 
        _RoverA.SetRover("55W", _Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverA.CurrentXCoordinate.Should().Be(5);
        _RoverA.CurrentYCoordinate.Should().Be(5);
        _RoverA.CurrentDirection.Should().Be('W');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_Direction()
    {
        _RoverA.SetRover("12X", _Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_XCoordinate()
    {
        _RoverA.SetRover("72E", _Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }

    [Test]
    public void Get_RoverA_CurrentPosition_With_InValid_Input_YCoordinate()
    {
        _RoverA.SetRover("27E", _Plateau.GridMaxXPosition, _Plateau.GridMaxYPosition);
        _RoverA.CurrentXCoordinate.Should().Be(0);
        _RoverA.CurrentYCoordinate.Should().Be(0);
        _RoverA.CurrentDirection.Should().Be('N');
    }
}