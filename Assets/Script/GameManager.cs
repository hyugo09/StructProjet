using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameTile[,] gameTiles;
    private int colCount = 20;
    private int rowCount = 10;
    [SerializeField] GameObject GameTilePrefab;
    private void Awake()
    {
        gameTiles = new GameTile[colCount, rowCount];
        for (int x = 0; x < colCount; x++)
        {
            for (int y = 0; y < rowCount; y++)
            {
                var spawnPosition = new Vector3(x, y, 0);
                gameTiles[x,y] = Instantiate(GameTilePrefab, spawnPosition, Quaternion.identity).GetComponent<GameTile>();
                if ((x + y) % 2 == 0)
                {
                    gameTiles[x,y].TurnGray();
                }
                
            }
        }
    }
}
