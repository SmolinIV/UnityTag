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

    public void Awake()
    {
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
        Debug.Log(emptyCubeGridPosition);
        Debug.Log(_cubeObjects.Length);
    }

    void Update()
    {

    }
}

