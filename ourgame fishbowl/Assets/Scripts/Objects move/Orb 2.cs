using UnityEngine;

public class Orb2 : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Orb 2");
        }


    }
}
