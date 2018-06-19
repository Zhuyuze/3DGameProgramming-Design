using UnityEngine;
using System.Collections;
using Com.MyGame;
using System.Collections.Generic;

public class gengameobjects : MonoBehaviour {
	public List<GameObject> Priests, Devils;
	public GameObject boat, bankLeft, bankRight;

	private boatbehaviour myBoatBehaviour;

	void Start () {
		Priests = new List<GameObject>();
		for (int i = 0; i < 3; i++) {
			GameObject priests = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			priests.name = "Priest " + (i + 1);
			priests.tag = "Priest";
			priests.GetComponent<MeshRenderer>().material.color = Color.white;
			priests.AddComponent<personstate>();
			Priests.Add(priests);
		}
		Priests[0].transform.position = LOCATION_SET.priests_1_LOC;
		Priests[1].transform.position = LOCATION_SET.priests_2_LOC;
		Priests[2].transform.position = LOCATION_SET.priests_3_LOC;

		Devils = new List<GameObject>();
		for (int i = 0; i < 3; i++) {
			GameObject devils = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			devils.name = "Devil " + (i + 1);
			devils.tag = "Devil";
			devils.GetComponent<MeshRenderer>().material.color = Color.red;
			devils.AddComponent<personstate>();
			Devils.Add(devils);
		}
		Devils[0].transform.position = LOCATION_SET.devils_1_LOC;
		Devils[1].transform.position = LOCATION_SET.devils_2_LOC;
		Devils[2].transform.position = LOCATION_SET.devils_3_LOC;

		boat = GameObject.CreatePrimitive(PrimitiveType.Cube);
		boat.name = "Boat";
		boat.AddComponent<boatbehaviour>();
		myBoatBehaviour = boat.GetComponent<boatbehaviour>();
		boat.GetComponent<MeshRenderer>().material.color = Color.blue;
		boat.transform.localScale = new Vector3(3, 1, 1);
		boat.transform.position = LOCATION_SET.boat_right_LOC;

		bankLeft = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		bankLeft.name = "BankLeft";
		bankLeft.transform.Rotate(new Vector3(0, 0, 90));
		bankLeft.transform.localScale = new Vector3(1, 4, 1);
		bankLeft.transform.position = LOCATION_SET.bank_left_LOC;

		bankRight = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		bankRight.name = "BankRight";
		bankRight.transform.Rotate(new Vector3(0, 0, 90));
		bankRight.transform.localScale = new Vector3(1, 4, 1);
		bankRight.transform.position = LOCATION_SET.bank_right_LOC;

		mainSceneController.getInstance().setGenGameObjects(this);
	}

	void Update () {

	}

	public void boatMove() {
		myBoatBehaviour.setBoatMove();
	}

	//牧师上船
	public void priestsGetOn() {
		if (myBoatBehaviour.isMoving)
			return;

		if (!myBoatBehaviour.isBoatAtLeftSide()) {  //船在右侧
			for (int i = 0; i < Priests.Count; i++) {
				if (Priests[i].GetComponent<personstate>().onBankRight) {
					//右侧岸上有牧师
					detectEmptySeat(true, i, DIRECTION.Right);
					break;
				}
			}
		}
		else {  //船在左侧
			for (int i = 0; i < Priests.Count; i++) {
				if (Priests[i].GetComponent<personstate>().onBankLeft) {
					//左侧岸上有牧师
					detectEmptySeat(true, i, DIRECTION.Left);
					break;
				}
			}
		}
	}
	//恶魔上船
	public void devilsGetOn() {
		if (myBoatBehaviour.isMoving)
			return;

		if (!myBoatBehaviour.isBoatAtLeftSide()) {  //船在右侧
			for (int i = 0; i < Devils.Count; i++) {
				if (Devils[i].GetComponent<personstate>().onBankRight) {
					//右侧岸上有恶魔
					detectEmptySeat(false, i, DIRECTION.Right);
					break;
				}
			}
		}
		else {  //船在左侧
			for (int i = 0; i < Devils.Count; i++) {
				if (Devils[i].GetComponent<personstate>().onBankLeft) {
					//左侧岸上有恶魔
					detectEmptySeat(false, i, DIRECTION.Left);
					break;
				}
			}
		}
	}
	//当岸上有牧师/恶魔的时候，检测船上是否有空位
	void detectEmptySeat(bool isPriests, int index, bool boatDir) {
		if (myBoatBehaviour.isLeftSeatEmpty()) {        //船上左位置没人
			seatThePersonAndModifyBoat(isPriests, index, boatDir, DIRECTION.Left);
		}
		else if (myBoatBehaviour.isRightSeatEmpty()) {  //船上左位置有人，右位置没人
			seatThePersonAndModifyBoat(isPriests, index, boatDir, DIRECTION.Right);
		}
	}
	//牧师/恶魔上船，并调整船的属性
	void seatThePersonAndModifyBoat(bool isPriests, int index, bool boatDir, bool seatDir) {
		if (isPriests) {
			Priests[index].GetComponent<personstate>().personSeatOnBoat(boatDir, seatDir);
			Priests[index].transform.parent = boat.transform;
		}
		else {
			Devils[index].GetComponent<personstate>().personSeatOnBoat(boatDir, seatDir);
			Devils[index].transform.parent = boat.transform;
		}
		myBoatBehaviour.seatOnPos(seatDir);
	}


	//牧师下船
	public void priestsGetOff() {
		if (myBoatBehaviour.isMoving)
			return;

		if (!myBoatBehaviour.isBoatAtLeftSide()) {  //船在右侧
			for (int i = Priests.Count - 1; i >= 0; i--) {
				if (detectIfPeopleOnBoat(true, i, DIRECTION.Right))
					break;
			}
		}
		else {  //船在左侧
			for (int i = Priests.Count - 1; i >= 0; i--) {
				if (detectIfPeopleOnBoat(true, i, DIRECTION.Left))
					break;
			}
		}
	}
	//恶魔下船
	public void devilsGetOff() {
		if (myBoatBehaviour.isMoving)
			return;

		if (!myBoatBehaviour.isBoatAtLeftSide()) {  //船在右侧
			for (int i = Devils.Count - 1; i >= 0; i--) {
				if (detectIfPeopleOnBoat(false, i, DIRECTION.Right))
					break;
			}
		}
		else {  //船在左侧
			for (int i = Devils.Count - 1; i >= 0; i--) {
				if (detectIfPeopleOnBoat(false, i, DIRECTION.Left))
					break;
			}
		}
	}
	//检测是否有牧师/恶魔在船上
	bool detectIfPeopleOnBoat(bool isPriests, int i, bool boatDir) {
		if (isPriests) {
			if (Priests[i].GetComponent<personstate>().onBoatLeft
				|| Priests[i].GetComponent<personstate>().onBoatRight) {
				//在船上
				myBoatBehaviour.jumpOutOfPos(Priests[i].GetComponent<personstate>().onBoatLeft);
				Priests[i].GetComponent<personstate>().landTheBank(boatDir);
				Priests[i].transform.parent = boat.transform.parent;

				return true;
			}
			return false;
		}
		else {
			if (Devils[i].GetComponent<personstate>().onBoatLeft
				|| Devils[i].GetComponent<personstate>().onBoatRight) {
				//在船上
				myBoatBehaviour.jumpOutOfPos(Devils[i].GetComponent<personstate>().onBoatLeft);
				Devils[i].GetComponent<personstate>().landTheBank(boatDir);
				Devils[i].transform.parent = boat.transform.parent;

				return true;
			}
			return false;
		}
	}

}