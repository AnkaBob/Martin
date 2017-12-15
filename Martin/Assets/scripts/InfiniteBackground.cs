using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{

    public SpriteRenderer background1;
    public SpriteRenderer background2;

    [Range(0f, 1f)]
    public float scrollY;

    private SpriteRenderer fixedBackground;
    private SpriteRenderer movableBackground;

    private float cameraInitialY;
    private float cameraOffsetY;

    private Vector3 previousCameraPos= Camera.main.transform.position;

    // Use this for initialization
    void Start()
    {
        cameraInitialY = Camera.main.transform.position.y;
        cameraOffsetY = background1.transform.position.y - cameraInitialY;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var delta1 = Camera.main.transform.position.x - background1.transform.position.x;
        var delta2 = Camera.main.transform.position.x - background2.transform.position.x;
        float closestDelta;

        if (Mathf.Abs(delta1) < Mathf.Abs(delta2))
        {
            fixedBackground = background1;
            movableBackground = background2;
            closestDelta = delta1;
        }
        else
        {
            fixedBackground = background2;
            movableBackground = background1;
            closestDelta = delta2;
        }

        //Debug.Log("test");

        if (closestDelta > 0 && movableBackground.transform.position.x < fixedBackground.transform.position.x)
        {
            var position = movableBackground.transform.position;
            position.x = fixedBackground.transform.position.x + fixedBackground.size.x;
            movableBackground.transform.position = position;
        }
        else if (closestDelta < 0 && movableBackground.transform.position.x > fixedBackground.transform.position.x)
        {
            var position = movableBackground.transform.position;
            position.x = fixedBackground.transform.position.x - fixedBackground.size.x;
            movableBackground.transform.position = position;
        }

        var cameraDeltaY = Camera.main.transform.position.y - cameraInitialY;
        var positionBackground1 = background1.transform.position;
        positionBackground1.y = Camera.main.transform.position.y + cameraOffsetY - cameraDeltaY * scrollY;
        background1.transform.position = positionBackground1;

        var positionBackground2 = background2.transform.position;
        positionBackground2.y = background1.transform.position.y;
        background2.transform.position = positionBackground2;


        //Parallax
        Vector3 CameraMovement = previousCameraPos - Camera.main.transform.position;
        background2.transform.position -= CameraMovement / 2;
        background1.transform.position -= CameraMovement / 2;
        previousCameraPos = Camera.main.transform.position;
}
}
