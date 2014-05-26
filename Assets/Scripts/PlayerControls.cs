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

		sizeOfMap = this.GetComponent<GenerateMaze>().sizeOfMap;

		this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		checkMove("button");

	}
	public void resetPosition()
	{
		i = 0;
		j = 0;
	}
	private void setColor(int i, int j)
	{
		if ( this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color == color )
			this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color = secondColor;
		else
			this.GetComponent<GenerateMaze>().mazeFloorPieces[i,j].gameObject.renderer.material.color = color;
	}
	private void checkMove ( string buttonMove )
	{
		maze = this.GetComponent<GenerateMaze>().getMaze();
		if ( Input.GetKeyDown (KeyCode.UpArrow) || buttonMove == "up" )
		{
			if ( i > 0 && !maze[i,j].top )
				setColor(--i,j);
		}
		else if ( Input.GetKeyDown (KeyCode.DownArrow) || buttonMove == "down" )
		{
			if ( i < sizeOfMap - 1 && !maze[i,j].bottom )
				setColor(++i,j);
			
		}
		else if ( Input.GetKeyDown (KeyCode.LeftArrow) || buttonMove == "left" )
		{
			if ( j > 0 && !maze[i,j].left )
				setColor(i,--j);
			
		}
		else if ( Input.GetKeyDown (KeyCode.RightArrow) || buttonMove == "right" )
		{
			if ( j < sizeOfMap - 1 && !maze[i,j].right )
				setColor(i,++j);
			
		}

	}
	void OnGUI () {
		float buttonSize = Screen.width / 12;
		if (GUI.Button (new Rect (Screen.width / 6 - buttonSize,Screen.height / 2 - buttonSize,buttonSize,buttonSize), "↑")) 
			checkMove("up");	
		if (GUI.Button (new Rect (Screen.width / 6 - buttonSize,Screen.height / 2 + buttonSize,buttonSize,buttonSize), "↓")) 
			checkMove("down");	
		if (GUI.Button (new Rect (Screen.width / 6 - 2*buttonSize,Screen.height / 2,buttonSize,buttonSize), "←")) 
			checkMove("left");
		if (GUI.Button (new Rect (Screen.width / 6 ,Screen.height / 2,buttonSize,buttonSize), "→")) 
			checkMove("right");
	
	}
}
