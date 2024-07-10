using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Animator animator;
   public void Punch()
    {
        animator.SetTrigger("Punch");
    }
}
