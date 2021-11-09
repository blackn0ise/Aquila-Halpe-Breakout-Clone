using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
	[SerializeField] private float speed = 0.05f;
	private Vector2 _movementValues = Vector2.zero;

	void Start()
    {

    }

	public void OnMouseMovement(InputAction.CallbackContext value)
	{
		if (Time.time > 0.5f) //input delay on start to prevent startup reads from mouse
		{
			Vector2 newPosition = transform.position;
			newPosition.x += value.ReadValue<Vector2>().x * speed;
			transform.position = newPosition; 
		}
	}
}
