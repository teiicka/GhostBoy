using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header ("SFX")]
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake(){
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn(){
        //move player to latest checkpoint or restart the game
        if(currentCheckpoint == null){
            //show game over screen
            uiManager.GameOver();
            return;
        }

        transform.position = currentCheckpoint.position;

        //restore player health after death
        playerHealth.Respawn();

        //move cam to lastest checkpoint
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.transform.tag == "Checkpoint"){
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
