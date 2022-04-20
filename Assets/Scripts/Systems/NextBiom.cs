using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NextBiom : MonoBehaviour
{
    public int biom;
    public float _camera_x;
    public float _camera_y;
    public Vector3 _player_res;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(NextScene());
        }
    }

    private IEnumerator NextScene()
    {
        GameObject.Find("Curtain").GetComponent<Animator>().Play("Base Layer.FadeIn");
        yield return new WaitForSeconds(2f);
        GameManager.instance.NextBiom(biom, _camera_x, _camera_y, _player_res);
    }
}
