using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : Item
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    protected override void ItemAction()
    {
        base.ItemAction();
        PlayerStats.playerStats.GetPoints((int)Random.Range(minValue, maxValue));
        gameObject.SetActive(false);
    }
    
}
