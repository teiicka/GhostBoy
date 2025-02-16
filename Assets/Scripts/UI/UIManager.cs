using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [Header ("Game Over")]
    [SerializeField] private GameObject pauseScreen;
    [Header ("SFX")]
    [SerializeField] private AudioClip gameOverSound;

    private void Awake(){
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void GameOver(){
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //game over functions

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainManu(){
        SceneManager.LoadScene(0);
    }

    public void Quit(){
        Application.Quit();
    }

    //Pause Menu
    public void PauseGame(bool status){
        pauseScreen.SetActive(status);

        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume(){
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume(){
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
}
