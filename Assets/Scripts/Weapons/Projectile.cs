using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        StartCoroutine("Despawn");
    }
    protected virtual void OnDisable()
    {
        StopCoroutine("Despawn");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
