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
			Room room = SpawnedRooms[i].AddComponent<Room>();
			SpawnedRooms[i].AddComponent<Rigidbody2D>().isKinematic = true;
			SpawnedRooms[i].AddComponent<BoxCollider2D>().isTrigger = true;
			BoxCollider2D box = SpawnedRooms[i].GetComponent<BoxCollider2D>();
			RoomSize roomSize = SpawnedRooms[i].GetComponent<RoomSize>();
			var main = box.size;
			main = new Vector2(roomSize.Size * 8, roomSize.Size * 8);
			box.size = main;

			if (SpawnedRooms[i].gameObject.name.Contains("corner"))
				room.CornerRoom = true;
		}
		AssignRoomTypes();
		Instantiate(RoomTemplates.instance.Player, PlayerspawnPoint.localPosition, Quaternion.identity);
		GameManager.instance.StartGame();
	}

	private void AssignRoomTypes()
	{
		roomTypes = new List<Room.RoomType>(new Room.RoomType[rooms.Count]);
		List<int> cornerRoomIndices = new List<int>();
		for (int i = 0; i < rooms.Count; i++)
		{
			if (rooms[i].TryGetComponent<Room>(out Room room) && room.CornerRoom)
			{
				cornerRoomIndices.Add(i);
			}
		}

		int shopIndex = cornerRoomIndices[Random.Range(0, cornerRoomIndices.Count - 1)];
		roomTypes[shopIndex] = Room.RoomType.shop;
		rooms[shopIndex].GetComponent<Room>().roomType = Room.RoomType.shop;
		if (cornerRoomIndices.Count == 0)
		{
			Debug.LogWarning("No corner rooms found!");
			roomTypes[shopIndex] = Room.RoomType.Empty;
			rooms[shopIndex].GetComponent<Room>().roomType = Room.RoomType.Empty;
		}

		

		for (int i = 0; i < rooms.Count; i++)
		{
			if (i == shopIndex)
			{
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
				Room.RoomType randomType = GetRandomRoomType(Room.RoomType.traps, Room.RoomType.Empty, Room.RoomType.Regular);
				roomTypes[i] = randomType;
				
				Room currentRoom = rooms[i].GetComponent<Room>();
				if (currentRoom.CornerRoom)
				{
					currentRoom.roomType = Room.RoomType.Empty;
				}
				else
				{
					currentRoom.roomType = randomType;
				}
			}
			
		}
		
		
		
	}
	public Room.RoomType GetRandomRoomType(params Room.RoomType[] types)
	{
		Room.RoomType[] validRoomTypes = types;
		int randomIndex = Random.Range(0, validRoomTypes.Length);
    
		return validRoomTypes[randomIndex];
	}

}
