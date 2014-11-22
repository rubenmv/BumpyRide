﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	Vector2 direction;
	Vector2 lastRotation;
	float friction = 0.98f;
	float speed = 3;
	Vector2 windowSize;
	float leftLimit;
	float rightLimit;
	public AudioClip clickSound;

	void Start()
	{
		windowSize = GameManager.Instance.getScreenSize();

		float margin = windowSize.x / 10;
    
		leftLimit = Camera.main.ScreenToWorldPoint(new Vector3(margin, 0, 0)).x;
		rightLimit = Camera.main.ScreenToWorldPoint(new Vector3(windowSize.x - margin, 0, 0)).x;
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        
		if (Input.GetMouseButtonDown(0))
		{
			// Posicion de raton en el mundo
			Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePosition = new Vector2(mouseWorld.x, mouseWorld.y);
            
			direction = (playerPosition - mousePosition) * speed;
			direction = new Vector2(Mathf.Clamp(direction.x, -12, 12), Mathf.Clamp(direction.y, -12, 12));

			applyForce(direction);

			// Audio
			audio.volume = 0.2f;
			audio.PlayOneShot(clickSound);
		}

		// Screen limits
		if (playerPosition.x > rightLimit)
		{
			direction = playerPosition - (new Vector2(playerPosition.x + 1, playerPosition.y));
			applyForce(direction);
		}
		else if (playerPosition.x < leftLimit)
		{
			direction = playerPosition - (new Vector2(playerPosition.x - 1, playerPosition.y));
			applyForce(direction);
		}

		rigidbody2D.velocity *= friction;

		// Rotation when moving
//		if (rigidbody2D.velocity != new Vector2(0, 0))
//		{
//			if (Mathf.Abs(transform.rotation.y) < 0.3)
//			{
//				float rotation = rigidbody2D.velocity.x;
//				if (direction.x < 0)
//				{
//					rotation *= -1;
//				}
//				transform.Rotate(0, rotation, 0);
//			}
//		}
	}

	void applyForce(Vector2 direction)
	{
		rigidbody2D.velocity *= 0;
		rigidbody2D.AddForce(direction * 50);
	}
}
