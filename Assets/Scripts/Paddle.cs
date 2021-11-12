using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
	[SerializeField] private float speed = 0.05f;
	private Vector2 _movementValues = Vector2.zero;
	private Ball _ball;
	private Vector2 _initialPosition;
	private GameManager _gameManager;

	void Start()
    {
		_ball = FindObjectOfType<Ball>();
		_initialPosition = transform.position;
		_gameManager = GameManager.GetGameManager();

	}

	public void OnMouseMovement(InputAction.CallbackContext value)
	{
		if (Time.time > 0.5f) //input delay on start to prevent startup reads from mouse
		{
			if (true)
			{
				Vector2 newPosition = transform.position;
				newPosition.x += value.ReadValue<Vector2>().x * speed;
				transform.position = newPosition;  
			}
		}
	}

	public void OnBallFired(InputAction.CallbackContext value)
	{
		if (_gameManager.GetGameState() == GameState.Pregame)
		{
			_gameManager.SetGameState(GameState.Game);
			_ball.Fire();
		}
	}

	public void ResetPosition()
	{
		transform.position = _initialPosition;
	}
}
