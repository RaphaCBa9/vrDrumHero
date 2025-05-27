using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOnAudio : MonoBehaviour
{
    public int band;
    public float minIntensity, maxIntensity;
    Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = (AudioPeer.audioBandBuffer[band] * (maxIntensity - minIntensity)) + minIntensity;
    }
}
