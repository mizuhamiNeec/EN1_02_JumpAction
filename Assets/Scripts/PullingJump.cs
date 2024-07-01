using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingJump : MonoBehaviour {
	private Rigidbody rb;
	private Vector3 clickPosition;
	[SerializeField] private float jumpPower = 10;

	private bool isCanJump;

	private void Awake() {
		rb = gameObject.GetComponent<Rigidbody>();
	}

	private void Update() {
		if(Input.GetMouseButtonDown(0)) {
			clickPosition = Input.mousePosition;
		}

		if(isCanJump && Input.GetMouseButtonUp(0)) {
			// クリックした座標と話した座標の差分を取得
			Vector3 dist = clickPosition - Input.mousePosition;
			// クリックとリリースが同じ座標ならば無視
			if(dist.sqrMagnitude == 0) {
				return;
			}

			// 差分を標準化し、jumpPowerをかけ合わせた値を移動量とする
			rb.velocity += dist.normalized * jumpPower;
		}
	}

	private void OnCollisionStay(Collision other) {
		// 衝突している点の情報が複数格納されている
		ContactPoint[] contacts = other.contacts;

		// 0番目の衝突情報から、衝突している点の法線を取得
		Vector3 otherNormal = contacts[0].normal;

		// 上方向を示すベクトル、長さは1
		Vector3 upVector = Vector3.up;
		// 上方向と法線の内積、２つのベクトルはともに長さが1なので、cosθの結果がdotUN変数に入る。
		float dotUN = Vector3.Dot(upVector, otherNormal);
		// 内積地に逆三角関数arccosを掛けて角度を算出。それを度数法へ変換する。これで角度が算出できた。
		float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;
		// ２つのベクトルがなす角度が45度より小さければ再びジャンプ可能とする
		if(dotDeg <= 45.0f) {
			isCanJump = true;
			Debug.DrawRay(contacts[0].point, contacts[0].normal * 5.0f, Color.green);
		} else {
			Debug.DrawRay(contacts[0].point, contacts[0].normal * 5.0f, Color.red);
		}
	}

	private void OnCollisionExit(Collision other) {
		isCanJump = false;
	}

	public Vector3 GetVelocity() {
		return rb.velocity;
	}

	public void AddVelocity(Vector3 vel) {
		rb.velocity += vel;
	}

	public void SetVelocity(Vector3 newVel) {
		rb.velocity = newVel;
	}

	public void SetPosition(Vector3 newPos) {
		transform.position = newPos;
	}
}