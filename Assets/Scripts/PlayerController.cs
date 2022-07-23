using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isCrouching = false;
    public string ARROW_PATH = "Prefabs/Arrow_Prefab";

    private AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip[] slashArray;


    private GameObject arrowPrefab;
    private Vector3 DEFAULT_ARROW_POSITION = new Vector3(500, 1, 502);

    private bool isWalking = false;
    private bool isRunning = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        arrowPrefab = Resources.Load(ARROW_PATH) as GameObject;
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
                audioSource.clip = slashArray[Random.Range(0, 4)];
                audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
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
                    audioSource.clip = shootClip;
                    Instantiate(arrowPrefab, DEFAULT_ARROW_POSITION, Quaternion.identity);
                }
                else
                {
                    // Shoot gangster style while standing
                    animator.SetInteger("nShoot", 2);
                    audioSource.PlayOneShot(shootClip, audioSource.volume);
                    audioSource.clip = shootClip;
                    Instantiate(arrowPrefab, DEFAULT_ARROW_POSITION, Quaternion.identity);
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
                isRunning = true;
            }
            else
            {
                // Run normally
                animator.SetInteger("nMove", 2);
                isRunning = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            // Stop running
            animator.SetInteger("nMove", 0);
            isRunning = false;
        }

        // Walking /////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Start walking
            animator.SetInteger("nMove", 1);
            isWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            // Stop walking
            animator.SetInteger("nMove", 0);
            isWalking = false;
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Start flipping
            animator.SetInteger("nKnockDown", 3);
        }
        if (Input.GetKeyUp(KeyCode.J))
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

        //////////////////////////////// This is for audio because I realized the first version only worked for as 
        // long as the audio played, not for how long the key was pressed

        if (isWalking) {
            if (!audioSource.isPlaying) { //if you are not playing audio
                Debug.Log(audioSource.clip);
                audioSource.PlayOneShot(walkClip, audioSource.volume);
            } else if (audioSource.clip == runClip) { //if we are playing the run audio while walking-- useful if person holds down one key and switches to the next without letting go
                audioSource.Stop();
                audioSource.PlayOneShot(walkClip, audioSource.volume);
            }
            audioSource.clip = walkClip;
        } else if (isRunning) {
            if (!audioSource.isPlaying) {
                Debug.Log(audioSource.clip);
                audioSource.PlayOneShot(runClip, audioSource.volume);
            } else if (audioSource.clip == walkClip) {
                audioSource.Stop();
                audioSource.PlayOneShot(runClip, audioSource.volume);
            }
            audioSource.clip = runClip;
        } else if (!isRunning && !isWalking && (audioSource.clip == runClip || audioSource.clip == walkClip)) {
            audioSource.Stop(); //if not walking or running
        }
    }
}
