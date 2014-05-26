using UnityEngine;
using System.Collections;

public class DataStructures : MonoBehaviour {
	public enum directions { Up, Right, Down, Left };
	public struct cellBlock
	{
		public bool top;
		public bool left;
		public bool bottom;
		public bool right;
		public bool visited;
	};
	public struct Position
	{
		public int x;
		public int y;
	};
}
