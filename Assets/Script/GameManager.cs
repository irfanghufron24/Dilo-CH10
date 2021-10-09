using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null) Debug.LogError("No Game Manager Found!!!");
            }
            return _instance;
        }
    }
    #endregion

    public int Score { get; private set; }

    [Header("Box Coin Controller")]
    public int coinSpawn;
    [SerializeField] BoxSpawner boxSpawnerPrefab;
    private List<BoxSpawner> boxSpawnerPool = new List<BoxSpawner>();

    [Header("Game area constraint")]
    public float areaConstraintValue = 8.5f;

    [Header("UI")]
    public Text scoreText;

    bool gameHasEnded = false;

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        for (int i = 0; i < coinSpawn; i++)
        {
            BoxSpawner coin = Instantiate(boxSpawnerPrefab);
            coin.Spawn();   
        }
        scoreText.text= $"Score : {Score}";
    }

    public Vector2 GetRandomPosition()
    {
        float xPosition = Random.Range(-areaConstraintValue, areaConstraintValue);
        float yPosition = Random.Range(-areaConstraintValue, areaConstraintValue);

        return new Vector2(xPosition, yPosition);
    }

    public void AddScore()
    {
        Score++;
        scoreText.text= $"Score : {Score}";
    }

    public void RespawnBox() => StartCoroutine(ReSpawnBox());
    IEnumerator ReSpawnBox()
    {
        yield return new WaitForSeconds(3);
        BoxSpawner spawner = GetBox();
        spawner.Spawn();
    }

    public BoxSpawner GetBox()
    {
        for (int i = 0; i < boxSpawnerPool.Count; i++)
        {
            if (!boxSpawnerPool[i].gameObject.activeSelf)
            {
                boxSpawnerPool[i].gameObject.SetActive(true);
                return boxSpawnerPool[i];
            }
        }

        BoxSpawner boxObject = Instantiate(boxSpawnerPrefab, transform);
        boxSpawnerPool.Add(boxObject);
        return boxObject;
    }
  
}