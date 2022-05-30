using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class Piece : MonoBehaviour
{
    public IPiece piece;
    public string name;
    public string color;
    public int possibleMovesCount;
    public bool isMovedBefore;
    public Possiblity possibleMoves;
    public Vector2 position;

    BoardManager bM;
    //  for local functions
    Vector3 pos;
    Vector3 scale;
    bool isHovered;
    private void Start()
    {
        bM = BoardManager._instance;
    }
    public void InitializeClass(string name, string color, Vector2 position)
    {
        if (name == pieceName.WPawn.ToString())
        {
            piece = new WPawn(color, position);
        }
        else if (name == pieceName.BPawn.ToString())
        {
            piece = new BPawn(color, position);
            
        }
        else if (name == pieceName.BKnight.ToString())
        {
            piece = new BKnight(color, position);

        }
        else if (name.Contains(pieceName.WKnight.ToString()))
        {
            
            piece = new BKnight(color, position);

        }
        else if (name.Contains(pieceName.BBishop.ToString()))
        {
            piece = new BBishop(color, position);

        }
        else if (name.Contains(pieceName.WBishop.ToString()))
        {
            piece = new WBishop(color, position);

        }
        else
        {
            
            piece = new WPawn(color, position);
        }

        piece.Name = name;
        this.name = piece.Name;
        this.color = piece.Color;
        gameObject.tag = piece.Color;
        possibleMovesCount = piece.PossibleMovesCount;
        isMovedBefore = piece.IsMovedBefore;
        possibleMoves = piece.PossibleMoves;
        this.position = piece.Position;


    }
    private void OnMouseDown()
    {
        print(piece);
        print(piece.Name);
        bM.Deselect();
        piece.GetAllMoves();
        bM.ValidatePossiblities(piece.Position);
    }
    public void OnMouseOver()
    {
        if (!isHovered)
        {
            pos = transform.position;
            pos.z -= 1;
            transform.position = pos;
            scale = transform.localScale;
            scale *= 1.3f;
            transform.localScale = scale;
            isHovered = true;
        }

    }
    private void OnMouseExit()
    {
        if (isHovered)
        {
            pos = transform.position;
            pos.z += 1;
            transform.position = pos;
            scale = transform.localScale;
            scale /= 1.3f;
            transform.localScale = scale;
            isHovered = false;
        }


    }

}



[System.Serializable]
public class WPawn : IPiece
{
    public WPawn(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        try
        {
            if (BoardManager.grid.py[(int)Position.y + 1].x[(int)Position.x] == null)
            {
                PossibleMoves.Possiblities.Add(Moves._instance.Up1(Position));
                try
                {
                    if (!IsMovedBefore && BoardManager.grid.py[(int)Position.y + 2].x[(int)Position.x] == null)
                    {
                        PossibleMoves.Possiblities.Add(Moves._instance.Up2(Position));
                    }
                }
                catch (System.Exception)
                {


                }
            }
        }
        catch (System.Exception)
        {



        }
        try
        {
            if (BoardManager.grid.py[(int)Position.y + 1].x[(int)Position.x + 1] != null &&
           BoardManager.grid.py[(int)Position.y + 1].x[(int)Position.x + 1].tag == colorSide.black.ToString())
            {
                PossibleMoves.Possiblities.Add(Moves._instance.UpR1(Position));
            }
        }
        catch (System.Exception)
        {


        }
        try
        {

            if (BoardManager.grid.py[(int)Position.y + 1].x[(int)Position.x - 1] != null &&
              BoardManager.grid.py[(int)Position.y + 1].x[(int)Position.x - 1].tag == colorSide.black.ToString())
            {
                PossibleMoves.Possiblities.Add(Moves._instance.UpL1(Position));
            }

        }
        catch (System.Exception)
        {


        }
        
        BoardManager.grid.possiblities = PossibleMoves;
        Validate();
    }

    public override void GetPossibleMovesCount()
    {
        PossibleMovesCount = PossibleMoves.Possiblities.Count;
    }

    public override void Move(Vector2 pos)
    {
        base.Move(pos);
    }

    public override void Validate()
    {
        GetPossibleMovesCount();
    }
}

