using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class H_Health : MonoBehaviour {

    public int startingHealth = 10;
    public int currentHealth;
    public float flashSpeed = 5f;
    Color flashColour = new Color(1f, 0f, 0f, .5f);
    public Image damageImage;
    public AudioClip deathClip;
    public bool isDead = false;

    AudioSource heroAudio;
    bool damaged;

    void Start()
    {
        heroAudio = GetComponent<AudioSource>();
        //currentHealth = startingHealth;
        currentHealth = 10;
    }
    void FixedUpdate()
    {

        if (damaged && damageImage != null)
        {
            damageImage.color = flashColour;

        }
        else if (damageImage != null)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;


    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        //healthSlider.value = currentHealth;

        heroAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;
        heroAudio.clip = deathClip;
        heroAudio.Play();

        LevelState.levelManager.StopLevel(false);

    }
}
