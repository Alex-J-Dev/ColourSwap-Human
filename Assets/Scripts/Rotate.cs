using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float rotateSpeed = 5;

    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, rotateSpeed);
    }
}
