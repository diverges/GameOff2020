using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOnHover : MonoBehaviour
{
    public Animator animator;

    public void OnMouseOver()
    {
        animator.SetBool("isHovering", true);
    }

    public void OnMouseExit()
    {
        animator.SetBool("isHovering", false);
    }
}
