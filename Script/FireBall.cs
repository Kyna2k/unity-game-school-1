using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed;
    private new Rigidbody2D rigidbody2D;
    public bool animation = false;
    private Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySounds("Sounds/Fire Ball");
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(speed, 0);
        animator= GetComponent<Animator>();
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "matdat")
        {
            rigidbody2D.velocity = new Vector2(speed, Mathf.Abs(speed));
            
        }if(collision.gameObject.tag == "CaiCong" )
        {
            gameObject.GetComponent<Animator>().SetBool("Kill", true);
            Destroy(gameObject,0.1f);
        }
    }
    public void setSpeed(float s)
    {
        speed = s;
    }
    public void PlaySounds(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }
}
