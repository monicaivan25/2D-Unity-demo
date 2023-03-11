using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
	private float spriteLength, spriteStartPosition;
	public GameObject camera;
	public float parallaxEffect;

    // Start is called before the first frame update
	void Start()
	{
		spriteStartPosition = transform.position.x;
        // for length we need the sprite renderer and find the length from there
		spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
	}

    // Update is called once per frame
	void Update()
	{
		// how far we've moved relative to the camera
		float temp = (camera.transform.position.x * (1 - parallaxEffect));

		// how far we've moved from the start point
		float distance = (camera.transform.position.x * parallaxEffect);

		//actually move the camera (x,y,x) - we only change the x
		transform.position = new Vector3(spriteStartPosition + distance, transform.position.y, transform.position.z);

		// if(temp > spriteStartPosition + spriteLength) spriteStartPosition += spriteLength;
		// else if(temp < spriteStartPosition - spriteLength) spriteStartPosition -= spriteLength;
	}
}
