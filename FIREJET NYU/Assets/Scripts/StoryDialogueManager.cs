using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public string content;
    public Sprite characterSprite;   // 这句话时角色的立绘(可以换表情/换人)
}

public class StoryDialogueManager : MonoBehaviour
{
    public DialogueLine[] lines;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    public Image characterImage;
    public float typeSpeed = 0.04f;
    public string nextSceneName = "MainGame"; // 剧情播完后跳转的场景

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        ShowLine();
    }

    void Update()
    {
        // 点击鼠标左键或按空格推进
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            OnClickNext();
        }
    }

    void ShowLine()
    {
        DialogueLine line = lines[currentIndex];
        nameText.text = line.characterName;

        if (line.characterSprite != null)
            characterImage.sprite = line.characterSprite;

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(line.content));
    }

    IEnumerator TypeText(string content)
    {
        isTyping = true;
        contentText.text = "";
        foreach (char c in content)
        {
            contentText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    void OnClickNext()
    {
        // 如果还在打字,先把这句话瞬间显示完整
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            contentText.text = lines[currentIndex].content;
            isTyping = false;
            return;
        }

        currentIndex++;
        if (currentIndex >= lines.Length)
        {
            // 所有台词播完,跳转正式游戏场景
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
            return;
        }
        ShowLine();
    }
}
