using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttons;
    private RectTransform arrow;
    private int currentPosition;
    [Header ("SFX")]
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;

    private void Awake(){
        arrow = GetComponent<RectTransform>();
    }

    private void Update(){
        //change position
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //interacting with options
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change){
        currentPosition += _change;

        if(_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if(currentPosition < 0)
            currentPosition = buttons.Length - 1;
        else if(currentPosition > buttons.Length - 1)
            currentPosition = 0;

        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }

    private void Interact(){
        SoundManager.instance.PlaySound(interactSound);

        //access the button on each option and call the function from the option
        buttons[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
