using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ShootThatUFO {

	public interface IJudgeEvents {
		void onHittingUFO ();

		void onNotHittingUFO ();

		int getTrial();

		void gameFailed();

		void updateHighscore();

		void setGameStatus (int gameStatus);

		int getPoint ();

		int getHighscore();

		int getRound ();
	}

	public interface IUserActions {

		//Game Status, 0 for init, 1 for in game, 2 for mid round, 3 for failed, 4 for game cleared
		int getGameStatus ();

		void gameStart();

		int getRound ();

		int getUFONum ();

		int getPoint ();

		int getTrial ();

		int getHighscore ();

		void resetGame ();
	}

	public class SceneController : System.Object, IJudgeEvents, IUserActions {

		//Global Variables
		public static float GROUND = 0f;

		//Init
		public Camera mainCam;

		SceneController () {
			mainCam = UnityEngine.Camera.main;
			mainCam.transform.position = new Vector3 (0f, 2.56f, -10f);
			mainCam.transform.eulerAngles = new Vector3 (6.475f, 0f, 0f);
			mainCam.fieldOfView = 76f;
			mainCam.backgroundColor = Color.black;

			Physics.gravity = new Vector3 (0, -9.8f, 0);

			GameObject lightObj = new GameObject ();
			lightObj.name = "light";
			Light light = lightObj.AddComponent<Light> ();
			light.type = LightType.Directional;

		}
		//Game Data
		public int gameStatus = 0;
		public float gameStartCountDown = 3f;
		public bool okToShoot = true;

		public int round = 0;
		public int point = 0;
		public int trials = 0;
		public int ufoToToss = 10;
		public int highscore = 0;

		public float[] intervalPerRound = {2f, 2f, 1f, 1f, 0.5f};
		public float[] tossRange = {10f, 10f, 15f, 20f, 20f};
		public float[] tossSpeed = {20f, 20f, 25f, 25f, 30f};
		public float[] ufoScale = {0.5f, 0.8f, 1f};
		public Color[] ufoColor = {Color.green, Color.yellow, Color.red};
		public float[] tossMaxAngle = {0.6f,0.6f,0.6f,0.6f,0.7f};

		public void updateHighscore (){
			highscore = point;
		}

		//UI Interface
		public int getGameStatus (){
			return gameStatus;
		}

		public void gameStart (){
			gameStatus = 1;
		}

		public int getRound (){
			return round;
		}

		public int getUFONum (){
			return ufoToToss;
		}

		public int getPoint (){
			return point;
		}

		public int getTrial (){
			return trials;
		}

		public void resetGame (){
			round = 0;
			point = 0;
			trials = 0;
			ufoToToss = 10;
			_base_code.gameModel.t = 0f;
		}

		public int getHighscore (){
			return highscore;
		}

		//Judge Interface
		public void onHittingUFO (){
			point += 100;
		}

		public void onNotHittingUFO (){
			trials++;
		}

		public void gameFailed (){
			gameStatus = 3;
		}

		public void setGameStatus (int _gameStatus){
			this.gameStatus = _gameStatus;
		}

		//Singleton
		private static SceneController _instance;
		private BaseCode _base_code;


		public static SceneController GetInstance() {
			if (_instance == null) {
				_instance = new SceneController ();
			}
			return _instance;
		}

		public BaseCode getBaseCode(){
			return _base_code;
		}

		internal void setBaseCode(BaseCode bc){
			if (_base_code == null) {
				_base_code = bc;
			}
		}

	}

	//End of Namespace
}