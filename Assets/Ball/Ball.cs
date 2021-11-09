using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager _gameManager;
    private Transform _paddle;
    // Start is called before the first frame update
    void Start()
    {
		_gameManager = FindObjectOfType<GameManager>();
		_paddle = FindObjectOfType<Paddle>().transform;
	}

	// Update is called once per frame
	void Update()
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		if (_gameManager.GetGameState() == GameState.Pregame)
		{
			Vector2 newPosition = transform.position;
			newPosition.x = _paddle.position.x;
			transform.position = newPosition;
		}
	}
}
