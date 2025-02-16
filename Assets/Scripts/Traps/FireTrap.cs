using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FireTrap : MonoBehaviour
{

    [SerializeField] private float damage;
    [Header ("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //when trap gets triggered
    private bool active; //when trap is active and can hurt the player

    private Health playerHealth;
    [Header ("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private void Awake(){
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if(playerHealth != null && active){
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            playerHealth = collision.GetComponent<Health>();
            
            if(!triggered){
                //player triggered the firetrap
                StartCoroutine(ActivateFireTrap());
            }
            if(active){
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFireTrap(){
        
        //turn the sprite red to notify the player
        triggered = true;
        spriteRend.color = Color.red;

        //wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //wait until x seconds, deactivated trap and resel all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
