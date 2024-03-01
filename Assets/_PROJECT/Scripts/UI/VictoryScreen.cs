using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Star1, Star2, Star3;
    public void SetScreen(int numberOfStars)
    {
        switch (numberOfStars)
        {
            case 1: Star1.SetActive(true); break;
            case 2: Star1.SetActive(true); Star2.SetActive(true);break;
            case 3: Star1.SetActive(true); Star2.SetActive(true); Star3.SetActive(true); break;
        }
    }
}
