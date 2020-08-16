using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraControl : MonoBehaviour
{
    public GameObject m_target;

    public float LeftPosition;
    public float RightPosition;
    public float MoveSpeed;

    public Vector3 m_positionOffset = new Vector3(-2.42f, -1.82f, 10f);

    public float m_shakeDuration = 0.2f;
    public float m_magnitude = 0.1f;
    private bool m_isLeft;
    
    void Start()
    {
        UpdateOffset();
        GlobalEvent.AddEvent("ShakeCamera", ShakeCamera);
    }

    private void OnDestroy()
    {
        GlobalEvent.RemoveEvent("ShakeCamera", ShakeCamera);
    }

    private void FixedUpdate()
    {
        Vector3 toPosition = CalculateCameraPosition();
        toPosition.x = Mathf.Clamp(toPosition.x, LeftPosition, RightPosition);
        toPosition.y = -0.1337692f;
        transform.position = Vector3.Slerp(transform.position, toPosition, Time.deltaTime * MoveSpeed);

        UpdateOffset();
    }

    private void UpdateOffset()
    {
        if (m_positionOffset.x > 0 && !m_isLeft)
        {
            m_positionOffset = m_target.transform.position - transform.position;
            m_isLeft = true;
        }
        else if (m_positionOffset.x < 0 && m_isLeft)
        {
            m_positionOffset = m_target.transform.position - transform.position;
            m_isLeft = false;
        }
    }

    private Vector3 CalculateCameraPosition()
    {
        return m_target.transform.position - m_positionOffset;
    }

#if UNITY_EDITOR
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ShakeCamera();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Vector3 offset = m_target.transform.position - transform.position;
            Debug.LogError(offset);
        }
    }
#endif

    private void ShakeCamera(params  object[] args)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(m_shakeDuration, m_magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return 0;
        }

        transform.localPosition = originalPos;
    }
}
