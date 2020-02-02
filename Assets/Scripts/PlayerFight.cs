using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
	[System.Serializable]
	private class Upgrade
	{
		public float damageNeeded = 5f;
		public float bonusDamage = 0f;
		public float damageIncrement = 0f;
		public GameObject swordModel;
	}
	[SerializeField]
	GameObject attackArea;
	[SerializeField]
	GameObject lifeBar;
	[SerializeField]
	GameObject weaponSlot;
	[SerializeField]
	Animator animator;
	[SerializeField]
	Upgrade[] upgrades = new Upgrade[0];


	List<Player> playersInRange = new List<Player>();

	[Header("Settings")]
	public float swordDotCone = 0.7f;

	[SerializeField]
	float swordDamage = 1f;

	private int currentUpgrade = 0;

	private void Start()
	{
		currentUpgrade = -1;
		UpgradeWeapon();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>() != null)
		{
			playersInRange.Add(other.GetComponent<Player>());
		}

	}
	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<Player>() != null)
		{
			playersInRange.Remove(other.GetComponent<Player>());
		}

	}

	public void AddDamage()
	{
		swordDamage += upgrades[currentUpgrade + 1].damageIncrement;
		if (currentUpgrade < upgrades.Length - 1)
		{
			if (swordDamage >= upgrades[currentUpgrade + 1].damageNeeded)
			{
				UpgradeWeapon();
			}
		}
	}
	public void ShowWeapon()
	{
		attackArea.SetActive(true);
		weaponSlot.SetActive(true);
		lifeBar.SetActive(true);
	}
	public void HideWeapon()
	{
		attackArea.SetActive(false);
		weaponSlot.SetActive(false);
		lifeBar.SetActive(false);

	}
	private void UpgradeWeapon()
	{
		if (currentUpgrade >= 0)
			upgrades[currentUpgrade].swordModel.SetActive(false);
		currentUpgrade += 1;
		upgrades[currentUpgrade].swordModel.SetActive(true);
		swordDamage += upgrades[currentUpgrade].bonusDamage;
	}

	public void Action()
	{
		while (playersInRange.Count > 0)
		{
			if (playersInRange[0] == null)
				playersInRange.RemoveAt(0);
			break;
		}
		foreach (Player player in playersInRange)
		{

			Vector3 dir = player.transform.position - transform.position;
			float dot = Vector3.Dot(transform.forward, dir.normalized);

			if (dot >= swordDotCone)
			{
				player.GetSwordToFace(transform.position, 10f);
			}
		}
		animator.SetTrigger("attack");
	}
}
