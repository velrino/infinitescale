using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    public float speed;
    public float speedLane;
    private Rigidbody rb;
    private int currentLane = 1;
    private Vector3 verticalTargetPosition;
    private bool isSwipping = false;
    private Vector2 startingTouch;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ChangeLane(1);
        }
        
        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, transform.position.y, verticalTargetPosition.z);
        float newSpeedLane = (speedLane * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, newSpeedLane);
    
            Touch isTouch = Input.GetTouch(0);

        if(Input.touchCount == 1) {
            if(isSwipping) {
                Vector2 diff = (isTouch.position - startingTouch);
                if(diff.magnitude > 0.01f){
                    if(diff.x < 0) { 
                        ChangeLane(-1);
                    } 
                    else { 
                        ChangeLane(1);
                    } 
                }
            }
        }

        if(isTouch.phase == TouchPhase.Began){
            startingTouch = isTouch.position;
            isSwipping = true;
        }
        else if(isTouch.phase == TouchPhase.Ended){
            isSwipping = false;
        }
    }

    void ChangeLane(int direction) {
        int targetLane = currentLane + direction;
        if(targetLane < 0 || targetLane > 2)
            return;
        
        currentLane = targetLane;
        int newCurrentLane = (currentLane -1);
        verticalTargetPosition = new Vector3(newCurrentLane, 0, 0);
    }

    private void FixedUpdate() {
        rb.velocity = Vector3.up * speed;
    }
}
