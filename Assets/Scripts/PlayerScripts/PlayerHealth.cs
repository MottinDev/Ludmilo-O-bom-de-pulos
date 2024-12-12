using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    private float health;
    private float lerpTimer;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    [Header("Audio")]
    public AudioClip[] damageClips;  // Array de diferentes sons de dano

    private float durationTimer;

    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();

       
    }
    

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (health <= 0)
        {
            Die(); 
        }
        if(overlay.color.a > 0)
        {
            if(health < 30)
               return;
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
        
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void PlayRandomDamageSound()
    {
        if (damageClips.Length > 0)
        {
            int randomIndex = Random.Range(0, damageClips.Length);
            AudioSource.PlayClipAtPoint(damageClips[randomIndex], transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        PlayRandomDamageSound();

    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }


    public void Die()
    {
        playerMotor.SetAlive(false);
        playerLook.SetAlive(false);
        StartCoroutine(WaitAndReload());
        
    }

    IEnumerator WaitAndReload()
    {
        
        yield return new WaitForSeconds(3f); // Aguarda 3 segundos
      
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

        PlayerMotor playerMotor = GetComponent<PlayerMotor>();
        if (playerMotor != null)
        {
            playerMotor.SetAlive(true);
        }
        PlayerLook playerLook = GetComponent<PlayerLook>();
        if (playerLook != null)
        {
            playerLook.SetAlive(true);
        }

    }
}



