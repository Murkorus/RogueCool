//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{

    [SerializeField] List<GameObject> startPoints;
    [SerializeField] List<GameObject> points;
    [SerializeField] List<GameObject> bossPoints;

    [SerializeField] List<GameObject> roomPrefabs;
    [SerializeField] GameObject startRoom;
    [SerializeField] GameObject bossRoom;
    
    
    List<GameObject> roomList =new List<GameObject>();
    int numberOfDoors;
    void Start()
    {
        int RoomNumber = 0;
        int whichStartPoint = Random.Range(0,startPoints.Count);
        roomList.Add( Instantiate(startRoom, startPoints[whichStartPoint].transform.position, Quaternion.identity));
        roomList[RoomNumber].transform.SetParent(startPoints[whichStartPoint].transform);


        for(int i = 0; startPoints.Count > i; i++)
        {
            if (whichStartPoint !=i)
            {
                roomList.Add(Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)], startPoints[i].transform.position, Quaternion.identity));
                RoomNumber++;
                roomList[RoomNumber].transform.SetParent(startPoints[i].transform);
            }

        }



        for (int i = 0; points.Count > i; i++)
        {
           
                roomList.Add(Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)], points[i].transform.position, Quaternion.identity));
                RoomNumber++;
                roomList[RoomNumber].transform.SetParent(points[i].transform);
            

        }

        int whichBossPoint =Random.Range(0,bossPoints.Count);
        
        roomList.Add(Instantiate(bossRoom, startPoints[whichBossPoint].transform.position, Quaternion.identity));
        RoomNumber++;
        roomList[RoomNumber].transform.SetParent(startPoints[whichStartPoint].transform);
        

        for (int i = 0; bossPoints.Count > i; i++)
        {
            if (whichBossPoint !=i)
            {
                roomList.Add(Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)], bossPoints[i].transform.position, Quaternion.identity));
                RoomNumber++;
                roomList[RoomNumber].transform.SetParent(bossPoints[i].transform);
            }

        }
    }
}
  
