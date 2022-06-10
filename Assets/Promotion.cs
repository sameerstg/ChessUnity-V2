using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promotion : MonoBehaviour
{
    bool isPressed;
    private void OnMouseDown()
    {
        if (!isPressed)
        {
            print(gameObject.GetInstanceID());
        BoardManager._instance.Promote(gameObject.name);
            print("pressed");
        transform.parent.GetComponent<DestroyScript>().Destroy();
            isPressed = true;
        }

    }
}
