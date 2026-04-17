using Unity.VisualScripting;
using UnityEngine;

public class Setofalarm : MonoBehaviour
{
    public GameObject triggerdoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (triggerdoor.activeSelf == true  && Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.D) ^ Input.GetKey(KeyCode.A))  
        {
            Debug.Log("gOT IT");

        }
    }

    
}
