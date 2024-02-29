using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat
{
    public static event Action<Piece> OnEat;
    public static void Eating(Piece targetFood)
    {
        targetFood.gameObject.SetActive(false);
        OnEat?.Invoke(targetFood);
    }
}
