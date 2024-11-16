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
    public float Stamina, MaxStamina = 100f;
    public float flyingUp = 25f;
    public float RunCost = 10f;
    public float ChargeRate = 5f;

    private Coroutine recharge;

    public int LaunchDir = 0;

    public Sprite groundSprite; 
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Stamina = MaxStamina;

        FaceRight = transform.localScale.x > 0;
    }

    public float groundSpeed = 2f;

    void Update()
    {
        if (isAlive)
        {
            float horizontalMove = Input.GetAxis("Horizontal") * (onPlat ? groundSpeed : runSpeed) * Time.deltaTime;
            float verticalMove = 0.0f;

            bool upArrowPressed = Input.GetKey(KeyCode.UpArrow);

            if (upArrowPressed && Stamina > 0 && !onPlat)
            {
                verticalMove = flyingUp * Time.deltaTime;

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

            if (onPlat)
            {
                animator.enabled = false;
                spriteRenderer.sprite = groundSprite; 
            }
            else
            {
                animator.enabled = true; 
            }
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
        if (other.gameObject.CompareTag("WindLeft"))
        {
            rb2D.velocity += new Vector2(-30f, 0);
            LaunchDir = 1;
            StartCoroutine(LaunchDelay());
            StopCoroutine(LaunchDelay());
        }
        if (other.gameObject.CompareTag("WindUp"))
        {
            rb2D.velocity += new Vector2(0, 30f);
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
        yield return new WaitForSeconds(0.3f);
        if (LaunchDir == 1)
        {
            rb2D.velocity += new Vector2(30f, 0);
        }
        if (LaunchDir == 2)
        {
            rb2D.velocity += new Vector2(0, -30f);
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

    IEnumerator LeftPlat()
    {
        leftPlat = true;
        yield return new WaitForSeconds(0.3f);
        leftPlat = false;
    }
}