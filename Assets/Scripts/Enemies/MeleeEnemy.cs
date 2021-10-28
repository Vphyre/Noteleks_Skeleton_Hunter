using UnityEngine;

public class MeleeEnemy : Enemy
{
    private bool attackTrigger;
    protected override void Start()
    {
        base.Start();
        attackTrigger = false;
        
    }
    protected override void Update()
    {
        base.Update();
        
        if(distance<=2f)
        {
            animator.SetFloat("Walk", 0f);
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack",false);
        }
    }
    protected override void EnemyBehavior()
    {
        base.EnemyBehavior();
    }
    private void ChaseTarget()
    {
        agent.SetDestination(target.position);
        animator.SetFloat("Walk", agent.acceleration);
        animator.SetBool("Idle", false);
    }
    /// <summary>
    /// The function can be called at animation event. Once used, It's necessary reuse to turnoff the collider.
    /// </summary>
    public void AttackTarget()
    {
       attackTrigger = !attackTrigger;
       weapon.GetComponent<MeshCollider>().enabled = attackTrigger;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if(animator == null)
            return;
    }
    protected override void SpecificBehavior()
    {
        base.SpecificBehavior();
        ChaseTarget();
    }
    
}
