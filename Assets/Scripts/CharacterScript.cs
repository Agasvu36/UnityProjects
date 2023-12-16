using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class CharacterScript : Sounds
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    private SpriteRenderer Sprite;

    public AudioSource AudioSource;

    public int SceneID;

    public int NextScene;

    private float HorizontalMove = 0.0f;

    public bool groundCollision = false;

    

    //private float DeathCounter = 0;

    [Header("Player Movement Settings")]
    [Range(0,10)] public float speed = 1.0f;
    [Range(0, 40)] public float jumpForce = 1.0f;

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    public Animator animator; 

    [Space]
    [Header("Ground Checker Settings")]
    public bool IsGrounded = true;
    [Range(-5f, 5f)] public float CheckGroundOffsetY = -1.8f;
    [Range(0f, 5f)] public float CheckGroundRadius = 0.3f;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        Sprite = rb.GetComponentInChildren<SpriteRenderer>();
        Pause();
        ShowAdv();
        if (SceneID > Progress.Instance.PlayerInfo.CurrentLevel) 
        {
            Progress.Instance.PlayerInfo.CurrentLevel = SceneID;
            Progress.Instance.Save();
        }
    }


    // Update is called once per frame
    void Update()
    {
        KeyInputs();
        AnimatorControl();

    }


    private void Pause() 
    {
        Time.timeScale = 0f;
        AudioSource.mute = true;
    }

    private void Resume() 
    {
        Time.timeScale = 1f;
        AudioSource.mute = false;
    }

    private void AnimatorControl() 
    {
        animator.SetFloat("HorizontalMove", Mathf.Abs(HorizontalMove));
        animator.SetBool("Jumping", !IsGrounded);
    }

    private void KeyInputs() 
    {
        if (Input.GetButton("Jump") && IsGrounded && groundCollision) 
        {
            Jump();
            PlaySound(sounds[0]);
        }

        if (Input.GetButton("Horizontal") )
        {
            Run();
        }
        else
        {
            Stay();
        }
    }

    private void Jump() 
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        groundCollision = false;
    }
    private void Run() 
    {
        HorizontalMove = Input.GetAxis("Horizontal") * speed;
        Vector3 dir = transform.right * HorizontalMove;
        Sprite.flipX = dir.x < 0.0f;
    }

    private void Stay() 
    {
        HorizontalMove = 0.0f;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + CheckGroundOffsetY), CheckGroundRadius);
        if (colliders.Length > 1)
        {
            IsGrounded = true;
        }
        else 
        {
            IsGrounded = false;
        }
    }

    private void Flip() 
    {
        Vector3 dir = transform.right * HorizontalMove;
        Sprite.flipX = dir.x < 0.0f;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Respawn")) 
        {
            SceneManager.LoadScene(SceneID);
        }
        if (other.gameObject.CompareTag("Finish")) 
        {
            SceneManager.LoadScene(NextScene);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            groundCollision = true;
        }
        else 
        {
            groundCollision = false;
        } 
    }

    private void FixedUpdate()
    {
        CheckGround();
        Vector2 targetVelosity = new Vector2(HorizontalMove * 10f, rb.velocity.y);
        rb.velocity = targetVelosity;
    }

}
