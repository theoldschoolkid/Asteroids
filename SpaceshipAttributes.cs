using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAttributes : MonoBehaviour
{

		public GameObject ship;
      
        private Rigidbody2D _rigidBody;
		private CircleCollider2D _collider;
		private float rad;

		private Transform _transform;
		private Vector3 thrustDirection;

		public float ThrustForce = 10f;
		public float rotateDegreesPerSecond = 5f;
		private float rotationAmount;
		private float rotationInput;
		private float currentRotation;

		private float colliderHalfWidth;
		private float colliderHalfHeight;

		[SerializeField]
		GameObject PrefabBullet , PrefabExplosion , UI;
		

        Animator _animator;

     void Start()
	{
		_collider = this.GetComponent<CircleCollider2D>();
		rad = _collider.radius;
		colliderHalfWidth = rad;
		colliderHalfHeight = rad;

        // Store componets of the ship at start
		_rigidBody = this.GetComponent<Rigidbody2D>();
		_transform = this.GetComponent<Transform>();
        _animator = this.GetComponent<Animator>();
	}

	 void Update()
	{
        
		rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
		thrustDirection = this._transform.eulerAngles;                      //Get current roatation
		currentRotation = (this.thrustDirection.z + 90f) * Mathf.Deg2Rad;   // For aligning spaceship vertically

		float num = Mathf.Cos(this.currentRotation);
		float num2 = Mathf.Sin(this.currentRotation);
		thrustDirection = new Vector2(num, num2);

		if (Input.GetAxis("Fire1") != 0f)
		{
			this.transform.Rotate(Vector3.forward, rotationAmount);
		}
		if (Input.GetAxis("Fire2") != 0f)
		{
			this.transform.Rotate(Vector3.back, rotationAmount);
		}

        // Fire bullet
		if (Input.GetKeyDown (KeyCode.LeftControl)) {

			GameObject bullet = Instantiate (PrefabBullet) as GameObject;            //Instantiate bullet at spaceships current position
			bullet.transform.position = this.transform.position;            
            bullet.GetComponent<Rigidbody2D>().rotation = this._rigidBody.rotation;  // Allign bullets direction as the spaceships
			bullet.GetComponent<BulletVelocity> ().AddForce (thrustDirection);       // fire bullet towards the direction the spaceship is facing
			AudioManager.Play(AudioClipName.PlayerShot);			
		}



	}

	 void FixedUpdate()
	{
		if (Input.GetAxis("Jump") != 0f)  // jump = space
		{
            _animator.SetBool("Thrust", true);   // Turn on thrust animation as long as "space" button is down
            this._rigidBody.AddForce(ThrustForce * thrustDirection, 0);   // Add thrust force to spaceship
            AudioManager.PlayThrust(AudioClipName.Thrust);
		}
        else
        {
            _animator.SetBool("Thrust", false);
          
        }
	}

    // When the object reaches the edge, spawn in the opposite direction
    void OnBecameInvisible()
	{
       
        Vector3 position = this.transform.position;
		if (position.x - this.colliderHalfWidth < ScreenUtils.ScreenLeft)
		{
			position.x = ScreenUtils.ScreenRight + this.colliderHalfWidth;
		}
		else if (position.x + this.colliderHalfWidth > ScreenUtils.ScreenRight)
		{
			position.x = ScreenUtils.ScreenLeft - this.colliderHalfWidth;
		}
		if (position.y + this.colliderHalfHeight > ScreenUtils.ScreenTop)
		{
			position.y = ScreenUtils.ScreenBottom - this.colliderHalfHeight;
		}
		else if (position.y - this.colliderHalfHeight < ScreenUtils.ScreenBottom)
		{
			position.y = ScreenUtils.ScreenTop + this.colliderHalfHeight;
		}
		this.transform.position = position;
	}

	 void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Asteroid")
		{
			UI.GetComponent<UI> ().running = false;  // turnoff UI timer
            UI.GetComponent<UI>().SetShipDestroyed();
            Camera.main.GetComponent<CheckForPause>().CallPauseMenu(); //call pause menu
            AudioManager.Play(AudioClipName.PlayerDeath);
			Destroy(this.gameObject);

			GameObject expls = Instantiate (PrefabExplosion) as GameObject;
			expls.transform.position = this.transform.position;
			Destroy (expls, 1.0f);

		}
	}
}
