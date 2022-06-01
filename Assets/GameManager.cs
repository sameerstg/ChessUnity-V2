using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public bool isFreeMode;
    public string turn;
    public bool check;
    public string checkBy;
    Camera mCamera;
    Color prColor, seColor;
    private void Awake()
    {
        _instance = this;
        mCamera = Camera.main;
    }
    private void Start()
    {
        prColor = mCamera.backgroundColor;
        seColor = Color.red;
        turn = colorSide.White.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchFreeMode();
        }
    }
    public void SwitchSide()
    {
        if (turn == colorSide.White.ToString())
        {
            turn = colorSide.Black.ToString();
        }
        else
        {
            turn = colorSide.White.ToString();
        }
    }public void SwitchFreeMode()
    {
        if (isFreeMode)
        {
            isFreeMode = false;
        }
        else
        {
            isFreeMode = true;
        }
    }
    public void MakeCheck(string color)
    {
        check = true;
        checkBy = color;
        mCamera.backgroundColor = seColor;
    }
    public void RemoveCheck()
    {
        mCamera.backgroundColor = prColor;

        check = false;
        checkBy = "";
    }
}
