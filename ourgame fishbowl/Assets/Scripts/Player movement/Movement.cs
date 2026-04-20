using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public Transform player;
    public Rigidbody2D rbPlayer;
    private SpriteRenderer render;
    private float playerheight;
    public float JumpForce = 3f;
    public float doubleJumpForce = 2f;
    private bool candoublejump;
    public GameObject canvas;
    public GameObject escapemenu;
    public GameObject HideInteractable;
    public TextMeshProUGUI gameboardtext;
    public Watermeter watermeter;
    private bool pause;
  

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
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
            canvas.SetActive(true);
            escapemenu.SetActive(true);
            GetComponent<Movement>().enabled = false;
            if (pause)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
            gameboardtext.text = "Game Paused";
            HideInteractable.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
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
    public void PauseGame()
    {
        pause = true;
        watermeter.PauseTime();
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        pause = false;
        watermeter.UnPause();
        Time.timeScale = 1f;
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
