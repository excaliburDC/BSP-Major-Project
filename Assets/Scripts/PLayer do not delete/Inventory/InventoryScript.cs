using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);
public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;
    private static InventoryScript instance;
    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton [] bagButtons;

    //for debugging
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get
        { return bags.Count < 4; }
    }
    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach(Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }
            return count;
        }
    }
    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;
            foreach(Bag bag in bags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }
            return count;
        }
    }
    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }
    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }
        set
        {
            fromSlot = value;
            if(value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initalize(16);
        bag.Use();
        bag.MyBagScript.OpenClose();
    }
    private void Update()
    {
        //add new bag
        if(Input.GetKeyDown(KeyCode.H))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initalize(8);
            bag.Use();
        }
        //add new 16 slot bag
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initalize(16);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            HealthPortion portion = (HealthPortion)Instantiate(items[1]);
            AddItem(portion);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HealthPortion portion = (HealthPortion)Instantiate(items[2]);
            AddItem(portion);
        }
    }
   //public  void AddBagIntoInventory(int size)
   // {
   //     Bag bag = (Bag)Instantiate(items[0]);
   //     bag.Initalize(size);
   //     AddItem(bag);
   // }
    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                bag.MyBagButton = bagButton;
                break;
            }
        }
    }
    public void AddBag(Bag bag,BagButton bagButton)
    {
        bags.Add(bag);
        bagButton.MyBag = bag;

    }
    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }
    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCount = (MyTotalSlotCount - oldBag.Slots) + newBag.Slots;
        if(newSlotCount-MyFullSlotCount >=0)
        {
            //do swap
            List<Item> bagItems = oldBag.MyBagScript.GetItem();

            RemoveBag(oldBag);
            newBag.MyBagButton = oldBag.MyBagButton;
            newBag.Use();
            foreach(Item item in bagItems)
            {
                if(item != newBag)
                {
                    AddItem(item);
                }
            }

            AddItem(oldBag);
            HandScript.MyInstance.Drop();
            MyInstance.fromSlot = null;
        }
    }
    public void AddItem(Item item)
    {
      if(item.MyStackSize>0)
      {
          if(PlaceInStack(item))
          {
              return;
          }
      }
        PlaceInEmpty(item);
    }
    private void PlaceInEmpty(Item item)
    {
        foreach(Bag bag in bags)
        {
            if(bag.MyBagScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return;
            }
        }
    }
    private bool PlaceInStack(Item item)
    {
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slots in bag.MyBagScript.MySlots)
            {
                if(slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;
    }
    public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);
        //if closed bag == true then opne th all closed bags

        foreach(Bag bag in bags)
        {
            if(bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose(); 
            }
        }
    }

    public Stack<IUseable> GetUseables(IUseable type,Item obj)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach(Bag bag in bags)
        {
            foreach(SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty/* && slot.MyItem.GetType() == type.GetType()*/ && slot.MyItem.objType == obj.objType)
                {
                    foreach(Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                        
                    }
                }
            }
        }
        //Debug.Log(type.GetType());
        return useables;
     }

    public void OnItemCountChanged(Item item)
    {
        if(itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);

        }
    }
}

  
