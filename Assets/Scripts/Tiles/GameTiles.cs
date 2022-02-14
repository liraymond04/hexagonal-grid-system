using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameTiles : MonoBehaviour {
	public static GameTiles instance;
	public GridLayout layout;
	public Tilemap Tilemap;

	public List<Tile> tileTypes;
	
	public Dictionary<Vector3Int, WorldTile> tiles;

	public Dictionary<Vector3Int, Unit> units;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		layout = GetComponent<GridLayout>();

		GetWorldTiles();
        units = new Dictionary<Vector3Int, Unit>();
	}

	private void GetWorldTiles() {
        tiles = new Dictionary<Vector3Int, WorldTile>();
		foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin) {
			var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

			if (!Tilemap.HasTile(localPlace)) continue;

			int index = tileTypes.FindIndex(a => a == Tilemap.GetTile(localPlace));
			var tile = new WorldTile {
				LocalPlace = localPlace,
				WorldLocation = Tilemap.CellToWorld(localPlace),
				TileBase = Tilemap.GetTile(localPlace),
				TilemapMember = Tilemap,
				Name = localPlace.x + "," + localPlace.y,
				Type = index,
				Cost = 1 // TODO: Change this with the proper cost from ruletile
			};
			
			tiles.Add(tile.LocalPlace, tile);
		}
	}

	public void UpdateTile(Vector3Int cellPosition, TileBase tileBase = null, int type = -1) {
		WorldTile worldTile;
		if (!tiles.ContainsKey(cellPosition)) {
			worldTile = new WorldTile {
				LocalPlace = cellPosition,
				WorldLocation = Tilemap.CellToWorld(cellPosition),
				TilemapMember = Tilemap,
				Name = cellPosition.x + "," + cellPosition.y,
				Cost = 1
			};
		} else {
			worldTile = tiles[cellPosition];
		}

		if (tileBase != null) {
			int index = tileTypes.FindIndex(a => a == tileBase);
			worldTile.TileBase = tileBase;
			worldTile.Type = index;
		} else {
			if (type != -1) {
				tileBase = tileTypes[type];
				worldTile.TileBase = tileBase;
				worldTile.Type = type;
			} else {
				worldTile.TileBase = tileBase;
				worldTile.Type = type;
			}
		}

		Tilemap.SetTile(cellPosition, tileBase);
		tiles[cellPosition] = worldTile;
	}
}