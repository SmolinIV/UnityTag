using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResults : MonoBehaviour
{
    [SerializeField] public Text bestTimeScore;
    [SerializeField] public Text bestStepsScore;
    [HideInInspector] public GameObject objBoard;
    [HideInInspector] public BoardManip board;
    [HideInInspector] public Timer timer;
    [HideInInspector] public int bestTimeInseconds;
    [HideInInspector] public int newTimeInSeconds;
    [HideInInspector] public int stepsScore;

    public void Awake()
    {
        objBoard = GameObject.FindGameObjectWithTag("board");
        board = objBoard.GetComponent<BoardManip>();
        timer = objBoard.GetComponent<Timer>();
        bestTimeInseconds = int.MaxValue;
        stepsScore = int.MaxValue;
    }

    public void Update()
    {
        if (board.isPlayerWin == true)
        {
            newTimeInSeconds = timer.sec + timer.min * 60;
            if (bestTimeInseconds > newTimeInSeconds)
            {
                bestTimeInseconds = newTimeInSeconds;
                bestTimeScore.text = timer.timerText.text;
            }

            if (stepsScore > board.playerSteps)
            {
                stepsScore = board.playerSteps;
                bestStepsScore.text = stepsScore.ToString();
            }
        }
    }
}
