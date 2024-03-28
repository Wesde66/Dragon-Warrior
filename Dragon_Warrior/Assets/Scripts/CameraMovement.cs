using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    private float direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y,transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * direction), cameraSpeed * Time.deltaTime);
    }

    public void DirectionPlayer(float _direction)
    {
        direction = _direction;
    }
}
