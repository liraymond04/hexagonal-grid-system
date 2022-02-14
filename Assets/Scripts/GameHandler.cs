using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    public enum GameState {
        Idle,
        GameStart,
        PlayerTurn,
        WaitingTurn,
        NextTurn,
        GameEnd
    }

    protected GameState gameState = GameState.Idle;

    public List<int> turnOrder;
    public int turn = 0;

    private int localPlayer = 1;
    private Dictionary<int, Player> players;

    private Player curPlayer;

    void Start() {
        
    }

    void Update() {
        switch (gameState) {
            case (GameState.Idle): {
                Idle();
                break;
            }
            case (GameState.GameStart): {
                GameStart();
                break;
            }
            case (GameState.PlayerTurn): {
                PlayerTurn();
                break;
            }
            case (GameState.WaitingTurn): {
                WaitingTurn();
                break;
            }
            case (GameState.NextTurn): {
                NextTurn();
                break;
            }
            case (GameState.GameEnd): {
                GameEnd();
                break;
            }
        }
    }

    private void Idle() {

    }

    private void GameStart() {

    }

    private void PlayerTurn() {
        curPlayer = players[localPlayer];

        gameState = GameState.NextTurn;
    }

    private void WaitingTurn() {
        curPlayer = players[turn];

        gameState = GameState.NextTurn;
    }

    private void NextTurn() {
        CycleTurnOrder();

        int curTurn = turnOrder[turn];
        if (curTurn == localPlayer) {
            gameState = GameState.PlayerTurn;
        } else {
            gameState = GameState.WaitingTurn;
        }
    }

    private void GameEnd() {

    }

    private void CycleTurnOrder() {
        if (++turn >= turnOrder.Capacity) {
            turn = 0;
        }
    }
}
