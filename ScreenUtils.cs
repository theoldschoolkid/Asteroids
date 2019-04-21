using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides screen utilities
/// </summary>
public static class ScreenUtils
{
	

	// cached for efficient boundary checking
	static float screenLeft;
	static float screenRight;
	static float screenTop;
	static float screenBottom;
    static Vector3 leftSpawner;
    static Vector3 rightSpawner;




    /// <summary>
    /// Gets the left edge of the screen in world coordinates
    /// </summary>
    /// <value>left edge of the screen</value>
    public static float ScreenLeft
    {
		get { return screenLeft; }
	}

	/// <summary>
	/// Gets the right edge of the screen in world coordinates
	/// </summary>
	/// <value>right edge of the screen</value>
	public static float ScreenRight
    {
		get { return screenRight; }
	}

	/// <summary>
	/// Gets the top edge of the screen in world coordinates
	/// </summary>
	/// <value>top edge of the screen</value>
	public static float ScreenTop
    {
		get { return screenTop; }
	}

	/// <summary>
	/// Gets the bottom edge of the screen in world coordinates
	/// </summary>
	/// <value>bottom edge of the screen</value>
	public static float ScreenBottom
    {
		get { return screenBottom; }
	}


    public static Vector3 LeftSpawner
    {
        get { return leftSpawner; }
    }

    public static Vector3 RightSpawner
    {
        get { return rightSpawner; }
    }

    /// <summary>
    /// Initializes the screen utilities
    /// </summary>
    public static void Initialize()
    {
        
		// save screen edges in world coordinates
		float screenZ = -Camera.main.transform.position.z;
		Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
		Vector3 upperRightCornerScreen = new Vector3(Screen.width, Screen.height, screenZ);
       
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
		Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        screenLeft = lowerLeftCornerWorld.x;
        
		screenRight = upperRightCornerWorld.x;//width

		screenTop = upperRightCornerWorld.y;//height

		screenBottom = lowerLeftCornerWorld.y;


        leftSpawner = lowerLeftCornerWorld;
        rightSpawner = upperRightCornerWorld;


     /* Debug.Log("Ls " + lowerLeftCornerScreen);
        Debug.Log("Us " + upperRightCornerScreen);
        Debug.Log("Lw " + lowerLeftCornerWorld);
        Debug.Log("Uw " + upperRightCornerWorld); */

    }

  
}
