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
        coordinate.text = gameObject.name;
    }
}
