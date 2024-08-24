using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAni : MonoBehaviour
{
    public GameObject[] dots;
    int idx = 0;
    float nextTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        idx = dots.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>nextTime){
        if(idx>dots.Length-1){
            idx=0;
            dots[dots.Length-1].SetActive(false);
        }
        dots[idx].SetActive(true);
        if(idx>0){
            dots[idx-1].SetActive(false);
        }

        idx++;
        nextTime = Time.time+ 0.3f;
        }
        
    }
}
