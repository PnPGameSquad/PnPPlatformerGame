using UnityEngine;

public class Portal : MonoBehaviour
{
    public string destinationTag = "whitehole1";
    public BeeMovement bee;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameObject dest = GameObject.FindGameObjectWithTag(destinationTag);
        if (dest == null) return;

        other.transform.position = dest.transform.position;

        if (bee != null)
        {
            bee.StopFollowing();
        }
    }
}
