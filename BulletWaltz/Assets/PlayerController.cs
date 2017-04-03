using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidbody;
    public float forceValue;
    public float maxSpeed;
    public float decreasingSpeed;
    public ParticleSystem playerKillEffect;

    public AudioSource playerKillSound;

    public UnityEvent playerKilledEvent;

    public void Reset()
    {
        this.transform.localPosition = new Vector3(2, 0, 0);
        this.gameObject.SetActive(true);
        playerKillEffect.Stop();
        playerKillEffect.gameObject.SetActive(false);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        playerKillSound.Play();
        this.gameObject.SetActive(false);
        playerKillEffect.transform.position = this.transform.position;
        playerKillEffect.gameObject.SetActive(true);

        if (playerKilledEvent != null)
        {
            playerKilledEvent.Invoke();
        }
    }

	// Use this for initialization
	void Start () {
        rigidbody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 force = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            force += new Vector2(0, forceValue);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            force += new Vector2(0, -forceValue);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            force += new Vector2(forceValue, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            force += new Vector2(-forceValue, 0);
        }

        if (force != Vector2.zero)
        {
            rigidbody.AddForce(force);
            float speed = rigidbody.velocity.magnitude;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        }
        else
        {
            float speed = rigidbody.velocity.magnitude;
            speed -= decreasingSpeed * Time.deltaTime;
            if (speed < 0)
            {
                speed = 0;
            }
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        }
        if (rigidbody.velocity == Vector2.zero || force == Vector2.zero)
        {
            this.transform.eulerAngles = Vector3.zero;
        }
        else
        {
            float rotationZ = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
            this.transform.eulerAngles = new Vector3(0, 0, rotationZ);
        }
    }
}
