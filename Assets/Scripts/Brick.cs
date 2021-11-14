using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : NetworkBehaviour
{
	[SerializeField] private int scoreValue = 100;
	private GameManager _gameManager;

	private void Start()
	{
		_gameManager = GameManager.GetGameManager();
	}

	public void DeleteSelf()
	{
		AwardPoints();
		Destroy(gameObject);
	}

	private void AwardPoints()
	{
		GameManager.SetScore(GameManager.GetScore() + scoreValue);
	}
}
