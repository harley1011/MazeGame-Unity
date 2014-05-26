using UnityEngine;
using System.Collections;

public class PlayerControls : DataStructures {
	private GameObject player;
	public float speed = 40;
	public int i = 0;
	public int j = 0;
	public Color color = new Color(255,0,0,0);
	public Color secondColor = new Color(0,255,0,0);
	private int sizeOfMap;
	private cellBlock[,] maze;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag("Player");
		//this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color = color;
		sizeOfMap = this.GetComponent<GenerateMaze>().sizeOfMap;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) &&  checkMove (directions.Up) )
			setColor(--i,j);
		else if (Input.GetKeyDown (KeyCode.DownArrow) &&  checkMove (directions.Down)) 
			setColor(++i,j);
		if (Input.GetKeyDown (KeyCode.LeftArrow) &&  checkMove (directions.Left)) 
			setColor(i,--j);
		if (Input.GetKeyDown (KeyCode.RightArrow) &&  checkMove (directions.Right)) 
			setColor(i,++j);

	}
	private void setColor(int i, int j)
	{
		if ( this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color == color )
			this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color = secondColor;
		else
			this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color = color;
	}
	private bool checkMove ( directions move  )
	{
		maze = this.GetComponent<GenerateMaze>().getMaze();
		if ( move == directions.Up )
		{
			if ( i > 0 && !maze[i,j].top )
				return true;
		}
		else if ( move == directions.Down )
		{
			if ( i < sizeOfMap - 1 && !maze[i,j].bottom )
				return true;
			
		}
		else if ( move == directions.Left )
		{
			if ( j > 0 && !maze[i,j].left )
				return true;
			
		}
		else if ( move == directions.Right )
		{
			if ( j < sizeOfMap - 1 && !maze[i,j].right )
				return true;
			
		}
		
		return false;
	}
}
