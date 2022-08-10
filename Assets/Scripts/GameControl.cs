using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControl : MonoBehaviour
{
    #region Singleton
    public static GameControl instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    [HideInInspector] public bool isGame;
    [HideInInspector] public bool isEnd;
    [HideInInspector] public bool paintWin;

    [SerializeField] List<GameObject> levels;

    public int level;
    int ranking;

    [SerializeField] List<Transform> characters;
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI rankingText;

    [SerializeField] GameObject paintPercent;
    [SerializeField] GameObject rankingObject;
    [SerializeField] GameObject startPanel;

    
    [SerializeField] GameObject confetti;

    [SerializeField] ParticleSystem midConfetti1;
    [SerializeField] ParticleSystem rightConfetti1;
    [SerializeField] ParticleSystem leftConfetti1;
    [SerializeField] ParticleSystem midConfetti2;
    [SerializeField] ParticleSystem rightConfetti2;
    [SerializeField] ParticleSystem leftConfetti2;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        paintWin = false;
        isGame = false;
        isEnd = false;
        timer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd && level == 0)
        {
            paintPercent.SetActive(true);
        }
        if(paintWin)
        {
            if(timer > 0f)
            {
                midConfetti1.Play();
                leftConfetti1.Play();
                rightConfetti1.Play();
                timer -= Time.deltaTime;
            }
            else if(timer <= 0f)
            {
                Debug.Log(timer);
                level++;
                levels[level - 1].SetActive(false);
                levels[level].SetActive(true);
                paintPercent.SetActive(false);
                startPanel.SetActive(true);
                rankingObject.SetActive(true);
                isEnd = false;
                paintWin = false;
                timer = 2f;
            }
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            startPanel.SetActive(false);
        }
        if (isGame && level == 1)
        {
            CharacterSorting();
        }
        if(level == 1 && isEnd)
        {
            if (timer > 0f)
            {
                midConfetti2.Play();
                leftConfetti2.Play();
                rightConfetti2.Play();
                timer -= Time.deltaTime;
            }
            else if (timer <= 0f)
            {
                confetti.SetActive(false);
            }
        }
        
    }

    void CharacterSorting()
    {
        characters.Sort((p1, p2) => p1.position.z.CompareTo(p2.position.z));
        ranking = characters.Count - characters.IndexOf(player);
        rankingText.text = ranking.ToString() + " / " + characters.Count.ToString();
    }


}
