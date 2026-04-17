using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public Transform player;
    public Rigidbody2D rbPlayer;
    public SpriteRenderer render;
    public float playerheight;
    public float JumpForce;
    public float doubleJumpForce;
    private bool candoublejump;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerheight = render.bounds.extents.y; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 3f);

        if (Input.GetKey(KeyCode.W) && GetIsGrounded())
        {
            Jump(JumpForce);   
        }
        else
            if (Input.GetKey(KeyCode.W) && !GetIsGrounded() && candoublejump)
            {
                rbPlayer.linearVelocity = Vector2.zero;
                rbPlayer.angularVelocity = 0;
                Jump(doubleJumpForce);
                candoublejump = false;
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
    private bool GetIsGrounded()
    {
      bool hit = Physics2D.Raycast(transform.position, Vector2.down, playerheight + 0.1f, LayerMask.GetMask("Ground"));
        if (hit)
        {
            candoublejump = true;
        }

        return hit;
    }
    private void Jump(float force)
    {
        rbPlayer.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }


   

}
