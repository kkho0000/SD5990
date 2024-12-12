using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public int turn;
    public Material blue;
    public Material red;
    public bool isRedTurn = true;
    // Start is called before the first frame update
    void Start()
    {
        UpdateColor();
        turn = 0;
    }
    void UpdateColor()
    {
        if (isRedTurn)
        {
            GetComponent<Renderer>().material = red;
        }
        else
        {
            GetComponent<Renderer>().material = blue;
        }
    }
    public void ChangeTurn()
    {
        isRedTurn = !isRedTurn;
        turn ++;
        UpdateColor();
    }
}
