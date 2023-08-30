using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{

    [HideInInspector] public Image buttonImage;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] bool SoundIsOn;
    [HideInInspector] public Color colorON, colorOFF;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        buttonImage = GetComponent<Image>();
        colorON = buttonImage.color;
        colorOFF = buttonImage.color;
        colorOFF.r = 20;
        colorOFF.g = 20;
        colorOFF.b = 20;
        colorOFF.a = 255;
        buttonImage.color = colorOFF;
    }
    void Start()
    {
        SoundIsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onOffMusic() {

        if (SoundIsOn == true) 
        {
            SoundIsOn = false;
            audioSource.volume = 0;
            buttonImage.color = colorON;  
        }
        else
        {
            SoundIsOn = true;
            audioSource.volume = 0.5f;
            buttonImage.color = colorOFF;
        }
    }
}
