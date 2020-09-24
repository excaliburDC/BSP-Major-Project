using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ActionButton : MonoBehaviour,IPointerClickHandler,IClickable
{
    public IUseable MyUseable { get; set; }
    [SerializeField]
    private Text stackSize;

    private Stack<IUseable> useables = new Stack<IUseable>();

    private int count;
    public Button MyButton { get; private set; }
   

    [SerializeField]
    private Image icon;

    public Image MyIcon { get => icon; set => icon = value; }

    public int MyCount
    {
        get
        {
            return count;
        }
    }
    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if(HandScript.MyInstance.MyMoveable ==null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            if(useables != null && useables.Count >0)
            {
                useables.Peek().Use();
            }
        }
        
    }
 

    public void SetUseable(IUseable useable,Item itemType)
    {
        if (useable is Item)
        {
            useables = InventoryScript.MyInstance.GetUseables(useable,itemType);
            count = useables.Count;
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
        }
        else
        {
            this.MyUseable = useable;
        }
        
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;
        if(count>1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUseable,HandScript.MyInstance.MyMoveable as Item);
            }
        }
    }
    public void UpdateItemCount(Item item)
    {
      if(item is IUseable && useables.Count >0)
      {
            Item TypeUse = useables.Peek() as Item;
            if (TypeUse.objType == item.objType)
            {
                useables = InventoryScript.MyInstance.GetUseables(item as IUseable, item);
                count = useables.Count;
                UIManager.MyInstance.UpdateStackSize(this);
            }

        }
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    //if(MyUseable != null)
    //    //  {
    //    //      UIManager.MyInstance.ShowToolTip(transform.position);
    //    //  }else 
    //    if (useables.Count>0)
    //    {
    //        UIManager.MyInstance.ShowToolTip(transform.position);
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //}
        
}
