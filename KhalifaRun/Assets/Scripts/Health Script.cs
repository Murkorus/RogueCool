using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] int health = 6;
    [SerializeField] GameObject[] heartContainers;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            TakingDamage(1);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            GainHearts(1);
        }
    }

    public void TakingDamage(int Damage)
    {

        for (int i = 0; i < Damage; i++)
        {
            health--;
            heartContainers[health].GetComponentInChildren<Image>().enabled = false;
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
