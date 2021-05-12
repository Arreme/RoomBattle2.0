using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    private Label playLabel;
    private Button playButton;

    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        playButton = rootVisualElement.Q<Button>("playButton");
        playButton.RegisterCallback<ClickEvent>(ev =>SceneManager.LoadScene("PickCharacter"));
    }
}
