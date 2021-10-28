using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float shootForce;
    [SerializeField] private float fireRate;
    [SerializeField] private Pooling pooling;
    private float fireRateAux;
    private GameObject castedFire;
    private bool started = false;

    protected override void Start()
    {
        base.Start();
        fireRateAux = fireRate;
    }
    protected override void Update()
    {
        base.Update();
        if(!started)
        {
            started = true;
        }

        if(life <= 0)
        {
            animator.SetBool("Revive", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Death", true);
            animator.SetBool("Attack", false);
        }        
    }
    /// <summary>
    /// The function can be called at animation event.
    /// </summary>
    public void AttackTarget()
    {
        castedFire.SetActive(true);
        castedFire.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z);
        castedFire.GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
        castedFire.GetComponent<Rigidbody>().AddForce(direction * shootForce, ForceMode.Impulse);      
    }
    protected override void SpecificBehavior()
    {
        base.SpecificBehavior();
        if(!started)
            return;
        fireRate -= Time.deltaTime;
        
        if(fireRate > 0)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
            castedFire = pooling.GetPooledObject();
        }
        else if(fireRate<2 && fireRate>0)
        {
            Debug.Log("Entrou");
            castedFire.transform.SetParent(pooling.transform);
        }
        else
        {
            castedFire.transform.SetParent(null);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", true);
            fireRate = fireRateAux;
        } 
    }

}
