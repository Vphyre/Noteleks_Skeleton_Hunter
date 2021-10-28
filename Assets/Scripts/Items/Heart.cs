using UnityEngine;

public class Heart : Item
{
    [SerializeField] private int groundLayer;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        transform.Rotate(new Vector3(0,speed*Time.deltaTime,0));   
    }
        
    private void OnEnable()
    {
        transform.GetComponent<Rigidbody>().useGravity = true;
    }
    protected override void ItemAction()
    {
        base.ItemAction();
        PlayerStats.playerStats.HealDamage(Random.Range(minValue, maxValue));
        gameObject.SetActive(false);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if(groundLayer == other.gameObject.layer)
        {
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }   
    }
}
