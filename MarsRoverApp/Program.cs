// See https://aka.ms/new-console-template for more information
using System;
using System.Drawing;
using System.Collections;
using MarsRoverApp;
using MarsRoverApp.Models;

Plateau Plateau = new Plateau(7,5);

Rover RoverA = new Rover();
RoverA.SetName("A");
Rover RoverB = new Rover();
RoverB.SetName("B");
Rover RoverC = new Rover();
RoverC.SetName("C");
var ObstaclePoints = new List<Point>();
//Plateau.SetGridMaxSixe(7, 5);
ObstaclePoints = Plateau.ObstaclesInfo();
foreach(var oPoint in ObstaclePoints)
{
    Console.WriteLine($"Obstacles Info: {oPoint.X}, {oPoint.Y}");
}
//RoverA.SetGridMaxSixe(Plateau.GridMaxXPosition, Plateau.GridMaxYPosition);
//RoverB.SetGridMaxSixe(Plateau.GridMaxXPosition, Plateau.GridMaxYPosition);
//RoverC.SetGridMaxSixe(Plateau.GridMaxXPosition, Plateau.GridMaxYPosition);


Console.WriteLine($"{Plateau.GridMaxXPosition}, {Plateau.GridMaxYPosition}");

string sOutput;
RoverA.SetRover("12N");

sOutput = RoverA.MoveRover("LMLMLMLMM");
Console.WriteLine("Rover A Moved to: " + sOutput); //Moved to 13N

RoverB.SetRover("33E");
sOutput = RoverB.MoveRover("MMRMMRMRRM");
Console.WriteLine("Rover B Moved to: " + sOutput); //Moved to 51E

sOutput = RoverA.MoveRover("MM");
Console.WriteLine("Rover A Moved to: " + sOutput); //Moved to 15N

RoverC.SetRover("31E");
sOutput = RoverC.MoveRover("MM");
Console.WriteLine("Rover C Moved to: " + sOutput); //Moved to 51E

if(Rover.RoverPresentPoints.Count > 0)
{
    foreach (DictionaryEntry oPoint in Rover.RoverPresentPoints)
    {
        Console.WriteLine("RoverName: {0}, Co-Ordinates: {1}", oPoint.Key, oPoint.Value);
    }
}


