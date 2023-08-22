using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class MovingCube : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] public int _number;
    [SerializeField] public int positionOnGrid;
    [HideInInspector] public Vector3 cubePositionOnScene;


    [HideInInspector] public BoardManip board;
    [HideInInspector] private int diff;
    [HideInInspector] private bool stillMoving;
    [HideInInspector] private bool onPlace;


    private void Awake()
    {
        diff = 0;
        
        board = GetComponentInParent<BoardManip>();
        positionOnGrid = _number - 1;
        stillMoving= false;
    }

    public void Start()
    {
        cubePositionOnScene = transform.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_number == 16) { return; }

        if (!board.alreadyMoving)
        {

            diff = Math.Abs(positionOnGrid - board.emptyCubeGridPosition);
            if (diff == 1 || diff == 4)
            {
                board.slidingCubeNumber = _number;
                board.alreadyMoving = true;
                stillMoving = true;
                diff = positionOnGrid - board.emptyCubeGridPosition;
                cubePositionOnScene = board.cubes[board.emptyCubeGridPosition].transform.position;
                board.cubes[board.emptyCubeGridPosition].transform.position = board.cubes[board.emptyCubeGridPosition].cubePositionOnScene = transform.position;
            }
        }
    }

    void Update()
    {
        if (board.alreadyMoving && _number == board.slidingCubeNumber)
        {
            if (stillMoving)
            {
                switch (diff)
                {
                    case 1: 
                        transform.Translate(-1 * board.cubesSpeed * Time.deltaTime, 0, 0);
                        onPlace = (transform.position.x <= cubePositionOnScene.x);
                        break;
                    case -1:
                        transform.Translate(board.cubesSpeed * Time.deltaTime, 0, 0);
                        onPlace = (transform.position.x >= cubePositionOnScene.x);
                        break;
                    case 4:
                        transform.Translate(0, board.cubesSpeed * Time.deltaTime, 0);
                        onPlace = (transform.position.y >= cubePositionOnScene.y);
                        break;
                    case -4:
                        transform.Translate(0, -1 * board.cubesSpeed * Time.deltaTime, 0);
                        onPlace = (transform.position.y <= cubePositionOnScene.y);
                        break;
                    default:
                        throw new Exception("Error during the move.");
                }

                if (onPlace)
                {
                    transform.position = cubePositionOnScene;
                    (board.cubes[positionOnGrid], board.cubes[board.emptyCubeGridPosition]) = (board.cubes[board.emptyCubeGridPosition], board.cubes[positionOnGrid]);
                    (positionOnGrid, board.emptyCubeGridPosition) = (board.emptyCubeGridPosition, positionOnGrid);
                    board.cubes[board.emptyCubeGridPosition].positionOnGrid = board.emptyCubeGridPosition;
                    board.alreadyMoving = false;
                    stillMoving = false;
                    onPlace = false;
                  
                }
            }
        }
    }
}
