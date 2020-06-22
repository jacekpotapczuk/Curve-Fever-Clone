using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreRankingController : MonoBehaviour
{
    public static ScoreRankingController Instance;

    [SerializeField]
    private GameObject entryContainer;

    [SerializeField]
    private GameObject entryTemplate;

    [SerializeField]
    private TextMeshProUGUI maxScore;

    private Dictionary<Player, ScoreEntry> entries;
    private float inActiveMultiplayer = 0.65f;
    private Vector2 anchorPosition;
    private float templateHeight = 38f;

    private void Awake ()
    {
        Instance = this;
        entries = new Dictionary<Player, ScoreEntry>();

        entryTemplate.gameObject.SetActive(false);

        int i = 0;
        float templateHeight = 33f;

        foreach(Player player in PlayerManager.Instance.players)
        {
            GameObject entry = Instantiate(entryTemplate, entryContainer.transform);
            ScoreEntry scoreEntry = new ScoreEntry(entry);

            if (anchorPosition == null)
            {
                float x = scoreEntry.rectTransform.anchoredPosition.x;
                float y = scoreEntry.rectTransform.anchoredPosition.y;
                anchorPosition = new Vector2(x, y);
            }
            scoreEntry.rectTransform.anchoredPosition = new Vector2(anchorPosition.x, anchorPosition.y - templateHeight * i);
            scoreEntry.gameObject.SetActive(true);

            scoreEntry.nick.text = player.nick;
            scoreEntry.nick.color = player.color;

            scoreEntry.score.text = player.score.ToString();
            scoreEntry.score.color = player.color;

            entries.Add(player, scoreEntry);

            i += 1;
        }
    }

    public void SetMaxScore(int score)
    {
        maxScore.text = score.ToString();
    }

    public void OnNewRound()
    {
        foreach(Player player in entries.Keys)
        {
            entries[player].nick.color = player.color;
            entries[player].score.color = player.color;
        }
    }

    public void OnPlayerDead(Player player)
    {
        PlayerManager.Instance.players.Sort(PlayerComparer);
        entries[player].nick.color = player.color * inActiveMultiplayer;
        entries[player].score.color = player.color * inActiveMultiplayer;

        int i = 0;
        foreach (Player p in PlayerManager.Instance.players)
        {

            entries[p].rectTransform.anchoredPosition = new Vector2(anchorPosition.x, anchorPosition.y - templateHeight * i);
            entries[p].score.text = p.score.ToString();
            i += 1;
        }
    }

    private int PlayerComparer(Player p1, Player p2)
    {
        // comparison is reversed. To get list from highest score to lowest
        if (p1.score > p2.score)
            return -1;
        else if (p2.score > p1.score)
            return 1;
        else
            return 0;
    }

    private class ScoreEntry
    {
        public GameObject gameObject;
        public RectTransform rectTransform;

        public TextMeshProUGUI nick;
        public TextMeshProUGUI score;

        public ScoreEntry(GameObject entry)
        {
            this.gameObject = entry;

            rectTransform = entry.GetComponent<RectTransform>();
            nick = entry.transform.Find("nick").GetComponent<TextMeshProUGUI>();
            score = entry.transform.Find("score").GetComponent<TextMeshProUGUI>();
        }
    }
}
