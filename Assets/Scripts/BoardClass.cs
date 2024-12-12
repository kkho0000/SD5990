using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardClass : MonoBehaviour
{
    public Material mat_default;
    public Transform[,] chesses;

    void Awake() {
        chesses = new Transform[4, 4];
        for (int i = 0; i < transform.childCount; i++)
        {
            chesses[i/4, i%4] = transform.GetChild(i);
            chesses[i/4, i%4].GetComponent<Renderer>().material = mat_default;
        }
    }

    void Start()
    {
        ShuffleChesses();
    }

    void ShuffleChesses() 
    {
        for (int i = 0; i < 4; i++)
        {
            for ( int j = 0; j < 4; j ++ ) 
            {
                int randomX = UnityEngine.Random.Range(0, 4);
                int randomY = UnityEngine.Random.Range(0, 4);
                Transform temp = chesses[i, j];
                chesses[i, j] = chesses[randomX, randomY];
                chesses[randomX, randomY] = temp;
                chesses[i, j].GetComponent<ChessClass>().SetPosition(i, j);
                chesses[randomX, randomY].GetComponent<ChessClass>().SetPosition(randomX, randomY);
            }
        }
    }

}
