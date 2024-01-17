using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using DG.Tweening;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Serialization;
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
    public GameObject Sword;
    public int SwordsAmount;
    public Transform spawnPoint;
    
    
    private float startAngel = 90f, EndAngel = 270f;
    private Vector2 BulletLainchDir;


    public enum Feeling
    {
        pacient,Angry,normal,crazy
    }
    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(10000,1000);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrepareAttack();
        }
    }


    public void PrepareAttack()
    {
        Emotion = (Feeling)Random.Range(0, 4);
        switch (Emotion)
        {
            case Feeling.Angry:
                SwordsAmount = 15;
                Head.GetComponent<SpriteRenderer>().sprite = Angry;
                PreparationTimer = 1f;
                break;
            
            case Feeling.crazy:
                SwordsAmount = 20;
                Head.GetComponent<SpriteRenderer>().sprite = Crazy;
                PreparationTimer = 0.4f;
                break;
            
            case Feeling.pacient:
                SwordsAmount = 30;
                PreparationTimer = 5f;
                Head.GetComponent<SpriteRenderer>().sprite = Pacient;
                break;
            
            case Feeling.normal:
                SwordsAmount = 10;
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
        yield return new WaitForSeconds(PreparationTimer);
        
        Anim_armL.Play("Boss_ArmL");
        yield return new WaitForSeconds(0.1f);
        Anim_armR.Play("Boss_ArmR");
        yield return new WaitForSeconds(0.1f);
        Anim_armL.Play("KnifeLaunch");
        yield return new WaitForSeconds(0.2f);
        LaunchKnives();
        
        SwordsAmount /= 2;
        
        Anim_armR.Play("KnifeLaunchR");
        yield return new WaitForSeconds(0.1f);
        LaunchKnives();
        Anim_armL.Play("IdleL");
        yield return new WaitForSeconds(0.1f);
        Anim_armR.Play("IdleR");
        yield return new WaitForSeconds(0.2f);
        PrepareAttack();
    }


    public void LaunchKnives()
    {
        
        float angelstep = (EndAngel - startAngel) / SwordsAmount;
        float angel = startAngel;

        for (int i = 0; i < SwordsAmount; i++)
        {
            int SwordType = Random.Range(0, Swords.Length);
            
            float SwordDirX = transform.position.x + Mathf.Sin((angel * Mathf.PI) / 180);
            float SwordDirY = transform.position.y + Mathf.Cos((angel * Mathf.PI) / 180);

            Vector3 swordMoveVector = new Vector3(SwordDirX, SwordDirY, 0);
            Vector2 swordDir = (swordMoveVector-transform.position).normalized;

            GameObject InstatiatetSword = Instantiate(Sword, spawnPoint.transform.position, transform.rotation);
            
            InstatiatetSword.GetComponent<BossKnife>().rotateSprite.GetComponent<SpriteRenderer>().sprite = Swords[SwordType];
            
            InstatiatetSword.GetComponent<BossKnife>().SetMoveDiraction(swordDir);

            angel += angelstep;

        }
    }

}
