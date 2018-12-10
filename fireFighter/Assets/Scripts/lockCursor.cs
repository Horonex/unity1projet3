using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockCursor : MonoBehaviour {

    [SerializeField] HUDUpdater canvas;
    [SerializeField] FireLevel fireLevel;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
        canvas.lose += OnGameEnd;
        fireLevel.win += OnGameEnd;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel")) { 
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (Input.GetButtonDown("Fire1")) { 
		Cursor.lockState = CursorLockMode.Confined;
		//Cursor.visible = false;
		}
	}

    public void OnGameEnd()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
    }

}
