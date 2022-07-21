using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;
    public float arrowSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * arrowSpeed);
        if (rb.velocity.magnitude > 0.01) {
            if (audioSource.isPlaying) {
                audioSource.Stop(); //can be replaced with oncollision?
            }
        }   
    }
}
