using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerObject : MonoBehaviour {
	public string nextSceneName;

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene(nextSceneName);
		}
	}
}
