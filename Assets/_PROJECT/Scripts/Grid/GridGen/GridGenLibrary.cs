using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.Mathf;

public class GridGenLibrary
{
    public delegate void Function(GridManager gm, GameObject tilePrefab, Vector2Int size, float thershold, int number);

    public enum FunctionName {MultipleSquares, PerlinNoise, SingleBlob}

    static Function[] functions = {MultipleSquares , PerlinNoise, SingleBlob};

    public static Function GetFunction(FunctionName name){
        return functions[(int)name];
    }

    private static void MultipleSquares(GridManager gm, GameObject tilePrefab, Vector2Int size, float thershold, int number)
    {
        for (int i = 0; i < number; i++)
        {
            GenerateSquare(gm, tilePrefab);
        }
    }


    private static void PerlinNoise(GridManager gm, GameObject tilePrefab, Vector2Int size, float thershold, int number)
    {
        throw new NotImplementedException();
    }

    private static void SingleBlob(GridManager gm, GameObject tilePrefab, Vector2Int size, float thershold, int number)
    {
        throw new NotImplementedException();
    }

    private static void GenerateSquare(GridManager gm, GameObject tilePrefab)
    {
        Vector2Int a = new Vector2Int(Range(-3, 3), Range(-7, 6));
        Vector2Int c = new Vector2Int(Range(-3, 3), Range(-7, 6));

        for (int i = 0; i < Abs(a.x - c.x); i++)
        {
            for (int j = 0; j < Abs(a.y - c.y); j++)
            {
                gm.InstanciateInGrid(tilePrefab, new Vector3Int(a.x + i, a.y +j, 0));
            }
        }
    }
}
