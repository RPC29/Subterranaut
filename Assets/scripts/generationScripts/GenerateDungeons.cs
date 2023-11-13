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

    [Header("Corridors")]
    public int corLen = 15;
    public int corCount=5;
    public float roomPercent = 0.8f;

    [Header("BSP")]
    public int minRoomX=4;
    public int minRoomY=4;
    public int dunWidth=20;
    public int dunHeight=20;
    public int offset=1;
    public bool randomWalkRooms = false;


    [Header("Tile Stuff")]
    public Tilemap floorTM, wallTM;  
    public TileBase floorTile, wallTile;


    List<Vector2> dirs = new List<Vector2>{
        new Vector2(0,1), //up
        new Vector2(1,0),//right
        new Vector2(0,-1),//down
        new Vector2(-1,0)//left
    };


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

    public void generateCorridors(){
        HashSet<Vector2> floors = new HashSet<Vector2>();
        HashSet<Vector2> possibleRooms = new HashSet<Vector2>();

        var cPos = Vector2.zero;
        possibleRooms.Add(cPos);
        for(int i = 0; i<corCount; i++){
            var corridor = randomWalkCorridor(cPos);
            cPos=corridor[corridor.Count-1];
            possibleRooms.Add(cPos);
            floors.UnionWith(corridor);   
        }

        HashSet<Vector2> rooms = new HashSet<Vector2>();
        List<Vector2> roomsToCreate = possibleRooms.OrderBy(x =>  Random.value).Take(Mathf.RoundToInt(possibleRooms.Count*roomPercent)).ToList();
        foreach(Vector2 room in roomsToCreate)
            rooms.UnionWith(generateRoom(room, iterations, walkLength));//to be changed


        HashSet<Vector2> deadends = new HashSet<Vector2>();
        foreach(var pos in floors){
            int neighb = 0;
            foreach(var dir in dirs)if(floors.Contains(pos+dir))neighb++;
            if(neighb==1)deadends.Add(pos);
        }

        //dealing with deadends
        foreach(var pos in deadends)
            if(!rooms.Contains(pos))rooms.UnionWith(generateRoom(pos, iterations, walkLength));
        
        floors.UnionWith(rooms);

        visualizeFloor(floors);
        createWalls(floors);
    }
    public List<Vector2> randomWalkCorridor(Vector2 sPos){
        List<Vector2> corridor = new List<Vector2>();
        var dir = dirs[Random.Range(0,dirs.Count)];
        var cPos=sPos;

        for(int i = 0; i<corLen; i++){
            cPos+=dir;
            corridor.Add(cPos);
            corridor.Add(cPos+Vector2.Perpendicular(dir));
            corridor.Add(cPos+Vector2.Perpendicular(dir)*-1);

        }

        return corridor;
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
                    roomsQ.Enqueue(new BoundsInt(room.min, new Vector3Int(room.min.x, ySplit, room.min.z))); 
                    roomsQ.Enqueue(new BoundsInt(new Vector3Int(room.min.x, room.min.y+ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y-ySplit, room.size.z))); 
                }
                else if(room.size.x>=minRoomX*2){
                    var xSplit = Random.Range(1, room.size.x);
                    roomsQ.Enqueue(new BoundsInt(room.min, new Vector3Int(xSplit, room.min.y, room.min.z))); 
                    roomsQ.Enqueue(new BoundsInt(new Vector3Int(room.min.x+xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x-xSplit, room.size.y, room.size.z))); 
                }
                else//add as it is, cant be further divided
                    roomsL.Add(room);
                
            }
        }

        return roomsL;
    }


    public void generateRooms(){
        var roomL = BSP(new BoundsInt(Vector3Int.zero, new Vector3Int(dunWidth, dunHeight, 0)));
        HashSet<Vector2> floors = new HashSet<Vector2>();

        foreach(var room in roomL) {
            for(int j = offset; j<room.size.x-offset; j++)
                for(int i = offset; i<room.size.y-offset; i++)
                    floors.Add((Vector2Int)room.min+new Vector2(j,i));
        }

        visualizeFloor(floors);
        createWalls(floors);
    }


}
