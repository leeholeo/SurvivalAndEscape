using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingBehavior : MonoBehaviour
{
    public PostProcessVolume volume;

    private Bloom _Bloom;
    private Vignette _Vignette;
    private bool vignette_inc_status;
    private ChromaticAberration _ChromaticAberration;

    // Health
    private PlayerScript _PlayerScript;

    void Start(){
        volume.profile.TryGetSettings(out _Bloom);

        volume.profile.TryGetSettings(out _Vignette);
        vignette_inc_status = true;

        volume.profile.TryGetSettings(out _ChromaticAberration);

        // 현재 플레이어의 status에 따라 보여지는 화면을 다르게 해줄것
        // _Bloom.intensity.value = 0;
        _Vignette.intensity.value = 0;
        // _ChromaticAberration.intensity.value = 0;

        // 
        _PlayerScript = GameObject.FindObjectOfType<PlayerScript>();
    }  
    
    void Update(){
        // 0-15 사이에 강도가 갈수록 강해지게 설정
        // 만약 체력이 낮다면, 주위의 테두리가 붉은색으로 깜빡거리게 설정. 

        if(_PlayerScript.playerObject.currentHealthPoint < 50){
            if(vignette_inc_status == true){
            _Vignette.intensity.value = Mathf.Lerp(_Vignette.intensity.value, 1.0f, 1.5f * Time.deltaTime);
            if(_Vignette.intensity.value > 0.9f){
                vignette_inc_status = false;
            }
        }
        else{
            _Vignette.intensity.value = Mathf.Lerp(_Vignette.intensity.value, 0.5f, 1.5f * Time.deltaTime);
            if(_Vignette.intensity.value < 0.6f){
                vignette_inc_status = true;
            }
        }

        }

        // _ChromaticAberration.intensity.value = Mathf.Lerp(_Vignette.intensity.value, 1, .05f * Time.deltaTime);
    }

}
