using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienGachVo : MonoBehaviour
{
    private AudioSource audioSource;
    private const float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySounds("Sounds/Break");
        GameObject[] listGach = { gameObject.transform.GetChild(0).gameObject, gameObject.transform.GetChild(1).gameObject, gameObject.transform.GetChild(2).gameObject, gameObject.transform.GetChild(3).gameObject };
        listGach[0].GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, speed);
        listGach[1].GetComponent<Rigidbody2D>().velocity = new Vector2(speed, speed);
        listGach[2].GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, -speed);
        listGach[3].GetComponent<Rigidbody2D>().velocity = new Vector2(speed, -speed);
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySounds(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }
}
