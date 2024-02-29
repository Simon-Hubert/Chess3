using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public event Action<Piece> OnEat;
    public void Eating(Piece targetFood)
    {
        targetFood.gameObject.SetActive(false);
        OnEat.Invoke(targetFood);
    }
}
