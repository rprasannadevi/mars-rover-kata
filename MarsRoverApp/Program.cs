﻿// See https://aka.ms/new-console-template for more information
using System;
using MarsRoverApp;
using MarsRoverApp.Models;

Console.WriteLine("Hello, World!");

Plateau Plateau = new Plateau();

Rover RoverA = new Rover();
Rover RoverB = new Rover();
Plateau.SetGridMaxSixe(5, 5);
RoverA.SetGridMaxSixe(Plateau.GridMaxXPosition, Plateau.GridMaxYPosition);
RoverB.SetGridMaxSixe(Plateau.GridMaxXPosition, Plateau.GridMaxYPosition);

Console.WriteLine($"{Plateau.GridMaxXPosition}, {Plateau.GridMaxYPosition}");
string sOutput;
sOutput=RoverA.SetRover("12N");
Console.WriteLine(sOutput);
sOutput = RoverA.MoveRover("LMLMLMLMM");
Console.WriteLine(sOutput);
sOutput = RoverB.SetRover("33E");
Console.WriteLine(sOutput);
sOutput = RoverB.MoveRover("MMRMMRMRRM");
Console.WriteLine(sOutput);
sOutput = RoverB.SetRover("33E");
Console.WriteLine(sOutput);
sOutput=RoverB.MoveRover("ABCMRMMRMRRM");
Console.WriteLine(sOutput);


