using UnityEngine;
using System.Collections.Generic;
public class Astar{
    private int length;
    private int width;
    private int counter;
    private List<Vertex> openList = new List<Vertex>();

    public Astar(int length, int width){
        this.length = length;
        this.width = width;
    }

    public void insertVertex(Vertex v){
        if(openList.Contains(v)){
            return;
        }
        for(int i = 0; i<openList.Count; i++){
            if(openList[i].heuristic > v.heuristic){
                openList.Insert(i, v);
                return;
            }
        }
        openList.Insert(openList.Count, v);
        return;
    }

    public char[,] duplicateMap(char [,] map){
        char[,] mapDuplicated = new char[length,width];

        for(int i = 0; i<length; i++){
            for(int j = 0; j<width; j++){
                mapDuplicated[i,j] = map[i,j];
            }
        }

        return mapDuplicated;
    }

    public char[,] trace(Vertex start, Vertex end, char[,] map){
        char[,] tempMap = duplicateMap(map);

        int[] dirX = {0, 1, 0, -1};
        int[] dirY = {1, 0, -1, 0};
        openList = new List<Vertex>();
        insertVertex(start);

        Vertex curr = null;
        int counter = 0;
        while(openList.Count > 0){
            curr = openList[0];
            openList.RemoveAt(0);

            tempMap[curr.posX, curr.posY] = 'X';

            if(counter > 4000){
                return traceBack(curr, map);
            }
            counter++;
            if(curr.posX == end.posX && curr.posY == end.posY){
                return traceBack(curr, map);
            }

            for(int i = 0; i<4; i++){
                if(curr.posX + dirX[i] <= 0 || curr.posY + dirY[i] <= 0 || curr.posX + dirX[i] >= length-1 || curr.posY >= width-1){
                    continue;
                }
                if (tempMap[curr.posX + dirX[i], curr.posY + dirY[i]] == '#' || tempMap[curr.posX + dirX[i], curr.posY + dirY[i]] == ' ' || tempMap[curr.posX + dirX[i], curr.posY + dirY[i]] == 'D')
                {
                    Vertex v = new Vertex(curr.posX + dirX[i], curr.posY + dirY[i]);
                    v.SetHeuristic(end.posX, end.posY);
                    v.prevTile = curr;

                    insertVertex(v);
                }
            }
        }
        return traceBack(curr, map);
    }

    public char[,] traceBack(Vertex v, char[,] map){
        Vertex curr = v;
        int counter = 0;
        while(true){
            if(curr == null){
                break;
            }

            if(map[curr.posX, curr.posY] != 'D'){
                map[curr.posX, curr.posY] = ' ';
            }
            curr = curr.prevTile;
            counter++;
            if(counter > 3000){
                break;
            }
        }
        return map;
    }
}