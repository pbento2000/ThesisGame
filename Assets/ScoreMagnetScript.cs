using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMagnetScript : MonoBehaviour
{

    [SerializeField] RectTransform posicao;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = posicao.TransformPoint(posicao.rect.center);
    }
}
