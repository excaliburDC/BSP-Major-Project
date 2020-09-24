using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons;
    [SerializeField]
    private GameObject toolTip;
    [SerializeField]
    private GameObject Inventory;

    private Text toolTipText;
    // Start is called before the first frame update
    void Awake()
    {
        toolTipText = toolTip.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            InventoryScript.MyInstance.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Inventory.active == true)
            {
                Inventory.SetActive(false);
            }
            else
            {
                Inventory.SetActive(true);
            }

        }
    }

    public void UpdateStackSize(IClickable clickable)
    {
      
        if (clickable.MyCount>1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }

    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }
    public void SetUseable(ActionButton btn,IUseable useable)
    {
        btn.MyButton.image.sprite = useable.MyIcon;
        btn.MyButton.image.color = Color.white;
        btn.MyUseable =useable;
    }
    public void ShowToolTip(Vector3 position,IDescribable description)
    {
        toolTip.SetActive(true);
        toolTip.transform.position = position;
        toolTipText.text = description.GetDescription();
    }
    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }
}
