using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForges : MonoBehaviour
{
	[SerializeField]
	Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		RockChunk chunk = other.GetComponent<RockChunk>();
		if (chunk)
		{
			Destroy(chunk.gameObject);
			player.materials += 1;
		}
	}
}
