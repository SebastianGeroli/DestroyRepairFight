using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarterScript : MonoBehaviour
{
	[SerializeField]
	PlayerReady[] playerReadies = new PlayerReady[4];

	[SerializeField]
	GameObject allSetView;
	[SerializeField]
	GameObject notSetView;

	[SerializeField]
	Image fill;
	float filled = 0f;
	float counterOfPlayers = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		bool allSet = true;
		foreach (PlayerReady player in playerReadies)
		{
			allSet &= player.allSet;
		}

		allSetView.SetActive(allSet);
		notSetView.SetActive(!allSet);
		if (allSet && Input.GetButton("Submit"))
		{
			filled += Time.deltaTime;
			if(filled >= 2f)
				SceneManager.LoadScene(1);

		}
		else
		{
			filled = 0f;
		}
		fill.fillAmount = filled / 2f;
    }
}
