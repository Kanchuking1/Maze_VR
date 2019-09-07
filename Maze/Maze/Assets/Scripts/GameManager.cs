﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;
    public Player playerPrefab;
    private Player playerInstance;

    private void Start () {
		StartCoroutine(BeginGame());
	}
	
	private void Update () { 
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

    //Begin the game
    private IEnumerator BeginGame () {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    }

    //Stop all Coroutines and destroy all the objects and begin the game again
	private void RestartGame () {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
    }
}