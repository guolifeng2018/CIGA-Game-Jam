using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject m_target;

    public float LeftPosition;
    public float RightPosition;
    public float MoveSpeed;

    public Vector3 m_positionOffset = new Vector3(-1.39f, -2.06f, 0f);
    
    void Start()
    {
        m_positionOffset = m_target.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 toPosition = CalculateCameraPosition();
        toPosition.x = Mathf.Clamp(toPosition.x, LeftPosition, RightPosition);
        transform.position = Vector3.Slerp(transform.position, toPosition, Time.deltaTime * MoveSpeed);
    }

    private Vector3 CalculateCameraPosition()
    {
        return m_target.transform.position - m_positionOffset;
    }
}
