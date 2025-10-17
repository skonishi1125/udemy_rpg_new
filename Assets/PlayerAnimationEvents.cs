using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void DisableMovementAndJump() => player.EnableMovementAndJump(false);

    private void EnableMovementAndJump() => player.EnableMovementAndJump(true);

    private void AttackStarted()
    {
        Debug.Log("Attack started!");
        // call method from 'Player' script
        // that method should stop movement of the player game object

    }
}
