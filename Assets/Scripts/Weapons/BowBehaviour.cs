using UnityEngine;

public class BowBehaviour : MonoBehaviour
{
    [SerializeField] private float arrowSpeed;
    [SerializeField] private Transform arrowPosition;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowPlaceholder;
    [SerializeField] private float fireRate;
    [SerializeField] private float maxHoldingTime;
    [SerializeField] private Pooling pooling;
    [SerializeField] private Animator bowRope;
    private float currentTimePrepareBow;
    private float currentFireRate;
    private GameObject arrow;
    
    // Start is called before the first frame update
    void Start()
    {
       SpawnArrow();
       currentFireRate = fireRate;
    }

    // Update is called once per frame
    private void Update()
    {
        currentFireRate+=Time.deltaTime;
        BowAttack();
    }
    //Create the Aim at screen center
    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/2, Screen.height/2, 100, 20),"+");    
    }
    private void BowAttack()
    {
        if(Input.GetMouseButtonDown(0) && currentFireRate>fireRate)
        {
            bowRope.SetBool("Prepare", true);
            bowRope.SetBool("Shoot", false);
            if(currentTimePrepareBow<maxHoldingTime)
            {
                currentTimePrepareBow += Time.deltaTime;
            }
        }
        if(Input.GetMouseButtonUp(0) && currentFireRate > fireRate)
        {
            ShootArrow();
            currentTimePrepareBow = 0;
            currentFireRate = 0f;
        }

        if(currentFireRate > fireRate && arrow != null && PlayerStats.playerStats._arrows > 0)
        {
            SpawnArrow();
            bowRope.SetBool("Prepare", false);
            bowRope.SetBool("Shoot", false);
        }
    }
    private void SpawnArrow()
    {
        arrowPlaceholder.SetActive(true);
        if (arrow != null)
        {
            arrow = null;
        }
    }
    private void ShootArrow()
    {
        bowRope.SetBool("Prepare", false);
        bowRope.SetBool("Shoot", true);
        arrow = pooling.GetPooledObject();
        arrow.transform.position = arrowPosition.position;
        arrow.transform.rotation = arrowPosition.rotation;
        arrow.SetActive(true);
        arrowPlaceholder.SetActive(false);
        arrow.GetComponent<Rigidbody>().AddForce(arrowSpeed*currentTimePrepareBow*Camera.main.transform.forward);
        PlayerStats.playerStats._arrows--;
    }
}
