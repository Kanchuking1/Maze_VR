using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;
    public Player playerPrefab;
    public Camera cam;
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
        cam.clearFlags = CameraClearFlags.Skybox;
        cam.rect = new Rect(0f, 0f, 1f, 1f);
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        cam.clearFlags = CameraClearFlags.Depth;
        cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
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