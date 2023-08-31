using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [SerializeField] public GameObject objBoard;
    [HideInInspector] public BoardManip board;
    // Start is called before the first frame update
   
    // Start is called before the first frame update
    public void Awake()
    {
        board = objBoard.GetComponent<BoardManip>();
    }

    public void Update()
    {
        if (board.numberofVictories % 2 == 0)
        {
            ShowAdv();
        }
    }

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [DllImport("__Internal")]
    private static extern void RateGame();



    public void RateGameButton()
    {
        RateGame();
    }
}
