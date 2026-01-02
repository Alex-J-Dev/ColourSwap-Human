using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource jumpSound;
    private ParticleSystem deathParticles;
    private Camera cam;
    private GameManager gameManager;

    private bool isAlive = true;

    public float force = 10;
    public List<Color> colours;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        jumpSound = gameObject.GetComponent<AudioSource>();
        deathParticles = transform.Find("Death Particles").GetComponent<ParticleSystem>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        cam = Camera.main;
    }

    void Update() {
        // Jump when the movement key is pressed
        if (isAlive && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))) {
            rb.velocity = Vector2.up * force;
            jumpSound.Play();
        }
        if (transform.position.y + 15 < cam.transform.position.y) death();
    }

    void LateUpdate() {
        // Have camera follow player
        if (transform.position.y > cam.transform.position.y) {
            cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "ColourChanger") {
            changeColour();
            Destroy(collision.gameObject);
            return;
        } else if (collision.tag == "Token") {
            gameManager.updateScore();
            Destroy(collision.gameObject);
            return;
        }

        SpriteRenderer collisionRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
        if (!isSameColour(collisionRenderer.color, sr.color)) death();
    }

    public void death() {
        isAlive = false;
        var main = deathParticles.main;
        main.startColor = sr.color;
        deathParticles.Play();
        gameObject.GetComponent<Collider2D>().enabled = false;
        sr.enabled = false;
        SceneManager.LoadSceneAsync(0);
    }

    public void changeColour() {
        int randomColour = Random.Range(0, colours.Count);
        while (colours[randomColour] == sr.color) {
            randomColour = Random.Range(0, colours.Count);
        }
        sr.color = colours[randomColour];
    }

    public bool isSameColour(Color colourA, Color colourB) {
        return (colourB.r - 0.15) <= colourA.r &&
               (colourB.r + 0.15) >= colourA.r &&
               (colourB.g - 0.15) <= colourA.g &&
               (colourB.g + 0.15) >= colourA.g &&
               (colourB.b - 0.15) <= colourA.b &&
               (colourB.b + 0.15) >= colourA.b;
    }
}
