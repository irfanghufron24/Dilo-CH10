using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public Vector2 scaleRandomValue;

    private Transform player;

    private void Awake()
    {
        player = FindObjectOfType<MouseControll>().transform;
    }


    public void Spawn()
    {
        bool boxSpawned = false;
        while (!boxSpawned)
        {
            Vector2 spawnPosition = GameManager.Instance.GetRandomPosition();
            if(((Vector2)player.position - spawnPosition).magnitude < 2)
            {
                continue;
            }
            else
            {
                transform.position = spawnPosition;
                Setup();
                boxSpawned = true;
            }
        }
    }

    private void Setup()
    {
        float xScale = Random.Range(scaleRandomValue.x, scaleRandomValue.y);
        float yScale = Random.Range(scaleRandomValue.x, scaleRandomValue.y);

        transform.localScale = new Vector2(xScale, yScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddScore();
            GameManager.Instance.RespawnBox();
            gameObject.SetActive(false);
        }
    }
}
