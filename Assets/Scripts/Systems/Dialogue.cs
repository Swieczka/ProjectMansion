using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public enum PersonSpeaking
    {
        player,
        wife,
        farmer
    }

    [System.Serializable]
    public class DialogueLine
    {
        public PersonSpeaking person;
        [TextArea]
        public string dialogue_text;
    }

    public List<DialogueLine> lines;
    public List<Sprite> personPictures;
    public Image speaker_picture;
    public TextMeshProUGUI text_area;

    public bool NextSceneAfter = false;
    public int dialogue_index;
    public int skip_index;
    public bool canInteract = true;
    public bool canSkip = true;
    public float showDelay;
    public PlayerMovement player;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        GetComponent<Image>().enabled = false;
        speaker_picture.gameObject.SetActive(false);
        text_area.gameObject.SetActive(false);
        text_area.text = "";
        dialogue_index = 0;
        canSkip = true;
        skip_index = 0;
        StartCoroutine(StartDialogue());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            canSkip = true;
            canInteract = false;
            
            UpdateDialogue();
            
        }
        else if(Input.GetKeyDown(KeyCode.E) && canSkip)
        {
            StopAllCoroutines();
            DisplayLinesQuick(lines[skip_index].dialogue_text);
            canSkip = false;
        }
    }

    void UpdateDialogue()
    {
        if (dialogue_index == lines.Count)
        {
            StartCoroutine(EndDialogue());
        }
        else
        {
            text_area.text = "";
            switch (lines[dialogue_index].person)
            {
                case PersonSpeaking.player:
                    speaker_picture.sprite = personPictures[0];
                    break;
                case PersonSpeaking.wife:
                    speaker_picture.sprite = personPictures[1];
                    break;
                case PersonSpeaking.farmer:
                    speaker_picture.sprite = personPictures[2];
                    break;
            }
            skip_index = dialogue_index;
            StartCoroutine(DisplayLine(lines[dialogue_index].dialogue_text));
            dialogue_index++;
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        for(int i = 0; i < line.Length; i++)
        {
            text_area.text+= line[i];
            yield return new WaitForSeconds(0.03f);
        }
        canInteract = true;
    }
    private void DisplayLinesQuick(string line)
    {
        text_area.text = line;
        canInteract = true;
    }
    private IEnumerator StartDialogue()
    {
        if(player !=null)
        {
            player._MoveRes = false;
            player.CutScene = true;
        }
        yield return new WaitForSeconds(showDelay);
        GetComponent<Image>().enabled = true;
        speaker_picture.gameObject.SetActive(true);
        text_area.gameObject.SetActive(true);
        UpdateDialogue();
    }

    private IEnumerator EndDialogue()
    {
        if (player != null)
        {
            player._MoveRes = true;
            player.CutScene = false;
        }
        GetComponent<Image>().enabled = false;
        speaker_picture.gameObject.SetActive(false);
        text_area.gameObject.SetActive(false);
        if (NextSceneAfter)
        {
            GameObject.Find("Curtain").GetComponent<Animator>().Play("Base Layer.FadeIn");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            yield return null;
        }
    }
}
