using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum PlayerState {
        IsPlayer,
        IsRemotePlayer,
        IsAI
    }

    protected PlayerState playerState;

    public bool isPlayer;

    public int playerID;

    private GameTiles gameTiles;
    private GridLayout gridLayout;
    public List<WorldTile> selected;

    void Start() {
        gameTiles = GameTiles.instance;
        gridLayout = gameTiles.layout;

        if (isPlayer) {
            playerState = PlayerState.IsPlayer;
        }

        selected = new List<WorldTile>();
    }

    void Update() {
        switch (playerState) {
            case (PlayerState.IsPlayer): {
                IsPlayer();
                break;
            }
            case (PlayerState.IsRemotePlayer): {
                IsRemotePlayer();
                break;
            }
            case (PlayerState.IsAI): {
                IsAI();
                break;
            }
        }
    }

    private void IsPlayer () {
        if (Input.GetKeyDown(KeyCode.S)) {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            
            Vector3Int cellPosition = gridLayout.WorldToCell(pz);

            WorldTile currentTile = gameTiles.tiles[cellPosition], selectedTile = null;

            if (selected.Count != 0) {
                selectedTile = selected[0];
                selected.Clear();
            }

            if (selectedTile != currentTile) {
                selected.Add(currentTile);
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            if (selected.Count != 0) {
                Debug.Log(selected[0].Name);
            } else {
                Debug.Log("No tile selected");
            }
        }
    }

    private void IsRemotePlayer() {

    }

    private void IsAI() {

    }
}
