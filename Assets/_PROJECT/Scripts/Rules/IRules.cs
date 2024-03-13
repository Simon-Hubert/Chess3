using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRules
{
    bool IsWon();
    bool IsLost(GameObject King);
}
