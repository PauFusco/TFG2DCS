using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private float oPositionX;

    private void Awake()
    {
        oPositionX = transform.localPosition.x;
    }

    public void UpdateDirection(bool currentPlayerDirection)
    {
        var tempPos = transform.localPosition;
        tempPos.x = oPositionX * (currentPlayerDirection ? 1 : -1);

        transform.localPosition = tempPos;
    }
}
