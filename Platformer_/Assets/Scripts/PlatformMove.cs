using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 3f;

    Vector3 startPos;
    Vector3 targetPos;
    bool moving;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + transform.forward * moveDistance;
    }

    public void StartMove()
    {
        moving = true;
    }

    void Update()
    {
        if (!moving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (transform.position == targetPos)
        {
            moving = false;
        }
    }
}


