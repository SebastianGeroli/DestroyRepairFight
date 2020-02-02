using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	public static PlayerManager manager;
	public bool[] players = new bool[4];
	void Awake()
	{
		if (manager != null && manager != this)
		{
			GameObject.Destroy(this);
		}
		else
		{
			manager = this;
		}
		DontDestroyOnLoad(this);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
