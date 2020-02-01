using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockChunk : MonoBehaviour
{
	new Rigidbody rigidbody;
	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();

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
