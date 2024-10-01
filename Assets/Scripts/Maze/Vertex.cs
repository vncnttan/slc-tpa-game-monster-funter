using UnityEngine;

public class Vertex{
    internal int posX;
	internal int posY;
	internal Vertex prevTile;
	internal double heuristic;
	
	public Vertex(int posX, int posY) {
		this.posX = posX;
		this.posY = posY;
	}

	public void SetHeuristic(int tarPosX, int tarPosY)
    {
        heuristic = Mathf.Sqrt(Mathf.Pow(posX - tarPosX, 2) + Mathf.Pow(posY - tarPosY, 2));
    }
}