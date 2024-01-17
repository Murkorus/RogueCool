using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
public class Flashligt : MonoBehaviour
{
    private Light2D light;
    [SerializeField] float d;
    void Start()
    {
        d = 50;
    }

    // Update is called once per frame
    void Update()
    {
        light = this.gameObject.GetComponent<Light2D>();
           d = light.intensity;
        
            DOTween.To(() =>light.intensity, x => light.intensity = x, 0f, 1.5f);
            Destroy(this.gameObject,2f);
    }
}
