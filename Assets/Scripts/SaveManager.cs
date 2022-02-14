using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(GameTiles))]
public class SaveManager : MonoBehaviour {
    private GameTiles gameTiles;
    private List<TileData> save;

    private void Start() {
        gameTiles = GameTiles.instance;
        save = new List<TileData>();
    }

    public void Save() {
        foreach (var item in gameTiles.tiles) {
            TileData data = new TileData {
                x = item.Key.x,
                y = item.Key.y,
                type = item.Value.Type,
            };
            save.Add(data);
        }
        ES3.Save("map", save, "saves/tut1.map");
    }

    public void Load() {
        save = (List<TileData>)ES3.Load("map", "saves/tut1.map");
        foreach (var item in save) {
            var localPlace = new Vector3Int(item.x, item.y, 0);

            gameTiles.Tilemap.SetTile(localPlace, gameTiles.tileTypes[item.type]);

            var tile = new WorldTile {
				LocalPlace = localPlace,
				WorldLocation = gameTiles.Tilemap.CellToWorld(localPlace),
				TileBase = gameTiles.Tilemap.GetTile(localPlace),
				TilemapMember = gameTiles.Tilemap,
				Name = localPlace.x + "," + localPlace.y,
				Type = item.type,
				Cost = 1
			};

            gameTiles.tiles[localPlace] = tile;
        }
    }
}

public class TileData {
    public int x;
    public int y;
    public int type;
}

public class UnitData {

}