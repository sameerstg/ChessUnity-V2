using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Enums;


public class BoardManager : MonoBehaviour
{
    public static BoardManager _instance;
    public List<GameObject> blackPiecePrefab, whitePiecePrefab;
    Vector3 pos;
    GameObject dotParrent;
    GameObject dot;
    GameManager gm;
    Moves moves;
    public Last last;
    public SecondLast secondLast;
    public GridX[] temp;
    public IPiece bKing, wKing;
    Transform pieceParrent;
    int totalMovesCount;
    TextMeshProUGUI movesCountText;
    public Grid grid;
    private void Awake()
    {
        _instance = this;
        last = new Last();
        secondLast = new SecondLast();
        movesCountText = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        moves = Moves._instance;
        gm = GameManager._instance;
        dot = dot = Resources.Load<GameObject>("Dot");
        dotParrent = transform.GetChild(2).gameObject;
        GenerateBoard();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GoMove(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GoMove(1);
        }
    }
    public void GenerateBoard()
    {
        grid = new Grid();
        Material black = Resources.Load<Material>("Black"), white = Resources.Load<Material>("White");
        GameObject Cube = Resources.Load<GameObject>("Cube");
        Material mat = white;
        Transform parrent = transform.GetChild(0);
        Vector3 firstTrans = parrent.GetChild(0).position;
        //  Setting Tiles
        for (int i = 7; i > -1; i--)
        {
            for (int j = 0; j < 8; j++)
            {
                if (mat == white)
                {
                    mat = black;
                }
                else
                {
                    mat = white;
                }
                GameObject tile = Instantiate(Cube, transform.position, Quaternion.identity);
                grid.allTiles.Add(tile);
                grid.y[i].x[j] = tile;
                tile.transform.parent = parrent;
                tile.GetComponent<MeshRenderer>().material = mat;
                tile.transform.position = firstTrans;
                firstTrans.x += Cube.transform.localScale.x;
            }
            if (mat == white)
            {
                mat = black;
            }
            else
            {
                mat = white;
            }
            firstTrans.x = parrent.transform.GetChild(0).position.x;
            firstTrans.y -= Cube.transform.localScale.y;
        }
        GameObject tileParrent = transform.GetChild(0).gameObject;
        tileParrent.transform.parent = transform.parent;
        transform.position = (grid.y[3].x[3].transform.position + grid.y[4].x[4].transform.position) / 2;
        tileParrent.transform.parent = transform;
        // Setting Pieces
        // Setting White Pieces
        pieceParrent = transform.GetChild(0);
        for (int i = 0; i < 8; i++)
        {
            grid.py[0].x[i] = Instantiate(whitePiecePrefab[i], grid.y[0].x[i].transform.position, Quaternion.identity);
            grid.py[0].x[i].transform.localScale = grid.y[0].x[i].transform.localScale;
            grid.py[0].x[i].transform.parent = pieceParrent;
            grid.allPiecesObj.Add(grid.py[0].x[i]);

        }

        for (int i = 0; i < 8; i++)
        {
            grid.py[1].x[i] = Instantiate(whitePiecePrefab[8], grid.y[1].x[i].transform.position, Quaternion.identity);
            grid.py[1].x[i].transform.localScale = grid.y[0].x[i].transform.localScale;
            grid.py[1].x[i].transform.parent = pieceParrent;
            grid.allPiecesObj.Add(grid.py[1].x[i]);

        }

        for (int i = 0; i < 8; i++)
        {
            grid.py[7].x[i] = Instantiate(blackPiecePrefab[i], grid.y[7].x[i].transform.position, Quaternion.identity);
            grid.py[7].x[i].transform.localScale = grid.y[7].x[i].transform.localScale;
            grid.py[7].x[i].transform.parent = pieceParrent;
            grid.allPiecesObj.Add(grid.py[7].x[i]);


        }
        for (int i = 0; i < 8; i++)
        {
            grid.py[6].x[i] = Instantiate(blackPiecePrefab[8], grid.y[6].x[i].transform.position, Quaternion.identity);
            grid.py[6].x[i].transform.localScale = grid.y[6].x[i].transform.localScale;
            grid.py[6].x[i].transform.parent = pieceParrent;
            grid.allPiecesObj.Add(grid.py[6].x[i]);

        }
        for (int i = 7; i >= 0; i--)
        {
            for (int j = 0; j <= 7; j++)
            {
                grid.y[i].x[j].name = $"{(coordinates)(j)}{i + 1}";
                if (grid.py[i].x[j] != null)
                {
                    string name = grid.py[i].x[j].name;
                    name = name.Replace("(Clone)", "");
                    grid.py[i].x[j].name = name.Replace(" ", "");

                    pos = grid.py[i].x[j].transform.position;
                    pos.z -= 2;
                    grid.py[i].x[j].transform.position = pos;
                    if (grid.py[i].x[j].name.StartsWith("B"))
                    {
                        grid.py[i].x[j].GetComponent<Piece>().InitializeClass(name, colorSide.Black.ToString(), new Vector2(j, i));
                    }
                    else
                    {

                        grid.py[i].x[j].GetComponent<Piece>().InitializeClass(name, colorSide.White.ToString(), new Vector2(j, i));

                    }

                }


            }
        }
        SaveMove();
        movesCountText.text = $"{totalMovesCount} || {grid.history.Count}";

        /*FlipBoard();*/
        FlipPieces();
    }
    public void ResetBoard()
    {
        foreach (var item in grid.allDots)
        {
            Destroy(item);
        }
        foreach (var item in grid.allPiecesObj)
        {
            Destroy(item);
        }
        foreach (var item in grid.allTiles)
        {
            Destroy(item);
        }
        transform.GetChild(2).transform.SetSiblingIndex(0);
        gm.turn = "White";
        GenerateBoard();
        totalMovesCount = 0;
        movesCountText.text = $"{totalMovesCount} || {grid.history.Count}";


    }
    public void FlipBoard()
    {
        if (transform.localRotation.z == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        FlipPieces();

    }
    public void FlipPieces()
    {
        if (grid.allPiecesObj[0].transform.rotation.z == 180)
        {
            foreach (var item in grid.allPiecesObj)
            {
                item.transform.Rotate(0, 0, 0);

            }
        }
        else
        {
            foreach (var item in grid.allPiecesObj)
            {
                item.transform.Rotate(0, 0, 180);

            }
        }

    }
    void Check()
    {
        if (gm.isFreeMode)
        {
            if (KingCheck(colorSide.White.ToString()))
            {

                gm.MakeCheck(colorSide.Black.ToString());
                return;
            }
            else if (KingCheck(colorSide.Black.ToString()))
            {

                gm.MakeCheck(colorSide.White.ToString());
                return;
            }


        }
        else if (gm.turn == colorSide.Black.ToString())
        {
            if (KingCheck(colorSide.Black.ToString()))
            {
                gm.MakeCheck(colorSide.White.ToString());
                return;

            }
        }
        else if (gm.turn == colorSide.White.ToString())
        {
            if (KingCheck(colorSide.White.ToString()))
            {

                gm.MakeCheck(colorSide.Black.ToString());
                return;
            }
        }
        gm.RemoveCheck();
    }
    bool KingCheck(string color)
    {
        Vector2 move;
        GameObject go;
        bool b1, b2, b3, b4, b5, b6, b7, b8;
        b1 = b2 = b3 = b4 = b5 = b6 = b7 = b8 = true;
        Tuple<bool, bool> a;
        Tuple<bool, GameObject> k;

        if (color == colorSide.Black.ToString())
        {
            pos = bKing.Position;

            move = new Vector2(pos.x - 1, pos.y - 1);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("WPawn") || k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }
            move = new Vector2(pos.x + 1, pos.y - 1);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("WPawn") || k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }
        }
        else
        {
            pos = wKing.Position;

            move = new Vector2(pos.x - 1, pos.y + 1);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("WPawn") || k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }
            move = new Vector2(pos.x + 1, pos.y + 1);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("WPawn") || k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }
            move = new Vector2(pos.x + 1, pos.y);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }
            move = new Vector2(pos.x - 1, pos.y);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }
            move = new Vector2(pos.x, pos.y + 1);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }
            move = new Vector2(pos.y, pos.y - 1);
            if (moves.InLimit(move))
            {
                k = moves.GetValitdationForCheck(move, color);
                if (k.Item1 && k.Item2.name.Contains("King"))
                {
                    return true;
                }
            }

        }
        for (int i = 1; i < 7; i++)
        {
            for (int j = 1; j < 7; j++)
            {


                if (b1)
                {

                    move = new Vector2(pos.x + i, pos.y + i);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsDiagonalTaker(go))
                        {

                            return true;
                        }
                        else
                        {
                            b1 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b1 = false;
                    }
                }

                if (b2)
                {

                    move = new Vector2(pos.x - i, pos.y - i);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsDiagonalTaker(go))
                        {

                            return true;
                        }
                        else
                        {
                            b2 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b2 = false;
                    }
                }

                if (b3)
                {

                    move = new Vector2(pos.x + i, pos.y - i);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsDiagonalTaker(go))
                        {

                            return true;
                        }
                        else
                        {
                            b3 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b3 = false;
                    }
                }

                if (b4)
                {

                    move = new Vector2(pos.x - i, pos.y + i);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsDiagonalTaker(go))
                        {

                            return true;
                        }
                        else
                        {
                            b4 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b4 = false;
                    }
                }
                if (b5)
                {

                    move = new Vector2(pos.x, pos.y + i);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsPlusTaker(go))
                        {

                            return true;
                        }
                        else
                        {
                            b5 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b5 = false;
                    }
                }

                if (b6)
                {

                    move = new Vector2(pos.x, pos.y - i);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsPlusTaker(go))
                        {
                            print(go.name);
                            return true;
                        }
                        else
                        {
                            b6 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b6 = false;
                    }
                }

                if (b7)
                {

                    move = new Vector2(pos.x + i, pos.y);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsPlusTaker(go))
                        {
                            print(go.name);
                            return true;
                        }
                        else
                        {
                            b7 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b7 = false;
                    }
                }

                if (b8)
                {

                    move = new Vector2(pos.x - i, pos.y);
                    a = moves.GetSequenceValitdationForCheck(move, color);
                    if (a.Item1)
                    {
                        go = moves.GetGoByVector2(move);
                        if (moves.IsPlusTaker(go))
                        {
                            print(go.name);
                            return true;
                        }
                        else
                        {
                            b8 = false;
                        }
                    }
                    else if (!a.Item2)
                    {
                        b8 = false;
                    }
                }

                if (i != j && i < 3 && j < 3)
                {
                    move = new Vector2(pos.x + i, pos.y + j);
                    if (moves.InLimit(move))
                    {
                        k = moves.GetValitdationForCheck(move, color);
                        if (k.Item1 && k.Item2.name.Contains("Knight"))
                        {
                            return true;
                        }
                    }
                    move = new Vector2(pos.x - i, pos.y - j);
                    if (moves.InLimit(move))
                    {
                        k = moves.GetValitdationForCheck(move, color);
                        if (k.Item1 && k.Item2.name.Contains("Knight"))
                        {
                            return true;
                        }
                    }
                    move = new Vector2(pos.x + i, pos.y - j);
                    if (moves.InLimit(move))
                    {
                        k = moves.GetValitdationForCheck(move, color);
                        if (k.Item1 && k.Item2.name.Contains("Knight"))
                        {
                            return true;
                        }
                    }
                    move = new Vector2(pos.x - i, pos.y + j);
                    if (moves.InLimit(move))
                    {
                        k = moves.GetValitdationForCheck(move, color);
                        if (k.Item1 && k.Item2.name.Contains("Knight"))
                        {
                            return true;
                        }
                    }
                }




            }
        }

        return false;

    }
    void ValidateDots(Vector2 pickup)
    {
        List<Vector2> toDelete = new List<Vector2>();
        GameObject piece = grid.py[(int)pickup.y].x[(int)pickup.x];
        GameObject takePiece;
        Vector2 drop;
        gm.check = false;
        for (int i = 0; i < grid.possiblities.Possiblities.Count; i++)
        {
            takePiece = null;
            drop = grid.possiblities.Possiblities[i];
            if (grid.py[(int)drop.y].x[(int)drop.x] != null)
            {
                takePiece = grid.py[(int)drop.y].x[(int)drop.x];

            }

            /*if (*//*same piece check*//*grid.py[(int)p.y].x[(int)p.x] != null &&
                grid.py[(int)p.y].x[(int)p.x].tag == grid.py[(int)pickup.y].x[(int)pickup.x].tag ||
               *//*if drop is king*//* grid.py[(int)p.y].x[(int)p.x] != null &&
               grid.py[(int)p.y].x[(int)p.x].name.Contains("King"))
            {
                toDelete.Add(p);
                print("deleted");
                continue;
            }*/

            if (grid.py[(int)drop.y].x[(int)drop.x] != null)
            {
                if (grid.py[(int)drop.y].x[(int)drop.x].tag == grid.py[(int)pickup.y].x[(int)pickup.x].tag
                    || grid.py[(int)drop.y].x[(int)drop.x].name.Contains("King"))
                {
                    toDelete.Add(drop);
                    
                }

            }
            grid.py[(int)drop.y].x[(int)drop.x] = null;
            grid.py[(int)pickup.y].x[(int)pickup.x] = null;
            grid.py[(int)drop.y].x[(int)drop.x] = piece;
            
            


            if (KingCheck(piece.tag))
            {
                print("check deleted");
                
                toDelete.Add(drop);
            }
            /*            gm.RemoveCheck();*/

            /* grid.py = null;
             grid.py = temp;*/
            grid.py[(int)pickup.y].x[(int)pickup.x] = piece;
            grid.py[(int)drop.y].x[(int)drop.x] = takePiece;

        }
       
        foreach (var item in toDelete)
        {
            grid.possiblities.Possiblities.Remove(item);

        }
    }
    public void ValidatePossiblities(Vector2 pickup)
    {
        ValidateDots(pickup);
        ShowMoves(pickup);
    }
    void ShowMoves(Vector2 pickup)
    {
        foreach (Vector2 p in grid.possiblities.Possiblities)
        {


            GameObject nDot = Instantiate(dot, grid.y[(int)p.y].x[(int)p.x].transform.position, Quaternion.identity);
            pos = nDot.transform.position;
            pos.z -= 3;
            nDot.transform.position = pos;
            nDot.transform.parent = dotParrent.transform;
            grid.allDots.Add(nDot);
            Dot dc = nDot.GetComponent<Dot>();
            dc.Set(pickup, p);
        }
    }
    public void Deselect()
    {
        foreach (var item in grid.allDots)
        {
            Destroy(item);
        }
    }
    public void Move(Vector2 pick, Vector2 drop)
    {
        secondLast.LastSelectedGo = last.LastSelectedGo;
        secondLast.LastSelectedGoPos = last.LastSelectedGoPos;
        if (moves.GetGoByVector2(pick).name == pieceName.WPawn.ToString()
            && moves.GetGoByVector2(drop) == null
            && pick.x != drop.x)
        {
            grid.py[(int)drop.y - 1].x[(int)drop.x].SetActive(false);
            grid.py[(int)drop.y - 1].x[(int)drop.x] = null;
        }
        else if (moves.GetGoByVector2(pick).name == pieceName.BPawn.ToString()
            && moves.GetGoByVector2(drop) == null
            && pick.x != drop.x)
        {
            grid.py[(int)drop.y + 1].x[(int)drop.x].SetActive(false);
            grid.py[(int)drop.y + 1].x[(int)drop.x] = null;
        }

        GameObject piece = grid.py[(int)pick.y].x[(int)pick.x];

        grid.py[(int)pick.y].x[(int)pick.x] = null;

        if (grid.py[(int)drop.y].x[(int)drop.x] != null)
        {
            grid.py[(int)drop.y].x[(int)drop.x].SetActive(false);
            grid.py[(int)drop.y].x[(int)drop.x] = null;
        }


        grid.py[(int)drop.y].x[(int)drop.x] = piece;

        Vector3 posToBe;
        posToBe.x = grid.y[(int)drop.y].x[(int)drop.x].transform.position.x;
        posToBe.y = grid.y[(int)drop.y].x[(int)drop.x].transform.position.y;
        posToBe.z = piece.transform.position.z;
        grid.py[(int)drop.y].x[(int)drop.x].transform.position = posToBe;

        piece.GetComponent<Piece>().piece.Move(drop);
        last.LastSelectedGo = grid.py[(int)drop.y].x[(int)drop.x];
        last.LastSelectedGoPos = drop;
        if (moves.GetGoByVector2(drop).name.Contains("Pawn"))
        {
            if (PromotionCheck())
            {
                PromotionInstantiation();
            }
        }
        else if (moves.GetGoByVector2(drop).name.Contains("King"))
        {
            if (pick.x - drop.x == 2)
            {
                piece = grid.py[(int)pick.y].x[0];
                grid.py[(int)pick.y].x[0] = null;
                grid.py[(int)drop.y].x[(int)drop.x + 1] = piece;


                posToBe.x = grid.y[(int)drop.y].x[(int)drop.x + 1].transform.position.x;
                posToBe.y = grid.y[(int)drop.y].x[(int)drop.x + 1].transform.position.y;
                posToBe.z = piece.transform.position.z;
                grid.py[(int)drop.y].x[(int)drop.x + 1].transform.position = posToBe;

                piece.GetComponent<Piece>().piece.Move(drop);
            }
            else if (pick.x - drop.x == -2)
            {
                piece = grid.py[(int)pick.y].x[7];
                grid.py[(int)pick.y].x[7] = null;
                grid.py[(int)drop.y].x[(int)drop.x - 1] = piece;
                posToBe.x = grid.y[(int)drop.y].x[(int)drop.x - 1].transform.position.x;
                posToBe.y = grid.y[(int)drop.y].x[(int)drop.x - 1].transform.position.y;
                posToBe.z = piece.transform.position.z;
                grid.py[(int)drop.y].x[(int)drop.x - 1].transform.position = posToBe;
            }
        }
        MovePlus();
        SaveMove();
        gm.SwitchSide();
        Deselect();
        Check();
    }

    void MovePlus()
    {
        totalMovesCount += 1;
        movesCountText.text = $"{totalMovesCount} || {grid.history.Count}";
    }
    void GoMove(int where)
    {
        if (totalMovesCount + where >= 0 && totalMovesCount + where <= grid.history.Count-1)
        {

            totalMovesCount += where;
            print(grid.history.Count);
            movesCountText.text = $"{totalMovesCount} || {grid.history.Count}";

            /*            grid.py = grid.history[currentMoveCount + where].y;
            */
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid.py[i].x[j]!=null)
                    {
                        /*grid.py[i].x[j].SetActive(false);*/
                        grid.py[i].x[j].SetActive(false);
                        grid.py[i].x[j] = null;

                    }
                    if (grid.history[totalMovesCount].y[i].x[j] != null)
                    {
                        print("damm");
                        grid.py[i].x[j] = grid.history[totalMovesCount].y[i].x[j];
                        Vector3 pos = grid.y[i].x[j].transform.position;
                        pos.z -= 2;

                        grid.py[i].x[j].transform.position = pos;
                        grid.py[i].x[j].SetActive(true);
                    }
                }
            }
        }
    }
    void SaveMove()
    {
        grid.history.Add(new History(grid.py));
    }
    public void CheckmateCheck()
    {
        if (gm.check)
        {
            if (gm.checkBy == Enums.colorSide.White.ToString())
            {
                foreach (var piece in grid.allPiecesObj)
                {
                    if (piece.CompareTag(Enums.colorSide.Black.ToString()))
                    {
                        var pieceScript = piece.GetComponent<Piece>().piece;
                        pieceScript.GetAllMoves();

                        if (pieceScript.PossibleMovesCount > 0)
                        {
                            Deselect();
                            return;
                        }
                        Deselect();
                    }
                }
            }
            else
            {
                foreach (var piece in grid.allPiecesObj)
                {
                    if (piece.CompareTag(Enums.colorSide.White.ToString()))
                    {
                        var pieceScript = piece.GetComponent<Piece>().piece;
                        pieceScript.GetAllMoves();
                        if (pieceScript.PossibleMovesCount > 0)
                        {
                            Deselect();
                            return;
                        }
                        Deselect();
                    }
                }
            }
        }
        print("checkmate");
    }
    bool PromotionCheck()
    {
        if (last.LastSelectedGo.name.Contains("Pawn"))
        {
            if (last.LastSelectedGoPos.y == 7 || last.LastSelectedGoPos.y == 0)
            {
                print("promotion time");
                return true;
            }
        }
        return false;
    }
    void PromotionInstantiation()
    {
        print(last.LastSelectedGoPos);
        if (last.LastSelectedGo.name.StartsWith("W"))
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Pieces/WPromotion"));
            obj.transform.parent = pieceParrent.transform;
            Vector3 pos = last.LastSelectedGo.transform.position;
            pos.y += 1f;
            pos.x = 0;
            obj.transform.position = pos;
        }
        else
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Pieces/BPromotion"));
            obj.transform.parent = pieceParrent.transform;
            obj.transform.position = last.LastSelectedGo.transform.position;
            pos.y += 1f;
            pos.x = 0;
            obj.transform.position = pos;

        }

    }

    public void Promote(string pieceName)
    {
        grid.allPiecesObj.Remove(last.LastSelectedGo);
        Destroy(last.LastSelectedGo);
        if (pieceName.StartsWith("W"))
        {

            foreach (var item in whitePiecePrefab)
            {
                if (item.name == pieceName)
                {


                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x] = null;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x] = Instantiate(item, grid.y[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.position, Quaternion.identity);
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.localScale = grid.y[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.localScale;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.parent = pieceParrent;
                    grid.allPiecesObj.Add(grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x]);


                    string name = grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].name;
                    name = name.Replace("(Clone)", "");
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].name = name.Replace(" ", "");

                    pos = grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.position;
                    pos.z -= 2;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.position = pos;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.Rotate(0, 0, 180);

                    if (grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].name.StartsWith("B"))
                    {
                        grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].GetComponent<Piece>().InitializeClass(name, colorSide.Black.ToString(), new Vector2((int)last.LastSelectedGoPos.x, (int)last.LastSelectedGoPos.y));
                    }
                    else
                    {

                        grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].GetComponent<Piece>().InitializeClass(name, colorSide.White.ToString(), new Vector2((int)last.LastSelectedGoPos.x, (int)last.LastSelectedGoPos.y));

                    }
                }
            }
        }
        else
        {
            foreach (var item in blackPiecePrefab)
            {
                if (item.name == pieceName)
                {

                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x] = null;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x] = Instantiate(item, grid.y[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.position, Quaternion.identity);
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.localScale = grid.y[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.localScale;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.parent = pieceParrent;
                    grid.allPiecesObj.Add(grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x]);

                    string name = grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].name;
                    name = name.Replace("(Clone)", "");
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].name = name.Replace(" ", "");

                    pos = grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.position;
                    pos.z -= 2;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.position = pos;
                    grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].transform.Rotate(0, 0, 180);
                    if (grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].name.StartsWith("B"))
                    {
                        grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].GetComponent<Piece>().InitializeClass(name, colorSide.Black.ToString(), new Vector2((int)last.LastSelectedGoPos.x, (int)last.LastSelectedGoPos.y));
                    }
                    else
                    {

                        grid.py[(int)last.LastSelectedGoPos.y].x[(int)last.LastSelectedGoPos.x].GetComponent<Piece>().InitializeClass(name, colorSide.White.ToString(), new Vector2((int)last.LastSelectedGoPos.x, (int)last.LastSelectedGoPos.y));

                    }
                }
            }
        }
    }
}
[System.Serializable]
public class GridX
{
    public GameObject[] x = new GameObject[8];
}
[System.Serializable]
public class PieceX
{
    public IPiece[] ppx = new IPiece[8];
}
[System.Serializable]
public class History
{
    public GridX[] y = new GridX[8];

