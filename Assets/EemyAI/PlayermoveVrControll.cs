using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayermoveVrControll : MonoBehaviour
{
    [SerializeField]
    //private Rigidbody myRigid;//��ü�� �� //Start�� ������  
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
        //�̷��� �Լ� �ȿ��� ���� ������ ������ �Լ� ȣ���� ������ �ı��Ǿ� �������
        float _moveDirX = Input.GetAxisRaw("Horizontal");//1��-1�� �ȴ����� 0���ϵǸ鼭 _moveDirX�� ���Եȴ�. Horizontal�� ����Ƽ���� ���
        //11.10 _moveDirX -> mov_rotate_y�� ��ü
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        //transform.Rotate(0, Time.deltaTime * mov_rotate_y * HeadturnSpeed, 0); //11.10 ���� Vector3 _moveHorizontal ���� 
        Vector3 _movehorizontal = transform.forward * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;//(0,0,1) * 1
        //���� �ϳ��� ��ĥ���̴�
        //(1,0,0) (0,0,1) = (1,0,1) = 2 =>normalized; �ϸ� (0.5, 0, 0.5) = 1 (�ﰢ�Լ� �밢���������� ����),(1�� �������� ����ȭ):����� ���ϰ� ����Ƽ ����
        Vector3 _velocity = (_moveVertical).normalized * applySpeed;//������ ���°Ϳ� �ӵ����� ������ (applySpeed�� walkSpeed�� �־ �������̴�
        //11.10 _moveHorizontal �����ذ� ����(ȸ������ ��ü)
        //��ü���ٰ� �����̰� �����               //�����̵��Ǵ°���*�뷫 0.016
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

        myRigid.trans
    }
*/
    private void Vrmove()
    {
        //�̷��� �Լ� �ȿ��� ���� ������ ������ �Լ� ȣ���� ������ �ı��Ǿ� �������
        float _movehori = Input.GetAxisRaw("Horizontal");//1��-1�� �ȴ����� 0���ϵǸ鼭 _moveDirX�� ���Եȴ�. Horizontal�� ����Ƽ���� ���
        //11.10 _moveDirX -> mov_rotate_y�� ��ü
        float _moveverti = Input.GetAxisRaw("Vertical");
        mytran.position = mytran.position + new Vector3(_movehori * applySpeed * Time.deltaTime, 0, _moveverti * applySpeed * Time.deltaTime);
    }

}
