using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
	[SerializeField] private Camera cam;
	[SerializeField] private PullingJump player;

	private void Awake() {
		cam = GetComponent<Camera>();
	}

	void Update() {
		Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

		newPos += player.GetVelocity().normalized * (player.GetVelocity().magnitude * 0.25f);

		transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 5.0f);
	}
}