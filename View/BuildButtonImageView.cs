using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonImageView : AppElement {

    private Image __image;
    public Image Image {get { return __image; }}

    public override void LastInAwake()
    {

        __image = GetComponent<Image>();
    }
}
