using UnityEngine;
using System.Collections;

//Attach to the monster
//Generate eight trigger colliders that expand at a constant/random rate

public class PathFinding : MonoBehaviour {

	public GameObject[] radialObjects;
	public GameObject radialPrefab;

	int radialCount;
	float angleIncrements;
	float angleChangeBy;
	float radialLength;
	float radialExtensionLimit;
	float scanDuration;
	float movementDuration;

	Rigidbody2D myrigidbody;
	float yMovement;
	float xMovement;

	public Collider2D[] radialColliders;

	int layerToCollideWith;

	// Use this for initialization
	void Start () {
		radialCount = 8;
		radialObjects = new GameObject[radialCount];
		angleIncrements = 0;
		angleChangeBy = 45;
		radialLength = 0f;
		radialExtensionLimit = 10f;//how far the colliders extend
		scanDuration = .01f;
		movementDuration = scanDuration * 30f;

		myrigidbody = GetComponent<Rigidbody2D> ();

		radialColliders = new Collider2D[radialCount];

		layerToCollideWith = 8;//corresponds to solid layer

		//creates the extending colliders
		for (int i = 0; i < radialCount; i++) {
			radialObjects [i] = (GameObject)Instantiate (radialPrefab);
			radialObjects [i].transform.localPosition = new Vector3 (
				transform.localPosition.x,
				transform.localPosition.y,
				transform.localPosition.z
			);
			radialObjects [i].transform.SetParent (transform);
			radialColliders [i] = radialObjects [i].GetComponent<Collider2D> ();
		}

		//reorients the extending colliders
		for (int i = 0; i < radialCount; i++) {
			radialObjects [i].transform.localScale = new Vector3 (radialLength, 1f, 1);
			radialObjects [i].transform.Rotate (0f, 0f, angleIncrements);
			angleIncrements = angleIncrements + angleChangeBy;
		}
		StartCoroutine(ExpandRadial());
		StartCoroutine (Navigate());
						
	}
		
	IEnumerator ExpandRadial(){
		while (true) {
			yield return new WaitForSeconds (scanDuration);
			radialLength = radialLength + 1f;
			//resets collider length
			if (radialLength > radialExtensionLimit) {
				radialLength = 0f;
			}
			//expands collider
			for (int i = 0; i < radialCount; i++) {
				radialObjects [i].transform.localScale = new Vector3 (radialLength, .5f, 1);
			}
		}

	}

	//Moves the object based on the available collider directions
	IEnumerator Navigate(){
		while(true){
			yield return new WaitForSeconds (movementDuration);
			switch (Random.Range (0, 8)) {
			case 0://East
				if (radialColliders [0].enabled) {
					Move (2f, 0f);
				}
				break;
			case 1://Northeast
				if (radialColliders [1].enabled) {
					Move (2f, 2f);
				}
				break;
			case 2://North
				if (radialColliders [2].enabled) {
					Move (0f, 2f);
				}
				break;
			case 3://Northwest
				if (radialColliders [3].enabled) {
					Move (-2f, 2f);
				}
				break;
			case 4://West
				if (radialColliders [4].enabled) {
					Move (-2f, 0f);
				}
				break;
			case 5://Southwest
				if (radialColliders [5].enabled) {
					Move (-2f, -2f);
				}
				break;
			case 6://South
				if (radialColliders [6].enabled) {
					Move (0f, -2f);
				}
				break;
			case 7://Southeast
				if (radialColliders [7].enabled) {
					Move (2f, -2f);
				}
				break;
			default://Stop
				Stop();
				break;
			}
		}
	}


	void Move(float xMove, float yMove){
		
		myrigidbody.velocity = new Vector2 (xMove, yMove);
	}

	void Stop(){
		myrigidbody.velocity = new Vector2 (0f,0f);
	}
		
}
