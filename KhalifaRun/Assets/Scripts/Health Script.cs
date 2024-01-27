using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] int health = 6;
    [SerializeField] GameObject[] heartContainers;
    public GameObject cam;
    private void Update()
    {
   
    }

    public void TakingDamage(int Damage)
    {

        for (int i = 0; i < Damage; i++)
        {
            health--;
            heartContainers[health].GetComponentInChildren<Image>().enabled = false;
            print("shake");
            
        }

    }

    public void GainHearts(int heal)
    {
        for(int i = 0; i< heal; i++)
        {
            health++;
            heartContainers[health].GetComponentInChildren<Image>().enabled=true;
        }
    }

}
