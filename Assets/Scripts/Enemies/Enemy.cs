using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float lookRadious = 10f;
    [SerializeField] protected int life = 30;
    [SerializeField] protected GameObject weapon;
    [SerializeField] private GameObject[] itemsDrop;
    private GameObject[] itemsInScene;
    protected Animator animator;
    protected Transform target;
    protected float distance;
    protected NavMeshAgent agent;
    protected Vector3 direction;
    protected bool isAlive;
    protected float deathTime;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        isAlive = true;
        deathTime = 2f;
        itemsInScene = new GameObject[itemsDrop.Length];

        for (int i = 0; i < itemsDrop.Length; i++)
        {
            itemsInScene[i] = Instantiate(itemsDrop[i]);
            itemsInScene[i].SetActive(false);
        }
    }
    protected virtual void Update()
    {  
        EnemyBehavior();
        direction = (target.position - transform.position).normalized;
    }
    private void FaceTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *5f);
    }
    public void TakeDamage(int hit)
    {
        life-=hit;
        if(life <= 0)
        {
            isAlive = false;
            agent.isStopped = true; 
            agent.velocity = Vector3.zero;
            agent.acceleration = 0;
            animator.SetBool("Death", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
            PlayerStats.playerStats._enemiesKilled++;
        }
    }
    private void DisableObject()
    {
        DropItem();
        gameObject.SetActive(false);
    }
    protected virtual void EnemyBehavior()
    {
        if(target == null)
            return;

        distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadious)
        {
            SpecificBehavior();
            FaceTarget();
        }
    }
    protected virtual void OnEnable()
    {
        if(animator == null)
            return;
        animator.SetBool("Idle", true);
        animator.SetBool("Death", false);
        animator.SetBool("Attack", false);
        isAlive = true;
        agent.acceleration = 20f;
        agent.isStopped = false; 
        life = 30;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadious);
    }
    protected virtual void SpecificBehavior()
    {

    }
    protected virtual void DropItem()
    {
        int sort;
        sort = (int)Random.Range(0, itemsInScene.Length-0.1f);
        itemsInScene[sort].transform.position = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
        itemsInScene[sort].SetActive(true);
        itemsInScene[0].SetActive(true);
        itemsInScene[0].transform.position = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
    }
    /// <summary>
    /// Function responsible for respawning enemies at some point on the map.
    /// </summary>
    /// <param name="sortedTransform">It is the position that the enemy will respawn on the map.</param>
    public virtual void Spawn(Vector3 sortedTransform)
    {
        gameObject.SetActive(true);
        transform.position = sortedTransform;
    }
    
}
