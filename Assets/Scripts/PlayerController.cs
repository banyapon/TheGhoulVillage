using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Transform GameObjectTransform;
    private Vector3 new_des_Position;
    private float dest_Distance;
    public float speed;
    Animator animator;
    private Camera mainCamera;
    public GameObject playerSprite, runEffect;
    public bool isRunning = false;
    SpriteRenderer m_SpriteRenderer;
    public AudioClip audioClip;
    public int healthbar = 20;
    LoadSceneManager loadSceneManager;
    public Slider sliderHp;
    public GameObject hurt;
    public AudioClip soundEffect;
    public float maxSpeed = 5f;
    public float speedIncrease = 1f;
    public AudioSource soundAudioSFX;
    private Rigidbody rb;

    void Start()
    {
        runEffect.SetActive(false);
        GameObjectTransform = transform;
        new_des_Position = GameObjectTransform.position;
        Time.timeScale = 1;
        mainCamera = Camera.main;
        animator = playerSprite.GetComponent<Animator>();
        m_SpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.material.renderQueue = 4000;
        rb = GetComponent<Rigidbody>();
        sliderHp.maxValue = healthbar;
        sliderHp.value = healthbar;
        hurt.SetActive(false);
        soundEffect = Resources.Load<AudioClip>("sounds/sfx/game-character-140506");
        if (loadSceneManager == null)
        {
            GameObject _loadScene = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            loadSceneManager = _loadScene.GetComponent<LoadSceneManager>();
        }
    }

    public void PlaySound()
    {
        if (soundEffect != null)
        {
            soundAudioSFX.PlayOneShot(soundEffect);
        }
        else
        {
            Debug.LogError("Audio clip not found.");
        }
    }

    void Update()
    {
        sliderHp.value = healthbar;
        dest_Distance = Vector3.Distance(new_des_Position, GameObjectTransform.position);
        if (dest_Distance < .5f)
        {
            speed = 0;
        }
        else if (dest_Distance > .5f)
        {
            speed = 3;
        }

        if (!isRunning)
        {
            animator.SetBool("isRun", false);
            runEffect.SetActive(false);
        }
        else
        {
            animator.SetBool("isRun", true);
            runEffect.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane PlanePlayer = new Plane(Vector3.up, GameObjectTransform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;

            if (PlanePlayer.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                new_des_Position = ray.GetPoint(hitdist);
                //Quaternion tRotate = Quaternion.LookRotation(targetPoint - transform.position);
                //GameObjectTransform.rotation = tRotate;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Plane PlanePlayer = new Plane(Vector3.up, GameObjectTransform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            if (PlanePlayer.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                new_des_Position = ray.GetPoint(hitdist);
                //Quaternion tRotate = Quaternion.LookRotation(targetPoint - transform.position);
                //GameObjectTransform.rotation = tRotate;
            }
        }
        if (dest_Distance > .5f)
        {
            isRunning = true;
            GameObjectTransform.position = Vector3.MoveTowards(GameObjectTransform.position, new_des_Position, speed * Time.deltaTime);
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            // Increase speed if it's below the maximum
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.velocity += rb.velocity.normalized * speedIncrease;
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }

        if(healthbar <= 0){
            healthbar = 0;
            loadSceneManager.LoadScene("GameOver");
        }
    }

    public void TakeDamage(int damage)
    {
        healthbar = healthbar - damage;
        hurt.SetActive(true);
        StartCoroutine(getHurt(0.07f));
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            PlaySound();
        }
    }

    IEnumerator getHurt(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        hurt.SetActive(false);
    }
}
