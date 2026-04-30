[System.Serializable]

public class DialogueLine
{
    public string line;

    public string character;
    //public enum Character { Romeo, Juliet, Tibalt };
    public enum Expression { Neutral, Angry, Surprised, Happy};
    
    //public Character characterPortrait = new Character();
    public Expression characterExpression = new Expression();
}