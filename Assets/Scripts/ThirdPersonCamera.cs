using Unity.Netcode;
using UnityEngine;

/// <summary>Attach to the camera. Follows only the local player's player object.</summary>
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 3.2f, -5.5f);
    [SerializeField] private float smooth = 8f;
    [SerializeField] private float lookHeight = 1.2f;

    private Transform target;

    private void LateUpdate()
    {
        if (target == null)
        {
            if (NetworkManager.Singleton == null || NetworkManager.Singleton.LocalClient == null)
                return;

            var playerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
            if (playerObject == null) return;

            target = playerObject.transform;
        }

        Vector3 desiredPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smooth * Time.deltaTime);
        Vector3 lookPoint = target.position + Vector3.up * lookHeight;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(lookPoint - transform.position),
            smooth * Time.deltaTime
        );
    }
}
