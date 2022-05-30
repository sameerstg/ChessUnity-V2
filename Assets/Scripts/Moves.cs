using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    public static Moves _instance;
    private void Awake()
    {
        _instance = this;
    }
    bool InLimit(Vector2 pos)
    {
        if (pos.x >= 0 && pos.x <= 7 && pos.y >=0 && pos.y <=7)
        {
            return true;
        }
        return false;
    }
    bool IsWay(Vector2 pos)
    {
        if (BoardManager.grid.py[(int)pos.y].x[(int)pos.x] == null  && pos.x >= 0 && pos.x <= 7 && pos.y >= 0 && pos.y <= 7)
        {
            return true;
        }
        return false;
    }
    public Vector2 Up1(Vector2 pos)
    {
        
            return new Vector2(pos.x, pos.y + 1);
        
        
        
    }
    public Vector2 Up2(Vector2 pos)
    {
        return new Vector2(pos.x, pos.y + 2);
    }
    public Vector2 UpR1(Vector2 pos)
    {
        return new Vector2(pos.x+1, pos.y + 1);
    }
    public Vector2 UpL1(Vector2 pos)
    {
        return new Vector2(pos.x-1, pos.y + 1);
    }public Vector2 Down1(Vector2 pos)
    {
        return new Vector2(pos.x, pos.y - 1);
    }
    public Vector2 Down2(Vector2 pos)
    {
        return new Vector2(pos.x, pos.y - 2);
    }
    public Vector2 DownR1(Vector2 pos)
    {
        return new Vector2(pos.x+1, pos.y - 1);
    }
    public Vector2 DownL1(Vector2 pos)
    {
        return new Vector2(pos.x-1, pos.y - 1);
    }
    public List<Vector2> L(Vector2 pos)
    {
        List<Vector2> list = new List<Vector2>();

        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {

                if (i==0||j==0  ||Math.Abs(i) == Math.Abs(j))
                {
                    continue;
                }

                if (InLimit(new Vector2(pos.x + i, pos.y + j)))
                    {
                        
                        list.Add(new Vector2(pos.x + i, pos.y + j));
                    }
                    
                
            }
        }


        return list;
    }
    public List<Vector2> Diagonal(Vector2 pos)
    {
        List<Vector2> list = new List<Vector2>();
        bool b1 = false, b2 = false, b3 = false, b4 = false;
        for (int i = 1; i < 7; i++)
        {
            for (int j = 1; j < 7; j++)
            {
                if (IsWay(new Vector2(pos.x + i, pos.y + i))&&!b1)
                {
                    list.Add(new Vector2(pos.x + i, pos.y + i));
                }
                else
                {
                    b1 = true;
                }
                if ( IsWay(new Vector2(pos.x + i, pos.y + i))&&!b2)
                {
                    list.Add(new Vector2(pos.x - i, pos.y - i));

                }
                else
                {
                    b2 = true;
                }
                if (IsWay(new Vector2(pos.x + i, pos.y + i))&&!b3)
                {
                    list.Add(new Vector2(pos.x + i, pos.y - i));

                }
                else
                {
                    b3 = true;
                }
                if (IsWay(new Vector2(pos.x + i, pos.y + i))&&!b4)
                {
                    list.Add(new Vector2(pos.x - i, pos.y + i));

                }
                else
                {
                    b4 = true;
                }
            }
        }
        return list;
    }
}
