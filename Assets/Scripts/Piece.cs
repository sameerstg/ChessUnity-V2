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
    Moves moves;
    //  for local functions
    Vector3 pos;
    Vector3 scale;
    bool isHovered;
    private void Awake()
    {
        moves = Moves._instance;
    }
    private void Start()
    {
        bM = BoardManager._instance;

    }
    public void InitializeClass(string name, string color, Vector2 position)
    {
        if (name.Contains(pieceName.WPawn.ToString()))
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
        else if (name.Contains(pieceName.WQueen.ToString()))
        {
            piece = new WQueen(color, position);

        }
        else if (name.Contains(pieceName.BQueen.ToString()))
        {
            piece = new BQueen(color, position);

        }
        else if (name.Contains(pieceName.WRook.ToString()))
        {
            piece = new WRook(color, position);

        }
        else if (name.Contains(pieceName.BRook.ToString()))
        {
            piece = new BRook(color, position);

        }
        else if (name.Contains(pieceName.WKing.ToString()))
        {
            piece = new Wking(color, position);

        }
        else if (name.Contains(pieceName.BKing.ToString()))
        {
            piece = new Bking(color, position);

        }
        piece.Moves = moves;
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
        /*        print(piece);
                print(piece.Name);
        */
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

        Vector2 move;
        GameObject go;
        move = Moves.Up1(Position);
        if (Moves.InLimit(move)&& Moves.GetGoByVector2(move) == null)
        {
            PossibleMoves.Possiblities.Add(move);
            if (!IsMovedBefore)
            {
                move = Moves.Up2(Position);
                if (Moves.InLimit(move)&& Moves.GetGoByVector2(move) == null)
                {
                    PossibleMoves.Possiblities.Add(move);
                }
            }
        }
        move = Moves.UpR1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move)&& go != null && go.tag != Color)
        {
            PossibleMoves.Possiblities.Add(move);
        }
        move = Moves.UpL1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move)&& go != null && go.tag != Color)
        {
            PossibleMoves.Possiblities.Add(move);
        }


        BoardManager._instance.grid.possiblities = PossibleMoves;
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
        Vector2 move;
        GameObject go;
        move = Moves.Down1(Position);
        if (Moves.InLimit(move)&& Moves.GetGoByVector2(move) == null)
        {
            PossibleMoves.Possiblities.Add(move);
            if (!IsMovedBefore)
            {
                move = Moves.Down2(Position);
                if (Moves.InLimit(move)&& Moves.GetGoByVector2(move) == null)
                {
                    PossibleMoves.Possiblities.Add(move);
                }
            }
        }
        move = Moves.DownR1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move)&& go != null && go.tag != Color)
        {
            PossibleMoves.Possiblities.Add(move);
        }
        move = Moves.DownL1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move)&& go != null && go.tag != Color)
        {
            PossibleMoves.Possiblities.Add(move);
        }

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
        PossibleMoves.Possiblities = Moves.L(Position);

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
        PossibleMoves.Possiblities = Moves.Diagonal(Position, Color);

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
        PossibleMoves.Possiblities = Moves.Diagonal(Position, Color);

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
public class WQueen : IPiece
{
    public WQueen(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        PossibleMoves.Possiblities = Moves.Diagonal(Position, Color);
        PossibleMoves.Possiblities.AddRange(Moves.Plus(Position, Color));

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
public class BQueen : IPiece
{
    public BQueen(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        PossibleMoves.Possiblities = Moves.Diagonal(Position, Color);
        PossibleMoves.Possiblities.AddRange(Moves.Plus(Position, Color));

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
public class WRook : IPiece
{
    public WRook(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        PossibleMoves.Possiblities = Moves.Plus(Position, Color);

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
public class BRook : IPiece
{
    public BRook(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        PossibleMoves.Possiblities = Moves.Plus(Position, Color);
        BoardManager._instance.grid.possiblities = PossibleMoves;
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
public class Wking : IPiece
{
    public Wking(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        Vector2 move;
        GameObject go;
        move = Moves.DownL1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.DownR1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Down1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.UpL1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.UpR1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Up1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Left(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Right(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move))
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if (go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
public class Bking : IPiece
{
    public Bking(string color, Vector2 position)
    {
        Color = color;
        Position = position;
    }

    public override void GetAllMoves()
    {
        PossibleMoves = new Possiblity();
        Vector2 move;
        GameObject go;
        move = Moves.DownL1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.DownR1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Down1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.UpL1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.UpR1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Up1(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Left(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }
        move = Moves.Right(Position);
        go = Moves.GetGoByVector2(move);
        if (Moves.InLimit(move) )
        {
            if (go == null)
            {
                PossibleMoves.Possiblities.Add(move);
            }
            else if(go.tag != Color)
            {
                PossibleMoves.Possiblities.Add(move);
            }
        }

        BoardManager._instance.grid.possiblities = PossibleMoves;
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
    Moves moves;
    public int PossibleMovesCount { get => possibleMovesCount; set => possibleMovesCount = value; }
    public bool IsMovedBefore { get => isMovedBefore; set => isMovedBefore = value; }
    public Possiblity PossibleMoves { get => possibleMoves; set => possibleMoves = value; }
    public string Name { get => name; set => name = value; }
    public Vector2 Position { get => position; set => position = value; }
    public string Color { get => color; set => color = value; }
    public Transform Transform { get => transform; set => transform = value; }
    public Moves Moves { get => moves; set => moves = value; }

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