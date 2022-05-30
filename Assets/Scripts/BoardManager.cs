using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;


public class BoardManager : MonoBehaviour
{
    public static BoardManager _instance;

    public static Grid grid;
    public List<GameObject> blackPiecePrefab, whitePiecePrefab;
    Vector3 pos;
    GameObject dotParrent;
    GameObject dot;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        dot = dot = Resources.Load<GameObject>("Dot");
        dotParrent = transform.GetChild(2).gameObject;
        GenerateBoard();
        
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
        Transform pieceParrent = transform.GetChild(0);
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
                      
                        grid.py[i].x[j].GetComponent<Piece>().InitializeClass(name, colorSide.black.ToString(), new Vector2(j, i));
                    }
                    else
                    {
                        grid.py[i].x[j].GetComponent<Piece>().InitializeClass(name, colorSide.white.ToString(), new Vector2(j, i));

                    }

                }


            }
        }
        /*FlipBoard();*/
        FlipPieces();
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
    public void ValidatePossiblities( Vector2 pickup)
    {
        ShowMoves(pickup);
    }
    void ShowMoves(Vector2 pickup)
    {
        foreach (var p in grid.possiblities.Possiblities)
        {
            
           
                if (grid.py[(int)p.y].x[(int)p.x]!= null && grid.py[(int)p.y].x[(int)p.x].tag == grid.py[(int)pickup.y].x[(int)pickup.x].tag)
                {
                continue;
                }
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

        GameObject piece = grid.py[(int)pick.y].x[(int)pick.x];
        grid.py[(int)pick.y].x[(int)pick.x] = null;

        if (grid.py[(int)drop.y].x[(int)drop.x] != null)
        {
            grid.py[(int)drop.y].x[(int)drop.x].SetActive(false);
            grid.py[(int)drop.y].x[(int)drop.x]= null;
        }

        grid.py[(int)drop.y].x[(int)drop.x] = piece;

        Vector3 posToBe;
        posToBe.x = grid.y[(int)drop.y].x[(int)drop.x].transform.position.x;
        posToBe.y = grid.y[(int)drop.y].x[(int)drop.x].transform.position.y;
        posToBe.z = piece.transform.position.z;
        grid.py[(int)drop.y].x[(int)drop.x].transform.position = posToBe;

        piece.GetComponent<Piece>().piece.Move(drop);

        Deselect();
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