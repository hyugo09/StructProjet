using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameTilePrefab;
    [SerializeField] GameObject enemyPrefab;
    GameTile[,] gameTiles;
    private GameTile spawnTile;
    const int ColCount = 20;
    const int RowCount = 10;
    private int tilePrice = 100;
    public GameTile TargetTile { get; internal set; }

    List<GameTile> pathToGoal = new List<GameTile>();

    GameTile LastPathTile;
    //public List<GameTile>[] paths = new List<GameTile>[5]; //peut etre pas

    internal Turret selectedTurret;

    PlayerStat playerStat;
    internal Turret tourelleSelectionner;
    public bool modeInfini = false;

    [SerializeField] private int[] posWallX;
    [SerializeField] private int[] posWallY;

    public EnnemyStat[] ennemyStats;
    private void Awake()
    {
        playerStat = GetComponent<PlayerStat>();
        gameTiles = new GameTile[ColCount, RowCount];

        for (int x = 0; x < ColCount; x++)
        {
            for (int y = 0; y < RowCount; y++)
            {
                var spawnPosition = new Vector3(x, y, 0);
                var tile = Instantiate(gameTilePrefab, spawnPosition, Quaternion.identity);
                gameTiles[x, y] = tile.GetComponent<GameTile>();
                gameTiles[x, y].GM = this;
                gameTiles[x, y].X = x;
                gameTiles[x, y].Y = y;

                if ((x + y) % 2 == 0)
                {
                    gameTiles[x, y].TurnGrey();
                }
            }
        }

        spawnTile = gameTiles[1, 7];
        LastPathTile = spawnTile;
        spawnTile.SetEnemySpawn();
        if (!modeInfini)
        {
            MakePath();
        }
    }
    private void MakePath()
    {
        for (int i = 0; i < posWallX.Length; i++)
        {
            var path = Pathfinding(LastPathTile, TargetTile);
            var tile = gameTiles[posWallX[i], posWallY[i]];

            if (LastPathTile == spawnTile)
            {
                while (tile != null)
                {
                    pathToGoal.Add(tile);
                    tile.SetPath(true);
                    tile = path[tile];
                    if (tile != null)
                    {

                        LastPathTile = tile;
                    }
                    else
                    {
                        LastPathTile = pathToGoal.ElementAt(0);
                    }
                }
                StartCoroutine(SpawnEnemyCoroutine());
            }
            else
            {
                GameTile[] originalPath = pathToGoal.ToArray();
                pathToGoal.Clear();
                while (tile != null)
                {
                    pathToGoal.Add(tile);
                    tile.SetPath(true);
                    tile = path[tile];
                    if (tile != null)
                    {

                        LastPathTile = tile;
                    }
                    else
                    {
                        LastPathTile = pathToGoal.ElementAt(0);
                    }
                }
                foreach (GameTile tuile in originalPath)
                {
                    pathToGoal.Add(tuile);
                }
            }
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TargetTile != null)
        {
            
            var path = Pathfinding(LastPathTile, TargetTile);
            var tile = TargetTile;

            if(LastPathTile == spawnTile)
            {
                foreach (var t in gameTiles)
                {
                    t.SetPath(false);
                }

                while (tile != null)
                {
                    pathToGoal.Add(tile);
                    tile.SetPath(true);
                    tile = path[tile];
                    if (tile != null)
                    {

                        LastPathTile = tile;
                    }
                    else
                    {
                        LastPathTile = pathToGoal.ElementAt(0);
                    }
                }
                StartCoroutine(SpawnEnemyCoroutine());
            }
            else
            { 
                GameTile[] originalPath = pathToGoal.ToArray();
                pathToGoal.Clear();
                while (tile != null)
                {
                    pathToGoal.Add(tile);
                    tile.SetPath(true);
                    tile = path[tile];
                    if (tile != null)
                    {

                        LastPathTile = tile;
                    }
                    else
                    {
                        LastPathTile = pathToGoal.ElementAt(0);
                    }
                }
                foreach(GameTile tuile in originalPath)
                {
                    pathToGoal.Add(tuile);
                }
            }
            
            

        }
    }

    private Dictionary<GameTile, GameTile> Pathfinding(GameTile sourceTile, GameTile targetTile)
    {
        var dist = new Dictionary<GameTile, int>();

        var prev = new Dictionary<GameTile, GameTile>();

        var Q = new List<GameTile>();

        foreach (var v in gameTiles)
        {
            dist.Add(v, 9999);

            prev.Add(v, null);

            Q.Add(v);
        }

        dist[sourceTile] = 0;

        while (Q.Count > 0)
        {
            GameTile u = null;
            int minDistance = int.MaxValue;

            foreach (var v in Q)
            {
                if (dist[v] < minDistance)
                {
                    minDistance = dist[v];
                    u = v;
                }
            }

            Q.Remove(u);

            foreach (var v in FindNeighbor(u))
            {
                if (!Q.Contains(v) || v.IsBlocked)
                {
                    continue;
                }

                int alt = dist[u] + 1;

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        return prev;
    }

    private List<GameTile> FindNeighbor(GameTile u)
    {
        var result = new List<GameTile>();

        if (u.X - 1 >= 0)
            result.Add(gameTiles[u.X - 1, u.Y]);
        if (u.X + 1 < ColCount)
            result.Add(gameTiles[u.X + 1, u.Y]);
        if (u.Y - 1 >= 0)
            result.Add(gameTiles[u.X, u.Y - 1]);
        if (u.Y + 1 < RowCount)
            result.Add(gameTiles[u.X, u.Y + 1]);

        return result;
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.6f);
                var enemy = Instantiate(enemyPrefab, spawnTile.transform.position, Quaternion.identity);
                
                enemy.GetComponent<Enemy>().stat = ennemyStats[Random.Range(0,ennemyStats.Length)];
                enemy.GetComponent<Enemy>().setSprite();
                enemy.GetComponent<Enemy>().SetPath(pathToGoal);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
