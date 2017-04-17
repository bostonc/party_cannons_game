using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    //public LayerMask passengerMask;

    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

	Transform prevParentGameObjOfRunner;

	private int direction = 0;

    public float speed;
    public bool cyclic;
    public float waitTime;
    [Range(0, 2)]
    public float easeAmount;

    int fromWaypointIndex;
    float percentBetweenWaypoints; //between 0 and 1
    float nextMoveTime;

    //List<PassengerMovement> passengerMovement;
    //Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();

	public void Start () {
        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; ++i) {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
	}
	
	void Update () {


        //UpdateRaycastOrigins();

        Vector3 velocity = CalculatePlatformMovement();
        //CalculatePassengerMovement(velocity);

		//destroy platforms
		if (velocity.x < 0) {
			direction = 1;
		}
		if (velocity.x > 0) {
			direction = 2;
		}

		if (transform.position.x < -20) {
			// Make sure to remove runner if it is a child of this platform
			// (Currently, players can stay on the platform and leave the screen, getting destoryed!)
			Transform t = this.gameObject.transform.FindChild ("Runner");
			if (t != null) { 
				t.parent = null;
			} 
		} else if (transform.position.x > 20) {
			Transform t = this.gameObject.transform.FindChild ("Runner");
			if (t != null) { 
				t.parent = null;
			} 
		}

		if (direction == 1 && transform.position.x < -50) {
			Destroy (gameObject);
		}
		if (direction == 2 && transform.position.x > 50) {
			Destroy (gameObject);
		}

        //MovePassengers(true);
        transform.Translate(velocity);
        //MovePassengers(false);
	}

    float Ease(float x) {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    Vector3 CalculatePlatformMovement() {

        if (Time.time < nextMoveTime) {
            return Vector3.zero;
        }

        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointInex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointInex]);
        percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;
        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointInex], easedPercentBetweenWaypoints);

        if (percentBetweenWaypoints >= 1) {
            percentBetweenWaypoints = 0;
            fromWaypointIndex++;

            if (!cyclic) {
                if (fromWaypointIndex >= globalWaypoints.Length - 1) {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            }

            nextMoveTime = Time.time + waitTime;
        }

        return newPos - transform.position;
    }

    /*void MovePassengers(bool beforeMovePlatform) {
        foreach(PassengerMovement passenger in passengerMovement) {
            if (!passengerDictionary.ContainsKey(passenger.transform)) {
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
            }
            if (passenger.moveBeforePlatform == beforeMovePlatform) {
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }
    }*/

    /*void CalculatePassengerMovement(Vector3 velocity) {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        passengerMovement = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        //vertically moving platform
        if (velocity.y != 0) {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for (int i = 0; i < verticalRayCount; ++i) {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                if (hit && hit.distance != 0) {
                    if (!movedPassengers.Contains(hit.transform)) {
                        movedPassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true));
                    }
                }
            }
        }

        //horizontally moving platform
        if (velocity.x != 0) {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for (int i = 0; i < horizontalRayCount; ++i) {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                if (hit && hit.distance != 0) {
                    if (!movedPassengers.Contains(hit.transform)) {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = -skinWidth;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true));
                    }
                }
            }
        }

        //on top of horizontal or move down
        if (directionY == -1 || velocity.y == 0 && velocity.x != 0) {
            float rayLength =skinWidth * 2;

            for (int i = 0; i < verticalRayCount; ++i) {
                Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                if (hit && hit.distance != 0) {
                    if (!movedPassengers.Contains(hit.transform)) {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, false));
                    }
                }
            }
        }
    }*/

    /*struct PassengerMovement {
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform) {
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }*/

	public void OnCollisionEnter(Collision coll) {
	if (coll.gameObject.name == "Runner") {
			if (coll.gameObject.transform.position.y > gameObject.transform.position.y) {
				prevParentGameObjOfRunner = coll.gameObject.transform.parent;
				coll.gameObject.transform.parent = this.gameObject.transform; 
			}
		}
	}

	public void OnCollisionExit(Collision coll) {
		if (coll.gameObject.name == "Runner") {
			coll.gameObject.transform.parent = prevParentGameObjOfRunner; 
		}
	} 

    void OnDrawGizmos() {
        if(localWaypoints != null) {
            Gizmos.color = Color.red;
            float size = .3f;

            for (int i = 0; i < localWaypoints.Length; ++i) {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
}

    