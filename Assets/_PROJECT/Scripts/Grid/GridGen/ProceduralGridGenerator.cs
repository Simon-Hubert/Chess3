using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ProceduralGridGenerator : MonoBehaviour
{
    [SerializeField] GridGenLibrary.FunctionName generationMethod;
    [SerializeField, ShowIf("generationMethod", GridGenLibrary.FunctionName.SingleBlob)] Vector2Int size = new Vector2Int(8,8);
    [SerializeField, ShowIf("generationMethod", GridGenLibrary.FunctionName.PerlinNoise)] float thershold = 0.01f;
    [SerializeField, ShowIf("generationMethod", GridGenLibrary.FunctionName.MultipleSquares)] int number = 1;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GridManager gm;

    [Button]
    private void Generate() {
        gm.ClearGrid();
        GridGenLibrary.GetFunction(generationMethod)(gm, tilePrefab, size, thershold, number);
    }
}
