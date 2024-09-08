using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable() //Calls everytime the object becoms disabled 
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            Debug.Log("Moving Left: Enemy position = " + enemy.position.x + ", Left Edge = " + leftEdge.position.x);
            if (enemy.position.x >= leftEdge.position.x) // Changed from >= to > to avoid getting stuck
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            Debug.Log("Moving Right: Enemy position = " + enemy.position.x + ", Right Edge = " + rightEdge.position.x);
            if (enemy.position.x <= rightEdge.position.x) // Changed from <= to < to avoid getting stuck
            {
                MoveInDirection(1);
            }
            else
            {
                Debug.Log("Reached Right Edge, changing direction.");
                DirectionChange();
            }
        }
    }



    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft; // Toggle the direction
        Debug.Log("Direction Changed: Now moving " + (movingLeft ? "Left" : "Right"));
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        Debug.Log("Moving in Direction: " + _direction);

        // Flip the enemy sprite's direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        // Move the enemy
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}