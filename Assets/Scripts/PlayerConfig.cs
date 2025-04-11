using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed; public float turnFrames; public float accelerationFrames; public float decelerationFrames;

    [Header("Dash")]
    public float dashSpeed; public float dashFrames; public float dashCooldownFrames;

    [Header("Jump")]
    public float jumpSpeed; public float jumpHeight; public float jumpCutoffFrames;

    [Header("Fall")]
    public float baseGravity; public float fallGravityMultiplier; 
}