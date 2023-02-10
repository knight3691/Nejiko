using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基本的にはScript上で動かすときはcomponentの値を操作したいので
//必ずそのcomponent型の変数をフィールドに用意しておく

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;//

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update() 
    //Update関数はPCのスペックによって1フレーム毎の描画する数が違うので
    //もしUpdateの変動時間じゃなくて一定の時間分を値を求めたい場合
    //Time.deltaTimeを使うと一定の値を出力してくれる
    {
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ,0,speedZ);//speedZで加速する限度

        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        moveDirection.y -= gravity * Time.deltaTime;
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);
        if(controller.isGrounded) moveDirection.y = 0;
        animator.SetBool("run",moveDirection.z);
        /*
        if (controller.isGrounded){//ここで挙動を示す値を決める
            if(Input.GetAxis("Vertical") > 0.0f)
            {
                moveDirection.z = Input.GetAxis("Vertical") * speedZ;
            }
            else
            {
                moveDirection.z = 0;
            }

            transform.Rotate(0,Input.GetAxis("Horizontal") * 3,0);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = speedJump;//1秒間に６０のフレームの中でjumpが押されたときにspeedjumpの値分動く処理
                animator.SetTrigger("Jump");
            }

            moveDirection.y -= gravity * Time.deltaTime;//1フレーム毎の時間を保持 

            //moveDirectionにさっきInputした値が入る
            Vector3 globalDirection = transform.TransformDirection(moveDirection);
            
            //TransformDirectionメソッドは動かしたいキャラクターの向きを基準にした座標を考慮してくれるメソッド
            controller.Move(globalDirection * Time.deltaTime);// 
            
            //controller.Move実際に動かす
            if (controller.isGrounded) moveDirection.y = 0;
            animator.SetBool("run",moveDirection.z > 0.0f);
        } 
        */     
    }
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }
    public void MoveToRight()
    {
    if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            animator.SetTrigger("jump");
        }
    }
}
