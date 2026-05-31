using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity;       // How fast camera will move with mouse
    public float offset = 0.5f;     // Distance to move camera so it matches player eye level
    public float constrainUp;       // How far camera can move up
    public float constrainDown;     // How far camera can move down
    public Transform player;        // Player's transform
    public GameObject editorOnly;    // Container for editor only objects

    private float rotationH;
    private float rotationV;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // Set initial rotation values to camera's current rotation values
        rotationH = transform.localEulerAngles.y;
        rotationV = transform.localEulerAngles.x;

        // Disable editor only objects. This should be done by them just having the tag afaik but it just doesn't work
        // This should probably be in a game manager script but that doesn't exist when I made this and I bet nobody will be bothered to move this
        editorOnly.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        // Lock cursor to window
        Cursor.lockState = CursorLockMode.Locked;
        
        // Update camera rotation with mouse movement, faster / slower based on sensitivity value
        rotationH += Input.GetAxis("Mouse X") * sensitivity;
        rotationV += Input.GetAxis("Mouse Y") * sensitivity * -1;

        // Clamp camera angle so it doesn't go too far up or down
        rotationV = Mathf.Clamp(rotationV, constrainDown, constrainUp);

        // Update camera's rotation
        transform.localEulerAngles = new Vector3(rotationV, rotationH);

        // Keep camera locked to player
        transform.position = player.position;
    }
}
