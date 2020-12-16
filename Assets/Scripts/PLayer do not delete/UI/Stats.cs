using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    [SerializeField]
    private Text statValue;

    [SerializeField]
    private float lerpSpeed;

    private Image content;

    private float currentfill;
    public float MyMaxValue { get; set; }

    private float currentValue;
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if(value >MyMaxValue)
            {
                currentValue = MyMaxValue;
            }else if(value<0)
            {
                currentValue = 0;
            }else
            {
                currentValue = value;
            }
          
            currentfill = currentValue / MyMaxValue;

            if (statValue != null) 
            {
                statValue.text = currentValue + " / " + MyMaxValue;
            }
            
        }
        
    }
   
    

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentfill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentfill, Time.deltaTime*lerpSpeed);
        }
     }
    public void Initialize(float currentValue,float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue; 
    }
    
}
