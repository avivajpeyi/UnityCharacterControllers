using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHardStop : MonoBehaviour
{
    private float originalDrag = 0f;

    private Rigidbody playerRb;
    private ClickController playerClickController;

    private void Start()
    {
        playerClickController = FindObjectOfType<ClickController>();
        playerRb = playerClickController.gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerClickController.isDashing)
            {
                playerRb.velocity = Vector3.zero;
                Debug.Log("Force stop");
                originalDrag = playerRb.drag;
                playerRb.drag = 1000;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerClickController.isDashing)
            {
                playerRb.drag = originalDrag;
            }
        }
    }

}