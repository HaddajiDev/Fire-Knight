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

		int shopIndex = Random.Range(0, rooms.Count - 1);

		roomTypes[shopIndex] = Room.RoomType.shop;
		

		for (int i = 0; i < rooms.Count; i++)
		{
			if (i == shopIndex)
			{
				Room currentRoom = rooms[i].GetComponent<Room>();
				currentRoom.roomType = Room.RoomType.shop;
				continue;
			}

			if (i == rooms.Count - 1)
			{
				roomTypes[i] = Room.RoomType.Boss;

				Room currentRoom = rooms[i].GetComponent<Room>();
				currentRoom.roomType = Room.RoomType.Boss;

				Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
			}
			else
			{
				Room.RoomType randomType = GetRandomRoomType();
				roomTypes[i] = randomType;

				Room currentRoom = rooms[i].GetComponent<Room>();
				currentRoom.roomType = randomType;
			}
			
		}
		
		
		
	}
	public Room.RoomType GetRandomRoomType()
	{
		Room.RoomType[] validRoomTypes = new Room.RoomType[]
		{
			Room.RoomType.Regular,
			Room.RoomType.traps,
			Room.RoomType.Empty
		};
		int randomIndex = Random.Range(0, validRoomTypes.Length);
    
		return validRoomTypes[randomIndex];
	}

}
