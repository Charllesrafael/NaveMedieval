using System.Collections.Generic;
using Doozy.Engine.Soundy;
using UnityEngine;

namespace Nephenthesys
{
    public class AudioSystem : MonoBehaviour
    {
        public static AudioSystem instance;
        public bool NewCallSoundShooter;

        private List<SoundyController> soundyControllers;

        public List<AudioScriptObject> BGS_Scriptable_Objs;
        public List<AudioScriptObject> SFX_Scriptable_Objs;

        public audio_player_holder new_sound_holder;

        private void Awake()
        {
            if (instance != null)
                Destroy(this.gameObject);
            else
            {
                instance = this;
                soundyControllers = new List<SoundyController>();
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            AudioConfigSetTrigger("Menu", true);
        }

        public void PlayEffect(SoundyData soundyData)
        {
            SoundyManager.Play(soundyData);
        }

        public void Play(SoundyData[] soundyData, bool musica = false)
        {
            if (musica) //musica
            { 
                foreach (var item in soundyControllers)
                {
                    if (item != null) item.Stop();
                }
                soundyControllers.Clear();

                foreach (var item in soundyData)
                    soundyControllers.Add(SoundyManager.Play(item));
            }
            else //efeito
            {
                foreach (var item in soundyData)
                    SoundyManager.Play(item);
            }
        }

        public void sound_controller_garbage_destroyer() //limpador de audio sources
        {
            //Debug.Log("Destroyeeeeerrr");
            SoundyPooler.ClearPool();
            soundyControllers.Clear();
        }

        float delayTimeToRecallABGS = 2f; bool calledaBGS = false;
        //usar essa fun��o para triggar audio ao inves da fun��o Play direto
        public void AudioConfigSetTrigger(string nameOfTheShot, bool isBGS)
        {

            ///////Debug.Log("Name: " + nameOfTheShot +  ", Is_BGS: " + isBGS +  ", calledaBGS: " + calledaBGS);
            if (isBGS)
            {
                if (!calledaBGS)
                {
                    calledaBGS = true;
                    Invoke("ZerifyDelayBGS", delayTimeToRecallABGS);

                    foreach (AudioScriptObject aso in BGS_Scriptable_Objs)
                    {
                        if (aso.audio_shot_name == nameOfTheShot)
                        {
                            //////////Debug.Log("Play.");
                            if (aso.isUsingSoundy)
                            {
                                Play(aso.soundyDatas, aso.musica);
                            }
                            else
                            {
                                foreach (AudioSource aud_s in new_sound_holder.audios)
                                {
                                    if (aud_s.gameObject.name == nameOfTheShot)
                                    {
                                        aud_s.Play();
                                        return;
                                    }
                                }
                            }
                            return;
                        }
                    }
                }
                else//chamou mais de uma vez uma bgs dentro de 4 segundos
                {
                    //////////Debug.Log("Return Musica.");
                    return;
                }
            }
            else
            {
                foreach (AudioScriptObject aso in SFX_Scriptable_Objs)
                {
                    if (aso.audio_shot_name == nameOfTheShot)
                    {
                        if (aso.isUsingSoundy)
                        {
                            Play(aso.soundyDatas, aso.musica);
                        }
                        else
                        {
                            foreach (AudioSource aud_s in new_sound_holder.audios)
                            {
                                if (aud_s.gameObject.name == nameOfTheShot)
                                {
                                    aud_s.Play();
                                    return;
                                }
                            }
                        }
                        return;
                    }
                }
            }

            Debug.Log("ERROR: Audio '" + nameOfTheShot + "' Inexistente na lista / Audio nao encontrado na lista");
            

        }


        //click e hilight para serem chamado de bot�es
        public void Click()
        {
            AudioConfigSetTrigger("Click", false);
        }
        public void SelectBtnClick()
        {
            AudioConfigSetTrigger("HilightClick", false);
        }


        void ZerifyDelayBGS()
        {
            calledaBGS = false;
        }


        private bool notRepeatBGS4RepeatingTheSameScene_bool = false;
        public void setRepeatBGS(bool key) { notRepeatBGS4RepeatingTheSameScene_bool = key; }
        public bool getRepeatBGS() { return notRepeatBGS4RepeatingTheSameScene_bool; }


        public void PlayNextSong()
        {
            if (isPlayingBossBgm) { isPlayingBossBgm = false; }
            if (notRepeatBGS4RepeatingTheSameScene_bool) { notRepeatBGS4RepeatingTheSameScene_bool = false; return; } // <- garante que n�o repetir� o mesmo bgs caso o player tenho reiniciado a mesma cena apos a morte

            sound_controller_garbage_destroyer(); // <- destroy os antigos audio sources

            if (ManagerScene.instance.idFaseAtual == 0) { AudioSystem.instance.AudioConfigSetTrigger("Fase1", true); }
            if (ManagerScene.instance.idFaseAtual == 1) { AudioSystem.instance.AudioConfigSetTrigger("Fase2", true); }
            if (ManagerScene.instance.idFaseAtual == 2) { AudioSystem.instance.AudioConfigSetTrigger("Fase3", true); }
            if (ManagerScene.instance.idFaseAtual == 3) { AudioSystem.instance.AudioConfigSetTrigger("Fase4", true); }
            if (ManagerScene.instance.idFaseAtual == 4) { AudioSystem.instance.AudioConfigSetTrigger("Fase5", true); }
        }


        //ARMAS
        public void ShotAudioManager(int idArma, int armaLevel, bool isLazerIdle)
        {
            if(!NewCallSoundShooter)
            {
                // FOGO
                if (idArma == 0 && armaLevel == 1) { TriggarAudioArmaTiro(); }
                if (idArma == 0 && armaLevel == 2) { TriggarAudioArmaTiro2(); }
                if (idArma == 0 && armaLevel == 3) { TriggarAudioArmaTiro3(); }

                // LAZER
                //                         colision                                         and                          idle
                if (idArma == 1 && armaLevel == 1 && !isLazerIdle) { TriggarAudioArmaTiro4(); } else if (idArma == 1 && armaLevel == 1 && isLazerIdle) { TriggarAudioArmaTiro13(); }
                if (idArma == 1 && armaLevel == 2 && !isLazerIdle) { TriggarAudioArmaTiro5(); } else if (idArma == 1 && armaLevel == 2 && isLazerIdle) { TriggarAudioArmaTiro14(); }
                if (idArma == 1 && armaLevel == 3 && !isLazerIdle) { TriggarAudioArmaTiro6(); } else if (idArma == 1 && armaLevel == 3 && isLazerIdle) { TriggarAudioArmaTiro15(); }

                // VENTO
                if (idArma == 2 && armaLevel == 1) { TriggarAudioArmaTiro7(); }
                if (idArma == 2 && armaLevel == 2) { TriggarAudioArmaTiro8(); }
                if (idArma == 2 && armaLevel == 3) { TriggarAudioArmaTiro9(); }

                // TERRA
                if (idArma == 3 && armaLevel == 1) { TriggarAudioArmaTiro10(); }
                if (idArma == 3 && armaLevel == 2) { TriggarAudioArmaTiro11(); }
                if (idArma == 3 && armaLevel == 3) { TriggarAudioArmaTiro12(); }
            }
        }

        //FOGO
        public void TriggarAudioArmaTiro() { AudioConfigSetTrigger("Shot1", false); }
        public void TriggarAudioArmaTiro2() { AudioConfigSetTrigger("Shot2", false); }
        public void TriggarAudioArmaTiro3() { AudioConfigSetTrigger("Shot3", false); }

        //LAZER
        //lazer idle / atirando
        public void TriggarAudioArmaTiro4() { AudioConfigSetTrigger("Shot4", false); }
        public void TriggarAudioArmaTiro5() { AudioConfigSetTrigger("Shot5", false); }
        public void TriggarAudioArmaTiro6() { AudioConfigSetTrigger("Shot6", false); }

        //lazer collision 
        public void TriggarAudioArmaTiro13() { AudioConfigSetTrigger("Shot13", false); }
        public void TriggarAudioArmaTiro14() { AudioConfigSetTrigger("Shot14", false); }
        public void TriggarAudioArmaTiro15() { AudioConfigSetTrigger("Shot15", false); }


        //VENTO
        public void TriggarAudioArmaTiro7() { AudioConfigSetTrigger("Shot7", false); }
        public void TriggarAudioArmaTiro8() { AudioConfigSetTrigger("Shot8", false); }
        public void TriggarAudioArmaTiro9() { AudioConfigSetTrigger("Shot9", false); }

        //TERRA
        public void TriggarAudioArmaTiro10() { AudioConfigSetTrigger("Shot10", false); }
        public void TriggarAudioArmaTiro11() { AudioConfigSetTrigger("Shot11", false); }
        public void TriggarAudioArmaTiro12() { AudioConfigSetTrigger("Shot12", false); }


        //BOSSES

        public bool isPlayingBossBgm;

        public void TriggarAudioBosses(int idfase)
        {
            notRepeatBGS4RepeatingTheSameScene_bool = false;

            isPlayingBossBgm = true;
            //audios BGMs Bosses
            if(idfase == 0) { AudioConfigSetTrigger("Boss_1", true); AudioConfigSetTrigger("Boss_1_Intimidation", false); }
            if(idfase == 1) { AudioConfigSetTrigger("Boss_2", true); AudioConfigSetTrigger("Boss_2_Intimidation", false); }
            if(idfase == 2) { AudioConfigSetTrigger("Boss_3", true); AudioConfigSetTrigger("Boss_3_Intimidation", false); }
            if(idfase == 3) { AudioConfigSetTrigger("Boss_4", true); AudioConfigSetTrigger("Boss_4_Intimidation", false); }
            if(idfase == 4) { AudioConfigSetTrigger("Boss_5", true); AudioConfigSetTrigger("Boss_5_Intimidation", false); }
        }

    }
}
