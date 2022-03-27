using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;

    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    [SerializeField] private float playerxMin;
    [SerializeField] private float playerxMax;
    [SerializeField] private float playeryMin;
    [SerializeField] private float playeryMax;

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, xMin, xMax), Mathf.Clamp(player.position.y, yMin, yMax), transform.position.z);
        player.position = new Vector3(Mathf.Clamp(player.position.x, playerxMin, playerxMax), Mathf.Clamp(player.position.y, playeryMin, playeryMax));
    }
    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = playerCam.position + offset;
        Vector3 SmoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = SmoothPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(xMin, yMax), new Vector2(xMax, yMax));
        Gizmos.DrawLine(new Vector2(xMax, yMax), new Vector2(xMax, yMin));
        Gizmos.DrawLine(new Vector2(xMax, yMin), new Vector2(xMin, yMin));
        Gizmos.DrawLine(new Vector2(xMin, yMin), new Vector2(xMin, yMax));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(playerxMin, playeryMax), new Vector2(playerxMax, playeryMax));
        Gizmos.DrawLine(new Vector2(playerxMax, playeryMax), new Vector2(playerxMax, playeryMin));
        Gizmos.DrawLine(new Vector2(playerxMax, playeryMin), new Vector2(playerxMin, playeryMin));
        Gizmos.DrawLine(new Vector2(playerxMin, playeryMin), new Vector2(playerxMin, playeryMax));
    }
}
