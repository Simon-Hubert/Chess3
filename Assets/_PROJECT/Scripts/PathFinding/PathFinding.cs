using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using NaughtyAttributes;
using System.Transactions;

public class PathFinding
{
    struct Properties
    {
        public Vector3 ghf;
        public Tile parent;

        public Properties(Vector3 ghf, Tile parent) : this()
        {
            this.ghf = ghf;
            this.parent = parent;
        }
    }
    static Dictionary<int, Properties> Map = new Dictionary<int, Properties>();
    GridManager gm;
    static List<Tile> tilesChecked = new List<Tile>();
    static List<Tile> toCheck = new List<Tile>();
    static int n = 10;


/*    private void Awake()
    {
        gm = FindObjectOfType<GridManager>();
        queen = GetComponent<Piece>();
        foreach(Tile tile in gm.Tiles)
        {
            Map.Add(tile.GetInstanceID(), new Properties(Vector3.zero, null));
        }
    }*/
    [Button]
    public static Tile Path(GridManager gm, Piece queen, Piece king)
    {
        foreach (Tile tile in gm.Tiles)
        {
            Map.Add(tile.GetInstanceID(), new Properties(Vector3.zero, null));
        }
        tilesChecked.Clear();
        toCheck.Clear();
        Map.Clear();
        Tile start = gm.GetTileAt(queen.transform.position);
        Tile end = gm.GetTileAt(king.transform.position);
        List<Tile> list = new List<Tile>();
        list.Add(start);
        while(list.Count != 0)
        {
            List<Tile> newList = new List<Tile>();
            foreach(Tile tile in list)
            {
                foreach(Tile newtile in CheckTile(tile, n, end, queen, gm)){
                    newList.Add(newtile);
                }
            }
            list = newList;
            Debug.Log(list.Count);
            n += 10;
        }
        toCheck.Add(start);
        Tile current = toCheck[0];
        Debug.Log(start.GetInstanceID());
        n = 10;
        //Tile current = start;
        tilesChecked.Clear();
        while (!tilesChecked.Contains(end))
        //for (int i = 0; i<5; i++)
        {
            List<Tile> newList = toCheck.ToList();
            current = toCheck[0];
            Debug.Log(tilesChecked.Count);
            Debug.Log(current.GetInstanceID());
            Debug.Log(toCheck.Count); 
            foreach ( Tile tile in toCheck)
            {
                if ((Map[tile.GetInstanceID()].ghf.z <= Map[current.GetInstanceID()].ghf.z))
                {
                    Debug.Log(tile.GetInstanceID());
                    current = tile;
                    tilesChecked.Add(current);
                    //toCheck.Remove(current);
                    newList.Remove(current);
                    foreach(Tile currentNeighbour in CheckMovements.CheckMove((Vector2Int)current.Coords, queen.Data.Pattern, gm))
                    {
                        if(!tilesChecked.Contains(currentNeighbour))
                        {
                            if(!newList.Contains(currentNeighbour))
                            {
                                newList.Add(currentNeighbour);
                                Properties pro = Map[currentNeighbour.GetInstanceID()];
                                pro.parent = current;
                                // On recalcul les propriétés de currentNeighbour ?
                                float H = Vector2Int.Distance((Vector2Int)end.Coords, (Vector2Int)start.Coords);
                                pro.ghf = new Vector3(n, H, n + H);
                                Map[currentNeighbour.GetInstanceID()] = pro;
                            }
                            else
                            {
                                // On recalcul les propriétés de currentNeighbour ?
                                if(n < Map[currentNeighbour.GetInstanceID()].ghf.x)
                                {
                                    Properties pro = Map[currentNeighbour.GetInstanceID()];
                                    pro.parent = current;
                                    float H = Vector2Int.Distance((Vector2Int)end.Coords, (Vector2Int)start.Coords);
                                    pro.ghf = new Vector3(n, H, n + H);
                                    Map[currentNeighbour.GetInstanceID()] = pro;
                                }

                            }
                        }
                    }
                    n += 10;
                }
            }
            toCheck = newList.ToList();

        }
        List<Tile> path = new List<Tile>();
        while (current != start)
        {
            path.Add(Map[current.GetInstanceID()].parent);
            current = Map[current.GetInstanceID()].parent;
        }
        Debug.Log(path.Count);
        Debug.Log(path[1]);
        queen.transform.position = path[1].transform.position;
        
        return path[1];
    }

    static List<Tile> CheckTile(Tile start, int n, Tile end, Piece queen, GridManager gm)
    {
        List<Tile> newTiles = new List<Tile>();
        foreach (Tile tile in CheckMovements.CheckMove((Vector2Int)start.Coords, queen.Data.Pattern, gm))
        {
            if(!tilesChecked.Contains(tile))
            {
                tilesChecked.Add(tile);
                newTiles.Add(tile);
                float H = Vector2Int.Distance((Vector2Int)end.Coords, (Vector2Int)start.Coords);
                Properties prop = Map[tile.GetInstanceID()];
                prop.ghf = new Vector3(n, H, n + H);
                if (gm.GetPieceAt(tile.Coords)) prop.ghf = new Vector3(Mathf.Infinity, 0,0);
                Map[tile.GetInstanceID()] = prop;
            }

            //if(!newTiles.Contains(tile)) newTiles.Add(tile);
        }
        return newTiles;
    }

}
