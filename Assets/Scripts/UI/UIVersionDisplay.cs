using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVersionDisplay : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "v" + Application.version;
    }
}
