using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform leftEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    [Header ("Movement")]
    [SerializeField] private float speed;
    [Header ("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private Vector3 initScale;
    private bool movingLeft;

    [Header ("Enemy Aniamator")]
    [SerializeField] private Animator anim;
    private void Awake(){
        initScale = enemy.localScale;
    }

    private void OnDisable(){
        anim.SetBool("moving", false);
    }

    private void Update(){

        if(movingLeft){
            if(enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else{
                DirectionChange();
            }
        }
        else{
            if(enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else{
                DirectionChange();
            }
        }
        
    }

    private void DirectionChange(){
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration){
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction){
        idleTimer = 0;
        anim.SetBool("moving", true);
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //Move in that direciton
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
