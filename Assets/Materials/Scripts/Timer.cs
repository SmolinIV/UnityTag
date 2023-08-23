using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [HideInInspector] public int sec, min;
    [HideInInspector] private Text timerText;

    public void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeFlow()
    {
        while (true)
        {
            if (sec== 60)
            {
                sec = -1;
                min++;
            }
            sec++;
            yield return new WaitForSeconds(1);
        }
    }
}
