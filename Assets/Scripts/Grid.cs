using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour {
    public Tilemap t;
    public Tile set;

    private GameTiles gameTiles;
    private GridLayout gridLayout;

    void Start() {
        gameTiles = GameTiles.instance;
        gridLayout = gameTiles.layout;
    }

    void Update() {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
            // get mouse click's position in 2d plane
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;

            // convert mouse click's position to Grid position
            Vector3Int cellPosition = gridLayout.WorldToCell(pz);

            gameTiles.UpdateTile(cellPosition, set);

            // Debug.Log(cellPosition);
        }

        if (Input.GetMouseButtonUp(1) && !EventSystem.current.IsPointerOverGameObject()) {
            // get mouse click's position in 2d plane
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;

            // convert mouse click's position to Grid position
            Vector3Int cellPosition = gridLayout.WorldToCell(pz);

            if (GameTiles.instance.units.ContainsKey(cellPosition)) {
                Debug.Log(GameTiles.instance.units[cellPosition].health);
            } else {
                Debug.Log("No unit");
            }

            // gameTiles.UpdateTile(cellPosition);

            // Debug.Log(cellPosition);
        }
    }

    public void HandleInputData(int val) {
        if (val == 0) {
            set = gameTiles.tileTypes[0];
        } else if (val == 1) {
            set = gameTiles.tileTypes[1];
        } else if (val == 2) {
            set = gameTiles.tileTypes[2];
        }
    }
}
