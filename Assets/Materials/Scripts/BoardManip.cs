using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    enum DIRECTION
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    public void Awake()
    {
        sortingDone = false;
        numberOfSwapingCubes = 50;
        alreadyMoving = false;
        numberOfCubes = _cubeObjects.Length;
        cubes = new MovingCube[numberOfCubes];
        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes[i] = _cubeObjects[i].GetComponent<MovingCube>();
        }
        emptyCubeGridPosition = numberOfCubes - 1;
    }

    private void Start()
    {

    }

    void Update()
    {
        if (sortingDone)
        {
            for (int i = 0; i < numberOfCubes; ++i)
            {
                cubes[i].transform.position = cubes[i].cubePositionOnScene;
            }
            sortingDone = false;
        }
    }

    public void SortingBoard()
    {
        DIRECTION dir;
        DIRECTION lastDir = DIRECTION.NONE;
        System.Random rand = new System.Random();


        for (int i = 0; i < numberOfSwapingCubes; ) {
            dir = (DIRECTION)rand.Next(0,3);
            if (dir == lastDir) { continue; }
            switch (dir)
            {
                case DIRECTION.UP:
                    if (emptyCubeGridPosition <= 3) { continue; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition - 4);
                    i++;
                    break;
                case DIRECTION.DOWN:
                    if (emptyCubeGridPosition >= 11) { continue; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition + 4);
                    i++;
                    break;
                case DIRECTION.LEFT:
                    if (emptyCubeGridPosition % 4 == 0) { continue; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition -1);
                    i++;
                    break;
                case DIRECTION.RIGHT:
                    if ((emptyCubeGridPosition + 1) % 4 == 0) { continue; }
                    swapCubes(emptyCubeGridPosition, emptyCubeGridPosition + 1);
                    i++;
                    break;
            }
            lastDir = dir;
        }

        sortingDone = true;
    }

    private void swapCubes(int c1, int c2)
    {
        (cubes[c1].positionOnGrid, cubes[c2].positionOnGrid) = (cubes[c2].positionOnGrid, cubes[c1].positionOnGrid);
        (cubes[c1].cubePositionOnScene, cubes[c2].cubePositionOnScene) = (cubes[c2].cubePositionOnScene, cubes[c1].cubePositionOnScene);
        (cubes[c1], cubes[c2]) = (cubes[c2], cubes[c1]);
        emptyCubeGridPosition = c2;
    }
}