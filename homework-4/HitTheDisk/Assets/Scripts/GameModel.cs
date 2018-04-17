
public class GameModel : IScore {

    public int Score { get; private set; }
    public int Round { get; private set; }
    private static GameModel gameModel;

    private GameModel() {
        Round = 0;
    }

    public static GameModel GetGameModel() {
        return gameModel ?? (gameModel = new GameModel());
    }

    public void AddScore() {
		if(Round%3==1)
        	Score += 10;
		else Score += 20;
        if (CheckUpdate()) {
            Round++;
            FirstSceneControllerBase.GetFirstSceneControllerBase().Update();
        }
    }

	public void AddScoreBonus() {
		
		if (CheckUpdate()) {
			Round++;
			FirstSceneControllerBase.GetFirstSceneControllerBase().Update();
		}
	}

    public void SubScore() {
        Score -= 10;
        if (Score < 0) {
            FirstSceneControllerBase.GetFirstSceneControllerBase().SetGameStatus(GameStatus.Lose);
        }
    }

    public bool CheckUpdate() {
        return Score >= Round * 20;
    }

    public int GetScore() {
        return GameModel.GetGameModel().Score;
    }
}