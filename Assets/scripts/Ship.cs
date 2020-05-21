using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ship
/// </summary>
public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;
    // thrust and rotation support
    Rigidbody2D rb2D;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 180;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		// saved for efficiency
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{

        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {   Vector3 location = new Vector3(x, y, -Camera.main.transform.position.z);
            Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
            GameObject bullet = Instantiate<GameObject>(prefabBullet, location, Quaternion.identity);
            Rigidbody2D rb2DB = bullet.GetComponent<Rigidbody2D>();
            rb2DB.AddForce(ThrustForce * (thrustDirection*20), ForceMode2D.Force);
            AudioManager.Play(AudioClipName.PlayerShot);


        }
        // check for rotation input
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0) {

            // calculate rotation amount and apply rotation
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }
	}

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate()
    {
        // thrust as appropriate
        if (Input.GetAxis("Thrust") != 0)
        {
            rb2D.AddForce(ThrustForce * thrustDirection,
                ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Destroys the ship on collision with an asteroid
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Asteroid"))
        {
            AudioManager.Play(AudioClipName.PlayerShot);
            Destroy(gameObject);

        }
    }
}
