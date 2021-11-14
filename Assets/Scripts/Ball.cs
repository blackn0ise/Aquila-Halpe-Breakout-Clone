using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
	Horizontal, Vertical
}

public class Ball : NetworkBehaviour
{

	[SerializeField] Transform spriteBody = default;
	[SerializeField] Rigidbody2D _rigidbody = default;
	[SerializeField] float startingSpeedFactor = 50;
	[SerializeField] float speedIncreaseFactor = 1.01f;
	[SerializeField] float _startCone = 0.3f;
	public List<Paddle> paddles { get; set; }
	private GameManager _gameManager;
	private Vector2 _initialPosition;

	// Start is called before the first frame update
	void Start()
    {
		_initialPosition = transform.position;
		_gameManager = FindObjectOfType<GameManager>();
		paddles = new List<Paddle>();
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
			Brick brick = collision.gameObject.GetComponentInParent<Brick>();
			brick.DeleteSelf();
			_rigidbody.velocity *= speedIncreaseFactor;
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

	public void Respawn()
	{
		RepositionSelfAndPaddle();
		_gameManager.RestartGame();
	}

	private void RepositionSelfAndPaddle()
	{
		_rigidbody.velocity = Vector2.zero;
		transform.position = _initialPosition;
		if (paddles.Count > 0)
			foreach(Paddle paddle in paddles)
			{
				paddle.ResetPosition();
			}
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
		if (paddles.Count > 0 && _gameManager.GetGameState() == GameState.Pregame)
		{
			Vector2 newPosition = transform.position;
			newPosition.x = paddles[0].transform.position.x;
			transform.position = newPosition;
		}
		Quaternion newRotation = spriteBody.rotation;
		//newRotation.SetEulerRotation(newRotation.eulerAngles.x, newRotation.eulerAngles.y, newRotation.eulerAngles.z + 10);
		//transform.rotation = newRotation;
		spriteBody.Rotate(transform.forward, 10);
	}

	internal void Fire()
	{
		Vector2 direction = new Vector2(UnityEngine.Random.Range(-_startCone, _startCone), 0.5f);
		_rigidbody.AddForce(direction * startingSpeedFactor); 
	}
}
