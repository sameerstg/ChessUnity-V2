using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum colorSide
    {
        White,Black
    }
    public enum pieceName
    {
        BPawn,WPawn, BKnight,WKnight,BBishop,WBishop,WQueen,BQueen,WRook,BRook,WKing, BKing
    }
    public enum coordinates
    {
        A=0,B=1,C=2,D=3,E=4,F=5,G=6,H=7
    }
}
