using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pigeonMove : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private bool FaceRight = false;
    public static float runSpeed = 20f;
    public bool isAlive = true;
    public bool onPlat = false;
    public bool leftPlat = false;
    private Rigidbody2D platformRigidBody;  

    public GameObject WallTop;
    public GameObject WallBottom;
    public GameObject WallLeft;
    public GameObject WallRight;

    [SerializeField] private AudioClip flyingClip;
    [SerializeField] private AudioClip walkingClip;
    public float fallSpeed = 10f;
    public float fastFallSpeed = 10f;

    public Image StaminaBar;
    public float Stamina, MaxStamina = 200f;
    public float flyingUp = 25f;
    public float RunCost = 10f;
    public float ChargeRate = 2f;

    public float baseSpeed = 30f; 

    private Coroutine recharge;
    public bool launched;

    public Sprite groundSprite;
    private SpriteRenderer spriteRenderer;
    
    public bool canFly = false;

    public GameObject flyingObject; 
    public GameObject walkingObject;

    //public AudioSource flySFX1;
    //public AudioSource flySFX2;
    //public AudioSource flySFX3;
    private AudioSource flySFX;
    private AudioSource walkSFX;

    private HealthManager healthManager; 

    void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Stamina = MaxStamina;

        FaceRight = transform.localScale.x > 0;
        // baseSpeed = 30f;
        //   if (SceneManager.GetActiveScene().name != "Level 1")
        // {
            TutorialManager.tutorialCompleted = true; 
            isAlive = true;
        // }

        UpdateSpriteState();

          healthManager = FindObjectOfType<HealthManager>();
        
    }

    public void Fly()
    {
        rb2D.velocity = Vector2.up * (flyingUp / 2);
        Debug.Log("flying!");
        //int flyNum = Random.Range(0, 3);
        //if (flyNum == 0) { flySFX = flySFX1; }
        //else if (flyNum == 1) { flySFX = flySFX2; }
        //else if (flyNum == 2) { flySFX = flySFX3; }
        //else { flySFX = flySFX1; }

        //if (!flySFX.isPlaying)
        //{
        //    flySFX.Play();
        //}

       StopCoroutine(BirdDrop());
       StartCoroutine(BirdDrop());
    }

    IEnumerator BirdDrop()
    {
        yield return new WaitForSeconds(0.5f);
        rb2D.velocity = Vector2.up * (flyingUp / 3);
        yield return new WaitForSeconds(0.5f);
        rb2D.velocity = Vector2.up * (flyingUp / 4);
        yield return new WaitForSeconds(1f);
        canFly = false;
    }

