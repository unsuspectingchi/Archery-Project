using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private const float FORWARD_VEL = 0.1f;
  private const float ROTATION_VEL = 1f;
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
  private Rigidbody rb;
  private float rotationVel = 0;
  private float moveDirection = 0;
  private float rotDirection = 0;
  private CameraController camScript;
  private BowController bowScript;
  private Camera cam;
  public GameObject bow;

  void Start()
  {
    animator = gameObject.GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    rb = GetComponent<Rigidbody>();
    cam = Camera.main;
    camScript = cam.GetComponent<CameraController>();
    bowScript = bow.GetComponent<BowController>();
  }

  void FixedUpdate()
  {
    rb.MovePosition(transform.position + transform.forward * moveDirection * FORWARD_VEL);
    rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotDirection * ROTATION_VEL));
  }

  void Update()
  {
    moveDirection = 0;
    rotDirection = 0;
    // Default Behavior
    isWalking = false;
    isRunning = false;
    animator.SetInteger("nMove", 0);

    // Shooting
    if (Input.GetMouseButtonDown(0))
    {
      isShooting = true;
      camScript.isShooting = true;
      bowScript.isShooting = true;
    }
    if (Input.GetMouseButtonUp(0))
    {
      audioSource.clip = shootClip;
      audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
    }

    if (Input.GetMouseButtonDown(1))
    {
      if (!isShooting)
      {
        isShooting = true;
        camScript.isShooting = true;
        bowScript.isShooting = true;
      }
      else
      {
        StopShooting();
      }
    }


    // Flipping
    if (Input.GetKey(KeyCode.F) && !isShooting && !isKicking && !isKnifing)
    {
      // Start flipping
      isFlipping = true;
      animator.SetInteger("nKnockDown", 3);
      StopShooting();
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
      StopShooting();
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
      StopShooting();
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

    // Forward Movement
    if (Input.GetKey(KeyCode.W) && !isCrouching && !isKnifing && !isKicking)
    {
      if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
      {
        // Run forward
        animator.SetInteger("nMove", 2);
        isRunning = true;
        moveDirection = 1.5f;
      }
      else
      {
        // Walk forward
        animator.SetInteger("nMove", 1);
        isWalking = true;
        moveDirection = 1;
      }
    }

    // Backward movement Left
    if (Input.GetKey(KeyCode.S) && !isWalking && !isRunning)
    {
      // Walk left
      animator.SetInteger("nMove", 1);
      isWalking = true;
      moveDirection = -1;
    }

    // Turning Left
    if (Input.GetKey(KeyCode.A))
    {
      rotDirection = -1;
      // Turn left
    }

    // Turning Right
    if (Input.GetKey(KeyCode.D))
    {
      rotDirection = 1;
      // Turn right
    }

    // Unity collision issues
    if (rotDirection == 0 && moveDirection == 0)
    {
      rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }
    else if (moveDirection == 0)
    {
      rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    else if (rotDirection == 0)
    {
      rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
    else
    {
      rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
  void StopShooting()
  {
    isShooting = false;
    bowScript.isShooting = false;
    camScript.isShooting = false;
  }
}
