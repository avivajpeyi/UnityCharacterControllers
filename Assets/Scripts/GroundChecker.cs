using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public float GroundDistance = 0.2f;
    public LayerMask GroundLayer;
    private Transform _parent;
    private float _ydisp;


    bool isGroundedCheck()
    {
        return Physics.CheckSphere(transform.position, GroundDistance,
            GroundLayer,
            QueryTriggerInteraction.Ignore);
    }

    void SetUp()
    {
        _parent = transform.parent;
        Vector3 disp = transform.position - _parent.position;
        _ydisp = Mathf.Abs(disp.y);
        transform.parent = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            _parent.position.x,
            _parent.position.y - _ydisp,
            _parent.position.z
        );
    }
}