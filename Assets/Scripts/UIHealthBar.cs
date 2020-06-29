using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
 

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    Image mask;
    float originalSize;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<Image>();
        originalSize = mask.rectTransform.rect.width; //get the originalsize of the mask
    }

    public void SetValue(float value)
    {
        // sets th size of the health bargetting from reference the horizontal anchor
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
