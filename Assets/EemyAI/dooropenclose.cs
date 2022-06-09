using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropenclose : MonoBehaviour
{
    private float defaluatrota;//초기 문각도 값
    private float nowrota;//현재 문 각도 값.
    private float closerota;//초기 문각도 값
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
            
            if (defaluatrota >= 0 && 0<= nowrota && nowrota <= defaluatrota) //양수면 0<= now <= default
            {
               
            }
            else if(defaluatrota < 0 && defaluatrota <= nowrota && nowrota <= 0) {// 음수면 de <= now <=0
                
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
        //testcode 끝

        //this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, nowrota, this.transform.rotation.z);
    }

    /*IEnumerator closedoorplus()
    {
        float callnow = nowrota;//호출당시 now값
        //양수일때 닫으려면 (+) -> 0으로 가야됨.
        for (float f = defaluatrota- callnow; f >= 0; f -= 0.1f)
         {
                nowrota += f;
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, nowrota, this.transform.rotation.z);
                yield return null;
         }    
    }

    IEnumerator closedoorminus()
    {
        float callnow = nowrota;//호출당시 now값
        //음수일때 닫으려면 (-) -> 0으로 가야됨.
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
        //양수일때 열려면 0 -> (+) 로 가야됨.
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
        //음일때 열려면 0 -> (-) 로 가야됨.
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
    부드럽게 동작하도록 한 프레임씩 쉬면서(코루틴) realCube.rotation 큐브의 회전 값이 destRot가 될 때까지 realCube의 현재 회전값을 게속 업뎃!
*/

}
