using UnityEngine;
using System.Collections;

/// <summary>
/// Objective arrow
/// </summary>
public class RotateObjectBehaviour : MonoBehaviour
{
    [SerializeField] protected float m_RotationSpeed;


    private float m_StartPosAxis;


    //Bounce the indicator up and down, and rotate it.
    void Update()
    {
        //transform.Rotate(Vector3.right * (m_RotationSpeed * Time.deltaTime));
        transform.Rotate(Vector3.up * (m_RotationSpeed * Time.deltaTime));
    }
}