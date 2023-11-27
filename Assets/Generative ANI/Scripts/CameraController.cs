using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 primayOffset = new(0, 3f, -6.5f);
    [SerializeField] private Vector3 secondaryOffset = new(0, 1.178f, -0.417f);
    [SerializeField] private bool primaryView = true;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (primaryView)
            {
                transform.localPosition = secondaryOffset;
                primaryView = false;
            }
            else
            {
                transform.localPosition = primayOffset;
                primaryView = true;
            }
        }
    }
}
