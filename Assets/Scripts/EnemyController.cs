using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float minSpeed = 3f;
    public float maxSpeed = 7f;
    private Transform playerTransform;
    private Rigidbody rb;
    private float currentSpeed;
    public GameObject enemySprite;
    public bool isRunning = true;
    SpriteRenderer m_SpriteRenderer;
    Animator animator;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        // Generate a random speed within the specified range
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        animator = enemySprite.GetComponent<Animator>();
        m_SpriteRenderer = enemySprite.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.material.renderQueue = 3800;
    }

    void Update()
    {
        if (!isRunning)
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
        }
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f; // Ignore height differences
        direction.Normalize();
        rb.velocity = direction * currentSpeed;

        Vector3 screenCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        // Check if the player is to the left or right of the center
        if (playerTransform.position.x < screenCenter.x)
        {
            // Player is to the left
            m_SpriteRenderer.flipX = true;
        }
        else if (playerTransform.position.x > screenCenter.x)
        {
            m_SpriteRenderer.flipX = false;
        }
        else
        {
            m_SpriteRenderer.flipX = false;
        }
    }
}
