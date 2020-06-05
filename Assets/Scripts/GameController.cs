using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private List<string> words;
    private string alphabet; // the alphabet caught in the game
    private string currentWord; // the current word displaying in the game
    private List<string> currentWordList;
    private string fileName = "data.json"; //json file where data is loaded from

    private int atWord;
    public static int score = 0;
    private int index;
    
    public Text wordTextDisplay; // to dislpay the current word

    public GameObject explosionPrefab;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        words = new List<string>();

        LoadGameData();

        currentWordList = new List<string>();

        index = -1;
        atWord = 0;
        score = 0;

        SetNextWord();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if left mouse button is clicked
        {
           Check();
        }
    }

    void Check(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // throw ray from screen at mouse position, into scene
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            alphabet = hit.transform.name;
            
            if(!alphabet.Equals("floor")){ // destroy any object that was hit, except floor. Don't wanna destroy the floor
                Destroy(hit.transform.gameObject);

                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, hit.transform.position, hit.transform.rotation);
                }
            }

            alphabet = alphabet.ToLower();
            index = currentWordList.IndexOf(alphabet[0].ToString());

            if(index != -1){ // check if alphabet we clicked exists in current word
                currentWordList.RemoveAt(index); //its been clicked, dont need it no more
                
                score+=1; // add 1 score for each right alphabet clicked

                if(currentWordList.Count == 0){ // calls for next word
                    atWord++;
                    score+=10; // add 10 score for each whole word
                
                    if(atWord == words.Count){
                    RoundOver();
                    }

                    SetNextWord(); // if words array have not been completed. Continue with next word
                }
            }
            else{
                RoundOver();
            }
        }
    }

    void SetNextWord(){
        atWord = Random.Range(0, words.Count);

        wordTextDisplay.text = words[atWord];

        currentWordList.Clear(); //empty the list first

        for(int i =0; i < words[atWord].Length; i++){ //populate list with alphabets in the word
            currentWordList.Add(words[atWord][i].ToString());
        }

        words.RemoveAt(atWord);
    }

    void RoundOver(){
        SceneManager.LoadScene("RoundOverScene");
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(0);
    }

    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonData = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);

            // Retrieve the allRoundData property of loadedData
            words = loadedData.words;
        }
        else
        {
            Debug.LogError("Could'nt load data!");
        }
    }
}