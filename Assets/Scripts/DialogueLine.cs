[System.Serializable]

public class DialogueLine
{
    public string line;
    public enum Character { Player, Fish1, Fish2, Fish3 };
    public enum Expression { Neutral, Angry, Surprised, Happy};
    
    public Character characterPortrait = new Character();
    public Expression characterExpression = new Expression();
}