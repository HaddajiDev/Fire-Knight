using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public static RoomTemplates instance;
	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;
	
	public GameObject boss;
	public GameObject Player;
	public Transform PlayerspawnPoint;

	private List<Room.RoomType> roomTypes;
	void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Invoke(nameof(DisableDestroyer), 2);
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
			RoomSize roomSize = SpawnedRooms[i].GetComponent<RoomSize>();
			var main = box.size;
			main = new Vector2(roomSize.Size * 8, roomSize.Size * 8);
			box.size = main;
		}
		AssignRoomTypes();
		Instantiate(RoomTemplates.instance.Player, PlayerspawnPoint.localPosition, Quaternion.identity);
	}

	private void AssignRoomTypes()
	{
		roomTypes = new List<Room.RoomType>(new Room.RoomType[rooms.Count]);

		int shopIndex;
		do
		{
			shopIndex = Random.Range(0, rooms.Count);
		} while (roomTypes[shopIndex] != Room.RoomType.Empty);

		roomTypes[shopIndex] = Room.RoomType.shop;
		
		for (int i = 0; i < rooms.Count; i++)
		{
			if (roomTypes[i] == Room.RoomType.Boss || roomTypes[i] == Room.RoomType.shop)
			{
				continue;
			}
			Room.RoomType randomType = (Room.RoomType)Random.Range(0, 3);
			rooms[i].GetComponent<Room>().roomType = randomType;
			
			if(i == rooms.Count -1 ){
				Room currentRoom = rooms[i].GetComponent<Room>();
				currentRoom.roomType = Room.RoomType.Boss;
						
				Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
			}
		}
	}

}
