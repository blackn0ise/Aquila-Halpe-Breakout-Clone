using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Pregame,
    Game
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject brick = default;
    [SerializeField] private GameObject brickStartPosition = default;
    [SerializeField] private int brickColumns = 10;
    [SerializeField] private int brickRows = 5;
    [SerializeField] private float brickWidthBuffer = 0.2f;
    [SerializeField] private float brickHeightBuffer = 0.2f;
    private GameState currentState = GameState.Pregame;
    private float brickWidth = 1;
    private float brickHeight = 1;
    public void SetGameState(GameState value) { currentState = value; }
    public GameState GetGameState() { return currentState; }

    private void Start()
    {
        SetBrickDimensions();
        CreateBricks();
    }

    private void SetBrickDimensions()
	{
        Debug.Log(brick.GetComponent<BoxCollider2D>().bounds);
        brickWidth = brick.GetComponent<BoxCollider2D>().bounds.extents.x;
        brickHeight = brick.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

	private void CreateBricks()
	{
        Vector2 brickPositionIterator = brickStartPosition.transform.position;

        for (int i = 0; i < brickRows; i++)
		{
            for (int j = 0; j < brickColumns; j++)
            {
                Instantiate(brick, brickPositionIterator, Quaternion.identity, null);
                brickPositionIterator.x += brickWidth + brickWidthBuffer;
            }
            brickPositionIterator.x = brickStartPosition.transform.position.x;
            brickPositionIterator.y -= brickHeight/2 + brickHeightBuffer;
        }
    }
}
