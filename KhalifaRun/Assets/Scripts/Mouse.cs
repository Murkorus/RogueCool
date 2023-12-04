using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
   
    public  Camera cam;
    public  Transform player;
    public  float MaxDistance;
   
    void Update()
    {
        Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetpos = (player.position + mousepos) / 2f;

        targetpos.x = Mathf.Clamp(targetpos.x, -MaxDistance + player.position.x, MaxDistance + player.position.x);
        targetpos.y = Mathf.Clamp(targetpos.y, -MaxDistance + player.position.y, MaxDistance + player.position.y);

        transform.position = targetpos;
    }
}
