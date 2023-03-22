using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public int scoreValue = 0;
    public int livesValue = 3;
    public AudioClip LoseMusic;
    public AudioClip WinMusic;
    public AudioClip GameMusic;
    public AudioSource musicSource;
    private bool facingRight = true;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: "+ livesValue.ToString();
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        musicSource.clip = GameMusic;
        musicSource.Play();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
            else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    void Update()
    {

     if (Input.GetKeyDown(KeyCode.D))

        {

          anim.SetInteger("State", 1);

         }

     if (Input.GetKeyUp(KeyCode.D))

        {

          anim.SetInteger("State", 0);

        }

     if (Input.GetKeyDown(KeyCode.A))

        {

          anim.SetInteger("State", 1);

        }

     if (Input.GetKeyUp(KeyCode.A))

        {

          anim.SetInteger("State", 0);
 
        }


     if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue == 4)
            {
                if(scoreValue >= 8)
                {
                WinTextObject.SetActive(true);
                musicSource.clip = WinMusic;
                 musicSource.Play();
                }
                transform.position = new Vector3(2.28f,21.67f,0f);
                Destroy(this);
                
            }
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue <= 0) 
            {
                LoseTextObject.SetActive(true);
                musicSource.clip = LoseMusic;
                musicSource.Play();
                Destroy (this);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 3);
            }
        }
    }



}