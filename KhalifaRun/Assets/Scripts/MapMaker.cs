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


    int numberOfDoors;
    void Start()
    {

    }
}
  
