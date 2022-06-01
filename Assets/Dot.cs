using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    Vector2 pickup, dropoff,secondPickup;
    bool isPressed;

    private void OnMouseDown()
    {
        if (!isPressed)
        {
        BoardManager._instance.Move(pickup, dropoff);
            isPressed = true;
        }
    }
    public void Set(Vector2 pickup, Vector2 dropoff)
    {
        this.pickup = pickup;
        this.dropoff = dropoff;
    }public void Set(Vector2 pickup,Vector2 secondPickup, Vector2 dropoff)
    {
        this.pickup = pickup;
        this.dropoff = dropoff;
        this.secondPickup = secondPickup;
    }
}
