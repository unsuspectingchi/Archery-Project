using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isCrouching = false;

    private AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip walkClip;
    public AudioClip runClip;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Crouching ///////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Start crouching
            animator.SetBool("isCrouching", true);
            isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            // Stop crouching
            animator.SetBool("isCrouching", false);
            isCrouching = false;
        }

        // Fighting ////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.RightShift))
            {
                // Fight with feet
                animator.SetInteger("nFight", 2);
            }
            else
            {
                // Fight with knife
                animator.SetInteger("nFight", 1);
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            // Stop fighting
            animator.SetInteger("nFight", 0);
        }

        // Shooting ////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Shifted shooting styles
            if (Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.RightShift))
            {
                if (isCrouching)
                {
                    // Jump from crouch to shoot
                    animator.SetInteger("nShoot", 3);
                    audioSource.PlayOneShot(shootClip, audioSource.volume);
                }
                else
                {
                    // Shoot gangster style while standing
                    animator.SetInteger("nShoot", 2);
                    audioSource.PlayOneShot(shootClip, audioSource.volume);
                }
            }
            // Unshifted shooting styles
            else
            {
                if (isCrouching)
                {
                    // Shoot while crouching
                    animator.SetInteger("nShoot", 4);
                }
                else
                {
                    // Short normally while standing
                    animator.SetInteger("nShoot", 1);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Stop shooting
            animator.SetInteger("nShoot", 0);
        }

        // Running /////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.RightShift))
            {
                // Run with bow ready
                animator.SetInteger("nMove", 3);
            }
            else
            {
                // Run normally
                animator.SetInteger("nMove", 2);
                if (!audioSource.isPlaying) {
                    audioSource.Stop();
                    audioSource.PlayOneShot(runClip, audioSource.volume);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            // Stop running
            animator.SetInteger("nMove", 0);
            audioSource.Stop();
        }

        // Walking /////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Start walking
            animator.SetInteger("nMove", 1);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(walkClip, audioSource.volume);
            }
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            // Stop walking
            animator.SetInteger("nMove", 0);
            audioSource.Stop();
        }


        // Stunned /////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.RightShift))
            {
                // Stunned with head hung
                animator.SetInteger("nStunned", 2);
            }
            else
            {
                // Stunned with head spinning
                animator.SetInteger("nStunned", 1);
            }
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            // Stop being stunned
            animator.SetInteger("nStunned", 0);
        }

        // Flipping ////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Start flipping
            animator.SetInteger("nKnockDown", 3);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            // Stop flipping
            animator.SetInteger("nKnockDown", 0);
        }

        // Knocked down ////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.RightShift))
            {
                // Knocked down with spin
                animator.SetInteger("nKnockDown", 2);
            }
            else
            {
                // Knocked down without spin
                animator.SetInteger("nKnockDown", 1);
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            // Stop being knocked down
            animator.SetInteger("nKnockDown", 0);
        }

    }
}
