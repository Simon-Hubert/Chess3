using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GridSettings : MonoBehaviour
{
    [SerializeField] bool inverted;
    [SerializeField] Material goodShader;
    [SerializeField] Material goodShader2;
    [SerializeField] Sprite goodSprite;

    public bool Inverted { get => inverted;}

    #if UNITY_EDITOR
    [Button]
    public void SetUpShader(){
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.SetUpNewShader(goodShader, goodShader2, goodSprite);
        }
    }
    #endif
}
