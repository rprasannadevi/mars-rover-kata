Mars Rover Kata
I have two interfaces called IPlateau and IRover.  Created Plateau Class from IPlateau and Rover Class from Plateau and IRover. Also, I have created two Enum files for Directions with N, E, S and W and Commands with L, R and M. 
As its Test-Driven Development (TDD) project, I have created unit test cases for this project. 
First, I have set the Grid Max Values for the Plateau. In the Plateau Constructor I have set the Start Position of X and Y are 0,0
In Rover class, I have current Co-ordinates and Facing direction of the Rover. It has two methods to setRover and MoveRover.  
SetRover gets X, Y Ordinates and Current Facing Direction.
Move Rover gets the Commands to Move the Rover.
I have created Do Command Class where I have added the functionality to move Rover. Also added a method to check for Obstacles.
I have included all the following testcases to test SetRover Method.
1.	If its VALID input, it will set the Rover to that position.
2.	If its INVALID w.r.to Directions, it will set the Rover Position to Default which ‘0,0, N’ and throw an exception as “Invalid Start Position for Rover - The Correct Directions are 'N, E, S, W"
3.	If its INVALID w.r.to Grid Max Size, it will set the Rover Position to Default which ‘0,0, N’ and throw an exception as “Invalid Start Position for Rover - The Co-Ordinates are Beyond the Grid Position”
4.	If its INVALID input w.r.to Length of string, it it will set the Rover Position to Default which ‘0,0, N’ and throw an exception as “Invalid Start Position for Rover - The Correct Input Format is 'XXX'”
I have included all the following testcases to test MoveRover Method.
1.	If its VALID input, it will move the Rover from source destination based on the Commands.
2.	If its INVALID input w.r.to wrong letters in Commands, it will not move the ROVER and throw an exception as “Move Rover is not Successful. The valid Commands are: L, R and M"
3.	If its VALID input and the List Of the Commands move the Rover beyond the Max Grid position and start Grid position, it will move the Rover till the boundary and throw an exception as “Cannot Move Rover Further As it is Grid's Edge Position"
I have created two Objects Rover A and B. Added few VALID Inputs to move Rovers from Position A to Position B. All is working fine.
I have added TakePicture and TakeSampleFromSurface methods in Rover Classes and tested with return message as “Success”. Added new Enum for RoverArmActions to take a sample from Surface. Its just a basic method.

I have created ObstacleInfo method in Plteau Class to have the List of Points where the Obstacles are. 
I use this List of Obstacles Points to check for the Obstacles Whenever the MoveRover method called. The Rover will be moved before the Obtacle Point and throw an exception as "Cannot Move Rover Further As it is Grid's Edge Position".



