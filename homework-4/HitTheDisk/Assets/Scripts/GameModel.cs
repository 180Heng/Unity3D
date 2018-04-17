
public class GameModel : IScore {

    public int Score { get; private set; }
    public int Round { get; private set; }
    private static GameModel gameModel;

    private GameModel() {
        Round = 1;
    }

    public static GameModel GetGameModel() {
        return gameModel ?? (gameModel = new GameModel());
    }

    public void AddScore() {
		if(Round  == 2|| Round  == 5)
        	Score += 20;
		else Score += 10;
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
    }

    public bool CheckUpdate() {
        return Score >= Round * 20;
    }

    public int GetScore() {
        return GameModel.GetGameModel().Score;
    }
}