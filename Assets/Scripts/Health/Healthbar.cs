using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthBar;

    private void Start(){
        totalHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update(){
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
        
    }

}
