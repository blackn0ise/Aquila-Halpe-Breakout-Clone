using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
	Horizontal, Vertical
}

public class Ball : MonoBehaviour
{
	
	[SerializeField] Rigidbody2D _rigidbody = default;
	[SerializeField] float startingSpeedFactor = 50;
	[SerializeField] float _startCone = 0.3f;
	private GameManager _gameManager;
    private Paddle _paddle;
	private Vector2 _initialPosition;

	// Start is called before the first frame update
	void Start()
    {
		_initialPosition = transform.position;
		_gameManager = FindObjectOfType<GameManager>();
		_paddle = FindObjectOfType<Paddle>();
	}

	// Update is called once per frame
	void Update()
	{
		UpdatePosition();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CheckOther(collision);
	}

	private void CheckOther(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Paddle"))
		{
			ReverseDirection(Directions.Vertical);
		}
		else if (collision.gameObject.CompareTag("Brick"))
		{
			ReverseDirection(Directions.Vertical);
			Brick brick = collision.gameObject.GetComponent<Brick>();
			brick.DeleteSelf();
		}
		else if (collision.gameObject.CompareTag("Wall"))
		{
			ReverseDirection(Directions.Horizontal);
		}
		else if (collision.gameObject.CompareTag("Ceiling"))
		{
			ReverseDirection(Directions.Vertical);
		}
		else if (collision.gameObject.CompareTag("KillZone"))
		{
			Respawn();
		}
	}

	private void Respawn()
	{
		RepositionSelfAndPaddle();
		_gameManager.RestartGame();
	}

	private void RepositionSelfAndPaddle()
	{
		_rigidbody.velocity = Vector2.zero;
		_paddle.ResetPosition();
		transform.position = _initialPosition;
	}

	private void ReverseDirection(Directions direction)
	{
		Vector2 newVelocity = _rigidbody.velocity;

		switch(direction)
		{
			case Directions.Horizontal:
				newVelocity.x = -_rigidbody.velocity.x;
				_rigidbody.velocity = newVelocity;
				break;

			case Directions.Vertical:
				newVelocity.y = -_rigidbody.velocity.y;
				_rigidbody.velocity = newVelocity;
				break;

		}
	}

	private void UpdatePosition()
	{
		if (_gameManager.GetGameState() == GameState.Pregame)
		{
			Vector2 newPosition = transform.position;
			newPosition.x = _paddle.transform.position.x;
			transform.position = newPosition;
		}
	}

	internal void Fire()
	{
		Vector2 direction = new Vector2(UnityEngine.Random.Range(-_startCone, _startCone), UnityEngine.Random.Range(0, 1.0f));
		Debug.Log(direction);
		_rigidbody.AddForce(direction * startingSpeedFactor); 
	}
}
