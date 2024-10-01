using UnityEngine;

public class Edge{
    internal Vertex source;
	internal Vertex destination;

	internal double price;
	public Edge(Vertex source, Vertex destination)
	{
		this.source = source;
		this.destination = destination;

		this.price = Mathf.Sqrt(Mathf.Pow((source.posX - destination.posX), 2) + Mathf.Pow((source.posY - destination.posY), 2));
	}
}