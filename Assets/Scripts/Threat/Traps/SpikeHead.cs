using UnityEngine;

public class SpikeHead : Trap
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private readonly Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void OnEnable() => Stop();

    private void Update()
    {
        //Move spikehead to destination only if attacking
        if (attacking)
            transform.Translate(speed * Time.deltaTime * destination);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                SearchForPlayer();
        }
    }
    private void SearchForPlayer()
    {
        CalculateDirections();

        //Check if spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }
    private void Stop()
    {
        destination = transform.position; //Set destination as current position so it doesn't move
        attacking = false;
    }


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        SoundManager.Instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collider);
        Stop(); //Stop spikehead once he hits something
    }
}