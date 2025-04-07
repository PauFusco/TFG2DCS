using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed; public float turnFrames; public float accelerationFrames; public float decelerationFrames;

    [Header("Dash")]
    public float dashSpeed; public float dashFrames; public float dashCooldownFrames;

    [Header("Jump")]
    public float jumpHeight; public float jumpMaxFrames; public float jumpCutoffFrames;

    [Header("Fall")]
    public float fallGravityMultiplier; public float fallDurationFrames;
}