using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour
{

    public KeyCode scrollLeft = KeyCode.A;
    public KeyCode scrollRight = KeyCode.D;
    public KeyCode scrollUp = KeyCode.W;
    public KeyCode scrollDown = KeyCode.S;

    public KeyCode scrollMouse = KeyCode.Mouse1;

    public float scrollSpeed = 2.0f;

    private float zoomMin = 40.0f;
    private float zoomMax = 120.0f;
    private float zoomSpeed = 40.0f;

    private Camera camera;


    // The target we are following
    Transform target;

    // The distance in the x-z plane to the target
    float distance = 10.0f;


    // the height we want the camera to be above the target
    float height = 5.0f;


    // How much we 
    float heightDamping = 2.0f;
    float rotationDamping = 3.0f;
    float mouseWheelDistanceRate = 5f;
    float mouseWheelHeightRate = 5f;


    // Place the script in the Camera-Control group in the component menu
    //@script AddComponentMenu("Camera-Control/Smooth Follow With Zoom")

    // Use this for initialization
    void Start()
    {
        camera = Camera.mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPosition = camera.transform.position;

        if (Input.GetKey(scrollMouse))
        {
            Screen.lockCursor = true;
            Screen.showCursor = false;

            if (Input.GetAxis("Mouse X") < 0) { camPosition.x += scrollSpeed * Input.GetAxis("Mouse X"); }
            if (Input.GetAxis("Mouse X") > 0) { camPosition.x += scrollSpeed * Input.GetAxis("Mouse X"); }
            if (Input.GetAxis("Mouse Y") < 0) { camPosition.z += scrollSpeed * Input.GetAxis("Mouse Y"); }
            if (Input.GetAxis("Mouse Y") > 0) { camPosition.z += scrollSpeed * Input.GetAxis("Mouse Y"); }
        }
        else
        {
            Screen.lockCursor = false;
            Screen.showCursor = true;

            if (Input.GetKey(scrollLeft)) { camPosition.x -= scrollSpeed; }
            if (Input.GetKey(scrollRight)) { camPosition.x += scrollSpeed; }
            if (Input.GetKey(scrollUp)) { camPosition.z += scrollSpeed; }
            if (Input.GetKey(scrollDown)) { camPosition.z -= scrollSpeed; }
        }

        //if (Input.GetAxis("Mouse ScrollWheel") != 0)
        //{
        //    // Mouse wheel moving forwards


        //    camPosition.y = Mathf.Clamp(camPosition.y - zoomSpeed * Input.GetAxis("Mouse ScrollWheel"), zoomMin, zoomMax);
        //}

        camera.transform.position = camPosition;
        //LateUpdate();
    }


    //private void LateUpdate()
    //{
    //    // Early out if we don't have a target


    //    //if (!target)
    //    //    return;
    //    if (Input.GetAxis("Mouse ScrollWheel") != 0)
    //    {
    //        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
    //        distance -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelDistanceRate;
    //        height -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelHeightRate;
        

    //    Transform target = camera.transform;


    //    // Calculate the current rotation angles
    //    float wantedRotationAngle = target.eulerAngles.y;
    //    float wantedHeight = target.position.y + height;
    //    float currentRotationAngle = transform.eulerAngles.y;
    //    float currentHeight = transform.position.y;

    //    // Damp the rotation around the y-axis
    //    currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

    //    // Damp the height
    //    currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

    //    // Convert the angle into a rotation
    //    Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

    //    // Set the position of the camera on the x-z plane to:
    //    // distance meters behind the target
    //    transform.position = target.position;
    //    transform.position -= currentRotation * Vector3.forward * distance;

    //    Vector3 pos = transform.position;
    //    pos.y = currentHeight;
    //    // Set the height of the camera
    //    transform.position = pos;

    //    // Always look at the target
    //    //transform.LookAt(target);

    //    }
    //}

}
