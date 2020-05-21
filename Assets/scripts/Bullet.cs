using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Timer explodeTimer;
    const int alive = 2;
    // Start is called before the first frame update
    void Start()
    {
        // create and run timer
        explodeTimer = gameObject.AddComponent<Timer>();
        explodeTimer.Duration = alive;
        explodeTimer.Run();

       
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // check for timer just finished
        if (explodeTimer.Finished)
        {

            GameObject bullet = GameObject.FindWithTag("Bullet");
            if (bullet != null)
            {
                Destroy(bullet);
            }

            // save start time and restart timer
            explodeTimer.Run();
        }
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
