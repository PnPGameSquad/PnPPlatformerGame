using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    [Range(0f, 10f)]
    public float slideFriction = 0.5f;

    [Range(0f, 10f)]
    public float control = 2f;

    [Range(0f, 1f)]
    public float reverseControlFactor = 0.2f;

    private Rigidbody playerRb;
    private Vector3 lastHorizontalVelocity;
    private bool hasLastVelocity = false;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.collider.GetComponentInParent<PlayerController>();
        if (player == null) return;

        playerRb = player.GetComponent<Rigidbody>();
        if (playerRb == null) return;

        Vector3 v = playerRb.linearVelocity;
        lastHorizontalVelocity = new Vector3(v.x, 0f, v.z);
        hasLastVelocity = true;
        Debug.Log("[IcePlatform] Enter");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (playerRb == null) return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        bool hasInput = Mathf.Abs(x) > 0.01f || Mathf.Abs(z) > 0.01f;

        Vector3 v = playerRb.linearVelocity;
        Vector3 currentHorizontal = new Vector3(v.x, 0f, v.z);
        Vector3 vertical = new Vector3(0f, v.y, 0f);

        if (!hasLastVelocity)
        {
            lastHorizontalVelocity = currentHorizontal;
            hasLastVelocity = true;
        }

        float t;

        if (!hasInput)
        {
            t = slideFriction * Time.fixedDeltaTime;
        }
        else
        {
            float dot = 0f;
            Vector3 lastDir = lastHorizontalVelocity;
            Vector3 currDir = currentHorizontal;

            if (lastDir.sqrMagnitude > 0.0001f && currDir.sqrMagnitude > 0.0001f)
            {
                lastDir.Normalize();
                currDir.Normalize();
                dot = Vector3.Dot(lastDir, currDir);
            }

            float controlFactor = control;
            if (dot < 0f)
            {
                controlFactor *= reverseControlFactor;
            }

            t = controlFactor * Time.fixedDeltaTime;
        }

        t = Mathf.Clamp01(t);

        Vector3 blended = Vector3.Lerp(lastHorizontalVelocity, currentHorizontal, t);
        lastHorizontalVelocity = blended;

        playerRb.linearVelocity = blended + vertical;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (playerRb == null) return;

        playerRb = null;
        hasLastVelocity = false;
        Debug.Log("[IcePlatform] Exit");
    }
}







