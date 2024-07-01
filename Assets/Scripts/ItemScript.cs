using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {
	private Animator _animator;
	private AudioSource _audioSource;
	private static readonly int Get = Animator.StringToHash("Get");

	private bool _played = false;

	private void Awake() {
		_animator = GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other) {
		if (_played) {
			return;
		}

		_animator.SetTrigger(Get);
		_audioSource.Play();
		_played = true;
	}

	private void DestroySelf() {
		Destroy(gameObject);
	}

	private void OnTriggerStay(Collider other) {
		//Debug.Log("Stay");
	}

	private void OnTriggerExit(Collider other) {
		//Debug.Log("Exit");
	}
}