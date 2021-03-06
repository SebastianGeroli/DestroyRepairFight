﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
	[SerializeField]
	GameObject pickaxeSlot;
	[SerializeField]
	Animator animator;
	[SerializeField]
	RockChunk rockChunkPrefab;
	[SerializeField]
	Transform rockChunkThrowTransform;
	[SerializeField]
	GameObject rockChankInHand;

	[Header("UI")]
	[SerializeField]
	GameObject arrow;
	[SerializeField]
	GameObject area;

	List<Player> playersInRange = new List<Player>();
	List<BigRock> bigRocksdInRange = new List<BigRock>();
	List<RockChunk> rockChunksInRange = new List<RockChunk>();
	[SerializeField]
	float throwForce = 50f;
	[Header("Settings")]
	public float pickaxeDot = 0.7f;
	private bool hasRockChunk = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>() != null)
		{
			playersInRange.Add(other.GetComponent<Player>());
		}
		if (other.GetComponent<BigRock>() != null)
		{
			bigRocksdInRange.Add(other.GetComponent<BigRock>());
		}
		if (other.GetComponent<RockChunk>() != null)
		{
			rockChunksInRange.Add(other.GetComponent<RockChunk>());
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<Player>() != null)
		{
			playersInRange.Remove(other.GetComponent<Player>());
		}
		if (other.GetComponent<BigRock>() != null)
		{
			bigRocksdInRange.Remove(other.GetComponent<BigRock>());
		}
		if (other.GetComponent<RockChunk>() != null)
		{
			rockChunksInRange.Remove(other.GetComponent<RockChunk>());
		}
	}

	public void HidePickaxe()
	{
		pickaxeSlot.SetActive(false);
		area.SetActive(false);
		arrow.SetActive(false);
		Debug.Log("HideArea");
	}



	public void ShowPickaxe()
	{
		pickaxeSlot.SetActive(true);
		area.SetActive(true);
		Debug.Log("ShowArea");
	}

	public void Action()
	{
		if (hasRockChunk)
		{
			ThrowRockChunk();
		}
		else if (rockChunksInRange.Count > 0)
		{
			while (rockChunksInRange.Count > 0)
			{
				if (rockChunksInRange[0] == null)
					rockChunksInRange.RemoveAt(0);
				else
					PickRockChunk(rockChunksInRange[0]);
				break;
			}

		}
		else
		{

			foreach (BigRock rock in bigRocksdInRange)
			{
				Vector3 dir = rock.transform.position - transform.position;
				float dot = Vector3.Dot(transform.forward, dir.normalized);

				if (dot >= pickaxeDot)
				{
					rock.GetHit();
				}
			}

			foreach (Player player in playersInRange)
			{
				Vector3 dir = player.transform.position - transform.position;
				float dot = Vector3.Dot(transform.forward, dir.normalized);

				if (dot >= pickaxeDot)
				{
					player.GetPickaxeToFace(transform.position);
				}
			}
			animator.SetTrigger("attack");

		}
	}
	public void PickRockChunk(RockChunk rockChunk)
	{
		if (!hasRockChunk)
		{
			Debug.Log("PickRockChunk");
			rockChunksInRange.Remove(rockChunk);
			hasRockChunk = true;
			Destroy(rockChunk.gameObject);
			rockChankInHand.SetActive(true);
			animator.SetBool("piedra", true);
			area.SetActive(false);
			arrow.SetActive(true);

		}

	}
	public void DropRockChunk()
	{
		if (hasRockChunk)
		{
			ClearRock();
			
			RockChunk rock = Instantiate(rockChunkPrefab, rockChunkThrowTransform.position, Quaternion.identity);
			Vector3 impulse = Quaternion.Euler(0, Random.Range(0, 360), 0) * rockChunkThrowTransform.forward;
			rock.AddImpulse(impulse);
		}
	}

	public void ClearRock()
	{
		rockChankInHand.SetActive(false);
		hasRockChunk = false;
		animator.SetBool("piedra", false);

		area.SetActive(true);
		arrow.SetActive(false);
	}
	public void ThrowRockChunk()
	{
		if (hasRockChunk)
		{
			Debug.Log("Throw rock");
			RockChunk rock = Instantiate(rockChunkPrefab, rockChunkThrowTransform.position, Quaternion.identity);
			Vector3 impulse = rockChunkThrowTransform.forward * throwForce;
			rock.AddImpulse(impulse);
			animator.SetTrigger("throw");
			ClearRock();
		}

	}


}
