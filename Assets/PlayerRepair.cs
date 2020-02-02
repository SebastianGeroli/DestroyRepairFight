using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepair : MonoBehaviour
{
	[SerializeField]
	AudioSource audioSource;
	[SerializeField]
	AudioClip hammerClip;
	[SerializeField]
	GameObject hammerSlot;
	[SerializeField]
	Player player;
	[SerializeField]
	Animator animator;
	[SerializeField]
	PlayerFight playerFight;

	float materialTop = 20f;
	float currentClicks = 0;
	float clicksToUpgrade = 3;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	internal void HideHammer()
	{
		hammerSlot.SetActive(false);
	}

	internal void ShowHammer()
	{
		hammerSlot.SetActive(true);
	}

	public void Action()
	{
		animator.SetTrigger("attack");
		currentClicks += Mathf.Lerp(1f, 3f, Mathf.Sqrt(Mathf.Clamp01(player.materials/ materialTop)));
		if (currentClicks >= clicksToUpgrade)
		{
			player.materials -= 1;
			currentClicks = 0;
			playerFight.AddDamage();
			if (audioSource.isPlaying)
			{

			}
			else {
				audioSource.PlayOneShot(hammerClip);
			}
		}
	}
}
