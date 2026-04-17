using Unity.Cinemachine;
using UnityEngine;

public class moveskatebaord : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("Skateboard");
         
       
    }
}
