using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float turnSpeed = 4.0f;
    float moveSpeed = 2.0f;

    float xRotate = 0.0f;
    float yRotate = 0.0f;

    void Update() {
        if(!Input.GetKey(KeyCode.Space))//임시로 테스트하기 위해 화면 정지하는 코드
        {
            MouseRotation();
        }   
    }
    
    // 마우스의 움직임에 따라 카메라를 회전 시킨다.
    void MouseRotation()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        //float yRotate = transform.eulerAngles.y + yRotateSize;
        yRotate = Mathf.Clamp(yRotateSize + yRotate, 120, 240);

        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
        // Clamp 는 값의 범위를 제한하는 함수
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 45);
    
        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }
}
