using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerNumber
{
	One,
	Two,
	Three,
	Four
}
public class PlayerReady : MonoBehaviour
{
	[SerializeField]
	PlayerNumber owner;

	[SerializeField]
	GameObject readyScreen;
	[SerializeField]
	GameObject waitingScreen;

	float wentReadyAt = 0f;

	public bool allSet => PlayerManager.manager.players[(int)owner] ? (wentReadyAt + 2f <= Time.time) : true;
	void Update()
	{
		int id = (int)owner;

		if (Input.GetButtonDown("Action" + owner) && !PlayerManager.manager.players[id])
		{
			PlayerManager.manager.players[id] = true;
			wentReadyAt = Time.time;
		}

		if (Input.GetButtonDown("Cancel" + owner))
		{
			PlayerManager.manager.players[id] = false;
		}

		bool isReady = PlayerManager.manager.players[id];

		readyScreen.SetActive(isReady);
		waitingScreen.SetActive(!isReady);

	}
}
