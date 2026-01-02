using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    public float speed;
    public bool teleport;
    private Vector3 target;

    void Start()
    {
        pos1.y = transform.position.y;
        pos2.y = transform.position.y;
        target = pos2;
    }

    void FixedUpdate()
    {
        if (transform.position != target) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
        else {
            if (teleport) {
                if (target == pos2) transform.position = pos1;
                else transform.position = pos2;
            } else {
                if (target == pos2) target = pos1;
                else target = pos2;
            }
        }
    }
}
