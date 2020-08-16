﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 30;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImage;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.up * speed; 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(col.tag == "Alien")
        {
            soundManager.Instance.playOneShot(soundManager.Instance.AlienDies);
            IncreaseTextUIScore();
            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;

            Destroy(gameObject);

            DestroyObject(col.gameObject, 0.5f);
        }

        if(col.tag == "Shield")
        {
            Destroy(gameObject);
            DestroyObject(col.gameObject);
        }
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

    void IncreaseTextUIScore()
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();  //score = name assigned in hirearchy
        int score = int.Parse(textUIComp.text);

        score += 10;

        textUIComp.text = score.ToString();
    }
}
