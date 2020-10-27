using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Unity101_CharacterController : MonoBehaviour
{
    [Header("Walk / Run Setting")] public float walkSpeed;
    public float runSpeed;

    [Header("Jump Force")] 
    private float playerJumpForce=5;
    public float jumpBoost;

    [Header("Double Jump")] public bool doubleJumpEnabled;


    private Collider col;
    private Rigidbody rb;
    

    private float distToGround;
    private bool playerIsJumping;
    private float currentSpeed;
    private float xAxis;
    private float zAxis;
    private bool leftShiftPressed;
    private int jumpCounter = 0;
    private float jumpDelay = 0.05f;
    private float timer = 0f;
    private bool jumpingHighSpeed;

    //my part
    
    [Header("my part")] public TextMeshProUGUI countText;
    public AudioSource collectsound;

    private int count;
    private Animator anim;
    private float animSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        
        col = GetComponent<Collider>();
        if(col == null) { Debug.LogError("Collision component missing"); enabled = false; }
        rb = GetComponent<Rigidbody>();
        if(rb == null) { Debug.LogError("Physic body component missing"); enabled = false; }
        
        // To assert character doesn't fall on the side
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        distToGround = col.bounds.extents.y;

        //my part
        anim = GetComponent<Animator>();
        if (anim == null) { Debug.LogError("Animator component missing"); enabled = false; }

        jumpBoost = jumpBoost/10;

        count = 0; //initiate count 
        SetCountText();
    }
    //Collecting Object
    //CountText
    void SetCountText()
    {
        countText.text = "Score : " + count.ToString();
        
        
    }
      
    IEnumerator Son()
    {
        collectsound.Play();
        yield return new WaitForSeconds(collectsound.clip.length);
        collectsound.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false); //PickUp objects disapeared
            StartCoroutine(Son());
            count += 1; //the score increase by one
            SetCountText();

        }

    }
    //End Collecting Object


    // Update is called once per frame
    void Update()
    {
        
        // Walk
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        currentSpeed = (leftShiftPressed && !playerIsJumping) || jumpingHighSpeed ? runSpeed : walkSpeed;

        //Run
        leftShiftPressed = Input.GetKey(KeyCode.LeftShift);

        //ANIMATION PART 
        if (xAxis != 0 || zAxis != 0) //moving forward or backward
        {
            if (zAxis > 0)
            {
                anim.SetFloat("direction", 1f); //forward
            }
            else
            {
                anim.SetFloat("direction", -1f); //backward
            }
            animSpeed = (currentSpeed == walkSpeed) ? 0.5f : 1f; //check the speed
        }
        else //is on idle
        {
            anim.SetFloat("direction", 1f);
            animSpeed = 0f;

        }
        anim.SetFloat("Speed", animSpeed);
        //END ANIMATION


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if (playerIsJumping)
        {
            timer += Time.deltaTime;
        }

        if(IsGrounded() && playerIsJumping && timer > jumpDelay)
        {
            playerIsJumping = false;
            jumpCounter = 0;
            timer = 0f;
            jumpingHighSpeed = false;
        }
    }

    // Fixed Update is called once per frame, at fixed time
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Time.deltaTime * currentSpeed * transform.TransformDirection(xAxis, 0f, zAxis));
    }


    // Check the distance between the player and a ground surface
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f);
    }

    private void Jump()
    {
        if (currentSpeed == runSpeed)
        {
            jumpingHighSpeed = true;
        }
        //simple jump
        if (IsGrounded() && !playerIsJumping && jumpCounter < 1)
        {
            rb.velocity = Vector3.up * playerJumpForce * jumpBoost;
            anim.SetTrigger("isJumping");
            jumpCounter++;
            playerIsJumping = true;
        }
        //double jump
        else if (playerIsJumping && (doubleJumpEnabled && jumpCounter < 2))
        {
            rb.velocity = Vector3.up * playerJumpForce;
            jumpCounter++;
        }

    }
}

