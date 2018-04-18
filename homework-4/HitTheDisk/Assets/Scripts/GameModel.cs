
public class GameModel : IScore {

    public int Score { get; private set; }
    public int Round { get; private set; }
	public int count;
    private static GameModel gameModel;

    private GameModel() {
        Round = 1;
		count = 0;
    }

    public static GameModel GetGameModel() {
        return gameModel ?? (gameModel = new GameModel());
    }

	public void Count()
	{
		count++;
	}

    public void AddScore() {
		if(count  == 2|| count  == 5)
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