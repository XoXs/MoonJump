using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    // script on camera to follow player
    public Transform playerPos;
    public float smoothness;
    public Vector3 Velocity;
    public float CameraSpeed;

    public bool follow = true;

    void Update()
    {
        if (!follow)
            return;

        if ((transform.position.x - (Camera.main.orthographicSize / 2f)) > playerPos.position.x)
        {
            dead();
        }
        Vector3 camPos = transform.position;
        camPos.x += CameraSpeed * Time.deltaTime;
        Vector3 target = new Vector3(playerPos.position.x + 2.8f, playerPos.position.y , -10f);

        transform.position = Vector3.SmoothDamp(transform.position, target, ref Velocity, smoothness);
    }
    void dead()
    {
        print("Player dead!!!!");
    }
}
