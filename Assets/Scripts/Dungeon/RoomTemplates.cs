using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public static RoomTemplates instance;
	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	private bool spawnedBoss;
	public GameObject boss;
	public GameObject Player;
	public Transform PlayerspawnPoint;

	void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Invoke(nameof(DisableDestroyer), 2);
	}

	void Update(){
		if (!spawnedBoss)
		{
			if(waitTime <= 0 && spawnedBoss == false){
				for (int i = 0; i < rooms.Count; i++) {
					if(i == rooms.Count -1 ){
						Room currentRoom = rooms[i].GetComponent<Room>();
						if (currentRoom)
						{
							currentRoom.roomType = Room.RoomType.Boss;
						}
						
						Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
						spawnedBoss = true;
					}
				}
			} else {
				waitTime -= Time.deltaTime;
			}
		}
		
	}
	private void DisableDestroyer()
	{
		Destroyer[] destroyers = GameObject.FindObjectsOfType<Destroyer>();
		for (int i = 0; i < destroyers.Length; i++)
		{
			Destroy(destroyers[i]);
		}

		GameObject[] SpawnedRooms = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < SpawnedRooms.Length; i++)
		{
			SpawnedRooms[i].AddComponent<Room>();
			SpawnedRooms[i].AddComponent<Rigidbody2D>().isKinematic = true;
			SpawnedRooms[i].AddComponent<BoxCollider2D>().isTrigger = true;
			BoxCollider2D box = SpawnedRooms[i].GetComponent<BoxCollider2D>();
			var main = box.size;
			main = new Vector2(16, 16);
			box.size = main;
		}

		Instantiate(RoomTemplates.instance.Player, PlayerspawnPoint.localPosition, Quaternion.identity);
	}


}
