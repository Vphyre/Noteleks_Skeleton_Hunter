using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int playerLayer;
    [SerializeField] protected int minValue = 1;
    [SerializeField] protected int maxValue = 5;
    protected Transform player;
    
    protected virtual void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    protected virtual void Update()
    {
       
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(playerLayer == other.gameObject.layer)
        {
            ItemAction();
        }    
    }
    protected virtual void ItemAction()
    {

    }
}
