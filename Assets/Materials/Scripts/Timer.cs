using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [HideInInspector] public int sec, min;
    [HideInInspector] public Text timerText;
    [HideInInspector] bool firstStarting = true;
    [HideInInspector] int codeToStartTimer = 1;
    [HideInInspector] public GameObject objBoard;
    [HideInInspector] public BoardManip board;

    public void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        sec = -1;
        min = 0;
        objBoard = GameObject.FindGameObjectWithTag("board");
        board = objBoard.GetComponent<BoardManip>();
    }

    void Update()
    {
    }

    IEnumerator TimeFlow()
    {
        while (true)
        {
            if (board.puzzleAssembled == false)
            {
                if (sec == 60)
                {
                    sec = -1;
                    min++;
                }
                sec++;
                timerText.text = min.ToString("D2") + ":" + sec.ToString("D2");
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void resetTimer(int launchPermissiion)
    {
        if (launchPermissiion == codeToStartTimer)
        {
            if (firstStarting == true)
            {
                StartCoroutine(TimeFlow());
                firstStarting = false;
            }
            else
            {
                sec = 0;
                min = 0;
                timerText.text = min.ToString("D2") + ":" + sec.ToString("D2");
            }
        }

    }
}
