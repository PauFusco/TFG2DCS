using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    [Header("Dash")]
    public float dashSpeed;

    public float dashCooldown;
    public float dashDuration;

    [Header("Jump")]
    public float jumpForce;

    // NO LESS THAN 0.3 !!!!!!
    public float jumpMaxDuration;

    public float fallGravityMultiplier;
}