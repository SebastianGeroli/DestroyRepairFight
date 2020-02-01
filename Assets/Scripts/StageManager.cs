using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Stages {
ReadyCheck,
Destroying,
Reparing,
Fighting
}
public class StageManager : MonoBehaviour
{
	[SerializeField]
	float firstStageTime = 20f;
	[SerializeField]
	float secondStageTime = 10f;


	[SerializeField]
	GameObject firstStageScreen;
	[SerializeField]
	GameObject secondStageSCreen;
	[SerializeField]
	GameObject thirdStageScreen;
	[SerializeField]
	PlayerInput[] players = new PlayerInput[4];
	[SerializeField]
	PlayerForges[] forges = new PlayerForges[4];
	void Start()
    {
		StartCoroutine(FirstSequence());
    }


	IEnumerator FirstSequence()
	{
		// First Round
		thirdStageScreen.SetActive(false);
		secondStageSCreen.SetActive(false);
		firstStageScreen.SetActive(true);
		for (int i=0; i < 4; i++)
		{
			players[i].gameObject.SetActive( PlayerManager.manager.players[i]);
			players[i].state = PlayerState.Waiting;
			forges[i].gameObject.SetActive( PlayerManager.manager.players[i]);
		}
		yield return new WaitForSeconds(3f);

		firstStageScreen.SetActive(false);

		for (int i = 0; i < 4; i++)
		{
			players[i].state = PlayerState.Collecting;
		}

		yield return new WaitForSeconds(firstStageTime);

		// Second Stage
		secondStageSCreen.SetActive(true);
		for (int i = 0; i < 4; i++)
		{
			players[i].transform.position = forges[i].transform.position;
			players[i].state = PlayerState.Waiting;
		}
		yield return new WaitForSeconds(3f);
		secondStageSCreen.SetActive(false);

		for (int i = 0; i < 4; i++)
		{
			players[i].state = PlayerState.Repairing;
		}

		yield return new WaitForSeconds(secondStageTime);

		// Second Stage
		thirdStageScreen.SetActive(true);
		for (int i = 0; i < 4; i++)
		{
			players[i].state = PlayerState.Waiting;
		}
		yield return new WaitForSeconds(3f);
		thirdStageScreen.SetActive(false);

		for (int i = 0; i < 4; i++)
		{
			players[i].state = PlayerState.Fighting;
		}

	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
