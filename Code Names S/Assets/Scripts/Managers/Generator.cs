using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using System.Text.RegularExpressions;
using System.Text;

public class Generator : MonoBehaviour
{

    public TextAsset wordsData;
    public bool lineByLine;
    [HideIf("lineByLine")]
    public string startsWith, endsWith;

    public List<ColorContainer> colors = new List<ColorContainer>();

    public int gridSize;

    public List<Card> words = new List<Card>();
    public List<string> allWords = new List<string>();
    List<string> forbiddenWords = new List<string>();

    public GameObject cardTemplate;
    public GameObject gridLayout;

    [Range(1,10)]
    public int doublesSecurityForce;
    public static Generator instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateColorContainers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) GetWordsFromData(lineByLine, startsWith, endsWith);
        //if (Input.GetKeyDown(KeyCode.H)) GenerateGrid();
    }

    public void StartGame()
    {
        GetWordsFromData(lineByLine, startsWith, endsWith);
    }


    public void GenerateGrid()
    {
        List<Card> wordsTemp = new List<Card>();

        for (int i = 0; i < words.Count; i++)
        {
            wordsTemp.Add(words[i]);
        }

        for (int i = 0; i < GridCount(); i++)
        {
            GameObject go = Instantiate(cardTemplate, gridLayout.transform);
            CardHolder holder = go.GetComponent<CardHolder>();

            holder.card = words[i];
            holder.card.holder = holder;
            holder.SetText();
            wordsTemp.Remove(holder.card);
        }

        EncodeGrid();
    }

    public void GetWordsFromData(bool lineByLine = false, string startsWith = "", string endsWith = "")
    {
        int count = GridCount();

        string[] result = new string[count];

        if (lineByLine)
        {
            string[] allTextAssetLines =  wordsData.text.Split('\n');

            for (int i = 0; i < count; i++)
            {
                result[i] = allTextAssetLines[Random.Range(0, allTextAssetLines.Length)];
            }

            for (int i = 0; i < allTextAssetLines.Length; i++)
            {
                allWords.Add(allTextAssetLines[i]);
            }
        }
        else
        {
            
            string finalReg = "\\" + startsWith + "\\b\\S+?\\b" + "\\" + endsWith;
            Regex regex = new Regex(finalReg);
      
            MatchCollection matches = regex.Matches(wordsData.text);

            if (matches.Count == 0)
            {
                Debug.LogError("No match found");
                return;
            }
            if (matches.Count < count)
            {
                Debug.LogError("Not enough words found");
                return;
            }

            for (int i = 0; i < matches.Count; i++)
            {
                allWords.Add(matches[i].Value);
            }

            for (int i = 0; i < count; i++)
            {
                result[i] = matches[Random.Range(0, matches.Count)].Value;
            }
        }
        
        RandomizeColors();

        StartCoroutine(DoubleWordsPrevention(result));
    }

    public string GetRandomWord()
    {
        return allWords[Random.Range(0, allWords.Count)];
    }



    public List<CardColor> randomColors = new List<CardColor>();
    public void RandomizeColors() //Faire un avertissement si la répartition des couleurs n'est pas égales au nombre de cases ou alors on met en jaune par défaut.
    {

        foreach (ColorContainer cc in colors)
        {
            for (int i = 0; i < cc.count; i++)
            {
                randomColors.Add(cc.cardColor);
            }
        }

        for (int i = randomColors.Count; i < GridCount(); i++)
        {
            randomColors.Add(CardColor.Yellow);
        }

        randomColors = randomColors.OrderBy(x => Random.value).ToList();
    }

    public CardColor GetCardColor(int index)
    {
        return randomColors[index];
    }

    public void RerollWord(Card card)
    {
        forbiddenWords.Add(card.wordName);

        string newWord = GetRandomWord();
        card.wordName = newWord;

        if (newWord == "") RerollWord(card);
    }

    public void SetGridSize(Slider slider)
    {
        gridSize = (int)slider.value;
    }

    string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public int GridCount()
    {
        return (int)Mathf.Pow(gridSize, 2);
    }

    public bool SimilarWordsSecurity(string word, int force)
    {
        foreach(Card c in words)
        {
            if (c.wordName == word || c.wordName.Contains(word.Substring(0, word.Length < force ? word.Length - 1 : force)))
            {
                Debug.Log(word + " == " + c.wordName);
                return true;
            }
        }

        return false;
    }

    public IEnumerator DoubleWordsPrevention(string[] result)
    {
        for (int i = 0; i < GridCount(); i++)
        {

            string s = "";

            if (startsWith != "" || endsWith != "") s = UppercaseFirst(result[i].Replace(startsWith, "").Replace(endsWith, ""));
            else s = UppercaseFirst(result[i]);

            while (SimilarWordsSecurity(s, doublesSecurityForce))
            {
                if (startsWith != "" || endsWith != "") s = UppercaseFirst(GetRandomWord().Replace(startsWith, "").Replace(endsWith, ""));
                else s = UppercaseFirst(GetRandomWord());

                yield return null;
            }

            words.Add(new Card(s, GetCardColor(i)));
        }

        GenerateGrid();
    }

    public void UpdateColorContainers()
    {
        foreach (ColorContainer cc in colors)
        {
            cc.SetValue();
        }
    }

    public void SetSecurityForceFromSlider(Slider slider)
    {
        doublesSecurityForce = (int)slider.value;
    }

    public GridEncoded ge;
    public string encodedGrid;
    public void EncodeGrid()
    {
        ge = new GridEncoded(GridCount(), Network.GetLocalIPAddress(), System.DateTime.Now.ToString());

        ge.colors = new int[GridCount()];

        int i = 0;
        foreach(CardColor cc in randomColors)
        {
            ge.colors[i] = (int)cc;
            i++;
        }

        encodedGrid = JsonUtility.ToJson(ge);
    }
}

[System.Serializable]
public class GridEncoded
{
    public string ip = "";
    public string timeCode = "";
    public int gridCount = 0;
    public int[] colors;

    public GridEncoded(int gridCount, string ip, string timeCode)
    {
        this.gridCount = gridCount;
        this.ip = ip;
        this.timeCode = timeCode;
    }
}