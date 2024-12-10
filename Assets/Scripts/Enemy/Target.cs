using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public AudioSource damageSound;  
    private Renderer rend;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            damageSound.Play();
            StartCoroutine(ChangeColorForDuration(1f, Color.red)); 
            
        }
    }

    void Die()
    {
        damageSound.Play();
        Destroy(gameObject);
    }

    IEnumerator ChangeColorForDuration(float duration, Color color)
    {
        rend.material.color = color;
        yield return new WaitForSeconds(duration);
        rend.material.color = UnityEngine.Color.white; 
    }
}
