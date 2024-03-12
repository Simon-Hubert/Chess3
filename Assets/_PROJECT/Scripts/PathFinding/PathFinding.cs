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
    [Button]
    public static Tile Path(GridManager gm, Piece queen, Piece king)
    {
        int n = 10;
        Dictionary<int, Properties> Map = new Dictionary<int, Properties>();
        foreach (Tile tile in gm.Tiles)
        {
            Map.Add(tile.GetInstanceID(), new Properties(Vector3.zero, null));
        }
        List<Tile> tilesChecked = new List<Tile>();
        List<Tile> toCheck = new List<Tile>();
        Tile start = gm.GetTileAt(queen.transform.position);
        Tile end = gm.GetTileAt(king.transform.position);
        List<Tile> list = new List<Tile>();
        list.Add(start);
        while(list.Count != 0)
        {
            List<Tile> newList = new List<Tile>();
            foreach(Tile tile in list)
            {
                foreach(Tile newtile in CheckTile(tile, n, end, queen, gm, tilesChecked, Map)){
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
                    newList.Remove(current);
                    foreach(Tile currentNeighbour in CheckMovements.CheckMove((Vector2Int)current.Coords, queen.Data.Pattern, gm, queen))
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
        path.Add(end);
        while (current != start)
        {
            path.Add(Map[current.GetInstanceID()].parent);
            current = Map[current.GetInstanceID()].parent;
        }
        Tile target = null;
        if(path.Count >= 2) target = path[path.Count - 2];
        if (path.Count < 2) target = path[0];
        queen.transform.position = target.transform.position;

        return target;
    }

    static List<Tile> CheckTile(Tile start, int n, Tile end, Piece queen, GridManager gm, List<Tile> tilesChecked, Dictionary<int, Properties> Map)
    {
        List<Tile> newTiles = new List<Tile>();
        foreach (Tile tile in CheckMovements.CheckMove((Vector2Int)start.Coords, queen.Data.Pattern, gm, queen))
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
