using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCoinTrigger : MonoBehaviour
{
    [SerializeField] private string _coinType;
    public GameObject tooltipPrefab; // ������ ���������

    private GameObject tooltip; // ������ �� ������� ��������� ���������

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
                    if (tooltip == null)
                    {
                        tooltip = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
                        tooltip.transform.SetParent(transform); // ����������� ��������� � �������

                        // ������������� ��������� ���� �������
                        tooltip.transform.localPosition = new Vector3(0f, 0.5f, 0f);
                    }
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (tooltip != null)
            {
                Destroy(tooltip);
                tooltip = null;
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

                        if (tooltip != null)
                        {
                            Destroy(tooltip);
                            tooltip = null;
                        }
                    }
                    break;
                case "SmallCoin":
                    // ������
                    break;
            }
        }
    }
}
