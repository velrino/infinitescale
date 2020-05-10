using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	public GameObject[] obstacles;
	public Vector2 numberOfObstacles;

	public List<GameObject> newObstacles;

    private Vector3 initialPosition;
    public float moveSpeed = -5;
	public float reSpawnPositionY = 0;
    public float minPositionY = -9;
    public float maxPositionY = -10;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
		int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);

		for (int i = 0; i < newNumberOfObstacles; i++)
		{
			newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
			newObstacles[i].SetActive(false);
		}

		PositionateObstacles();

	}
	
	void PositionateObstacles()
	{
		for (int i = 0; i < newObstacles.Count; i++)
		{
			float posZMin = (20f / newObstacles.Count) + (20f / newObstacles.Count) * i;
			float posZMax = (20f / newObstacles.Count) + (20f / newObstacles.Count) * i + 1;
			newObstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));
			newObstacles[i].SetActive(true);
		}
	}

    void Update()
    {   
        rb.velocity = new Vector3(0, (moveSpeed*2) , 0);
		print(transform.position.y);
        if(transform.position.y > maxPositionY && transform.position.y < minPositionY) {
            transform.position = new Vector3(transform.position.x, reSpawnPositionY, transform.position.z);
        }
         //transform.Translate((Vector3.down / MoveSpeed) * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//transform.position = new Vector3(7, 40 , 9);
			//Invoke("PositionateObstacles", 5f);
		}
	}


}