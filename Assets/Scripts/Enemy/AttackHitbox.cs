using UnityEngine;
using UnityEngine.Events;

public class AttackHitbox : MonoBehaviour
{
    public UnityEvent hitPlayer;
    public UnityEvent parry;

    // Layers:
    // 6 Player
    // 7 Enemy
    // 8 Attack
    // 9 Parry

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            hitPlayer.Invoke();

            Debug.Log("Player");
        }

        if (collision.gameObject.layer == 9)
        {
            parry.Invoke();

            Debug.Log("Parry");
        }
    }
}
