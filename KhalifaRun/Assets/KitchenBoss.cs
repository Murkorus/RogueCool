using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class KitchenBoss : MonoBehaviour
{
    public Feeling Emotion;
    
    [Header("HeadFaces")] public Sprite Pacient, Angry, Normal, Crazy;
    
    [Header("BodyParts")]
    public GameObject ArmL, armR, Head, Tabel;
    [Header("Animation")]
    public Animator Anim_armL,Anim_armR,Anim_Head;
    
    [Header("Attack1")] public float PreparationTimer;
    public Sprite[] Swords;


    public enum Feeling
    {
        pacient,Angry,normal,crazy
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Emotion = (Feeling)Random.Range(0, 4);
            PrepareAttack();
        }
    }


    public void PrepareAttack()
    {
        switch (Emotion)
        {
            case Feeling.Angry:
                Head.GetComponent<SpriteRenderer>().sprite = Angry;
                PreparationTimer = 1f;
                break;
            
            case Feeling.crazy:
                Head.GetComponent<SpriteRenderer>().sprite = Crazy;
                PreparationTimer = 0.4f;
                break;
            
            case Feeling.pacient:
                PreparationTimer = 5f;
                Head.GetComponent<SpriteRenderer>().sprite = Pacient;
                break;
            
            case Feeling.normal:
                Head.GetComponent<SpriteRenderer>().sprite = Normal;
                PreparationTimer = 2;
            break;
        }
        PreparationTime();
    }

    public void PreparationTime()
    {
        StartCoroutine(Attack1());
    }


    public IEnumerator Attack1()
    {
        Anim_armL.Play("Boss_ArmL");
        yield return new WaitForSeconds(0.1f);
        Anim_armR.Play("Boss_ArmR");
        
        yield return new WaitForSeconds(PreparationTimer);
        
        Anim_armL.Play("KnifeLaunch");
        yield return new WaitForSeconds(0.2f);
        Anim_armR.Play("KnifeLaunchR");
        
        yield return new WaitForSeconds(0.1f);
        
        Anim_armL.Play("IdleL");
        yield return new WaitForSeconds(0.1f);
        Anim_armR.Play("IdleR");
        
    }


}
