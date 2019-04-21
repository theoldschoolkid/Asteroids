using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This is to initialize small Asteroid, once the asteroid is created this gives random speed and direction
/// </summary>

public class SmallAsteroidIni : MonoBehaviour {

	CircleCollider2D _collider;
	float colliderHalfWidth;
	float colliderHalfHeight;
	float rad;

	[SerializeField]	
	GameObject expl;    // Explosion Prefab


	const float MinImpulseForce = 1.5f;
	const float MaxImpulseForce = 2.5f;
	float magnitude;
	Rigidbody2D _rigidBody;


	void Start()
	{
        // Initialize components of this gameobject
        _rigidBody = this.GetComponent<Rigidbody2D> ();
		_collider = this.GetComponent<CircleCollider2D> ();
		rad = _collider.radius; 
		colliderHalfWidth = rad;
		colliderHalfHeight = rad;
		
	}

	public void Initialize(Vector2 dir)
	{       
        magnitude = Random.Range (MinImpulseForce, MaxImpulseForce);
		GetComponent<Rigidbody2D> ().AddForce (dir * magnitude,ForceMode2D.Impulse);   // Add a random direction and force when initialized
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet") {

                AudioManager.Play (AudioClipName.AsteroidHit);   
			    Destroy (this.gameObject);
			    Destroy (other.gameObject); // Destroy bullet
		  	    GameObject anim = Instantiate (expl) as GameObject; // Play explosion prefab 
                anim.transform.position = this.transform.position;
				Destroy (anim, 2.0f);
			}

	}

    // When the object reaches the edge, spawn in the opposite direction
    void OnBecameInvisible()
	{

		Vector3 position = transform.position;
		if (position.x - colliderHalfWidth < ScreenUtils.ScreenLeft)
		{
			position.x = ScreenUtils.ScreenRight + colliderHalfWidth;
		}
		else if (position.x + colliderHalfWidth > ScreenUtils.ScreenRight)
		{
			position.x = ScreenUtils.ScreenLeft - colliderHalfWidth;
		}
		if (position.y + colliderHalfHeight > ScreenUtils.ScreenTop)
		{
			position.y = ScreenUtils.ScreenBottom - colliderHalfHeight;
		}
		else if (position.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
		{
			position.y = ScreenUtils.ScreenTop + colliderHalfHeight;
		}
		transform.position = position;	


	}
}
