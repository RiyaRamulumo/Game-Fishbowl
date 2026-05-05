using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


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
    public ParticleSystem trail;
    public TextMeshProUGUI gameboardtext;
    public Watermeter watermeter;
    public Light2D light2D;
    private bool pause;
    [SerializeField] private Animator jumpeffect;
   
    Audiomanager audiomanager;
  

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        playerheight = render.bounds.extents.y;
       
    }

    private void Awake()
    {
        audiomanager = GameObject.FindWithTag("Audio").GetComponent<Audiomanager>();
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
                Audiomanager.instance.PlaySFX(Audiomanager.instance.jump);
                    doit();
               

            }
            else if (candoublejump)
            {
                jumpeffect.SetTrigger("Jump");
                rbPlayer.linearVelocity = Vector3.zero;
                Jump(doubleJumpForce);
                candoublejump = false;
                Audiomanager.instance.PlaySFX(Audiomanager.instance.jump);
                
                doit();             
               
            }

       
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            light2D.intensity = 1;
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
            doit();
            
        }
        if (Input.GetKey(KeyCode.A) )
        {
            move.x -= 1;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3((float)0.07972806, (float)0.07972806, (float)0.07972806);
            doit();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                Jump(JumpForce);
                Audiomanager.instance.PlaySFX(Audiomanager.instance.jump);
                doit();

            }
            else if (candoublejump)
            {
                rbPlayer.linearVelocity = Vector3.zero;
                Jump(doubleJumpForce);
                candoublejump = false;


                Audiomanager.instance.PlaySFX(Audiomanager.instance.jump);
                doit();
                
            }


        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x += 1;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3((float)-0.079728061, (float)0.079728061, (float)0.079728061);
            doit();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x -= 1;
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3((float)0.07972806, (float)0.07972806, (float)0.07972806);
            doit();
        }




        transform.position += move.normalized * speed * Time.deltaTime;


       

        /* player.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, player.linearVelocity.y);

     if (Input.GetKey(KeyCode.W))
     {
         player.linearVelocity = new Vector2(player.linearVelocity.x , speed);

     } */  

    }
    private void doit()
    {
        trail.Play();
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

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Water"))
        {

        }
    }
   
    private void Jump(float force)
    {
        rbPlayer.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }




}
