using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropping_Lopped_Enemy : MonoBehaviour
{
    public GameObject objToDrop;
    public bool spawner;
    public bool destroyable;
    private void Start()
    {
        if (spawner)
        {
            StartCoroutine(Drop());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!spawner && destroyable)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Drop()
    {
        while (true)
        {
            float timeWait = Random.Range(1f, 3f);
            yield return new WaitForSeconds(timeWait);
            GameObject obj = GameObject.Instantiate(objToDrop, transform.position, Quaternion.identity, gameObject.transform);
            obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            obj.GetComponent<Rigidbody2D>().gravityScale = 1;
            yield return new WaitForSeconds(0.5f);
            obj.GetComponent<Dropping_Lopped_Enemy>().destroyable = true;
        }
    }
}
