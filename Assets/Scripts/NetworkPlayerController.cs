using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Minimal server-authoritative third-person movement.
/// Keyboard input is read by the owning client and sent to the server.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class NetworkPlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -18f;
    [SerializeField] private float turnSpeed = 12f;

    private CharacterController controller;
    private Vector2 serverMoveInput;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!IsSpawned) return;

        if (IsOwner)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            input = Vector2.ClampMagnitude(input, 1f);
            SubmitMoveInputRpc(input);
        }

        if (IsServer)
        {
            MoveOnServer();
        }
    }

    [Rpc(SendTo.Server, Delivery = RpcDelivery.Unreliable)]
    private void SubmitMoveInputRpc(Vector2 input)
    {
        serverMoveInput = Vector2.ClampMagnitude(input, 1f);
    }

    private void MoveOnServer()
    {
        Vector3 direction = new Vector3(serverMoveInput.x, 0f, serverMoveInput.y);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, turnSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 velocity = direction * moveSpeed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}