    public History(GridX[] g)
    {
        for (int i = 0; i < 8; i++)
        {
            y[i] = new GridX();
        }
        y = g;
    }
}

[System.Serializable]
public class Grid
{
    public List<GameObject> allTiles;
    public List<GameObject> allPiecesObj;
    public List<GameObject> allDots;
    public GridX[] y;
    public GridX[] dy;
    public GridX[] py;
    public PieceX[] ppy;
    public Possiblity possiblities;
    public List<History> history;
    public Grid()
    {
        allTiles = new List<GameObject>();
        allPiecesObj = new List<GameObject>();
        possiblities = new Possiblity();
        allDots = new List<GameObject>();
        y = new GridX[8];
        dy = new GridX[8];
        ppy = new PieceX[8];
        py = new GridX[8];
       history = new List<History>();

        for (int i = 0; i < 8; i++)
        {
            y[i] = new GridX();
            dy[i] = new GridX();
            py[i] = new GridX();
            ppy[i] = new PieceX();
        }
        
    }
}
[System.Serializable]
public class Possiblity
{
    List<Vector2> possiblities;
    public Possiblity()
    {
        Possiblities = new List<Vector2>();
    }
    public List<Vector2> Possiblities { get => possiblities; set => possiblities = value; }
}
[System.Serializable]
public class Last
{
    GameObject lastSelectedGo;
    Vector2 lastSelectedGoPos;

    public Last()
    {
    }

    public GameObject LastSelectedGo { get => lastSelectedGo; set => lastSelectedGo = value; }
    public Vector2 LastSelectedGoPos { get => lastSelectedGoPos; set => lastSelectedGoPos = value; }
}
[System.Serializable]
public class SecondLast
{
    GameObject lastSelectedGo;
    Vector2 lastSelectedGoPos;

    public SecondLast()
    {

    }

    public GameObject LastSelectedGo { get => lastSelectedGo; set => lastSelectedGo = value; }
    public Vector2 LastSelectedGoPos { get => lastSelectedGoPos; set => lastSelectedGoPos = value; }
}