using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hands : MonoBehaviour
{
    private Vector3 mousepos;
    public Camera cam;
    private Rigidbody2D rb;
    private Camera maincam;
    public float Zrotation;
    void Start()
    {
        maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        Zrotation = transform.rotation.eulerAngles.z;

        if(Zrotation >=90 && Zrotation <= 270)
        {
            gameObject.transform.localScale =  new Vector2(1,-1); 
        }
        else
        {
            gameObject.transform.localScale =  new Vector2(1,1); 
        } 

        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimdirection = mousepos - transform.position;
        float angel = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angel);         
    }
}
