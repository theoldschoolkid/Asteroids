using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns Asteroids
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{

	const float MinSpawnDelay = 0.5f;
	const float MaxSpawnDelay = 1f;

	int x,y;	
	int minSpawnX;
	int maxSpawnX;
	int minSpawnY;
	int maxSpawnY;

    int asteroidCount = 4;
    float defaultAsteroidTimer = 10;  // Increase asteroid count every "defaultAsteroidTimer" seconds
    float astTimer;                   // Asteroid timer Counter
    int spawnPoint;

	Vector3 Left;
    Vector3 Top;

    // Spawn Directions for asteroid --- Need to Randomize ---
	Vector2 topD = new Vector2(0.5f,-1);    
	Vector2 BottomD = new Vector2(0.3f,1);
	Vector2 LeftD = new Vector2(1,0.7f);
	Vector2 RightD = new Vector2(-1,0.4f);

	[SerializeField]
	GameObject PrefabAsteroid , PrefabSmallAsteroid;	

	SpriteRenderer _sprite;
	List<GameObject> _sprer = new List<GameObject>(); // A list that stores different color asteroids to spawn


	void Start()
	{

        // Load different types of asteroids from the Resources folder into _sprer variable
		_sprer.Add ((GameObject)Resources.Load (@"AsteroidSprites\GreenAsteroid"));
		_sprer.Add ((GameObject)Resources.Load (@"AsteroidSprites\MagentaAsteroid"));
		_sprer.Add ((GameObject)Resources.Load (@"AsteroidSprites\WhiteAsteroid"));
    
        // Get the Top and the left spawn point co-oradinates
		Left = new Vector3( ScreenUtils.LeftSpawner.x,0, ScreenUtils.RightSpawner.z);
        Top = new Vector3(0, ScreenUtils.RightSpawner.y, ScreenUtils.RightSpawner.z);
       
        // Initialize 4 asteroids at start from 4 different locations
        SpawnAsteroid (Left , LeftD);
		SpawnAsteroid (Top, topD);
		SpawnAsteroid (-Left , RightD);
		SpawnAsteroid (-Top , BottomD);

    }


	void Update()
	{
        IncreseAstroidCount();  // check if defaultAsteroidTimer is reached

        // Get number of Asteroids in the current scene incuding the small asteroids
        GameObject[] Astcount = GameObject.FindGameObjectsWithTag ("Asteroid"); 

		if( Astcount.Length < asteroidCount)  // If Astcount is less than asteroidCount spawn asteroid
        {
            spawnPoint = Random.Range(0, 4);    // 0,1,2,3 represents the spawn points, spawn asteroid in random spawnpoints

            switch(spawnPoint)
            {
                case 0:
                    SpawnAsteroid(Left, LeftD);
                    break;

                case 1:
                    SpawnAsteroid(Top, topD);
                    break;

                case 2:
                    SpawnAsteroid(-Left, RightD);
                    break;

                case 3:
                    SpawnAsteroid(-Top, BottomD);
                    break;

                default:
                    SpawnAsteroid(Top , topD);
                    break;
            }
            
		}
	}


	 void SpawnAsteroid( Vector3 pos, Vector2 dir)
	{
        
        // Create Asteroid, set its postion to "pos" and add direction and force by calling AsteroidIni script's Initialize method
        GameObject Ast = Instantiate(PrefabAsteroid) as GameObject;
		Ast.transform.position = pos;
		Ast.GetComponent<AsteroidIni> ().Initialize (dir);

		_sprite = Ast.GetComponent<SpriteRenderer> ();  // Get Sprite rendered component of the newly created asteroid

        // Select a random sprite for the asteroid
		x = Random.Range(0, 3);
		if (x == 0)
		{
			_sprite.sprite = _sprer [x].GetComponent<SpriteRenderer> ().sprite;;
		}
		else if (x == 1)
		{
			_sprite.sprite = _sprer [x].GetComponent<SpriteRenderer> ().sprite;;
		}
		else
		{
			_sprite.sprite = _sprer [x].GetComponent<SpriteRenderer> ().sprite;;
		}


	}


	public void SpawnSmallAsteroid( Vector3 pos , Vector2 dir)

	{
        // Create Small Asteroid, set its postion using to "pos" and add direction and force by calling SmallAsteroidIni script's Initialize method
        GameObject Sast = Instantiate(PrefabSmallAsteroid) as GameObject;
		Sast.transform.position = pos;
        Sast.GetComponent<SmallAsteroidIni>().Initialize(dir);

        _sprite = Sast.GetComponent<SpriteRenderer> ();

		y = Random.Range(0, 3);
        if (y == 0)
		{
			_sprite.sprite = _sprer [y].GetComponent<SpriteRenderer> ().sprite;;
		}
		else if (y == 1)
		{
			_sprite.sprite = _sprer [y].GetComponent<SpriteRenderer> ().sprite;;
		}
		else
		{
			_sprite.sprite = _sprer [y].GetComponent<SpriteRenderer> ().sprite;;
		}


	}


    void IncreseAstroidCount()
    {
        astTimer += Time.deltaTime;          
        if (astTimer > defaultAsteroidTimer)
        {
            // increase the asteroidcount number every "defaultAsteroidTimer" seconds
            asteroidCount++;  
            astTimer = 0;  // Reset timer
        }
    }


}



