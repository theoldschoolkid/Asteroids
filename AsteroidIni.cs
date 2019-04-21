using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This is to initialize Big Asteroid, once the asteroid is created this gives random speed and direction and if this asteroid is 
///  destroyed, it calls small asteroid spawner to generate 2 small asteroids
/// </summary>
public class AsteroidIni : MonoBehaviour {       

	CircleCollider2D _collider;
	float colliderHalfWidth;
	float colliderHalfHeight;
	float rad;

	[SerializeField]	
	GameObject explosion;                //Explosion Prefab

	[SerializeField]
	float minImpulseForce = 1f;
	[SerializeField]
	float maxImpulseForce = 2f;	
	float magnitude;

    AsteroidSpawner _asteroidSpawner;
	Rigidbody2D _rigidBody;

    Vector2 dir1 , dir2;                // Spawn Directions for Small Asteroids
    float x, y;


    void Start()
	{
        // Initialize components 
        _asteroidSpawner = Camera.main.GetComponent<AsteroidSpawner>();
        _rigidBody = this.GetComponent<Rigidbody2D> ();
		_collider = this.GetComponent<CircleCollider2D> ();

		rad = _collider.radius; 
		colliderHalfWidth = rad;
		colliderHalfHeight = rad;

	}

	public void Initialize(Vector2 dir)
	{       
		magnitude = Random.Range (minImpulseForce, maxImpulseForce);
		GetComponent<Rigidbody2D> ().AddForce (dir * magnitude,ForceMode2D.Impulse);	// Add a random direction and force when initialized
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet") {


            // Generate Random directions for the new small asteroids that needs to be spawned after this asteroid is destroyed
            CreateRandomDir(); 

            // Spawn two small Asteroids by calling AsteroidSpawner script attached to camera
            _asteroidSpawner.SpawnSmallAsteroid (this.transform.position , dir1);
            _asteroidSpawner.SpawnSmallAsteroid (this.transform.position, dir2);

			Destroy (this.gameObject); 
			Destroy (other.gameObject); // Destroy Bullet

			GameObject anim = Instantiate (explosion) as GameObject; // Play explosion prefab 
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

    
    void CreateRandomDir()
    {
        x = Random.Range(-0.1f, 1f);
        y = Random.Range(-0.5f, 1f);
        dir1 = new Vector2(x, y);
        dir2 = new Vector2(y, x);
    }


}
