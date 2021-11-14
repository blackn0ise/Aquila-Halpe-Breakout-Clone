using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    Pregame,
    Game
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab = default;
    [SerializeField] private TextMeshProUGUI scoreText = default;
    [SerializeField] private GameObject brickStartPosition = default;
    [SerializeField] private int brickColumns = 10;
    [SerializeField] private int brickRows = 5;
    [SerializeField] private float brickWidthBuffer = 0.2f;
    [SerializeField] private float brickHeightBuffer = 0.2f;
    [SerializeField] private Color[] brickColours = default;
    private GameState currentState = GameState.Pregame;
    private float _brickWidth = 1;
    private float _brickHeight = 1;
    private int _score = 0;
    private static GameManager instance = null;
    public void SetGameState(GameState value) { currentState = value; }
    public void SetScoreText(string value) { scoreText.text = value; }
    public void SetScore(int value) { _score = value; }
    public static GameManager GetGameManager() { return instance; }
    public GameState GetGameState() { return currentState; }
    public string GetScoreText() { return scoreText.text; }
    public int GetScore() { return _score; }

    private void Start()
    {
        instance = this;
        SetScoreText("Score = 0");
        SetBrickDimensions();
        CreateBricks();
    }

    public void RestartGame()
    {
        ClearBricks();
        CreateBricks();
        SetGameState(GameState.Pregame);
        SetScore(0);
        SetScoreText("Score = 0");
    }

    public void ClearBricks()
	{
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
		{
            Destroy(brick);
		}
    }

    public void CreateBricks()
    {
        Vector2 brickPositionIterator = brickStartPosition.transform.position;

        for (int i = 0; i < brickRows; i++)
        {
            for (int j = 0; j < brickColumns; j++)
            {
                GameObject brickInstance = Instantiate(brickPrefab, brickPositionIterator, Quaternion.identity, null);
                brickInstance.name = brickPrefab.name;
                brickPositionIterator.x += _brickWidth + brickWidthBuffer;
                brickInstance.GetComponent<SpriteRenderer>().color = brickColours[i];
            }
            brickPositionIterator.x = brickStartPosition.transform.position.x;
            brickPositionIterator.y -= _brickHeight / 2 + brickHeightBuffer;
        }
    }

    private void SetBrickDimensions()
	{
        var brickCollider = brickPrefab.GetComponentInChildren<BoxCollider2D>();
        _brickWidth = brickCollider.bounds.extents.x;
        _brickHeight = brickCollider.bounds.extents.y;
    }
}
