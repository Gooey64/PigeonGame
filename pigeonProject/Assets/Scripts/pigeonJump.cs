using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class pigeonJump : MonoBehaviour {

      //public Animator anim;
      public Rigidbody2D rb;
      public float jumpForce = 20f;
      public Transform feet;
      public LayerMask groundLayer;
      public LayerMask enemyLayer;
      public bool canJump = false;
      public int jumpTimes = 0;
      public bool isAlive = true;

      public Image StaminaBar;
      public float Stamina, MaxStamina = 100f;
      public float flyingUp = 25f; 
      public float RunCost = 10f; 
      public float ChargeRate = 5f; 

      private Coroutine recharge;

      //public AudioSource JumpSFX;

      void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            Stamina = MaxStamina; 
      }

     void Update() {
            if ((IsGrounded()) && Stamina >= RunCost){
            // if ((IsGrounded()) && (jumpTimes <= 1)){ // for single jump only
                  canJump = true;
            }  else if (jumpTimes > 1){
            // else { // for single jump only
                  canJump = false;
            }

           if ((Input.GetButtonDown("Jump")) && (canJump) && (isAlive == true)) {
                  Jump();
            }
      }

      public void Jump() {
            jumpTimes += 1;
            rb.velocity = Vector2.up * jumpForce;

            Stamina -= RunCost;
                if (Stamina <= 0) {
                    Stamina = 0;
                    
                }
               
                StaminaBar.fillAmount = Stamina / MaxStamina;

                
                if (recharge != null){
                    StopCoroutine(recharge);
                    recharge = null;
                }
            // anim.SetTrigger("Jump");
            // JumpSFX.Play();

            //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            //rb.velocity = movement;
      }

      public bool IsGrounded() {
            Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
            Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
            if ((groundCheck != null) || (enemyCheck != null)) {
                  //Debug.Log("I am touching ground!");
                  jumpTimes = 0;
                  return true;
            }
            if (recharge == null && Stamina < MaxStamina){
                    recharge = StartCoroutine(RechargeStamina());
                }
            return false;
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
}