using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    public static Moves _instance;
    BoardManager bm;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        bm = BoardManager._instance;
    }
    public bool IsDiagonalTaker(GameObject go)
    {
        if (go.name.Contains("Bishop")|| go.name.Contains("Queen"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }public bool IsPlusTaker(GameObject go)
    {
        if (go.name.Contains("Rook")|| go.name.Contains("Queen"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool InLimit(Vector2 pos)
    {
        if (pos.x >= 0 && pos.x <= 7 && pos.y >= 0 && pos.y <= 7)
        {
            return true;
        }
        return false;
    }
    public Tuple<Vector2, bool> GetSequenceValitdation(Vector2 pos, string color)
    {
        if (InLimit(new Vector2(pos.x, pos.y)))
        {

            if (bm.grid.py[(int)pos.y].x[(int)pos.x] == null)
            {
                return new Tuple<Vector2, bool>(new Vector2(pos.x, pos.y), true);
            }
            else if (color != bm.grid.py[(int)pos.y].x[(int)pos.x].tag)
            {
                return new Tuple<Vector2, bool>(new Vector2(pos.x, pos.y), false);
            }
            else
            {
                return new Tuple<Vector2, bool>(new Vector2(-1, -1), false);

            }
        }
        else
        {
            return new Tuple<Vector2, bool>(new Vector2(-1, -1), false);
        }
    }
    // first is check and second is permit to iterate further
    public Tuple<bool, bool> GetSequenceValitdationForCheck(Vector2 pos, string color)
    {
        if (InLimit(new Vector2(pos.x, pos.y)))
        {

            if (bm.grid.py[(int)pos.y].x[(int)pos.x] == null)
            {
                return new Tuple<bool, bool>(false, true);
            }
            else if (color != bm.grid.py[(int)pos.y].x[(int)pos.x].tag)
            {
                return new Tuple<bool, bool>(true, false);
            }
            else
            {
                return new Tuple<bool, bool>(false, false);

            }
        }
        else
        {
            return new Tuple<bool, bool>(false, false);
        }
    }
    public Tuple< bool,GameObject> GetValitdationForCheck(Vector2 pos,string color)
    {

        if (InLimit(pos) )
        {
            GameObject go = GetGoByVector2(pos);
            if (go != null && go.tag !=color)
            {
                

                return new Tuple<bool, GameObject>(true, go);
            }
        }
        return new Tuple<bool, GameObject>(false, null);

    }
    public Vector2 GetPosByName(string obj)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bm.grid.py[i].x[j]!=null)
                {
                    if (bm.grid.py[i].x[j].name.Contains(obj))
                    {
                        return new Vector2(j, i);
                    }
                }
                
            }
        }
        print(obj);
        print("king not found");
        return new Vector2(-1, -1);
    }
    public GameObject GetGoByVector2(Vector2 position)
    {
        try
        {
            return bm.grid.py[(int)position.y].x[(int)position.x];
        }
        catch (Exception)
        {
            return null;
        }
           
    }
    public Vector2 Enpassant(Vector2 pos)
    {
        if (GetGoByVector2(pos).CompareTag("White") && pos.y ==4)
        {
            
            if (GetGoByVector2(new Vector2(pos.x - 1, pos.y)) != null && GetGoByVector2(new Vector2(pos.x - 1, pos.y)).name == Enums.pieceName.BPawn.ToString()
                && GetGoByVector2(new Vector2(pos.x - 1, pos.y)).GetComponent<Piece>().piece.MovesCount ==1
                && bm.last.LastSelectedGo == GetGoByVector2(new Vector2(pos.x - 1, pos.y)))
            {
                return UpL1(pos);
            }
            if (GetGoByVector2(new Vector2(pos.x + 1, pos.y))!= null && GetGoByVector2(new Vector2(pos.x + 1, pos.y)).name == Enums.pieceName.BPawn.ToString()
                && GetGoByVector2(new Vector2(pos.x + 1, pos.y)).GetComponent<Piece>().piece.MovesCount ==1
                &&  bm.last.LastSelectedGo == GetGoByVector2(new Vector2(pos.x + 1, pos.y)))
            {
                return UpR1(pos);
            }
        }else if (GetGoByVector2(pos).CompareTag("Black") && pos.y ==3)
        {
            if (GetGoByVector2(new Vector2(pos.x - 1, pos.y)) != null && GetGoByVector2(new Vector2(pos.x - 1, pos.y)).name == Enums.pieceName.WPawn.ToString()
                && GetGoByVector2(new Vector2(pos.x - 1, pos.y)).GetComponent<Piece>().piece.MovesCount ==1
                && bm.last.LastSelectedGo == GetGoByVector2(new Vector2(pos.x - 1, pos.y)))
            {
                return DownL1(pos);
            }
            if (GetGoByVector2(new Vector2(pos.x + 1, pos.y))!= null && GetGoByVector2(new Vector2(pos.x + 1, pos.y)).name == Enums.pieceName.WPawn.ToString()
                && GetGoByVector2(new Vector2(pos.x + 1, pos.y)).GetComponent<Piece>().piece.MovesCount ==1
                &&  bm.last.LastSelectedGo == GetGoByVector2(new Vector2(pos.x + 1, pos.y)))
            {
                return DownR1(pos);
            }
        }
        return new Vector2(-1, -1);
    }
    public Vector2 Up1(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x, pos.y + 1);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);

    }
    public Vector2 Up2(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x, pos.y + 2);
        if (InLimit(move))
        {
            return move;
        }
            return new Vector2(-1, -1);
    }
    public Vector2 UpR1(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x + 1, pos.y + 1);
            if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
       
    }
    public Vector2 UpL1(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x - 1, pos.y + 1);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
    }
    public Vector2 Down1(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x, pos.y - 1);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
    }
    public Vector2 Down2(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x, pos.y - 2);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
    }
    public Vector2 DownR1(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x + 1, pos.y - 1);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
    }
    public Vector2 DownL1(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x - 1, pos.y - 1);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
    }
    public Vector2 Right(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x + 1, pos.y);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
    }
    public Vector2 Left(Vector2 pos)
    {
        Vector2 move = new Vector2(pos.x - 1, pos.y);
        if (InLimit(move))
        {
            return move;
        }
        return new Vector2(-1, -1);
    }
    public List<Vector2> L(Vector2 pos)
    {
        List<Vector2> list = new List<Vector2>();

        for (int i = 1; i < 3; i++)
        {
            for (int j = 1; j < 3; j++)
            {

                if ( i == j)
                {
                    continue;
                }

                if (InLimit(new Vector2(pos.x + i, pos.y + j)))
                {

                    list.Add(new Vector2(pos.x + i, pos.y + j));
                }if (InLimit(new Vector2(pos.x - i, pos.y - j)))
                {

                    list.Add(new Vector2(pos.x - i, pos.y - j));
                }if (InLimit(new Vector2(pos.x + i, pos.y - j)))
                {

                    list.Add(new Vector2(pos.x + i, pos.y - j));
                }if (InLimit(new Vector2(pos.x - i, pos.y + j)))
                {

                    list.Add(new Vector2(pos.x - i, pos.y + j));
                }


            }
        }
        return list;
    }
    public List<Vector2> Castling(Vector2 pos,string color)
    {
        List<Vector2> list = new List<Vector2>();
        if (color == Enums.colorSide.White.ToString())
        {
            for (int i = (int)pos.x + 1; i <= 7; i++)
            {
                if (GetGoByVector2(new Vector2(i, pos.y)) == null)
                {
                    continue;
                }
                else if (GetGoByVector2(new Vector2(i, pos.y)).name.Contains("WRook") && !GetGoByVector2(new Vector2(i, pos.y)).GetComponent<Piece>().isMovedBefore)
                {
                    list.Add(new Vector2(pos.x + 2, pos.y));
                }
                else
                {
                    break;
                }
            }
            for (int i = (int)pos.x - 1; i >= 0; i--)
            {
                if (GetGoByVector2(new Vector2(i, pos.y)) == null)
                {
                    continue;
                }
                else if (GetGoByVector2(new Vector2(i, pos.y)).name.Contains("WRook") && !GetGoByVector2(new Vector2(i, pos.y)).GetComponent<Piece>().isMovedBefore)
                {
                    list.Add(new Vector2(pos.x - 2, pos.y));
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            for (int i = (int)pos.x + 1; i <= 7; i++)
            {
                if (GetGoByVector2(new Vector2(i, pos.y)) == null)
                {
                    continue;
                }
                else if (GetGoByVector2(new Vector2(i, pos.y)).name.Contains("BRook") && !GetGoByVector2(new Vector2(i, pos.y)).GetComponent<Piece>().isMovedBefore)
                {
                    list.Add(new Vector2(pos.x + 2, pos.y));
                }
                else
                {
                    break;
                }
            }
            for (int i = (int)pos.x - 1; i >= 0; i--)
            {
                if (GetGoByVector2(new Vector2(i, pos.y)) == null)
                {
                    continue;
                }
                else if (GetGoByVector2(new Vector2(i, pos.y)).name.Contains("BRook") && !GetGoByVector2(new Vector2(i, pos.y)).GetComponent<Piece>().isMovedBefore)
                {
                    list.Add(new Vector2(pos.x - 2, pos.y));
                }
                else
                {
                    break;
                }
            }
        }
        
        return list;
    }
    public List<Vector2> Diagonal(Vector2 pos, string color)
    {
        List<Vector2> list = new List<Vector2>();
        bool b1 = true, b2 = true, b3 = true, b4 = true;
        for (int i = 1; i < 7; i++)
        {
            for (int j = 1; j < 7; j++)
            {
                if (b1)
                {
                    Tuple<Vector2, bool> a = GetSequenceValitdation(new Vector2(pos.x + i, pos.y + i), color);
                    if (a.Item1.x >= 0)
                    {
                        list.Add(a.Item1);
                        if (!a.Item2)
                        {
                            b1 = false;
                        }
                    }
                    else
                    {
                        b1 = false;
                    }
                }

                if (b2)
                {
                    Tuple<Vector2, bool> b = GetSequenceValitdation(new Vector2(pos.x - i, pos.y - i), color);
                    if (b.Item1.x >= 0)
                    {
                        list.Add(b.Item1);
                        if (!b.Item2)
                        {
                            b2 = false;
                        }
                    }
                    else
                    {
                        b2 = false;
                    }
                }


                if (b3)
                {
                    Tuple<Vector2, bool> c = GetSequenceValitdation(new Vector2(pos.x + i, pos.y - i), color);
                    if (c.Item1.x >= 0)
                    {
                        list.Add(c.Item1);
                        if (!c.Item2)
                        {
                            b3 = false;
                        }
                    }
                    else
                    {
                        b3 = false;
                    }
                }


                if (b4)
                {
                    Tuple<Vector2, bool> d = GetSequenceValitdation(new Vector2(pos.x - i, pos.y + i), color);
                    if (d.Item1.x >= 0)
                    {
                        list.Add(d.Item1);
                        if (!d.Item2)
                        {
                            b4 = false;
                        }
                    }
                    else
                    {
                        b4 = false;
                    }
                }
            }
        }
        return list;
    }
    public List<Vector2> Plus(Vector2 pos, string color)
    {
        List<Vector2> list = new List<Vector2>();
        bool b1 = true, b2 = true, b3 = true, b4 = true;
        for (int i = 1; i < 7; i++)
        {
            for (int j = 1; j < 7; j++)
            {
                if (b1)
                {
                    Tuple<Vector2, bool> a = GetSequenceValitdation(new Vector2(pos.x, pos.y + i), color);
                    if (a.Item1.x >= 0)
                    {
                        list.Add(a.Item1);
                        if (!a.Item2)
                        {
                            b1 = false;
                        }
                    }
                    else
                    {
                        b1 = false;
                    }
                }

                if (b2)
                {
                    Tuple<Vector2, bool> b = GetSequenceValitdation(new Vector2(pos.x, pos.y - i), color);
                    if (b.Item1.x >= 0)
                    {
                        list.Add(b.Item1);
                        if (!b.Item2)
                        {
                            b2 = false;
                        }
                    }
                    else
                    {
                        b2 = false;
                    }
                }


                if (b3)
                {
                    Tuple<Vector2, bool> c = GetSequenceValitdation(new Vector2(pos.x + i, pos.y), color);
                    if (c.Item1.x >= 0)
                    {
                        list.Add(c.Item1);
                        if (!c.Item2)
                        {
                            b3 = false;
                        }
                    }
                    else
                    {
                        b3 = false;
                    }
                }


                if (b4)
                {
                    Tuple<Vector2, bool> d = GetSequenceValitdation(new Vector2(pos.x - i, pos.y), color);
                    if (d.Item1.x >= 0)
                    {
                        list.Add(d.Item1);
                        if (!d.Item2)
                        {
                            b4 = false;
                        }
                    }
                    else
                    {
                        b4 = false;
                    }
                }
            }
        }
        return list;
    }
}
