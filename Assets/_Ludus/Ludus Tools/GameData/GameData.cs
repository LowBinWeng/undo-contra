using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData  {

	public string 	charName;
	public int 		level;
	public float 	speed;
	public Vector3 	position;

	public GameData( string _name, int _level, float _speed, Vector3 _position ) {
		charName = _name;
		level = _level;
		speed = _speed;
		position = _position;
	}

}
