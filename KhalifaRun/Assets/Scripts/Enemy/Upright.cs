using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Upright : MonoBehaviour
{
    private Transform originalTransform;
    // Start is called before the first frame update
    void Start()
    {
        originalTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,transform.rotation.z - originalTransform.rotation.z));
    }
}
