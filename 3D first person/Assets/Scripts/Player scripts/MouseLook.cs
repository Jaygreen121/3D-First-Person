using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float lookSensitivity = 2f; // , lookSmoothDamp = 0.5f;
    private float yRot, xRot;
    // private float currentY, currentX;
    // private float yRotationV, xRotationV;
    private Camera currentCamera;
    public Camera mainCamera;
    public Camera secondaryCamera;

    private bool isThirdPerson = false;

    // Start is called before the first frame update
    void Start()
    {
        secondaryCamera.enabled = false;
        
        mainCamera = Camera.main;

        currentCamera = mainCamera;

        Cursor.lockState = CursorLockMode.Locked; // Locks cursor to the game screen when clicked
    }

    // Update is called once per frame
    void Update()
    {
        yRot += Input.GetAxis("Mouse X") * lookSensitivity;
        xRot += Input.GetAxis("Mouse Y") * lookSensitivity;

        //currentX = Mathf.SmoothDamp(currentX, xRot, ref xRotationV, lookSmoothDamp);
        //currentY = Mathf.SmoothDamp(currentY, yRot, ref yRotationV, lookSmoothDamp);
        // SmoothDamp basically moves a float value from current to desired value over a period  of time

        xRot = Mathf.Clamp(xRot, -80, 80); // Restricts the xRotation value to be less than 80 and more than -80, so that the player does not backflip



        transform.rotation = Quaternion.Euler(0 , yRot, 0); // Setting the left and right rotation of the gameobject

        currentCamera.transform.localRotation = Quaternion.Euler(-xRot, 0 , 0); // Setting the up and down rotation to the main camera

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && isThirdPerson) // Forward
        {
            firstPerson();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && !isThirdPerson)
        {
            thirdPerson();
        }
    }

    void firstPerson()
    {
        // currentCamera.transform.localPosition = new Vector3(0f, 0.45f, 0f);

        // Enable the main camera
        mainCamera.enabled = true;

        // Then disable the secondary camera
        secondaryCamera.enabled = false;

        currentCamera = mainCamera;

        isThirdPerson = !isThirdPerson;
    }

    void thirdPerson()
    {
        // currentCamera.transform.localPosition = new Vector3(0f, 2f, -3f);

        // Enable the secondary camera
        secondaryCamera.enabled = true;

        // Then disable the main camera
        mainCamera.enabled = false;

        currentCamera = secondaryCamera;

        isThirdPerson = !isThirdPerson;
    }
}
