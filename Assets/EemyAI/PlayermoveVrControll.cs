using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayermoveVrControll : MonoBehaviour
{
    [SerializeField]
    //private Rigidbody myRigid;//육체적 몸 //Start에 넣을것  
    public float applySpeed;
    private Transform mytran;

    // Start is called before the first frame update
    void Start()
    {
        mytran = this.transform;
        //myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vrmove();
    }

    /*private void Vrmove()
    {
        //이렇게 함수 안에서 선언 생성된 변수는 함수 호출이 끝나면 파괴되어 사라진다
        float _moveDirX = Input.GetAxisRaw("Horizontal");//1과-1이 안누르면 0리턴되면서 _moveDirX에 들어가게된다. Horizontal는 유니티에서 명시
        //11.10 _moveDirX -> mov_rotate_y로 교체
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        //transform.Rotate(0, Time.deltaTime * mov_rotate_y * HeadturnSpeed, 0); //11.10 원래 Vector3 _moveHorizontal 변수 
        Vector3 _movehorizontal = transform.forward * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;//(0,0,1) * 1
        //이제 하나로 합칠것이다
        //(1,0,0) (0,0,1) = (1,0,1) = 2 =>normalized; 하면 (0.5, 0, 0.5) = 1 (삼각함수 대각선방향으로 나감),(1이 나오도록 정규화):계산이 편리하고 유니티 권장
        Vector3 _velocity = (_moveVertical).normalized * applySpeed;//방향이 나온것에 속도까지 곱해줌 (applySpeed에 walkSpeed가 있어서 걸을것이다
        //11.10 _moveHorizontal 더해준거 없앰(회전으로 교체)
        //강체에다가 움직이게 만들것               //순간이동되는것을*대략 0.016
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

        myRigid.trans
    }
*/
    private void Vrmove()
    {
        //이렇게 함수 안에서 선언 생성된 변수는 함수 호출이 끝나면 파괴되어 사라진다
        float _movehori = Input.GetAxisRaw("Horizontal");//1과-1이 안누르면 0리턴되면서 _moveDirX에 들어가게된다. Horizontal는 유니티에서 명시
        //11.10 _moveDirX -> mov_rotate_y로 교체
        float _moveverti = Input.GetAxisRaw("Vertical");
        mytran.position = mytran.position + new Vector3(_movehori * applySpeed * Time.deltaTime, 0, _moveverti * applySpeed * Time.deltaTime);
    }

}
