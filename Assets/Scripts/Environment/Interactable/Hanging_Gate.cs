using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanging_Gate : Interactable
{
    [SerializeField] Animator animator;
    [SerializeField] bool isActivated;
    [SerializeField] string _animationName;
    private void Update()
    {
        
    }
    public override void Action()
    {
        isActivated = !isActivated;
        if (isActivated)
        {
            animator.SetFloat("AnimSpeed",1f);
            animator.Play(_animationName);
        }
        else
        {
            animator.SetFloat("AnimSpeed", -1f);
            animator.Play(_animationName);
        }
    }
}
