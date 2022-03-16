using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] bool _is_Collected;
    void Start()
    {
        _is_Collected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _is_Collected = true;
            Debug.Log("Collected");
            gameObject.SetActive(false);
        }
    }
}
