using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Tile : MonoBehaviour
{
    TextMeshProUGUI coordinate;
    private void Start()
    {
        coordinate = GameObject.FindGameObjectWithTag("Coordinate").GetComponent<TextMeshProUGUI>();
    }
    private void OnMouseOver()
    {
        coordinate.text = gameObject.name+$" || {(int)Enum.Parse(typeof(Enums.coordinates),gameObject.name.Substring(0,1)) }" +
            $"{(int.Parse(gameObject.name.Substring(1,1))-1)}";
    }
}
