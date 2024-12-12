using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChessClass : MonoBehaviour
{
    public enum Camp { U, D }
    public Camp camp;
    public int level;
    public Material material;
    public bool isActive = false;
    
    private int x;
    private int y;

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(2*x-3, 0, 2*y-3);
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public void Activate()
    {
        isActive = true;
        GetComponent<Renderer>().material = material;
    }

    public bool CanBeat(ChessClass opponent) 
    {
        if ( opponent.isActive == false )
        {
            return false;
        }
        if ( this.camp == opponent.camp ) 
        {
            return false;
        }
        if ( Math.Abs( this.GetX() - opponent.GetX() ) + Math.Abs( this.GetY() - opponent.GetY() ) > 1 ) 
        {
            return false;
        }
        if ( this.level == 0 && opponent.level == 3 ) 
        {
            return true;
        }
        if ( this.level >= opponent.level ) 
        {
            return true;
        }
        else return false;
    }
}
