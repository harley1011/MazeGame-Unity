/*
   		Name: Harley McPhee
   		Date: 5/23/2014

 Description: Script to generate a randomly 2D array with specified dimensions.
			  Once the 2D array is set it is used in-order to instantiate the
			  wall object passed to the script and a 3D maze is generated




*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateMaze : DataStructures {
	public int sizeOfMap;
	
	private float wallScaleX;
	private float wallScaleY;
	private float wallScaleZ;
	private cellBlock[,] maze;
	private Position currentPosition;
	private Position endPosition;
	private Position next;
	private Position current;
	private directions aDirection;
	private Position playerPosition;
	public GameObject[,] mazeWalls;
	public GameObject wallPrefab;
	public GameObject floorPrefab;
	public GameObject[,] mazeFloorPieces;

	public cellBlock[,] getMaze()
	{
		return maze;
	}
	public void Start () {

		mazeWalls = null;
		maze = null;
		wallScaleX = wallPrefab.transform.localScale.x;
		wallScaleY = wallPrefab.transform.localScale.y;
		wallScaleZ = wallPrefab.transform.localScale.z;

		this.transform.position = new Vector3 (sizeOfMap * wallScaleX / 2, sizeOfMap * (wallScaleX / 2) ,sizeOfMap * wallScaleX / 2);
		currentPosition.x  = 0;
		currentPosition.y = 0;
		//floorPrefab.transform.localScale = new Vector3(sizeOfMap * wallScaleX, 1,sizeOfMap * wallScaleX);
		floorPrefab.transform.localScale = new Vector3( wallScaleX + (float).1, 1,wallScaleX + (float).1);
		//Instantiate(floorPrefab,new Vector3(sizeOfMap * wallScaleX / 2 - wallScaleX / 2, 0, sizeOfMap * wallScaleX / 2), Quaternion.identity);
		maze = new cellBlock[sizeOfMap,sizeOfMap];
		initilize();
		endPosition = makeStartAndEnd();
		buildMaze(); // Generates a 2D array of a an array
		makeWalls(); // Instantiates game objects based upon 2D array built
		
	}
	public void deleteMaze()
	{
		GameObject[] theWalls = GameObject.FindGameObjectsWithTag("Wall");
		for (int i = 0; i <= theWalls.Length - 1; i++ )
				Destroy(theWalls[i]);
		Destroy(GameObject.FindGameObjectWithTag("Floor"));
				                
	}
	private void makeWalls() // Function is used to instantiate walls from a 2D array
	{
		Vector3 positionOfWall;
		mazeWalls = new GameObject[sizeOfMap,sizeOfMap];
		mazeFloorPieces = new GameObject[sizeOfMap,sizeOfMap];
		for ( int i = 0; i <= sizeOfMap - 1; i++ ) // Traverse through 2D array in
		{
			positionOfWall = new Vector3( wallScaleX * i, wallScaleY/2, 0 ); // Instantiate top border
			mazeWalls[i,0] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.identity);
			for ( int k = 0; k <= sizeOfMap - 1; k++ )
			{
				if ( i == 0 ) // Checks if we at the first column, if so we know to instantiate left wall
				{	
					positionOfWall = new Vector3( -wallScaleX/2, wallScaleY/2, wallScaleX * k +  wallScaleX/2 + wallScaleZ/2);
					mazeWalls[i,k] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.AngleAxis(90, Vector3.up));		
				}
				if ( maze[i,k].bottom ) // Checks if the cellbocks bottom wall is true or false
				{  
					positionOfWall = new Vector3( wallScaleX * i + wallScaleX/2, wallScaleY/2, wallScaleX * k  + wallScaleX/2 + wallScaleZ/2);
					mazeWalls[i,k] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.AngleAxis(90, Vector3.up));	
				}
				if ( maze[i,k].right ) // Checks if the cellbocks right wall is true or false
				{
					positionOfWall = new Vector3( wallScaleX * i , wallScaleY/2, wallScaleX * k + wallScaleX + wallScaleZ/2);
					mazeWalls[i,k] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.identity);	
					
				}
				mazeFloorPieces[i,k] = (GameObject)Instantiate(floorPrefab,new Vector3( i * wallScaleX , 0, k * wallScaleX + wallScaleX/2 ), Quaternion.identity);
			}
			
		}		
		
	}
	private Position makeStartAndEnd()
	{
		//Random randomGenerate = new Random();
		int random =  UnityEngine.Random.Range(0,4);
		int random2 = UnityEngine.Random.Range(0,4);
		
		if ( random == 0 ) // top Left corner
		{
			playerPosition.x = 0;
			playerPosition.y = 0;
			random2 = 2;
		}
		if ( random == 1 ) //top right corner
		{
			playerPosition.x = sizeOfMap - 1;
			playerPosition.y = 0;
			random2 = 3;
	
		}
		if ( random == 2 ) // bottom right corner
		{
			playerPosition.x = sizeOfMap - 1;
			playerPosition.y = sizeOfMap - 1;	
			random2 = 0;
		}
		if ( random == 3 ) // bottom left corner
		{
			playerPosition.x = 0;
			playerPosition.y = sizeOfMap - 1;
		    random2 = 1;
		}
		while ( random == random2 )
		{
			random2 = UnityEngine.Random.Range(0,4);
		}
	
	
		if ( random2 == 0 ) 
		{
			endPosition.x = 0;
			endPosition.y = 0;
			return endPosition;
		}
		if ( random2 == 1 ) 
		{
			endPosition.x = sizeOfMap - 1;
			endPosition.y = 0;
			return endPosition;
		}
		
		if ( random2 == 2 ) 
		{
			endPosition.x = sizeOfMap - 1;
			endPosition.y = sizeOfMap - 1;
			return endPosition;
		}
		if ( random2 == 3 ) 
		{
			endPosition.x = 0;
			endPosition.y = sizeOfMap - 1;
			return endPosition;
		}
		return endPosition;
		
	}
	private void buildMaze()
	{
		Stack theStack = new Stack(); // Stack is used to store the previous positions 	
		int visited = 1; // Used to check if all positions have been visited
		current.x = 0; // Starting points
		current.y = 0;

	    maze[0,0].visited = true; 
		
		while ( visited < sizeOfMap * sizeOfMap ) // All cellblocks have to be visited
		{
			next = randomDirection();
			if ( next.x == -1 && next.y == -1 )
			{
				current = (Position)theStack.Pop();
			}
			else
			{
				theStack.Push(current);
				visited++;
				next.x += current.x;
				next.y += current.y;
				maze[next.x,next.y].visited= true;
				if (aDirection == directions.Up)
				{
					maze[current.x,current.y].top = false;
					maze[next.x,next.y].bottom = false;
				}
				else if (aDirection == directions.Right)
				{
					maze[current.x,current.y].right = false;
					maze[next.x,next.y].left = false;
				}
				else if (aDirection == directions.Down)
				{
					maze[current.x,current.y].bottom = false;
					maze[next.x,next.y].top = false;
				}
				else if (aDirection == directions.Left)
				{
					maze[current.x,current.y].left = false;
					maze[next.x,next.y].right = false;
				}
				current = next;			
			}	
		}
		
		
	}
	// Used to generate a random direction and check to see if it's a valid direction
	private Position randomDirection() 
	{
		bool[] ar = new bool[] {false,false,false,false};
		Position Chosen;
		Chosen.x = 0;
		Chosen.y = 0;

		while( !ar[0] || !ar[1] || !ar[2] || !ar[3] ) // Checks to see if any walls can be broken down
		{
			int random = UnityEngine.Random.Range(0,4);;
			if ( random == 0 && !ar[0])
			{
				ar[0] = true;

				if (current.x > 0 && maze[current.x - 1,current.y].visited == false) // Checks if we aren't at top border
				{
					aDirection = directions.Up;
					Chosen.x--;
					return Chosen;
				}
			}
			else if (random == 1 && !ar[1] ) // Checks if we aren't at right border
			{
				ar[1] = true;
				
				if ( current.y < ( sizeOfMap - 1) && maze[current.x,current.y + 1].visited == false)
				{
					aDirection = directions.Right;
					Chosen.y++;
					return Chosen;
				}
			}
			else if (random == 2 && !ar[2] ) // Checks if we aren't at bottom border
			{
				ar[2] = true;
				
				if (current.x <(sizeOfMap  - 1) && maze[current.x + 1,current.y].visited == false)
				{
					aDirection = directions.Down;
					Chosen.x++;		
					return Chosen;
				}
			}
            else if (random == 3 && !ar[3] ) // Checks if we aren't at the left border
			{
				ar[3] = true;
			
				if (current.y > 0 && maze[current.x,current.y - 1].visited == false)
				{
					aDirection = directions.Left;
					Chosen.y--;
					return Chosen;
				}
			}
		}
		Chosen.x = -1; // Can't move anywhere so we must go back
		Chosen.y = -1;
		return Chosen;
	}
	// Used to initilize the mazes cellblocks
	private void initilize()
	{
		for ( int i = 0; i < sizeOfMap; i++ )
		{
			for ( int n = 0; n < sizeOfMap; n++)
			{
				maze[i,n].left = true;
				maze[i,n].top = true;
				maze[i,n].right = true;
				maze[i,n].bottom = true;
				maze[i,n].visited = false;
			}
	}
		
	}
}
