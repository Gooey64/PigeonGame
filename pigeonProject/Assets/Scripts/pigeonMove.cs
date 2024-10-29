using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class pigeonMove : MonoBehaviour {

      //public Animator anim;
      //public AudioSource WalkSFX;
      public Rigidbody2D rb2D;
      private bool FaceRight = false; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;
      public GameObject WallTop;
      public GameObject WallBottom;
      public GameObject WallLeft;
      public GameObject WallRight;
    [SerializeField] AudioSource WalkSFX;

    void Start(){
           //anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
           if (isAlive == true){

                  Vector3 movement = transform.position + hvMove * runSpeed * Time.deltaTime;
                  transform.position = new Vector3(
                  Mathf.Clamp(movement.x, WallLeft.transform.position.x + 1, WallRight.transform.position.x - 1),
                  Mathf.Clamp(movement.y, WallBottom.transform.position.y + 1, WallTop.transform.position.y - 1),
                  movement.z);

                  if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                  //     anim.SetBool ("Walk", true);
                       if (!WalkSFX.isPlaying){
                             WalkSFX.Play();
                       }
                  } else {
                  //     anim.SetBool ("Walk", false);
                       WalkSFX.Stop();
                 }

                  // Turning. Reverse if input is moving the Player right and Player faces left.
                 if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                        playerTurn();
                  }
            }
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