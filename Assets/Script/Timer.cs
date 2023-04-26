using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//[RequireComponent(typeof(Slider))]

public class Timer : MonoBehaviour
{
    [SerializeField] private float maxTime;
     private float currentTime;
     private Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        slide = GetComponent<Slider>();
        slide.maxValue = maxTime;
        slide.value = currentTime;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        if(currentTime > maxTime){
            Debug.Log("cabo");
            currentTime = 0f;
        }
        slide.value = currentTime;
    }
}
