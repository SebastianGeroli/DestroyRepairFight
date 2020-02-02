using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockChunk : MonoBehaviour
{

	public float value = 3f;
	new Rigidbody rigidbody;
	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void AddImpulse(Vector3 impulse)
	{
		if (rigidbody == null)
			rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = impulse;
	}
}
