using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerTransform;
    Transform CamTransform;
    //public Transform player;
    public Vector3 offset;
    [Range(1, 10)] // csuszkat csinal a kovetkezo ertekre
    public float smoothFactor;

    
    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        CamTransform = Camera.main.transform;
    }
    

    private void Update() // fixed update is smoother
    {
        Follow();
    }

    void Follow()
    {
        // smoothing
        // WITH UP/DOWN MOVEMENT + SMOOTHNESS
        //Vector3 playerPosition = player.position + offset;
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, playerPosition, smoothFactor * Time.deltaTime); // Linear Interpolation, which means moving from a pont to another in a linear way
        // * Time.fixedDeltaTime ==> makes sure the numbers remain the same on all PC's
        //transform.position = playerPosition;
        // ONLY ON X AXIS
        
        CamTransform.position = new Vector3(playerTransform.position.x, CamTransform.position.y,  CamTransform.position.z);
        //Debug.Log(playerTransform.position.x);
    }
}
