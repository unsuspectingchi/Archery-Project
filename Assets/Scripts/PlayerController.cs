using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Animator animator;

  private AudioSource audioSource;
  public AudioClip shootClip;
  public AudioClip walkClip;
  public AudioClip runClip;
  public AudioClip[] slashArray;
  private bool isWalking = false;
  private bool isRunning = false;
  private bool isCrouching = false;
  private bool isKnifing = false;
  private bool isKicking = false;
  private bool isShooting = false;
  private bool isFlipping = false;

  void Start()
  {
    animator = gameObject.GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    // Default Behavior
    isWalking = false;
    isRunning = false;
    animator.SetInteger("nMove", 0);

    // Flipping
    if (Input.GetKey(KeyCode.F) && !isShooting && !isKicking && !isKnifing)
    {
      Debug.Log("Flipping!");
      // Start flipping
      isFlipping = true;
      animator.SetInteger("nKnockDown", 3);
    }
    else
    {
      // Stop flipping
      isFlipping = false;
      animator.SetInteger("nKnockDown", 0);
    }

    // Crouching
    if (Input.GetKey(KeyCode.C) && !isShooting && !isKicking && !isKnifing)
    {
      Debug.Log("Crouching!");
      // Start crouching
      animator.SetBool("isCrouching", true);
      isCrouching = true;
    }
    else
    {
      // Stop crouching
      animator.SetBool("isCrouching", false);
      isCrouching = false;
    }

    // Fighting with knife
    if (Input.GetKey(KeyCode.O))
    {
      Debug.Log("Fighting with knife!");
      // Fight with knife
      animator.SetInteger("nFight", 1);
      if (!isKnifing)
      {
        audioSource.clip = slashArray[Random.Range(0, 4)];
        audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
      }
      isKnifing = true;
    }
    else
    {
      isKnifing = false;
    }

    // Fighting with feet
    if (Input.GetKey(KeyCode.I))
    {
      Debug.Log("Fighting with feet!");
      // Fight with feet
      isKicking = true;
      animator.SetInteger("nFight", 2);
    }
    else
    {
      isKicking = false;
    }


    // Stop fighting
    if (!isKnifing && !isKicking)
    {
      animator.SetInteger("nFight", 0);
    }

    // Normal shooting
    if (Input.GetKey(KeyCode.Space))
    {
      if (isCrouching && !isShooting)
      {

        // Shoot while crouching
        animator.SetInteger("nShoot", 4);
        isShooting = true;
        // SHOOT HERE
      }
      else if (!isShooting)
      {
        {
          // Short normally while standing
          animator.SetInteger("nShoot", 1);
          isShooting = true;
          // SHOOT HERE
        }
      }
    }
    else
    {
      isShooting = false;
    }

    // Style shooting
    if (Input.GetKey(KeyCode.P))
    {
      if (isCrouching)
      {
        Debug.Log("Not knifing!");
        // Jump from crouch to shoot
        if (!isShooting)
        {
          animator.SetInteger("nShoot", 3);
          audioSource.PlayOneShot(shootClip, audioSource.volume);
          audioSource.clip = shootClip;
          // SHOOT HERE
        }
      }
      else
      {
        if (!isShooting)
        {
          // Shoot gangster style while standing
          animator.SetInteger("nShoot", 2);
          audioSource.PlayOneShot(shootClip, audioSource.volume);
          audioSource.clip = shootClip;
          // SHOOT HERE
        }
      }
      isShooting = true;
    }
    else
    {
      isShooting = false;
    }

    // Stop shooting
    if (!isShooting)
    {
      animator.SetInteger("nShoot", 0);
    }

    // Forward Movement
    if (Input.GetKey(KeyCode.W) && !isCrouching && !isKnifing && !isKicking)
    {
      if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
      {
        Debug.Log("Running!");
        // Run forward
        animator.SetInteger("nMove", 2);
        isRunning = true;
      }
      else
      {
        Debug.Log("Walking forward!");
        // Walk forward
        animator.SetInteger("nMove", 1);
        isWalking = true;
      }
    }

    // Backward movement Left
    if (Input.GetKey(KeyCode.S) && !isWalking && !isRunning)
    {
      Debug.Log("Walking backward!");
      // Walk left
      animator.SetInteger("nMove", 1);
      isWalking = true;
    }

    // Turning Left
    if (Input.GetKey(KeyCode.A))
    {
      Debug.Log("Turning left!");
      // Turn left
      // TODO
    }

    // Turning Right
    if (Input.GetKey(KeyCode.D))
    {
      Debug.Log("Turning right!");
      // Turn right
      // TODO
    }


    // Audio
    if (isWalking)
    {
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(walkClip, audioSource.volume);
      }
      else if (audioSource.clip == runClip)
      {
        audioSource.Stop();
        audioSource.PlayOneShot(walkClip, audioSource.volume);
      }
      audioSource.clip = walkClip;
    }
    else if (isRunning)
    {
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(runClip, audioSource.volume);
      }
      else if (audioSource.clip == walkClip)
      {
        audioSource.Stop();
        audioSource.PlayOneShot(runClip, audioSource.volume);
      }
      audioSource.clip = runClip;
    }
    else if (!isRunning && !isWalking && (audioSource.clip == runClip || audioSource.clip == walkClip))
    {
      audioSource.Stop();
    }
  }
}
