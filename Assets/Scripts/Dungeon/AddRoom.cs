using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AddRoom : MonoBehaviour {
	void Start(){
		RoomTemplates.instance.rooms.Add(this.gameObject);
	}
}
