using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public void DeleteSelf()
	{
		AwardPoints();
		Destroy(gameObject);
	}

	private void AwardPoints()
	{
		Debug.Log("YouGetPoint");
	}
}
