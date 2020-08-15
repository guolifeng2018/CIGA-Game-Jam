using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateItemLayer : MonoBehaviour
{
    void Start()
    {
        Vector3 position = transform.position;
        position.z = position.y;
        transform.position = position;
    }
}
