using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour {

	public float bulletForce  = 5;
	float colliderHalfWidth;
	float colliderHalfHeight;
	CircleCollider2D _collider;
	float rad;


	void Start () {

		_collider = GetComponent<CircleCollider2D> ();
		rad = _collider.radius; 
		colliderHalfWidth = rad;
		colliderHalfHeight = rad;

        Invoke("DestroyBullet", 0.7f); // Destroy bullet after 0.7s after its initialization if it doesnt hit asteroid

	} 

	public void AddForce(Vector3 direction)
	{
		GetComponent<Rigidbody2D> ().AddForce (bulletForce * direction, ForceMode2D.Impulse);
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


    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }


}
