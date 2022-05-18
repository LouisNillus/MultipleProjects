using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AccessRequest : MonoBehaviour
{
    public void RequestCard()
    {
        CardSelector.instance.RequestCardInfo(this.gameObject);
    }
}
