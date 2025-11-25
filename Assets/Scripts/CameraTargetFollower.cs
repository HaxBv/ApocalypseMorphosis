using UnityEngine;

public class CameraTargetFollower : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    public float SpeedFollow = 0.025f;
    public Vector3 MoveOffset;

    private void LateUpdate()
    {
        if (player == null)
            return;

        CameraFollow();
    }

    public void UpdatePlayerReference(Transform newPlayer)
    {
        player = newPlayer;
    }

    private void CameraFollow()
    {
        Vector3 desiredPosition = player.position + MoveOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, SpeedFollow);
        transform.position = smoothPosition;
    }
}
