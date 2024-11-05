using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class pigeonMove : MonoBehaviour {

    public Rigidbody2D rb2D;
    private bool FaceRight = false; 
    public static float runSpeed = 10f;
    public float startSpeed = 10f;
    public bool isAlive = true;
    public GameObject WallTop;
    public GameObject WallBottom;
    public GameObject WallLeft;
    public GameObject WallRight;
    [SerializeField] AudioSource WalkSFX;
    public float fallSpeed = 10f; 
    public float fastFallSpeed = 10f; 

    public Image StaminaBar;
    public float Stamina, MaxStamina = 100f; 
    public float flyingUp = 25f; 
    public float RunCost = 10f; 
    public float ChargeRate = 5f; 

    private Coroutine recharge;

    public int LaunchDir = 0;


    void Start(){
        rb2D = transform.GetComponent<Rigidbody2D>();
        Stamina = MaxStamina; 
    }

    void Update(){
      //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
      //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
        if (isAlive){
           
            float horizontalMove = Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime;
            float verticalMove = 0.0f;

           
            if (Input.GetAxis("Vertical") > 0 && Stamina > 0){
                verticalMove = Input.GetAxis("Vertical") * flyingUp * Time.deltaTime;

                
                Stamina -= RunCost * Time.deltaTime;
                if (Stamina < 0) Stamina = 0;

               
                StaminaBar.fillAmount = Stamina / MaxStamina;

                
                if (recharge != null){
                    StopCoroutine(recharge);
                    recharge = null;
                }

            } else if (Input.GetAxis("Vertical") < 0){
                
                verticalMove = -fastFallSpeed * Time.deltaTime;
            } else {
                
                verticalMove = -fallSpeed * Time.deltaTime;

                
                if (recharge == null && Stamina < MaxStamina){
                    recharge = StartCoroutine(RechargeStamina());
                }
            }

            
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + horizontalMove, WallLeft.transform.position.x + 1, WallRight.transform.position.x - 1),
                Mathf.Clamp(transform.position.y + verticalMove, WallBottom.transform.position.y + 1, WallTop.transform.position.y - 1),
                transform.position.z
            );

            
            if (horizontalMove != 0 || Input.GetAxis("Vertical") != 0){
            //     anim.SetBool ("Walk", true);
                if (!WalkSFX.isPlaying){
                    WalkSFX.Play();
                }
            } else {
            //     anim.SetBool ("Walk", false);
                WalkSFX.Stop();
            }

            // Turning. Reverse if input is moving the Player right and Player faces left.
            if ((horizontalMove < 0 && !FaceRight) || (horizontalMove > 0 && FaceRight)){
                playerTurn();
            }
        }
    }

    private IEnumerator RechargeStamina(){
        yield return new WaitForSeconds(1f);

        while (Stamina < MaxStamina){
            Stamina += ChargeRate / 10f;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds(0.1f);
        }

        recharge = null; 
    }

    private void playerTurn(){
      // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
/*
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("WindLeft"))
        {
           
            rb2D.velocity += new Vector2(-30f,0);
            LaunchDir = 1;
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindUp"))
        {
           
            rb2D.velocity += new Vector2(0,30f);
            LaunchDir = 2;
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindRight"))
        {
           
            rb2D.velocity += new Vector2(30f, 0);
            LaunchDir = 3;
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindDown"))
        {
           
            rb2D.velocity += new Vector2(0, -30f);
            LaunchDir = 4;
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());


        }
    }

    IEnumerator LaunchDelay()
    {
        yield return new WaitForSeconds(0.3f);
        if (LaunchDir == 1)
        {
            rb2D.velocity += new Vector2(30f,0);
        }
        if (LaunchDir == 2)
        {
            rb2D.velocity += new Vector2(0,-30f);
        }
        if (LaunchDir == 3)
        {
            rb2D.velocity += new Vector2(-30f, 0);
        }
         if (LaunchDir == 4)
        {
            rb2D.velocity += new Vector2(0, 30f);
        }
        LaunchDir = 0;
    }
*/