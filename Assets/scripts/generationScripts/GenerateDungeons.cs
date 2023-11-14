using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Random=UnityEngine.Random;
public class GenerateDungeons : MonoBehaviour
{
    [Header("Random Walker")]
    public int iterations = 100;
    public int walkLength = 20;
    public bool startRandom=true;



    [Header("BSP")]
    public int minRoomNum = 10;
    public int minRoomX=4;
    public int minRoomY=4;
    public int dunWidth=20;
    public int dunHeight=20;
    public int offset=1;
    public bool randomWalkRooms = false;
    public bool thickCorridor = false;


    [Header("Tile Stuff")]
    public Tilemap floorTM;
    public Tilemap wallTM;  
    public TileBase floorTile;
    public TileBase wallTile;


    List<Vector2> dirs = new List<Vector2>{
        new Vector2(0,1), //up
        new Vector2(1,0),//right
        new Vector2(0,-1),//down
        new Vector2(-1,0)//left
    };

    public List<List<Vector2>> generateCave(bool randomW, bool thick){
        List<BoundsInt> roomL = null;
        List<List<Vector2>> everything = new List<List<Vector2>>();
        int c = 0;
        do{
            if(c++>100)break;
            roomL=BSP(new BoundsInt(new Vector3Int(-dunWidth/2, -dunHeight/2, 0), new Vector3Int(dunWidth, dunHeight, 0)));
        }
        while(roomL.Count<=minRoomNum);
        HashSet<Vector2> floors = new HashSet<Vector2>();
        if(!randomW){
            foreach(var room in roomL) {
                for(int j = offset; j<room.size.x-offset; j++)
                    for(int i = offset; i<room.size.y-offset; i++)
                        floors.Add((Vector2Int)room.min+new Vector2(j,i));
            }
        }
        else{
            foreach(var room in roomL){
                var center = (Vector2Int)Vector3Int.RoundToInt(room.center);
                foreach(var floor in generateRoom(center, iterations, walkLength)){
                    if(floor.x >= room.xMin+offset && floor.x<=room.xMax-offset && floor.y>=room.yMin+offset && floor.y<=room.yMax+offset)
                        floors.Add(floor);
                }

            }
        }

        List<Vector2Int> roomC = new List<Vector2Int>();
        foreach(var room in roomL)
            roomC.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        
        HashSet<Vector2> corridors = new HashSet<Vector2>();
        var currRoomC = roomC[Random.Range(0,roomC.Count)];
        roomC.Remove(currRoomC);
        while(roomC.Count>0){
            Vector2Int closest = findClosest(currRoomC, roomC);
            roomC.Remove(closest);
            corridors.UnionWith(createCorridor(currRoomC, closest, thick));
            currRoomC=closest;
        }
        floors.UnionWith(corridors);
        everything.Add(floors.ToList<Vector2>());
        //add enemy spawn list and ore position list here
        return everything;
    }



    public HashSet<Vector2> generateRoom(Vector2 pos, int iteration, int walkLen){
        var cPos=pos;
        HashSet<Vector2> floors = new HashSet<Vector2>();
        for(int i = 0; i<iteration; i++) {
            floors.UnionWith(randomWalk(cPos, walkLen));
            if(startRandom)
                cPos=floors.ElementAt(Random.Range(0,floors.Count));
        }

        return floors;
    }


    public HashSet<Vector2> randomWalk(Vector2 sPos, int wLength) {
        HashSet<Vector2> path = new HashSet<Vector2>();
        path.Add(sPos);
        var pPos=sPos;
        for(int i = 0; i<wLength; i++){
            var nPos = pPos+dirs[Random.Range(0,dirs.Count)];
            path.Add(nPos);
            pPos=nPos;
        }
        return path;
    }


    public void visualizeFloor(IEnumerable<Vector2> floorPositions){
        foreach(var pos in floorPositions)
           drawSingleTile(pos, floorTM, floorTile);
        
    }

    public void clearTiles(){
        floorTM.ClearAllTiles();
        wallTM.ClearAllTiles();
    }

    public void drawSingleTile(Vector2 pos, Tilemap tilemap, TileBase tile){
        var tPos = floorTM.WorldToCell((Vector3)pos);
        tilemap.SetTile(tPos, tile);
    }


