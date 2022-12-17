using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public GameObject playerObject;
    private Animator animator;
    public PlayerMovement movPlayer;
    public GameSettings gameSets;

    void Start()
    {
        animator = playerObject.GetComponent<Animator>();
        if (PlayerPrefs.GetInt("playerDead") == 0)
        {
            animator.speed = gameSets.GetAnimBaseSpeed();
        }
    }

    void Update()
    {
        var playerDead = PlayerPrefs.GetInt("playerDead");
        var runON = PlayerPrefs.GetInt("runON");


        if (playerDead == 0) //alive
        {
            if (runON == 1) // run is ON
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.speed = gameSets.GetAnimRunSpeed();
                }
                else
                {
                    animator.speed = gameSets.GetAnimBaseSpeed();
                }

                animator.SetBool("startRunON", true);

                if (movPlayer.ctr.isGrounded)
                {
                    animator.SetBool("jumpON", false);
                    animator.SetBool("fallON", false);
                    animator.SetBool("runON", true);

                }
                else
                {
                    if (movPlayer.verticalVelocity > 0)
                    {
                        animator.SetBool("jumpON", true);
                        animator.SetBool("runON", false);
                    }
                    if (movPlayer.verticalVelocity < 0)
                    {
                        animator.SetBool("fallON", true);
                        animator.SetBool("runON", false);
                    }
                }
            }
            else
            {
                animator.speed = 1f;
            }
        }
        else
        {
            animator.speed = 1f;
            animator.SetBool("pullON", true);
        }      
    }
}
