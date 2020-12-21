using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;
    private SpriteRenderer SpriteRenderer;
    private Animator animator;
    private bool liberaCor = false;
    [SerializeField]
    private bool danoCritico = false;
    [SerializeField]
    private CapsuleCollider2D ataqueEfeito;
    void Awake () {
        ataqueEnab();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}

    private void Update()
    {
        
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if(move !=Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyUp(KeyCode.LeftArrow))
            {
                ataqueEfeito.offset = new Vector2(move.x = -1.0f, (move.y + 0.06f));
            }
           
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                ataqueEfeito.offset = new Vector2(move.x = -0.31f, (move.y + 0.06f));
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            maxSpeed = 0;
            animacaoAtaque();
            StartCoroutine(teste());
        }
        else
        {
            ataqueEnab();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            maxSpeed = 7f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animacaoAgacha();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow)) 
        {
            velocity.x = 0.01f;
        }

            if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
 
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0){
                velocity.y = velocity.y * .5f;

            }

        }
        if (liberaCor == true)
        {
            PingPongColor(8);
        }
        if(danoCritico==true)
        {
            PingPongColor(1);
        }


        bool flipSprite = (SpriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
        }


        IEnumerator teste()
        {
            yield return new WaitForSeconds(0.35f);
            ataqueEfeito.enabled = true;
        }


        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        targetVelocity = move * maxSpeed;

    }

    public void ataqueEnab()
    {

        ataqueEfeito.enabled = false;
    }


    void PingPongColor(int c)
    {
        SpriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(c * Time.time, 0.05f));
    }
    void animacaoAtaque()
    {
        animator.SetTrigger("ataque");
    }
    void animacaoAgacha()
    {
        animator.SetTrigger("agacha");
    }

}
