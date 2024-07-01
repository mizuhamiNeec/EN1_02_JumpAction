using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private Transform targetTransform;
	[SerializeField] private Transform futureTransform;
	[SerializeField] private float targetVelocity;
	[SerializeField] private float angle;
	[SerializeField] private bool overrideVelocity;
	[SerializeField] private bool overridePosition;

	private void OnDrawGizmos() {
		if(lineRenderer != null && targetTransform != null) {

			Vector3 velocity = CalcThrowVel(transform.position, targetTransform.position, targetVelocity, angle);
			int resolution = 30; // 描画する軌道のポイント数
			lineRenderer.positionCount = resolution;

			for(int i = 0; i < resolution; i++) {
				float time = (float)i / (resolution - 1) * (2 * velocity.y / Physics.gravity.magnitude);
				Vector3 position = transform.position + velocity * time + 0.5f * Physics.gravity * time * time;
				lineRenderer.SetPosition(i, position);
			}
		}
	}

	public static Vector3 CalcThrowVel(Vector3 start, Vector3 target, float speed, float angle) {
		Vector3 direction = target - start;
		float gravity = Physics.gravity.y;
		float yOffset = direction.y;
		direction.y = 0;
		float distance = direction.magnitude;

		float angleInRadians = angle * Mathf.Deg2Rad;
		float timeTaken = distance / (speed * Mathf.Cos(angleInRadians));

		float yVelocity = speed * Mathf.Sin(angleInRadians) - gravity * timeTaken * 0.5f;

		Vector3 result = direction.normalized * speed * Mathf.Cos(angleInRadians);
		result.y = yVelocity;

		return result;
	}

	private void OnTriggerEnter(Collider other) {
		if(other.TryGetComponent(out PullingJump player)) {
			Vector3 start = transform.position;
			Vector3 end = targetTransform.position;
			Vector3 velocity = CalcThrowVel(start, end, targetVelocity, angle);

			if(overridePosition) {
				player.SetPosition(start);
			}

			if(overrideVelocity) {
				player.SetVelocity(velocity);
			} else {
				player.AddVelocity(velocity);
			}
		}
	}
}