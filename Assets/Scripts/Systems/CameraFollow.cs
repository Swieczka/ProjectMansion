using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float timeOffset;

    [SerializeField] Vector2 posOffset;

    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float topLimit;
    [SerializeField] float bottomLimit;

    private Vector3 velocity;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = Player.transform.position;

        endPos.x = posOffset.x;
        endPos.y = posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset*Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit),Mathf.Clamp(transform.position.y, topLimit, bottomLimit),transform.position.z);
    }
}
