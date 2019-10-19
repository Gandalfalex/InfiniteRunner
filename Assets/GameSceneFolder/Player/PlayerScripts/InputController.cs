
using UnityEngine;

public  class InputController : MonoBehaviour{


    private enum Swipe { LEFT, RIGHT, UP, NONE};
    private Vector2 startTouchPoint;
    
    private const float threshold = 100;
    private bool blocked = true;

    [SerializeField]
    public Transform transformFloor;
    [SerializeField]
    public Rigidbody rb;

   
    private void Update() {
        transform.position = ForwardMovement.Move(transform.position);
        
        if (LeftRightMoveMent.GetInMotion()) {
            transform.position = LeftRightMoveMent.ContinueMove(transform.position);
        }
      
        if (Input.anyKeyDown) {
            Vector2 temp = WorkWithInputsButton();
            if (temp.x != 0)
                LeftRightMoveMent.FirstMotion(transform.position, temp.x * transformFloor.localScale.x);
            else if (temp.y != 0)
                rb.velocity = Vector3.up * 6;
        }

        if(Input.touchCount >= 1) {
            Touch touch = Input.touches[0];
            if (touch.phase.Equals(TouchPhase.Began)) {
                blocked = true;
                startTouchPoint = touch.position;
            }
            else if(touch.phase.Equals(TouchPhase.Ended) || touch.phase.Equals(TouchPhase.Canceled)) {
                Reset();
            }
            if (blocked) {
                UseInputInformation(touch);
            }
        }
        CheckNearDeath();
    }

    private void UseInputInformation(Touch touch) {
        Swipe s = Direction(touch.position - startTouchPoint);

        if (s.Equals(Swipe.LEFT))
            LeftRightMoveMent.FirstMotion(transform.position, -transformFloor.localScale.x);
        else if (s.Equals(Swipe.RIGHT))
            LeftRightMoveMent.FirstMotion(transform.position, transformFloor.localScale.x);
        else if (s.Equals(Swipe.UP))
            rb.velocity = Vector3.up * 6;
    }

    private void CheckNearDeath() {
        if (PlayerManager.Instance.getNearDeath()) {
            LeftRightMoveMent.SetSiteHit();
            PlayerManager.Instance.setNearDeath(false);
        }
    }

    public Vector2 WorkWithInputsButton() {
        float dirct_LR = Input.GetAxisRaw("Horizontal");
        float direct_TL = Input.GetAxisRaw("Vertical") * 6;
        return new Vector2(dirct_LR, direct_TL);
    }

    private Swipe Direction(Vector2 deltaVector) {
        if(deltaVector.magnitude > threshold) {
            if (Mathf.Abs(deltaVector.x) > Mathf.Abs(deltaVector.y)) {
                if (deltaVector.x > 0) 
                    return Swipe.RIGHT;
                else return Swipe.LEFT;
            }
            else {
                if (deltaVector.y > 0)
                    return Swipe.UP;
            }
        }
        return Swipe.NONE;
    }

    private void Reset() {
        startTouchPoint = Vector2.zero;
        blocked = false;
    }

}
