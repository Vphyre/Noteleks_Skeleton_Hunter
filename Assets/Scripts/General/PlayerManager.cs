using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;

    //Singleton
    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }
}
