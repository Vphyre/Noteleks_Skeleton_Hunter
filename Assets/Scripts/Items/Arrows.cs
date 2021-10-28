using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : Item
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        transform.Rotate(new Vector3(0,speed*Time.deltaTime,0));   
    }
    protected override void ItemAction()
    {
        base.ItemAction();
        PlayerStats.playerStats.GetArrows(Random.Range(minValue, maxValue));
        gameObject.SetActive(false);
    }
}
