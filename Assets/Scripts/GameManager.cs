using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public string pathToPlayer;
	public string pathToScenePrefab;

	public GameObject player;
	public Camera gameCamera;
    // Use this for initialization
    void Start () 
	{
		Object playerPrefab = Resources.Load (pathToPlayer);

		if (playerPrefab != null) 
		{
			player = (GameObject) Instantiate (playerPrefab, transform);
		}

		SceneManager.LoadScene(pathToScenePrefab, LoadSceneMode.Additive);

		SmoothFollow followScript = gameCamera.GetComponent<SmoothFollow> ();
		followScript.target = player.transform;
		followScript.UpdateOffsetOnStart ();
	}
}
