using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public Transform player;
    public Rigidbody2D rbPlayer;
    public SpriteRenderer render;
    public float playerheight;
    public float JumpForce = 3f;
    public float doubleJumpForce = 2f;
    private bool candoublejump;
    public GameObject escapemenu;
    public GameObject interactobject;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerheight = render.bounds.extents.y; 
    }
    private bool GetIsGrounded()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, playerheight + 0.1f, LayerMask.GetMask("Ground"));
      
        if (hit)
        {
            candoublejump = true;
        }

        return hit;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
         bool hit = Physics2D.Raycast(transform.position, Vector2.down, playerheight + 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 3f);

      
        bool isGrounded = GetIsGrounded();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                Jump(JumpForce);

            }
            else if (candoublejump)
            {
                rbPlayer.linearVelocity = Vector3.zero;
                Jump(doubleJumpForce);
                candoublejump = false;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            escapemenu.SetActive(true);
            GetComponent<Movement>().enabled = false;
            interactobject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.D) )
        {
            move.x += 1;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3((float)-0.079728061, (float)0.079728061, (float)0.079728061);
        }
        if (Input.GetKey(KeyCode.A) )
        {
            move.x -= 1;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3((float)0.07972806, (float)0.07972806, (float)0.07972806);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.y += 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x += 1;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3((float)-0.079728061, (float)0.079728061, (float)0.079728061);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x -= 1;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3((float)0.07972806, (float)0.07972806, (float)0.07972806);
        }


        transform.position += move.normalized * speed * Time.deltaTime;


       

        /* player.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, player.linearVelocity.y);

     if (Input.GetKey(KeyCode.W))
     {
         player.linearVelocity = new Vector2(player.linearVelocity.x , speed);

     } */  

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetIsGrounded();
    }
   
    private void Jump(float force)
    {
        rbPlayer.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }


   

}
