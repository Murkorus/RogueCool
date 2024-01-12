using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KitchenBoss : MonoBehaviour
{
    [Header("BodyParts")]
    public GameObject ArmL, armR, Head, Tabel;
    
    
    [Header("Animation")]
    public Animator Anim_armL,Anim_armR,Anim_Head;
    // Start is called before the first frame update
    void Start()
    {
        Anim_armR.Play("aRMSr");
        Anim_armL.Play("armsL");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
