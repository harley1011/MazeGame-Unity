using UnityEngine;
using System.Collections;

public class EditMazeGUI : MonoBehaviour {
	public string stringToEdit = "Enter number";
	void OnGUI () {
		stringToEdit = GUI.TextField(new Rect(10, 10, 150, 40), stringToEdit, 25);
		if (GUI.Button (new Rect (10,55,150,40), "Submit Maze Size")) {
			this.GetComponent<GenerateMaze>().deleteMaze();
			this.GetComponent<GenerateMaze>().sizeOfMap = int.Parse(stringToEdit);
			this.GetComponent<GenerateMaze>().Start();
			print ("Submit Maze Size");
		}
	}
}
