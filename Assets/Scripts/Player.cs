using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	float punchbackForce = 1f;
	[SerializeField]
	PlayerInput playerInput;

	[SerializeField]
	new Rigidbody rigidbody;
	[SerializeField]
	public float life , materials;
	[SerializeField]
	Animator animator;

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	private void Fallback(Vector3 force)
	{
		rigidbody.AddForce(force, ForceMode.Impulse);
		playerInput.Freeze(0.2f);
		animator.SetTrigger("hit");
	}
	public void GetPickaxeToFace(Vector3 origin)
	{
		Fallback((transform.position - origin + Vector3.up) * punchbackForce);
	}
	public void GetSwordToFace(Vector3 origin, float damage)
	{
		Fallback((transform.position - origin + Vector3.up) * punchbackForce);
		life = Mathf.Max(0f, life - damage);
		if (life <= 0f)
		{
			Destroy(gameObject);
		}
	}

}
