using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossKnife : MonoBehaviour
{
       public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
       public float speed = 1f;
       public int damage;
       public GameObject rotateSprite;
       private Vector2 MoveDir;
    
       // Start is called before the first frame update
       void Start()
       {
          Destroy(gameObject,bulletLife);
          
          rotateSprite.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1,LoopType.Restart);
       }
   
   
       // Update is called once per frame
       void Update()
       {
           transform.Translate(MoveDir * speed * Time.deltaTime);
           
       }

       public void SetMoveDiraction(Vector2 Dir)
       {
           MoveDir = Dir;
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.CompareTag("Player"))
           {
               other.GetComponent<Stats>().health =- damage;
               other.GetComponent<HealthScript>().TakingDamage(damage);
            Destroy(gameObject);
           }
       }
}

