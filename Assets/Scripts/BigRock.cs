using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRock : MonoBehaviour
{
	new Rigidbody rigidbody;

	public GameObject hitParticles;

	public void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	public void GetHit()
	{
		Instantiate(hitParticles, transform.position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
	}



}
