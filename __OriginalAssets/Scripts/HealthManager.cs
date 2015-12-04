using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    void Start()
    {
        InitializeValues();
        SwitchInvincibility(false);
    } 

    private new AudioSource audio;

    void InitializeValues ()
    {
        health = maxHealth;
        audio = GetComponent<AudioSource>();
    }

    public float maxHealth;
    private float _health;
    public float health
    {
        get { return _health; }
        set
        {
            value = (int)Mathf.Clamp(value, 0, maxHealth);
            _health = value;
            //Debug.Log(_health);
            if (_health == 0)
            {
                NotifyDeath();
            }
        }
    }

    public float healthNormalized
    {
        get { return health / maxHealth; }
    }

    public delegate void notificationHandler();
    public event notificationHandler OnDeath;
    private bool notified;

    public void NotifyDeath()
    {
        EndLevel();
        if (OnDeath != null && !notified)
        {
            OnDeath();
        }
    }

    void EndLevel ()
    {
        Debug.Log("You died");
        GameManager.instance.EndLevel();
    }

    private bool invincible;
    public int[] threatLayers;

    void SwitchInvincibility(bool invincible)
    {
        this.invincible = invincible;
        foreach (int threatLayer in threatLayers)
        {
            Physics.IgnoreLayerCollision(gameObject.layer, threatLayer, invincible);
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Damager damager = otherCollider.gameObject.GetComponent<Damager>();
        if (damager != null)
        { 
            if (!invincible)
            {
                RecieveDamage(damager);
            }
            if (otherCollider.tag == "EnemyProjectile")
            {
                Destroy(otherCollider.gameObject);
            }
        }
    }

    void RecieveDamage(Damager damager)
    {
        UpdateHealth(-damager.damage);
        StartCoroutine(InvincibilityMode());
    }

    public void UpdateHealth(float toAdd)
    {
        health += toAdd;
        NotifyHit();
        if (audio.clip != null)
        {
            audio.Play();
        }
    }

    public event notificationHandler OnHit;

    /// <summary>
    /// Used for hit effects like changing the UI color momentarily
    /// </summary>
    public void NotifyHit()
    {
        if (OnHit != null)
        {
            OnHit();
        }
    }

    public float coolDown;

    IEnumerator InvincibilityMode()
    {
        SwitchInvincibility(true);
        yield return new WaitForSeconds(coolDown);
        SwitchInvincibility(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        Damager damager = collision.gameObject.GetComponent<Damager>();
        if (damager != null)
        {     
            if (!invincible)
            {
                RecieveDamage(damager);
            }
            if (collision.gameObject.tag == "EnemyProjectile")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
