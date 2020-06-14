using System;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour
{
    private TMP_InputField nameInputField;

    private TMP_Dropdown dropdownField;

    private bool isActive;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }
    private void Awake()
    {
        nameInputField = GetComponentInChildren<TMP_InputField>();
        dropdownField = GetComponentInChildren<TMP_Dropdown>();
        SetActive(GetComponentInChildren<Toggle>().isOn);
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        UpdateFontTransparency();
    }

    public void UpdateFontTransparency()
    {
        if (isActive)
            nameInputField.textComponent.alpha = 1f;
        else
            nameInputField.textComponent.alpha = 0.5f;

    }

    public void ChangeText(string text, Color color)
    {
        if (text.Length > nameInputField.characterLimit)
        {
            Debug.LogError("Can't change text. Text is too long. Character limit is set to " + nameInputField.characterLimit);
            return;
        }
        nameInputField.text = text;
    }

    public Color GetColor()
    {
        switch (dropdownField.value)
        {
            case 0: return new Color(41, 255, 251);
            case 1: return new Color(134, 255, 105);
            case 2: return new Color(248, 255, 102);
            case 3: return new Color(255, 99, 114);
            case 4: return new Color(255, 94, 247);
            default: return Color.white;
        }
    }

    public string GetNick()
    {
        return nameInputField.text;
    }
}
