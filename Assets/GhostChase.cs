
using UnityEngine;

public class GhostChase : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;

    private bool chasing;
    private bool escaped;
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void StartChasing()
    {
        chasing = true;
    }

    public void StopChasing()
    {
        escaped = true;
        chasing = false;
    }

    void Update()
    {
        if (!chasing || escaped || player == null)
            return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        direction.Normalize();

        transform.rotation = Quaternion.LookRotation(direction);

        if (controller != null && controller.enabled)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
