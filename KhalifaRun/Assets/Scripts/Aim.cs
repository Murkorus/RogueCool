using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera maincam;

    public GameObject player;
    void Start()
    {
        maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        float Zrotation = transform.rotation.eulerAngles.z;

        if(Zrotation >=90 && Zrotation <= 270)
        {
            gameObject.transform.localScale =  new Vector3(1,-1,1); 
        }
        else
        {
            gameObject.transform.localScale =  new Vector3(1,1,1); 
        }

        Vector3 mousepos = maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimdirection = mousepos - transform.position;
        float angel = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angel);         
    }
}

