using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoardManip : MonoBehaviour
{
    [SerializeField] public float cubesSpeed;
    [SerializeField] public GameObject[] _cubeObjects;
    [HideInInspector] public MovingCube[] cubes;
    [HideInInspector] public int numberOfCubes;
    [HideInInspector] public bool alreadyMoving;
    [HideInInspector] public int movingCubeNumber;
    [HideInInspector] public int emptyCubeGridPosition;
    [HideInInspector] public int slidingCubeNumber;

    [HideInInspector] public int numberOfSwapingCubes;
    [HideInInspector] public bool sortingDone;
    [HideInInspector] public bool puzzleAssembled;
    [HideInInspector] public bool gameIsStarting;
    [HideInInspector] public int playerSteps;
    [HideInInspector] public int bestStepsScore;
    [HideInInspector] public int best_time;
    [SerializeField] public Text objStep;
    [SerializeField] public GameObject OK;
    [SerializeField] public AudioSource OKsound;
    [HideInInspector] public bool isPlayerWin;
    [HideInInspector] public bool soundsON;

    [HideInInspector] public enum DIRECTION
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    public void Awake()
    {
        soundsON = true;
        OKsound = GetComponent<AudioSource>();
        playerSteps = 0;
        bestStepsScore = 10000;
        gameIsStarting = false;
        sortingDone = false;
        numberOfSwapingCubes = 100;
        alreadyMoving = false;
        numberOfCubes = _cubeObjects.Length;
        cubes = new MovingCube[numberOfCubes];
        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes[i] = _cubeObjects[i].GetComponent<MovingCube>();
        }
        emptyCubeGridPosition = numberOfCubes - 1;
        OK.gameObject.SetActive(false);
    }

    private void Start()
    {
        puzzleAssembled = true;
        isPlayerWin = false;
    }

    void Update()
    {
        objStep.text = playerSteps.ToString();

        if (sortingDone)
        {
            float deltaX, deltaY;
            sortingDone = false;
            for (int i = 0; i < numberOfCubes; ++i)
            {
               if (cubes[i].transform.position != cubes[i].cubePositionOnScene)
                {
                    sortingDone = true;
                    deltaX = cubes[i].transform.position.x - cubes[i].cubePositionOnScene.x;
                    deltaY = cubes[i].transform.position.y - cubes[i].cubePositionOnScene.y;
                    cubes[i].transform.Translate(cubesSpeed * Time.deltaTime * (deltaX > 0 ? (-1) : 1), cubesSpeed * Time.deltaTime * (deltaY > 0 ? (-1) : 1), 0);
                    if (Math.Abs(cubes[i].transform.position.x - cubes[i].cubePositionOnScene.x) <= 0.1 &&
                        Math.Abs(cubes[i].transform.position.y - cubes[i].cubePositionOnScene.y) <= 0.1)
                    {
                        cubes[i].transform.position = cubes[i].cubePositionOnScene;
                    }
                }
            }
        }

        if (!puzzleAssembled && cubes[numberOfCubes-1]._number == numberOfCubes)
        {
            foreach(var cube in cubes)
            {
                if(cube._number != cube.positionOnGrid+1) { return; }
            }

            puzzleAssembled = true;
            gameIsStarting = false;
            OK.gameObject.SetActive(true);
            isPlayerWin = true;
            if (soundsON == true)
            {
                OKsound.Play();
            }
        }

    }

    public void SortingBoard()
    {
        OK.gameObject.SetActive (false);
        isPlayerWin = false;
        System.Random rand = new System.Random();
        DIRECTION dir;
        DIRECTION lastDir = DIRECTION.NONE;

        for (int i = 0; i < numberOfSwapingCubes;)
        {
            dir = (DIRECTION)(rand.Next(0, 100) % 4);

            switch (dir)
            {
                case DIRECTION.UP:
                    if (emptyCubeGridPosition <= 3 || (lastDir == DIRECTION.DOWN)) { break; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition - 4);
                    i++;
                    break;
                case DIRECTION.DOWN:
                    if (emptyCubeGridPosition >= 11 || (lastDir == DIRECTION.UP)) { break; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition + 4);
                    i++;
                    break;
                case DIRECTION.LEFT:
                    if (emptyCubeGridPosition % 4 == 0 || (lastDir == DIRECTION.RIGHT)) { break; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition - 1);
                    i++;
                    break;
                case DIRECTION.RIGHT:
                    if ((emptyCubeGridPosition + 1) % 4 == 0 || (lastDir == DIRECTION.LEFT)) { break; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition + 1);
                    i++;
                    break;
            }

        }

        sortingDone = true;
        puzzleAssembled = false;
        gameIsStarting = true;

        if (playerSteps < bestStepsScore)
        {
            bestStepsScore = playerSteps;
        }
        playerSteps = 0;

        
    }

    private void swapCubes(int c1, int c2)
    {
        (cubes[c1].positionOnGrid, cubes[c2].positionOnGrid) = (cubes[c2].positionOnGrid, cubes[c1].positionOnGrid);
        (cubes[c1].cubePositionOnScene, cubes[c2].cubePositionOnScene) = (cubes[c2].cubePositionOnScene, cubes[c1].cubePositionOnScene);
        (cubes[c1], cubes[c2]) = (cubes[c2], cubes[c1]);
        emptyCubeGridPosition = c2;
    }
    public void onOffSoubds()
    {
        soundsON = !soundsON;
    }
}
