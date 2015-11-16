using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void StartGame() {
		Application.LoadLevel("Test scene 1");
	}

	public void BackToMenu() {
		Application.LoadLevel("TitleMenu");
	}

	public void QuitGame() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
