using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Stages
{
	ReadyCheck,
	Destroying,
	Reparing,
	Fighting
}
public class StageManager : MonoBehaviour
{
	[System.Serializable]
	private class Stage
	{
		public float duration = 15f;
		public Sprite icon;
		public AudioClip clip;
	}
	[SerializeField]
	GameObject musicMasterFirst;
	[SerializeField]
	GameObject musicMasterSecond;
	[SerializeField]
	GameObject musicMaster;
	[SerializeField]
	GameObject winScreen;
	[SerializeField]
	Image winImage;

	[SerializeField]
	float betweenTime = 3f;
	[SerializeField]
	Stage[] stages = new Stage[3];
	[Header("Audio")]
	[SerializeField]
	AudioSource audioSource;
	[SerializeField]
	AudioSource musicSource;
	[SerializeField]
	AudioClip clipStageEnd;
	[SerializeField]
	AudioClip clipWin;


	bool checkWin = false;
	int whoWon = -1;
	[Header("Screens")]
	[SerializeField]
	GameObject stageScreen;
	[SerializeField]
	Image stageIcon;
	[Header("Players")]
	[SerializeField]
	Color[] playerColors = new Color[4];
	[SerializeField]
	PlayerInput[] players = new PlayerInput[4];
	[SerializeField]
	PlayerForges[] forges = new PlayerForges[4];
	//[SerializeField]
	//PlayerCollect[] playersCollects = new PlayerCollect[4]; 
	private bool spawn;
	void Start()
	{
		StartCoroutine(FirstSequence());
	}

	public void CheckWin()
	{
		int playersAlive = 0;
		foreach (PlayerInput player in players)
		{
			if (player.State == PlayerState.Fighting && player.gameObject.activeInHierarchy)
			{
				playersAlive += 1;
			}
		}
		if (playersAlive == 1)
		{
			for (int i = 0; i < 4; i++)
			{
				if (players[i].State == PlayerState.Fighting)
				{
					Debug.Log("Player " + (i + 1) + "Has won");
					audioSource.PlayOneShot(clipWin);
					checkWin = false;
					whoWon = i;
					return;
				}
			}
		}
	}

	IEnumerator FirstSequence()
	{
		// First Round
		winScreen.SetActive(false);
		stageScreen.SetActive(false);
		for (int i = 0; i < 4; i++)
		{
			players[i].SetState(PlayerState.Waiting);
		}
		yield return new WaitForSeconds(betweenTime);
		audioSource.PlayOneShot(stages[0].clip);
		stageScreen.SetActive(true);
		stageIcon.sprite = stages[0].icon;
		spawn = true;

		yield return null;
		for (int i = 0; i < 4; i++)
		{
			players[i].gameObject.SetActive(PlayerManager.manager.players[i]);
			players[i].SetState(PlayerState.Waiting);
			players[i].SetOwner((PlayerNumber)i, playerColors[i]);
			Debug.Log(i);
			forges[i].gameObject.SetActive(PlayerManager.manager.players[i]);
			yield return null;
		}
		yield return new WaitForSeconds(3f);

		stageScreen.SetActive(false);

		for (int i = 0; i < 4; i++)
		{
			players[i].SetState(PlayerState.Collecting);
		}

		//Round End-----------------------------
		yield return new WaitForSeconds(stages[0].duration);
		spawn = false;



		audioSource.PlayOneShot(clipStageEnd);
		for (int i = 0; i < 4; i++)
		{
			players[i].SetState(PlayerState.Waiting);
		}
		yield return new WaitForSeconds(betweenTime);
		//Round End-----------------------------

		// Second Stage screen ########################
		audioSource.PlayOneShot(stages[1].clip);
		stageScreen.SetActive(true);
		stageIcon.sprite = stages[1].icon;
		musicMasterFirst.SetActive(false);
		musicMasterSecond.SetActive(true);

		for (int i = 0; i < 4; i++)
		{
			players[i].transform.position = forges[i].transform.position;
			players[i].SetState(PlayerState.Waiting);
		}
		yield return new WaitForSeconds(3f);
		// Second Stage screen #######################

		stageScreen.SetActive(false);
		for (int i = 0; i < 4; i++)
		{
			players[i].SetState(PlayerState.Repairing);
		}

		//Round End-----------------------------
		yield return new WaitForSeconds(stages[1].duration);
		audioSource.PlayOneShot(clipStageEnd);
		for (int i = 0; i < 4; i++)
		{
			players[i].SetState(PlayerState.Waiting);
		}
		yield return new WaitForSeconds(betweenTime);
		//Round End-----------------------------


		// Second Stage screen ###########################
		audioSource.PlayOneShot(stages[2].clip);
		stageScreen.SetActive(true);
		stageIcon.sprite = stages[2].icon;
		musicMasterSecond.SetActive(false);
		musicMaster.SetActive(true);

		for (int i = 0; i < 4; i++)
		{
			players[i].SetState(PlayerState.Waiting);
		}
		yield return new WaitForSeconds(3f);
		// Second Stage screen ###########################
		stageScreen.SetActive(false);

		for (int i = 0; i < 4; i++)
		{
			players[i].SetState(PlayerState.Fighting);
		}
		checkWin = true;


		while (whoWon < 0)
			yield return null;

		winScreen.SetActive(true);
		winImage.color = playerColors[whoWon];

		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(0);
	}
	// Update is called once per frame
	void Update()
	{
		if (checkWin)
		{
			CheckWin();
		}
		if (spawn)
		{

		}
	}
}
