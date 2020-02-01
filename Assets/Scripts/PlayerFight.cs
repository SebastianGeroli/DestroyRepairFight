using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
	[SerializeField]
	Animator animator;

	[SerializeField]
	GameObject Sword;

	List<Player> playersInRange = new List<Player>();

	[Header("Settings")]
	public float swordDotCone = 0.7f;

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
				player.GetSwordToFace(transform.position,10f);
			}
		}
		animator.SetTrigger("attack");
	}
}
