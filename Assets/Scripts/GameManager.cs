﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public string pathToPlayer;
	public string pathToScenePrefab;

	public GameObject player;
	public Camera gameCamera;
    public GameObject fakeCursor;

    private static GameManager _Instance = null;
    public MsgControl msgControl;

    void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
        else if(_Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () 
	{
		Object playerPrefab = Resources.Load (pathToPlayer);

		if (playerPrefab != null) 
		{
			player = (GameObject) Instantiate (playerPrefab, transform);
		}

        player.GetComponent<FakeCursor>().cursor = fakeCursor.transform;

		SceneManager.LoadScene(pathToScenePrefab, LoadSceneMode.Additive);

		SmoothFollow followScript = gameCamera.GetComponent<SmoothFollow> ();
		followScript.target = player.transform;
		followScript.UpdateOffsetOnStart ();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static GameManager Instance()
    {
        return _Instance;
    }

    public void ShowGameMsg(string msg)
    {
        msgControl.SetMessage(msg);
    }
}
