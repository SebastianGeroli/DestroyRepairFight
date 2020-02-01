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
	public float life, materials;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void GetPickaxeToFace(Vector3 origin)
	{
		rigidbody.AddForce((transform.position - origin + Vector3.up) * punchbackForce, ForceMode.Impulse);
		playerInput.Freeze(0.2f);
	}


}