void Update()
{
    if (isAlive)
    {
        float horizontalMove = 0.0f;
        float verticalMove = 0.0f;

        if (Input.GetAxis("Vertical") > 0 && Stamina > 0)
        {
            verticalMove = flyingUp * Time.deltaTime;

            if (flySFX == null)
            {
                flySFX = SoundFXManager.instance.StartLoopingSoundFXClip(flyingClip);
            }
            else if(!flySFX.isPlaying)
            {
                flySFX.Play();
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                horizontalMove = Input.GetAxis("Horizontal") * baseSpeed * Time.deltaTime; 
            }

            if (!onPlat)
            {
                Stamina -= RunCost * Time.deltaTime;
                if (Stamina < 0) Stamina = 0;

                StaminaBar.fillAmount = Stamina / MaxStamina;

                if (recharge != null)
                {
                    StopCoroutine(recharge);
                    recharge = null;
                }
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && !onPlat)
        {
            verticalMove = flyingUp * Time.deltaTime * -1;
        }
        else if (!onPlat)
        {
            if(flySFX != null && flySFX.isPlaying)
            {
                flySFX.Stop();
            }
            verticalMove = -fallSpeed * Time.deltaTime;

            if (recharge == null && Stamina < MaxStamina)
            {
                recharge = StartCoroutine(RechargeStamina());
            }
        }

        if (!onPlat)
        {
            if(walkSFX != null && walkSFX.isPlaying)
            {
                walkSFX.Stop();
            }
            horizontalMove = Input.GetAxis("Horizontal") * baseSpeed * Time.deltaTime;
        }
        else if (onPlat)
        {
            horizontalMove = Input.GetAxis("Horizontal") * baseSpeed * Time.deltaTime * 0.3f;
            if(horizontalMove != 0)
            {
                if(walkSFX == null)
                {
                    walkSFX = SoundFXManager.instance.StartLoopingSoundFXClip(walkingClip);
                }
                else if(!walkSFX.isPlaying)
                {
                    walkSFX.Play();
                }
            }
            else
            {
                if(walkSFX != null && walkSFX.isPlaying)
                {
                    walkSFX.Stop();
                }
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + horizontalMove, WallLeft.transform.position.x + 1, WallRight.transform.position.x - 1),
            Mathf.Clamp(transform.position.y + verticalMove, WallBottom.transform.position.y + 1, WallTop.transform.position.y - 1),
            transform.position.z
        );

        if (horizontalMove < 0 && FaceRight)
        {
            playerTurn();
        }
        else if (horizontalMove > 0 && !FaceRight)
        {
            playerTurn();
        }

        UpdateSpriteState();
    }
}

void UpdateSpriteState()
{
    if (onPlat)
    {
        flyingObject.SetActive(false);
        walkingObject.SetActive(true);
    }
    else
    {
        flyingObject.SetActive(true);
        walkingObject.SetActive(false);
    }
}


    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (Stamina < MaxStamina)
        {
            Stamina += ChargeRate / 10f;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds(0.1f);
        }

        recharge = null;
    }

    private void playerTurn()
    {
        FaceRight = !FaceRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WindLeft") && launched == false)
        {
            rb2D.velocity += new Vector2(-60f, 0);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindUp") && launched == false)
        {
            rb2D.velocity += new Vector2(0, 60f);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindRight") && launched == false)
        {
            rb2D.velocity += new Vector2(60f, 0);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindDown") && launched == false)
        {
            rb2D.velocity += new Vector2(0, -60f);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }

        if (other.gameObject.CompareTag("Platform") && !leftPlat || other.gameObject.CompareTag("platSpecial"))
        {
            onPlat = true;
            platformRigidBody = other.GetComponent<Rigidbody2D>();
        }
        if (other.gameObject.CompareTag("Sidewalk"))
        {
            onPlat = true;
        }
         if (SceneManager.GetActiveScene().name == "Level 3" && other.gameObject.CompareTag("ground"))
        {
            Debug.Log("Hello");
            if (healthManager != null)
            {
                healthManager.TakeDamage(10f); 
                Debug.Log("Pigeon hit a wall!");
            }
        }
        
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Platform") && onPlat && platformRigidBody != null)
        {
            rb2D.velocity = new Vector2(platformRigidBody.velocity.x, platformRigidBody.velocity.y);
          
        }
        if(other.gameObject.CompareTag("platSpecial") && onPlat && platformRigidBody != null)
        {
           // transform.position = new Vector3 (transform.position.x, other.transform.position.y + 1.5f, 0 );
            transform.SetParent(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("platSpecial") )
        {
            onPlat = false;
            platformRigidBody = null;
            transform.SetParent(null);
            StartCoroutine(LeftPlat());
            StopCoroutine(LeftPlat()); 
            StartCoroutine(LeftPlatMomentum());
        }
        if (other.gameObject.CompareTag("Sidewalk")) 
        {
            onPlat = false;
        }
    }

    IEnumerator LaunchDelay()
    {
        launched = true;
        yield return new WaitForSeconds(0.3f);
        rb2D.velocity = Vector2.zero;
        launched = false;
    }

    IEnumerator LeftPlat()
    {
        leftPlat = true;
        yield return new WaitForSeconds(0.3f);
        leftPlat = false;
    }

    IEnumerator LeftPlatMomentum()
    {
        yield return new WaitForSeconds(0.3f);
        rb2D.velocity = new Vector2(0, 0);
    }
}
