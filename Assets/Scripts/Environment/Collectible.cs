using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Collectible : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] bool _is_Collected;

    //message 
    bool canInteract = false;
    public Sprite collectible_Icon;
    
    int index;
    [TextArea]
    public List<string> message_Text;
    public Image collectible_Picture;
    public TextMeshProUGUI text_area;
    public Image message_background;

    void Start()
    {
        index = 0;
        _is_Collected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_is_Collected)
        {
            canInteract = true;
            _audioSource.Play();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            canInteract = false;
            
            if (index == 0)
            {
                StartCoroutine(StartDialogue());
            }
                UpdateMessage();
        }
    }
    void UpdateMessage()
    {
        if (index == message_Text.Count)
        {
            StartCoroutine(EndDialogue());
        }
        else
        {
            text_area.text = "";
            StartCoroutine(DisplayLine(message_Text[index]));
            index++;
        }
    }
    private IEnumerator DisplayLine(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            text_area.text += line[i];
            yield return new WaitForSeconds(0.03f);
        }
        canInteract = true;
    }
    private IEnumerator EndDialogue()
    {
        message_background.gameObject.SetActive(false);
        collectible_Picture.gameObject.SetActive(false);
        text_area.gameObject.SetActive(false);
        gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator StartDialogue()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        _is_Collected = true;
        yield return new WaitForSeconds(0.5f);
        message_background.gameObject.GetComponent<Image>().enabled = true;
        message_background.gameObject.SetActive(true);
        collectible_Picture.gameObject.SetActive(true);
        text_area.gameObject.SetActive(true);
        collectible_Picture.sprite = collectible_Icon;
    }
}
