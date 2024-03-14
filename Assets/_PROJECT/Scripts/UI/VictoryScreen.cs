using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Star1, Star2, Star3;
    private void OnValidate()
    {
        Star1 = GetComponent<PartyManager>().PanelVictory.transform.Find("Star1").gameObject;
        Star2 = GetComponent<PartyManager>().PanelVictory.transform.Find("Star2").gameObject;
        Star3 = GetComponent<PartyManager>().PanelVictory.transform.Find("Star3").gameObject;
    }
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
