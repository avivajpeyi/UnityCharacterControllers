using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    
    public float GroundDistance = 0.2f;
    public LayerMask GroundLayer;
    
    
    bool isGroundedCheck()
    {
        return Physics.CheckSphere(transform.position, GroundDistance,
            GroundLayer,
            QueryTriggerInteraction.Ignore);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
