using UnityEngine;
using UnityEngine.Timeline;

public class PlayerLightController : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject playerLight;

    // Update is called once per frame
    void Update()
    {
        // Light rotation = Camera rotation
        playerLight.transform.localEulerAngles = playerCamera.transform.localEulerAngles;

        // Toggle light
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerLight.SetActive(!playerLight.activeSelf);
        }
    }
}
