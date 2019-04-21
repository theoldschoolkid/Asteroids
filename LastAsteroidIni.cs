using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAsteroidIni : MonoBehaviour {

	CircleCollider2D _collider;
	float colliderHalfWidth;
	float colliderHalfHeight;
	float rad;

	[SerializeField]	
	GameObject expl;


	const float MinImpulseForce = 0.5f;
	const float MaxImpulseForce = 2f;
	float angle;
	Vector2 direction;
	float magnitude;

	AsteroidSpawner astspwn;
	Rigidbody2D velocity;




	void Start()
	{
		velocity = this.GetComponent<Rigidbody2D> ();
		_collider = this.GetComponent<CircleCollider2D> ();
		rad = _collider.radius; 
		colliderHalfWidth = rad;
		colliderHalfHeight = rad;
		float ag = Mathf.Deg2Rad * 240;
		angle = Random.Range (0, ag);
		direction = new Vector2 (Mathf.Cos (angle + (75 * Mathf.Deg2Rad)), Mathf.Sin (angle) + (75 * Mathf.Deg2Rad));
		magnitude = Random.Range (MinImpulseForce, MaxImpulseForce);

		GetComponent<Rigidbody2D> ().AddForce (direction * magnitude,ForceMode2D.Impulse);
	
	}

	public void Initialize()
	{
		/*magnitude = Random.Range (MinImpulseForce, MaxImpulseForce);
		GetComponent<Rigidbody2D> ().AddForce (dir * magnitude,ForceMode2D.Impulse);*/

	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet") {

			Destroy (this.gameObject);
			Destroy (other.gameObject);


			AudioManager.Play (AudioClipName.AsteroidHit);
			Destroy (this.gameObject);
			Destroy (other.gameObject);
			GameObject anim = Instantiate (expl) as GameObject;
				anim.transform.position = this.transform.position;
				Destroy (anim, 2.0f);
			}

	}


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
