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

	void Start()
    {
		_ball = FindObjectOfType<Ball>();
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
		_ball.Fire();
	}
}
