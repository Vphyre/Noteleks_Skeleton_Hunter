using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int points = 0;
    public int _points
    {
        get{return this.points; }
        set{this.points = value; }
    }
    [SerializeField] private int life = 100;
    public int _life
    {
        get {return this.life; }
        set {this.life = value; }
    }
    [SerializeField] private int arrows = 40;
    public int _arrows
    {
        get {return this.arrows;}
        set {this.arrows = value;}
    }
    [SerializeField] private int enemiesKilled = 0;
    public int _enemiesKilled
    {
        get {return this.enemiesKilled;}
        set {this.enemiesKilled = value;}
    }
    public static PlayerStats playerStats;


    void Start()
    {
        playerStats = this;
    }

    /// <summary>
    /// The function can be used to heal or give damage to the player.
    /// </summary>
    /// <param name="hit">Will represent the function context. Positive value will heal, and negatve value will give damage.</param>
    public void HealDamage(int hit)
    {
        playerStats.life+=hit;
        if(playerStats.life <= 0)
        {
            playerStats.life = 0;
            UI.instance.GameOver();
        }
        else if(playerStats.life>100)
        {
            playerStats.life = 100;
        }
    }
    /// <summary>
    /// The function will increase the player's points.
    /// </summary>
    /// <param name="pointsAmount">The points amount recived</param>
    public void GetPoints(int pointsAmount)
    {
        _points += pointsAmount;
    }
    /// <summary>
    /// The function will increase the player's arrows.
    /// </summary>
    /// <param name="arrowsAmount">The arrow amount recived.</param>
    public void GetArrows(int arrowsAmount)
    {
        _arrows += arrowsAmount;
    }

}
