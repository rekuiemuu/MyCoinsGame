using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCoinTrigger : MonoBehaviour
{
    [SerializeField] private string _coinType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            switch (_coinType)
            {
                case "SmallCoin":
                    ScoreManager.Instance.addScore(1);
                    Destroy(gameObject);
                    break;
                case "BigCoin":
                    // Ничего
                    break;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            switch (_coinType)
            {
                case "BigCoin":
                    if (Input.GetKey(KeyCode.E))
                    {
                        ScoreManager.Instance.addScore(2);
                        Destroy(gameObject);
                    }
                    break;
                case "SmallCoin":
                    // Ничего
                    break;
            }
        }
    }
}
