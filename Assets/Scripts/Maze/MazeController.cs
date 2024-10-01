using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeController : MonoBehaviour
{
    private char[,] map;
    [SerializeField] private int length = 80;
    [SerializeField] private int width = 80;
	[SerializeField] private GameObject Floor;
	[SerializeField] private GameObject Ceiling;
	[SerializeField] private GameObject WallT;
	[SerializeField] private GameObject WallL;
	[SerializeField] private GameObject WallR;
	[SerializeField] private GameObject WallB;
	[SerializeField] private GameObject DoorT;
	[SerializeField] private GameObject DoorL;
	[SerializeField] private GameObject DoorR;
	[SerializeField] private GameObject DoorB;
	[SerializeField] private GameObject NormalRoom1;
	[SerializeField] private GameObject NormalRoom2;
	[SerializeField] private GameObject NormalRoom3;
	[SerializeField] private GameObject ItemRoom1;
	[SerializeField] private GameObject ItemRoom2;
	[SerializeField] private GameObject ItemRoom3;
	[SerializeField] private GameObject ItemRoom4;
	[SerializeField] private GameObject SpawnRoom;
	[SerializeField] private GameObject BossRoom;
	[SerializeField] private GameObject EnemyRoom1;
	[SerializeField] private GameObject EnemyRoom2;
	[SerializeField] private GameObject EnemyRoom3;
	private List<GameObject> walls;
	
    void Start()
    {
        MazeGen();
		InstantiateMaze();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private List<Edge> edges;
	private int[] ancestor;
	private void InstantiateCorridorWall(int i, int j, bool isDoorAble){
		if((j <= 1) || map[i, j-1] == '#'){
			Instantiate(WallL, new Vector3(i*4, 0f, j*4-4), Quaternion.identity);
		}else if(((j <= 1) || map[i, j-1] != ' ') && isDoorAble){
			Instantiate(DoorL, new Vector3(i*4, 0f, j*4-4), Quaternion.identity);
		}

		if((j >= length-1) || map[i, j+1] == '#'){
			Instantiate(WallR, new Vector3(i*4, 0f, j*4+4), Quaternion.identity);
		} else if(((j >= length-1) || map[i, j+1] != ' ') && isDoorAble){
			Instantiate(DoorR, new Vector3(i*4, 0f, j*4+4), Quaternion.identity);
		}
		
		if(i - 1 <= 0 || map[i-1 , j] == '#'){
			Instantiate(WallT, new Vector3(i*4 - 4, 0f, j*4), Quaternion.identity);
		} else if((i - 1 <= 0 || map[i-1 , j] != ' ' ) && isDoorAble){
			Instantiate(DoorT, new Vector3(i*4 - 4, 0f, j*4), Quaternion.identity);
		}

		if((i + 1 >= width || map[i+1, j] == '#')){
			Instantiate(WallB, new Vector3(i*4+4, 0f, j*4), Quaternion.identity);
		} else if((i + 1 >= width || map[i+1, j] != ' ') && isDoorAble){
			Instantiate(DoorB, new Vector3(i*4+4, 0f, j*4), Quaternion.identity);
		}
	}
	private void InstantiateMaze(){
		walls = new List<GameObject>() {WallB, WallR, WallT, WallL};
		int normalCounter = 1;
		int itemCounter = 1;
		int enemyCounter = 1;

		for(int i = 0; i<length; i++){
			for(int j = 0; j<width; j++){
				if(map[i, j] == ' ' || map[i, j] == 'D'){
					Instantiate(Floor, new Vector3(i*4, 0f, j*4), Quaternion.identity);
					Instantiate(Ceiling, new Vector3(i*4, 0f, j*4), Quaternion.identity);
					InstantiateCorridorWall(i, j, map[i, j] == 'D');
				}

				if(map[i, j] == 'N'){
					if(normalCounter == 1){
						InstantiateRoom(i, j, NormalRoom1, 3, 3);
					} else if(normalCounter == 2){
						InstantiateRoom(i, j, NormalRoom2, 3, 3);
					} else {
						InstantiateRoom(i, j, NormalRoom3, 3, 3);
					}
					normalCounter++;
				}

				if(map[i, j] == 'I'){
					if(itemCounter == 1){
						InstantiateRoom(i, j, ItemRoom1, 3, 3);
					} else if(itemCounter == 2){
						InstantiateRoom(i, j, ItemRoom2, 3, 3);
					} else if(itemCounter == 3){
						InstantiateRoom(i, j, ItemRoom3, 3, 3);
					} else {
						InstantiateRoom(i, j, ItemRoom4, 3, 3);
					}
					itemCounter++;
				}

				if(map[i, j] == 'E'){
					if(enemyCounter == 1){
						InstantiateRoom(i, j, EnemyRoom1, 3, 3);
					} else if(enemyCounter == 2){
						InstantiateRoom(i, j, EnemyRoom2, 3, 3);
					} else {
						InstantiateRoom(i, j, EnemyRoom3, 3, 3);
					}
					enemyCounter++;
				}

				if(map[i, j] == 'S'){
					InstantiateRoom(i, j, SpawnRoom, 4, 4);
					GameObject.Find("Main").transform.position = new Vector3(i*4 + 4, 5f, j*4 + 4);
					GameObject.Find("Char").transform.position = new Vector3(i*4 + 4, 5f, j*4 + 4);
					GameObject.Find("Char").transform.position = new Vector3(i*4 + 4, 5f, j*4 + 4);
					GameObject.Find("Char").transform.position = new Vector3(i*4 + 4, 5f, j*4 + 4);
					GameObject.Find("Char").transform.position = new Vector3(i*4 + 4, 5f, j*4 + 4);
				}

				if(map[i, j] == 'B'){
					InstantiateRoom(i, j, BossRoom, 6, 6);
				}
			}
		}
	}

	public void InstantiateRoom(int i, int j, GameObject room, int roomw, int rooml){
		Instantiate(room, new Vector3(i*4, 0f, j*4), Quaternion.identity);

		for(int k = 0; k<length; k++){
			string s = "";
			for(int l = 0; l<length; l++){
				s += map[k, l];
			}
		}

		// Give Left wall
		for(int k = 0; k<rooml; k++){
			if((j <= 1) ||  map[i + k, j - 1] != 'D'){
				Instantiate(WallL, new Vector3(i*4 + k*4, 0f, j*4 - 4), Quaternion.identity);
			}
		}
		// Give Right wall
		for(int k = 0; k<rooml; k++){
			if((j >= length-1) || map[i + k, j + rooml] != 'D'){
				Instantiate(WallR, new Vector3(i*4 + k*4, 0f, j*4 + roomw*4), Quaternion.identity);
			}
		}
		// Give Top wall
		for(int k = 0; k<roomw; k++){
			if(i - 1 <= 0 || map[i-1, j + k] != 'D'){
				Instantiate(WallT, new Vector3(i*4 - 4, 0f, j*4 + k*4), Quaternion.identity);
			}
		}
		//Give Bottom Wall
		for(int k = 0; k<roomw; k++){
			Instantiate(WallB, new Vector3(i*4 + rooml*4, 0f, j*4 + k*4), Quaternion.identity);
		}
	}
    public void MazeGen(){
		ancestor = new int[1800];
		map = new char[length+1, width+1];
		
		for(int i = 0; i<length+1; i++){
			for(int j = 0; j<width+1; j++){
				map[i, j] = '#';
			}
		}
		
		GenNecessaryRoom();
		
		List<Vertex> vertices = new List<Vertex>();
		for(int i = 0; i<length; i++) {
			for(int j = 0; j<width; j++) {
				if(map[i,j] == 'D') {
					vertices.Add(new Vertex(i, j));
				}
			}
		}

        edges = new List<Edge>();
        for(int i = 0; i<vertices.Count; i++){
            for(int j = 1 + i; j<vertices.Count; j++){
                if(vertices[i] != vertices[j]){
                    pushEdgeMid(new Edge(vertices[i], vertices[j]));
                }
            }
        }

		for(int i = 0; i < vertices.Count; i++)
        {
            ancestor[i] = i;
        }

		int county = 0;
		List<Vertex> connected = new List<Vertex>();
		List<Edge> graphRes = new List<Edge>();
		foreach(Edge e in edges){
			county++;
			if(connected.Count == 0){
				connected.Add(e.destination);
				connected.Add(e.source);
				graphRes.Add(e);
				union(vertices.IndexOf(e.destination), vertices.IndexOf(e.source));
			} else if (connected.Contains(e.destination) && !connected.Contains(e.source)){
				connected.Add(e.source);
				graphRes.Add(e);
				union(vertices.IndexOf(e.destination), vertices.IndexOf(e.source));
			} else if (!connected.Contains(e.destination) && connected.Contains(e.source)){
				connected.Add(e.destination);
				graphRes.Add(e);
				union(vertices.IndexOf(e.destination), vertices.IndexOf(e.source));
			} else if (!connected.Contains(e.destination) && !connected.Contains(e.source)){
				connected.Add(e.source);
				connected.Add(e.destination);
				graphRes.Add(e);
				union(vertices.IndexOf(e.destination), vertices.IndexOf(e.source));
			} else if (connected.Contains(e.destination) && connected.Contains(e.source) && findAncestor(vertices.IndexOf(e.destination)) != findAncestor(vertices.IndexOf(e.source))){
				graphRes.Add(e);
				union(vertices.IndexOf(e.destination), vertices.IndexOf(e.source));
			}
		}
 		
		Astar astar = new Astar(length, width);


		int count = 0;
		foreach(Edge e in graphRes){
			map = astar.trace(e.source, e.destination, map);
			count++;
		}

		return;
	}

	private int findAncestor(int idx){
		if(ancestor[idx] != idx){
			return findAncestor(ancestor[idx]);
		}
		return idx;
	}
	private void union(int idx1, int idx2){
		int ancestor1 = findAncestor(idx1);
		int ancestor2 = findAncestor(idx2);
		ancestor[ancestor1] = ancestor2;
	}

    public void pushEdgeMid(Edge edge){
        for(int i = 0; i < edges.Count; i++){
            if(edges[i].price > edge.price){
                edges.Insert(i, edge);
                return;
            }
        }
		edges.Insert(edges.Count, edge);
        return;
    }
    
	public bool GenerateDoor(int door, int top, int left, int rooml, int roomw) {
		for(int i = 0; i<door; i++) {
			int counter = 0;
			while(true) {
				counter++;
				if(counter > 1000) {
					return false;
				}
				int randomizeLength = Random.Range(0, rooml);
				int randomizeWidth = Random.Range(0, roomw);
				
				int randomizeSide = Random.Range(0, 3);
				
				if(randomizeSide == 0 && randomizeWidth + left - 1 < 0) {
					continue;
				} else if (randomizeSide == 1 && randomizeWidth + left + 1 > width) {
					continue;
				} else if (randomizeSide == 2 && randomizeLength + top - 1 < 0) {
					continue;
				} else if (randomizeSide == 3 && randomizeLength + top + 1 > length) {
					continue;
				}
				
				if(randomizeSide == 0) {
					if(map[randomizeLength + top, randomizeWidth + left - 1] == '#') {
						map[randomizeLength + top, randomizeWidth + left - 1] = 'D';
						break;
					}
				} else if (randomizeSide == 1) {
					if(map[randomizeLength + top, randomizeWidth + left + 1] == '#') {
						map[randomizeLength + top, randomizeWidth + left + 1] = 'D';
						break;
					}
				} else if (randomizeSide == 2) {
					if(map[randomizeLength + top - 1, randomizeWidth + left] == '#') {
						map[randomizeLength + top - 1, randomizeWidth + left] = 'D';
						break;
					}
				} else if (randomizeSide == 3) {
					if(map[randomizeLength + top + 1, randomizeWidth + left] == '#') {
						map[randomizeLength + top + 1, randomizeWidth + left] = 'D';
						break;
					}
				}
			}
		}
		return true;
	}

	public void RoomGen(int rooml, int roomw, char sym, int door) {
	int top = 0;
	int left = 0;
	bool valid = true;
	int counter = 0;
	do {
		if(counter > 1000){
			break;
		}
		counter++;
		valid = true;
		top = Random.Range(2, length-(rooml+2));
		left = Random.Range(2, width-(roomw+2));
		
		for(int i = 0; i<rooml+4; i++) {
			if(rooml + 2 > length || rooml - 2 < 0){
				continue;
			}
			for(int j = 0; j<roomw+4; j++) {
				if(roomw + 2 > width || roomw - 2 < 0){
					continue;
				}
				if(map[top + i - 2, left + j - 2] != '#') {
					valid = false;
				}
			}
		}
	} while (!valid);
	
	for(int i = 0; i<rooml; i++) {
		for(int j = 0; j<roomw; j++) {
			map[i+top, j+left] = sym;
		}
	}
	map[top, left] = (sym.ToString().ToUpper()).ToCharArray()[0];			
	GenerateDoor(door, top, left, rooml, roomw);
	}
	
	public void GenNecessaryRoom() {
		for (int i = 0; i < 2; i++) {
			RoomGen(3, 3, 'n', 1);
			RoomGen(3, 3, 'e', 1);
		}
	    RoomGen(3, 3, 'n', 2);
	    RoomGen(3, 3, 'e', 2);
	    
	    for(int i = 0; i<4; i++) {
	    	RoomGen(3, 3, 'i', 1);	    	
	    }
	    
	    for(int i = 0; i<2; i++) {
	    	RoomGen(3, 3, 'i', 2);
	    }
	    
	    RoomGen(4, 4, 's', 2);
	    RoomGen(6, 6, 'b', 2);
	}

}
