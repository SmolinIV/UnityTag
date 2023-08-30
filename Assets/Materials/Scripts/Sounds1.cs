using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sounds1 : MonoBehaviour
{
    [HideInInspector] public Image buttonImage;
    [HideInInspector] public Color ColorOff, colorOn;
    [HideInInspector] public bool soundIsOn;

    public void Awake()
    {
        buttonImage = GetComponent<Image>();
        ColorOff = buttonImage.color;
        colorOn = buttonImage.color;
        colorOn.r = 20;
        colorOn.g = 20;
        colorOn.b = 20;
        colorOn.a = 255;
        buttonImage.color = colorOn;
    }
    void Start()
    {
        soundIsOn = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onOffMusic()
    {
        if (soundIsOn == true)
        {
            soundIsOn = false;
            buttonImage.color = ColorOff;
        }
        else
        {
            soundIsOn = true;
            buttonImage.color = colorOn;
        }
    }
}
