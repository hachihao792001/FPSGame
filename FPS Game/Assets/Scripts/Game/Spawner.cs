using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Text remainingText;
    public GameObject SpawnerScreen;
    public GameObject[] Icons;
    public float theNum;
    public int currentRound;
    public static int currentRoundEnemiesLeft;
    public int[] currentRoundEachEnemyCount = new int[8];
    public int[] eachEnemyMaxCount;
    public GameObject[] Enemies;
    public float[] EnemiesFreq;
    public float ySpawn;
    public Vector2 xArea, zArea;

    private void Start()
    {
        currentRound = 0;
        theNum = 0;
        currentRoundEnemiesLeft = 0;
    }

    Vector3 randomPos()
    {
        return new Vector3(Random.Range(xArea.x, xArea.y), ySpawn, Random.Range(zArea.x, zArea.y));
    }

    void Update()
    {
        if (!GameManager.playing) return;
        remainingText.text = currentRoundEnemiesLeft.ToString();
        if (currentRoundEnemiesLeft == 0)
        {
            GameManager.audioM.PlaySound("NewRound", transform, 0f, 5, 1);

            StartCoroutine(ShowSpawnerScreenAndOff());
            currentRound++;
            SpawnerScreen.transform.GetChild(0).GetComponent<Text>().text = "Vòng " + currentRound;
            theNum++;

            //số enemy từng loại trong vòng này
            for (int i = 0; i < currentRoundEachEnemyCount.Length; i++)
            {
                currentRoundEachEnemyCount[i] = (int)Mathf.Round(theNum * EnemiesFreq[i]) + Random.Range(-2,3);
                currentRoundEachEnemyCount[i] = Mathf.Clamp(currentRoundEachEnemyCount[i], 0, eachEnemyMaxCount[i]);
                Icons[i].transform.GetChild(0).GetComponent<Text>().text = "x" + currentRoundEachEnemyCount[i];
            }
            
            //Spawn ra một số lượng enemy loại 1
            for (int i = 0; i < currentRoundEachEnemyCount[0]; i++)
                Instantiate(Enemies[0], randomPos(), Quaternion.identity, transform);
            Icons[0].gameObject.SetActive(true);

            for (int i = 1; i < Enemies.Length; i++)   //vòng for này chỉ đại diện cho nhiều cái if (vòng 3,6,9,... thì spawn thêm gì)
                if (currentRound >= i * 3)
                {
                    for (int j = 0; j < currentRoundEachEnemyCount[i]; j++)   //spawn enemy 
                        Instantiate(Enemies[i], randomPos(), Quaternion.identity, transform);
                    Icons[i].gameObject.SetActive(true);
                }
        }
    }

    IEnumerator ShowSpawnerScreenAndOff()
    {
        SpawnerScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        SpawnerScreen.SetActive(false);
    }

    public static void add_CREL() {
        currentRoundEnemiesLeft++;
    }
    public static void minus_CREL() {
        currentRoundEnemiesLeft--;
    }
}
