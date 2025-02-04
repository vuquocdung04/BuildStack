using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostTile : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating(nameof(DesTroyObj),2f,2f);
    }


    void DesTroyObj()
    {
        Destroy(gameObject);
    }
}