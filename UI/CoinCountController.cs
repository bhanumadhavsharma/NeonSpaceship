using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCountController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinCountText;

    // Start is called before the first frame update
    void Start()
    {
        coinCountText = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        coinCountText.text = PlayerStats.instance.coinCount.ToString();
    }
}
