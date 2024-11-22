using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class pigeonMove : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private bool FaceRight = false;
    public static float runSpeed = 10f;
    public float startSpeed = 10f;
    public bool isAlive = true;
    public bool onPlat = false;
    public bool leftPlat = false;

    public GameObject WallTop;
    public GameObject WallBottom;
    public GameObject WallLeft;
    public GameObject WallRight;

    [SerializeField] private AudioClip flyingClip;
    public float fallSpeed = 10f;
    public float fastFallSpeed = 10f;

    public Image StaminaBar;
    public float Stamina, MaxStamina = 200f;
    public float flyingUp = 25f;
    public float RunCost = 10f;
    public float ChargeRate = 5f;

    public float groundSpeed = 3f; // You can adjust the value as needed

    private Coroutine recharge;
    public bool launched;

    public Sprite groundSprite;
    private SpriteRenderer spriteRenderer;
    // private Animator animator;

    // Added properties and methods
    public bool canFly = false;
    public static bool flyEnergyEnough = true;
    public float groundPos = 0;

    public AudioSource flySFX1;
    public AudioSource flySFX2;
    public AudioSource flySFX3;
    private AudioSource flySFX;

    void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();
        Stamina = MaxStamina;

        FaceRight = transform.localScale.x > 0;
    }

    public void Fly()
    {
        rb2D.velocity = Vector2.up * (flyingUp / 2);

        int flyNum = Random.Range(0, 3);
        if (flyNum == 0) { flySFX = flySFX1; }
        else if (flyNum == 1) { flySFX = flySFX2; }
        else if (flyNum == 2) { flySFX = flySFX3; }
        else { flySFX = flySFX1; }

        if (!flySFX.isPlaying)
        {
            flySFX.Play();
        }

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
            float horizontalMove = Input.GetAxis("Horizontal") * (onPlat ? groundSpeed : runSpeed) * Time.deltaTime;
            float verticalMove = 0.0f;

            bool upArrowPressed = Input.GetKey(KeyCode.UpArrow);

            if (upArrowPressed && Stamina > 0/* && !onPlat*/)
            {
                verticalMove = flyingUp * Time.deltaTime;
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

                    if (!SoundFXManager.instance.IsPlaying())
                    {
                        SoundFXManager.instance.PlaySoundFXClip(flyingClip, transform, 1f);
                    }
                }
              
            }
            else if (Input.GetAxis("Vertical") < 0 && !onPlat)
            {
                verticalMove = -fastFallSpeed * Time.deltaTime;
            }
            else if (!onPlat)
            {
                verticalMove = -fallSpeed * Time.deltaTime;

                if (recharge == null && Stamina < MaxStamina)
                {
                    recharge = StartCoroutine(RechargeStamina());
                }

                SoundFXManager.instance.StopSoundFX();
            }
            else if (onPlat)
            {
                SoundFXManager.instance.StopSoundFX();
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

            // if (onPlat)
            // {
            //     animator.enabled = false;
            //     spriteRenderer.sprite = groundSprite;
            // }
            // else
            // {
            //     animator.enabled = true;
            // }
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
            rb2D.velocity += new Vector2(-30f, 0);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindUp") && launched == false)
        {
            rb2D.velocity += new Vector2(0, 30f);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindRight") && launched == false)
        {
            rb2D.velocity += new Vector2(30f, 0);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindDown") && launched == false)
        {
            rb2D.velocity += new Vector2(0, -30f);
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }

        if (other.gameObject.CompareTag("Platform") && !leftPlat)
        {
            onPlat = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            onPlat = false;
            StartCoroutine(LeftPlat());
            StopCoroutine(LeftPlat());
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
}
