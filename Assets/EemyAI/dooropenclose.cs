using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropenclose : MonoBehaviour
{
    private float defaluatrota;//�ʱ� ������ ��
    private float nowrota;//���� �� ���� ��.
    private float closerota;//�ʱ� ������ ��
    // Start is called before the first frame update
    public bool statetest = false; 

    enum doorstate { open, close }

    doorstate _doorstate = doorstate.open;

    void Start()
    {
        defaluatrota = this.transform.rotation.y;
        nowrota = this.transform.rotation.y;
        closerota = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_doorstate == doorstate.open) {
            
            if (defaluatrota >= 0 && 0<= nowrota && nowrota <= defaluatrota) //����� 0<= now <= default
            {
               
            }
            else if(defaluatrota < 0 && defaluatrota <= nowrota && nowrota <= 0) {// ������ de <= now <=0
                
            }
                
        }
        if (_doorstate == doorstate.close) {
            if (defaluatrota >= 0 && 0 <= nowrota && nowrota <= defaluatrota) {
                
                
            }
            else if(defaluatrota < 0 && defaluatrota <= nowrota && nowrota <= 0) {
                
                

            }

        }

        //test code
        if (statetest == true) _doorstate = doorstate.open;
        else _doorstate = doorstate.close;
        //testcode ��

        //this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, nowrota, this.transform.rotation.z);
    }

    /*IEnumerator closedoorplus()
    {
        float callnow = nowrota;//ȣ���� now��
        //����϶� �������� (+) -> 0���� ���ߵ�.
        for (float f = defaluatrota- callnow; f >= 0; f -= 0.1f)
         {
                nowrota += f;
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, nowrota, this.transform.rotation.z);
                yield return null;
         }    
    }

    IEnumerator closedoorminus()
    {
        float callnow = nowrota;//ȣ���� now��
        //�����϶� �������� (-) -> 0���� ���ߵ�.
        for (float f = defaluatrota- callnow; f <= 0; f += 0.1f)
        {
             nowrota += f;
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, nowrota, this.transform.rotation.z);
            yield return null;
        }
    }


    IEnumerator opendoorplus()
    {
        float callnow = nowrota;
        //����϶� ������ 0 -> (+) �� ���ߵ�.
        for (float f = 0; f <= defaluatrota- callnow; f += 0.1f)
        {
            nowrota += f;
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, nowrota, this.transform.rotation.z);
            yield return null;
        }
    }

    IEnumerator opendoorminus()
    {
        float callnow = nowrota;
        //���϶� ������ 0 -> (-) �� ���ߵ�.
        for (float f = 0; f >= defaluatrota- callnow; f -= 0.1f)
        {
            nowrota += f;
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, nowrota, this.transform.rotation.z);
            yield return null;
        }
    }*/

    /*IEnumerator SpinCo()
    {
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        realCube.rotation = destRot;
    }
    �ε巴�� �����ϵ��� �� �����Ӿ� ���鼭(�ڷ�ƾ) realCube.rotation ť���� ȸ�� ���� destRot�� �� ������ realCube�� ���� ȸ������ �Լ� ����!
*/

}
