using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PS.PlayerState state;

    private void Awake()
    {
        state = PS.PlayerState.idle;
    }
}