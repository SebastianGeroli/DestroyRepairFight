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
	PlayerNumber owner;
	[SerializeField]
	PlayerMovement movement;
	[SerializeField]
	PlayerCollect playerCollect;
	[SerializeField]
	float freezeMaxTime = 0.3f;

	[SerializeField]
	PlayerState state = PlayerState.Waiting;
	Vector2 movementInput = Vector2.zero;
	bool action = false;

	bool isFrozen = false;
	float freezeAt = 0f;
	float freezeTime = 0f;

	public GameObject freezeObject;

	void Start()
	{

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
					movement.Move(Vector2.zero);

					break;
				case PlayerState.Fighting:
					movement.Move(movementInput);

					break;
				case PlayerState.Dead:
					movement.Move(Vector2.zero);

					break;
				default:
					break;
			}
	}
}
