
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    public Transform player;
    public GameOverManager gameOverManager;

    void Update()
    {
        if (player == null || gameOverManager == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance < 1f)
        {
            enabled = false;
            gameOverManager.ShowGameOver();
        }
    }
}
