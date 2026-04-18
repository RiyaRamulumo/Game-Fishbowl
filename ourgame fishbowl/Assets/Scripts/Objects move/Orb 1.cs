using UnityEngine;

public class waterbubble : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("orb 1");
        }


    }
}
