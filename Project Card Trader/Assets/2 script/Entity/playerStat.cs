using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStat : MonoBehaviour
{
    public static playerStat current;
    public int charisme;
    public int money = 50;

    private void Awake()
    {
        current = this;
    }
}