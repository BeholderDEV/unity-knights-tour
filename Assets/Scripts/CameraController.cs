using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraDistance = 1.3f;
    public float smothTime = .3f;
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, target, ref velocity, smothTime);
    }

    public void AdjustPosition(int boardSize)
    {
        Vector3 objectSizes = new Vector3(boardSize, 1, boardSize);
        float objectSize = boardSize;
        float cameraView = 2f * Mathf.Tan(.5f * Mathf.Deg2Rad * Camera.main.fieldOfView);
        float distance = cameraDistance * objectSize / cameraView;
        target = new Vector3(boardSize/2f, .5f, boardSize / 2f) - distance * Camera.main.transform.forward;
        
    }
}
