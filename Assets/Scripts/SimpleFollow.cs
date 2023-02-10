using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;

    public GameObject target;
    public float followSpeed;

    void Start() {
        diff = target.transform.position - transform.position;
    }

    void LateUpdate() {
        transform.position = Vector3.Lerp(
            transform.position,//第一引数の値
            target.transform.position -diff,//第二引数の値
            Time.deltaTime * followSpeed //１と２の
        );
    }
}
//Updateの中身説明
/*
Lerpとは
線形補完
A地点からB地点に移動する場合

*/