[System.Serializable]
public class BPawn : IPiece
{
    public BPawn(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        try
        {
            if (BoardManager.grid.py[(int)Position.y - 1].x[(int)Position.x] == null)
            {
                PossibleMoves.Possiblities.Add(Moves._instance.Down1(Position));
                try
                {
                    if (!IsMovedBefore && BoardManager.grid.py[(int)Position.y - 2].x[(int)Position.x] == null)
                    {
                        PossibleMoves.Possiblities.Add(Moves._instance.Down2(Position));
                    }
                }
                catch (System.Exception)
                {


                }
            }
        }
        catch (System.Exception)
        {



        }
        
        try
        {
            if (BoardManager.grid.py[(int)Position.y - 1].x[(int)Position.x + 1] != null &&
           BoardManager.grid.py[(int)Position.y - 1].x[(int)Position.x + 1].tag == colorSide.white.ToString())
            {
                PossibleMoves.Possiblities.Add(Moves._instance.DownR1(Position));
            }
        }
        catch (System.Exception)
        {


        }
        try
        {

            if (BoardManager.grid.py[(int)Position.y - 1].x[(int)Position.x - 1] != null &&
              BoardManager.grid.py[(int)Position.y - 1].x[(int)Position.x - 1].tag == colorSide.white.ToString())
            {
                PossibleMoves.Possiblities.Add(Moves._instance.DownL1(Position));
            }

        }
        catch (System.Exception)
        {


        }

        BoardManager.grid.possiblities = PossibleMoves;
        Validate();
    }

    public override void GetPossibleMovesCount()
    {
        PossibleMovesCount = PossibleMoves.Possiblities.Count;
    }

    public override void Move(Vector2 pos)
    {
        base.Move(pos);
    }

    public override void Validate()
    {
        GetPossibleMovesCount();
    }
}

[System.Serializable]
public class BKnight : IPiece
{
    public BKnight(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        PossibleMoves.Possiblities = Moves._instance.L(Position);

        BoardManager.grid.possiblities = PossibleMoves;
        Validate();
    }

    public override void GetPossibleMovesCount()
    {
        PossibleMovesCount = PossibleMoves.Possiblities.Count;
    }

    public override void Move(Vector2 pos)
    {
        base.Move(pos);
    }

    public override void Validate()
    {

        GetPossibleMovesCount();
    }
}
[System.Serializable]
public class WBishop : IPiece
{
    public WBishop(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        PossibleMoves.Possiblities = Moves._instance.Diagonal(Position);

        BoardManager.grid.possiblities = PossibleMoves;
        Validate();
    }

    public override void GetPossibleMovesCount()
    {
        PossibleMovesCount = PossibleMoves.Possiblities.Count;
    }

    public override void Move(Vector2 pos)
    {
        base.Move(pos);
    }

    public override void Validate()
    {

        GetPossibleMovesCount();
    }
}
[System.Serializable]
public class BBishop : IPiece
{
    public BBishop(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        PossibleMoves.Possiblities = Moves._instance.Diagonal(Position);

        BoardManager.grid.possiblities = PossibleMoves;
        Validate();
    }

    public override void GetPossibleMovesCount()
    {
        PossibleMovesCount = PossibleMoves.Possiblities.Count;
    }

    public override void Move(Vector2 pos)
    {
        base.Move(pos);
    }

    public override void Validate()
    {

        GetPossibleMovesCount();
    }
}

public abstract class IPiece
{
    string name;
    string color;
    int possibleMovesCount;
    bool isMovedBefore;
    Possiblity possibleMoves;
    Vector2 position;
    Transform transform;
    public int PossibleMovesCount { get => possibleMovesCount; set => possibleMovesCount = value; }
    public bool IsMovedBefore { get => isMovedBefore; set => isMovedBefore = value; }
    public Possiblity PossibleMoves { get => possibleMoves; set => possibleMoves = value; }
    public string Name { get => name; set => name = value; }
    public Vector2 Position { get => position; set => position = value; }
    public string Color { get => color; set => color = value; }
    public Transform Transform { get => transform; set => transform = value; }

    public abstract void GetAllMoves();
    public abstract void GetPossibleMovesCount();
    public virtual void Move(Vector2 pos)
    {
        IsMovedBefore = true;
        Position = pos;
    }

    public abstract void Validate();




}
public class PieceClass
{
    public bool isMovedBefore;
    public int possibleMovesCount;
    public Grid possibleMoves;
}