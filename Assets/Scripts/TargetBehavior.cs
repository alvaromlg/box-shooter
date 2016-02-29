using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{

	// target impact on game
	public int scoreAmount = 0;
	public float timeAmount = 0.0f;
    //public GameObject playerCamera;

	// explosion when hit?
	public GameObject explosionPrefab;

    //private float shake = 10f;
    //private float shakeAmount = 0.7f;
    //private float decreaseFactor = 1.0f;

	// when collided with another gameObject
	void OnCollisionEnter (Collision newCollision)
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

        // only do stuff if hit by a projectile
        if (newCollision.gameObject.tag == "Projectile") {
            /*
            if (shake > 0)
            {
                Debug.Log("shaking");
                playerCamera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
                shake -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shake = 0;
            }
            */
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			// if game manager exists, make adjustments based on target properties
			if (GameManager.gm) {
				GameManager.gm.targetHit (scoreAmount, timeAmount);
			}
				
			// destroy the projectile
			Destroy (newCollision.gameObject);
				
			// destroy self
			Destroy (gameObject);
		}
	}
}
