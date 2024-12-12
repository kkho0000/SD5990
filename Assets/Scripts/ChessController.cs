using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessController : MonoBehaviour
{   
    Transform isChoosing;
    BoardClass board;
    TurnController turn;

    void Awake() {
        board = GetComponent<BoardClass>();
        turn = GameObject.Find("Turn").GetComponent<TurnController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isChoosing = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.IsChildOf(transform)) // 检测点击的是否是当前物体的子物体
                {
                    HandelClick(hit.transform);
                }
                if (hit.transform.CompareTag("Board")) // 检测点击的是否是棋盘
                {
                    HandleMove(hit.point);
                }
            }
        }
    }

    void HandelClick(Transform hit) 
    {
        // 如果没有拿起棋子
        if ( isChoosing == null ) 
        {
            if ( hit.GetComponent<ChessClass>().isActive == false ) 
            {
                hit.GetComponent<ChessClass>().Activate();
                turn.ChangeTurn();
            }
            else 
            {
                if ( turn.isRedTurn == (hit.GetComponent<ChessClass>().camp == ChessClass.Camp.U) ) return;
                PickUpChoosing(hit);
            }
        }
        else
        {
            // 如果点击的是已经拿起的棋子
            if ( isChoosing == hit )
            {
                PutDownChoosing();
            }
            // 如果点击的是另一个棋子
            else
            {
                Beat(isChoosing.GetComponent<ChessClass>(), hit.GetComponent<ChessClass>());
            }
        }
    }

    void HandleMove( Vector3 pos ) 
    {
        int[] axis = GetAxis(pos);
        if ( isChoosing == null ) return;
        if ( board.chesses[axis[0], axis[1]] != null ) return;
        if ( Math.Abs( isChoosing.GetComponent<ChessClass>().GetX() - axis[0] ) + Math.Abs( isChoosing.GetComponent<ChessClass>().GetY() - axis[1] ) > 1 ) return;
        Move(isChoosing.GetComponent<ChessClass>(), axis[0], axis[1]);
        turn.ChangeTurn();
    }

    void PickUpChoosing( Transform Choosing ) 
    {
        PutDownChoosing();
        isChoosing = Choosing;
        isChoosing.position += new Vector3(0, 1, 0); // 拿起来
    }

    void PutDownChoosing() 
    {
        if ( isChoosing != null ) 
        {
            isChoosing.position += new Vector3(0, -1, 0); // 放下
            isChoosing = null;
        }
    }

    void Move ( ChessClass chess, int x, int y ) 
    {
        board.chesses[chess.GetX(), chess.GetY()] = null;
        board.chesses[x, y] = chess.transform;
        chess.SetPosition(x, y);
        isChoosing = null;
    }

    void Beat ( ChessClass attacker, ChessClass defender ) 
    {
        if ( attacker.CanBeat(defender) ) 
        {
            board.chesses[attacker.GetX(), attacker.GetY()] = null;
            board.chesses[defender.GetX(), defender.GetY()] = attacker.transform;
            attacker.SetPosition(defender.GetX(), defender.GetY());
            Destroy(defender.gameObject);
            isChoosing = null;
            turn.ChangeTurn();
        }
    }

    int[] GetAxis( Vector3 pos )
    {
        int x = (int)Mathf.Round((pos.x+3)/2);
        int y = (int)Mathf.Round((pos.z+3)/2);
        return new int[] {x, y};
    }
}