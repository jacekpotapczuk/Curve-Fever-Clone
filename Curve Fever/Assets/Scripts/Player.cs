using UnityEngine;

public class Player
{
    public string nick;
    public Color color;
    public int score;
    public string inputName;
    public PlayerBody body;

   
    public Player(string nick, Color color, string inputName, int score = 0)
    {
        this.nick = nick;
        this.color = color;
        this.inputName = inputName;
        this.score = score;
    }

}
