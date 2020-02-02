using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateFix : MonoBehaviour
{
	[SerializeField]
	Image fill;
	[SerializeField]
	Player player;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.rotation = Quaternion.identity * Quaternion.Euler(90f,0f,0f);
		fill.fillAmount = Mathf.Clamp01(player.life / player.MaxLife);

	}
}
