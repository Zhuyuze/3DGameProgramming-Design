using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.ShootThatUFO;

public class Judge : MonoBehaviour{

	IJudgeEvents iJudege;

	void Start (){
		iJudege = SceneController.GetInstance() as IJudgeEvents;
	}

	void Update (){

		if (iJudege.getPoint() > iJudege.getHighscore()) {
			iJudege.updateHighscore();
		}

		if (iJudege.getRound() == 5) {
			iJudege.setGameStatus(4);
		}

		if (iJudege.getTrial() > 49) {
			iJudege.gameFailed();
		}
	}

}