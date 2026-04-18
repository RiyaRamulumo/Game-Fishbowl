using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool toplay = true;
    public GameObject sawyou;
    public float speedopen;

    private void Start()
    {
        StartCoroutine(LoopAnimations());
    }

    IEnumerator LoopAnimations()
    {
        while (toplay == true)
        {
            yield return new WaitForSeconds(speedopen);
            animator.Play("Sliding door animation");

            yield return new WaitForSeconds(speedopen);
            animator.Play("Sliding door animation 0");
        }
    }

    
}
