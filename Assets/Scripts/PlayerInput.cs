using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	Waiting,
	Collecting,
	Repairing,
	Fighting,
	Dead
}
public class PlayerInput : MonoBehaviour
{
	[SerializeField]
	Renderer[] renderersColor;
	[SerializeField]
	private PlayerNumber owner;
	[SerializeField]
	PlayerMovement movement;
	[SerializeField]
	PlayerCollect playerCollect;
	[SerializeField]
	PlayerRepair playerRepair;
	[SerializeField]
	PlayerFight playerFight;
	[SerializeField]
	float freezeMaxTime = 0.3f;

	[SerializeField]
	private PlayerState state = PlayerState.Waiting;
	public PlayerState State => state;

	Vector2 movementInput = Vector2.zero;
	bool action = false;

	bool isFrozen = false;
	float freezeAt = 0f;
	float freezeTime = 0f;

	public GameObject freezeObject;

	void Start()
	{

	}
	public void SetOwner(PlayerNumber number, Color color)
	{
		foreach(Renderer renderer in renderersColor)
		{
			renderer.material.SetColor("_EmissionColor", color);
		}
		owner = number;
	}
	public void Freeze(float time)
	{
		if (!isFrozen)
		{
			isFrozen = true;
			freezeTime = 0f;
			freezeAt = Time.time;
		}

		freezeTime = Mathf.Clamp(freezeTime + time, 0f, freezeMaxTime);
		freezeObject.SetActive(true);
		movement.Move(Vector2.zero);
	}

	public void Unfreeze()
	{
		if (isFrozen)
		{
			isFrozen = false;
			freezeObject.SetActive(false);
		}
	}


	public void SetState(PlayerState state)
	{
		this.state = state;
		//Waiting
		if (state == PlayerState.Waiting)
		{
			movement.Move(Vector2.zero);

		}
		else
		{

		}

		//Dead
		if (state == PlayerState.Dead)
		{
			movement.Move(Vector2.zero);

		}
		else
		{

		}

		//Collecting
		if (state == PlayerState.Collecting)
		{
			playerCollect.ShowPickaxe();
		}
		else
		{
			playerCollect.ClearRock();
			playerCollect.HidePickaxe();
		}
		//Collecting
		if (state == PlayerState.Repairing)
		{
			playerRepair.ShowHammer();
		}
		else
		{
			playerRepair.HideHammer();

		}
		

		//Fighting
		if (state == PlayerState.Fighting)
		{
			playerFight.ShowWeapon();

		}
		else
		{
			playerFight.HideWeapon();
		}

		
	}
	void Update()
	{
		movementInput.x = Input.GetAxis("Horizontal" + owner);
		movementInput.y = Input.GetAxis("Vertical" + owner);

		action = Input.GetButtonDown("Action" + owner);

		if (isFrozen)
		{

			if (freezeAt + freezeTime <= Time.time)
			{
				Unfreeze();
			}
		}
		else
			switch (state)
			{
				case PlayerState.Waiting:
					movement.Move(Vector2.zero);
					break;
				case PlayerState.Collecting:
					movement.Move(movementInput);
					if (action) playerCollect.Action();
					break;
				case PlayerState.Repairing:
					if (action) playerRepair.Action();

					break;
				case PlayerState.Fighting:
					movement.Move(movementInput);
					if (action) playerFight.Action();
					break;
				case PlayerState.Dead:
					movement.Move(Vector2.zero);

					break;
				default:
					break;
			}
	}
}
