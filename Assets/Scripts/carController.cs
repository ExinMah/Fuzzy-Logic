using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carController : MonoBehaviour
{
    public float carSpeed;
    public float maxPox = 8.0f;
    public static int hp = 3;
    public static float timeSurvived;
    public static float playingTimer;
    public AudioSource source;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        playingTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseGame.gameIsPaused)
        {
            if (hp == 0)
            {
                timeSurvived = playingTimer;
                AICarMove.previousLifetime = UIManager.score / 100f;
                UIManager.score = 0;
                playingTimer = 0f;
                AICarMove.lifetime = 0f;
                PlayerPrefs.SetFloat("previousLifetime", AICarMove.previousLifetime);
                hp = 3;
                SceneManager.LoadScene("LostScene");
            }

            playingTimer += Time.deltaTime;

            if (playingTimer > 60f)
                AICarMove.speedMultiplier = 0.1f;
            else if (playingTimer > 120f)
                AICarMove.speedMultiplier = 0.15f;
            else if (playingTimer > 180f)
                AICarMove.speedMultiplier = 0.2f;
            else if (playingTimer > 240f)
                AICarMove.speedMultiplier = 0.25f;
            else
                AICarMove.speedMultiplier = 0.3f;

            position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
            position.y += Input.GetAxis("Vertical") * carSpeed * Time.deltaTime;

            // Limit the value of variable into the min and max value 
            position.x = Mathf.Clamp(position.x, -maxPox, maxPox);
            position.y = Mathf.Clamp(position.y, -maxPox, maxPox);

            transform.position = position;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "AI Car")
        {
            source.Play();
            hp -= 1;
            Destroy(collision.gameObject);
        }
    }

}
