using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackDistance = 0.5f;

    Transform target;
    bool isFollowing;
    bool hasAttacked;
    Animator anim;

    public void StartFollowing(Transform t)
    {
        target = t;
        isFollowing = true;
        hasAttacked = false;
    }

    public void StopFollowing()
    {
        isFollowing = false;
        target = null;
        hasAttacked = false;
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isFollowing || target == null) return;

        Vector3 dir = target.position - transform.position;
        float dist = dir.magnitude;

        if (!hasAttacked)
        {
            if (dist > attackDistance)
            {
                Vector3 move = dir.normalized * moveSpeed * Time.deltaTime;
                float maxMove = dist - attackDistance;
                if (move.magnitude > maxMove) move = dir.normalized * maxMove;
                transform.position += move;
            }
            else
            {
                if (anim != null)
                {
                    anim.SetTrigger("Attack");
                }
                hasAttacked = true;
                isFollowing = false;
            }
        }

        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 10f * Time.deltaTime);
        }
    }
}



