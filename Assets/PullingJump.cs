using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingJump : MonoBehaviour {
	private Rigidbody rb;
	private Vector3 clickPosition;
	[SerializeField] private float jumpPower = 10;

	private void Awake() {
		rb = gameObject.GetComponent<Rigidbody>();
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			clickPosition = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0)) {
			// クリックした座標と話した座標の差分を取得
			Vector3 dist = clickPosition - Input.mousePosition;
			// クリックとリリースが同じ座標ならば無視
			if (dist.sqrMagnitude == 0) {
				return;
			}

			// 差分を標準化し、jumpPowerをかけ合わせた値を移動量とする
			rb.velocity += dist.normalized * jumpPower;
		}
	}
}