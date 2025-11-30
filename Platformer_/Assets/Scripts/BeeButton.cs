using UnityEngine;

public class BeeButton : MonoBehaviour
{
    public BeeMovement bee;
    public PlatformMove platform;
    public bool oneShot = true;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (bee != null)
        {
            bee.StartFollowing(other.transform);
        }

        if (platform != null)
        {
            platform.StartMove();
        }

        if (oneShot)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}