    public void createWalls(HashSet<Vector2> floorPos){
        HashSet<Vector2> wallPositions = new HashSet<Vector2>();
        foreach(var pos in floorPos){
            foreach(var dir in dirs){
                var nPos=pos+dir;
                if(!floorPos.Contains(nPos))
                    wallPositions.Add(nPos);
            }
        }

        foreach(var pos in wallPositions)
            drawSingleTile(pos, wallTM, wallTile);
    }


    public List<BoundsInt> BSP(BoundsInt space){
        Queue<BoundsInt> roomsQ = new Queue<BoundsInt>();
        List<BoundsInt> roomsL = new List<BoundsInt>();
        roomsQ.Enqueue(space);
        while(roomsQ.Count>0){
            var room = roomsQ.Dequeue();
            if(room.size.y>=minRoomY && room.size.x>=minRoomX){
                if(Random.value<0.5 && room.size.y>=minRoomY*2){
                    var ySplit = Random.Range(1, room.size.y);
                    roomsQ.Enqueue(new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z))); 
                    roomsQ.Enqueue(new BoundsInt(new Vector3Int(room.min.x, room.min.y+ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y-ySplit, room.size.z))); 
                }
                else if(room.size.x>=minRoomX*2){
                    var xSplit = Random.Range(1, room.size.x);
                    roomsQ.Enqueue(new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z))); 
                    roomsQ.Enqueue(new BoundsInt(new Vector3Int(room.min.x+xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x-xSplit, room.size.y, room.size.z))); 
                }
                else//add as it is, cant be further divided
                    roomsL.Add(room);
            }
        }

        return roomsL;
    }


    public void generateRooms(){
        List<BoundsInt> roomL = null;
        int c = 0;
        do{
            if(c++>100)break;
            roomL=BSP(new BoundsInt(new Vector3Int(-dunWidth/2, -dunHeight/2, 0), new Vector3Int(dunWidth, dunHeight, 0)));
        }
        while(roomL.Count<=minRoomNum);
        HashSet<Vector2> floors = new HashSet<Vector2>();
        if(!randomWalkRooms){
            foreach(var room in roomL) {
                for(int j = offset; j<room.size.x-offset; j++)
                    for(int i = offset; i<room.size.y-offset; i++)
                        floors.Add((Vector2Int)room.min+new Vector2(j,i));
            }
        }
        else{
            foreach(var room in roomL){
                var center = (Vector2Int)Vector3Int.RoundToInt(room.center);
                foreach(var floor in generateRoom(center, iterations, walkLength)){
                    if(floor.x >= room.xMin+offset && floor.x<=room.xMax-offset && floor.y>=room.yMin+offset && floor.y<=room.yMax+offset)
                        floors.Add(floor);
                }

            }
        }

        List<Vector2Int> roomC = new List<Vector2Int>();
        foreach(var room in roomL)
            roomC.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        
        HashSet<Vector2> corridors = new HashSet<Vector2>();
        var currRoomC = roomC[Random.Range(0,roomC.Count)];
        roomC.Remove(currRoomC);
        while(roomC.Count>0){
            Vector2Int closest = findClosest(currRoomC, roomC);
            roomC.Remove(closest);
            corridors.UnionWith(createCorridor(currRoomC, closest, thickCorridor));
            currRoomC=closest;
        }
        floors.UnionWith(corridors);
         

        visualizeFloor(floors);
        createWalls(floors);
    }

    public Vector2Int findClosest(Vector2Int currRoomC, List<Vector2Int> roomC){
        Vector2Int closest = Vector2Int.zero;
        float length = float.MaxValue;
        foreach(var pos in roomC){
            float currLen = Vector2.Distance(pos, currRoomC);
            if(currLen<length){
                length=currLen;
                closest=pos;
            }
        }
        return closest;
    }

    public HashSet<Vector2> createCorridor(Vector2Int currRoomC, Vector2Int closest, bool thick){
        HashSet<Vector2> corridor = new HashSet<Vector2>();
        var position = currRoomC;
        corridor.Add(position);
        while(position.y!=closest.y){
            if(closest.y>position.y)position+=Vector2Int.up;
            else position+=Vector2Int.down;
            corridor.Add(position);
            if(thick){
                corridor.Add(position+Vector2Int.right);
                corridor.Add(position+Vector2Int.left);
            }
        }
        while(position.x!=closest.x){
            if(closest.x>position.x)position+=Vector2Int.right;
            else position+=Vector2Int.left;
            corridor.Add(position);
            if(thick){
                corridor.Add(position+Vector2Int.up);
                corridor.Add(position+Vector2Int.down);
            }
        }
        return corridor;
    }


}
