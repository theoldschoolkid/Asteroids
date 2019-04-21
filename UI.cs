using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	Text gameTimer; // The UI varialbe for timer

	float timer = 1f;
	string filter;
	int dotLoc;
	public bool running = true;
    public bool shipDestroyed = false;

	
	void Update () {	

		if (running == true && shipDestroyed == false)  // Run until ship is not destroyed
        {
			timer = timer + Time.deltaTime;

            // Filter out extra number from float variable so that it can display 1 number after "."
			filter = timer.ToString ();
			dotLoc = filter.IndexOf (".");  // Find the dot Location
			filter = filter.Substring (0, dotLoc + 2);
			gameTimer.text = filter;
		}
               		
	}

    public void SetShipDestroyed()  
    {
        shipDestroyed = true;
    }
}
