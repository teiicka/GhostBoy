using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth{ get; private set;}
    private Animator anim;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer spriteRend;

    private bool invulnerable;
    [Header ("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake(){
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    
    public void Respawn(){
        dead = false;
        
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        if(GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = true;

    }
    public void TakeDamage(float _damage){
        
        if(invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if(currentHealth > 0){
            anim.SetTrigger("hurt");

            //iFrames
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else{
            if(!dead){
                //Player
                if(GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                //Enemy
                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                if(GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    private IEnumerator Invunerability(){
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        //invunerability duration
        for (int i = 0; i < numOfFlashes; i++){
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}
