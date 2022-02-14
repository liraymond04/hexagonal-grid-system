using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [Serializable]
    public struct UnitStats {
        public int attack;
        public int defense;
        public int movementRange;
        public int attackRange;
        public int health;
    }
    
    public Vector3Int pos;
    public bool canMove = false;
    public bool canAttack = false;
    public int teamID = -1;
    public int unitType = -1; // -1 - Null; 0 - Infrantry; 1 - Knight; 2 - Mage;
    public int health = 0;

    public List<UnitStats> stats;

    void Start() {
        Vector3 wp = GameTiles.instance.tiles[pos].WorldLocation;
        GameTiles.instance.units[pos] = this;
        transform.position = new Vector3(wp.x, wp.y, -1);
    }

    void Update() {
        
    }
}