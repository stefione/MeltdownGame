using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    Camera cam;
    [SerializeField] Vector3 _cameraOffset;
    [SerializeField] Vector3 _cameraLookAt;
    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
    }
    protected override void Update()
    {
        cam.transform.position = transform.position + transform.TransformVector(_cameraOffset);
        cam.transform.forward = (_cameraLookAt - transform.TransformVector(_cameraOffset));

        if (Input.GetKeyDown(KeyCode.Space) && !ActionInProgress)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.C) && !ActionInProgress)
        {
            Duck();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            DuckFinish();
        }
    }

    public override void ActivateRagdoll()
    {
        base.ActivateRagdoll();
        GameManager.Instance.GameOver(false);
    }
}
