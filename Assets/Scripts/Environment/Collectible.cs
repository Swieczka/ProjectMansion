using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Collectible : MonoBehaviour
{
    [SerializeField] bool _is_Collected;
    public PlayerMovement player;

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
        player = GameObject.FindObjectOfType<PlayerMovement>();
        index = 0;
        _is_Collected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_is_Collected)
        {
            canInteract = true;
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
        text_area.text = line;
        yield return new WaitForSeconds(0.1f);
        canInteract = true;
    }
    private IEnumerator EndDialogue()
    {
        if (player != null)
        {
            player._MoveRes = true;
            player.CutScene = false;
        }
        message_background.gameObject.SetActive(false);
        collectible_Picture.gameObject.SetActive(false);
        text_area.gameObject.SetActive(false);
        gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator StartDialogue()
    {
        if (player != null)
        {
            player._MoveRes = false;
            player.CutScene = true;
        }
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
