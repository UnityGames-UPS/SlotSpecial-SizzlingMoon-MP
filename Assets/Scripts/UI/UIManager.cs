using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private GameObject Menu_Object;
    [SerializeField]
    private RectTransform Menu_RT;
    [SerializeField]
    private GameObject Info_Object;
    [SerializeField]
    private Button Info_Button;
    [SerializeField]
    private Button Info_Exit;
    [SerializeField]
    private RectTransform Info_RT;

    //[SerializeField]
    //private Button About_Button;
    //[SerializeField]
    //private GameObject About_Object;
    //[SerializeField]
    //private RectTransform About_RT;

    [Header("Settings UI")]
    [SerializeField]
    private Button Settings_Button;
    [SerializeField]
    private GameObject Settings_Object;
    [SerializeField]
    private RectTransform Settings_RT;
    [SerializeField]
    private Button Terms_Button;
    [SerializeField]
    private Button Privacy_Button;

    [SerializeField]
    private Button Exit_Button;
    [SerializeField]
    private GameObject Exit_Object;
    [SerializeField]
    private RectTransform Exit_RT;

    [SerializeField]
    private ImageAnimation m_PaytableMajor;
    [SerializeField]
    private ImageAnimation m_PaytableMinor;
    [SerializeField]
    private ImageAnimation m_PaytableMini;

    [SerializeField]
    private Button Paytable_Button;
    [SerializeField]
    private GameObject Paytable_Object;
    [SerializeField]
    private RectTransform Paytable_RT;

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("About Popup")]
    [SerializeField]
    private GameObject AboutPopup_Object;
    [SerializeField]
    private Button AboutExit_Button;
    [SerializeField]
    private Image AboutLogo_Image;
    [SerializeField]
    private Button Support_Button;

    [Header("Paytable Popup")]
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;
    [SerializeField]
    private TMP_Text[] SymbolsText;

    [SerializeField]
    private TMP_Text StickyBonus_Text;
    [SerializeField]
    private TMP_Text Bonus_Text;
    [SerializeField]
    private TMP_Text Mystery_Text;
    [SerializeField]
    private TMP_Text Mini_Text;
    [SerializeField]
    private TMP_Text Wild_Text;

    [Header("Settings Popup")]
    [SerializeField]
    private GameObject SettingsPopup_Object;
    [SerializeField]
    private Button SettingsExit_Button;
    [SerializeField]
    private Button SoundOn_Button;
    [SerializeField]
    private Button SoundOff_Button;
    [SerializeField]
    private Button Music_Button;

    [SerializeField]
    private GameObject MusicOn_Object;
    [SerializeField]
    private GameObject MusicOff_Object;
    [SerializeField]
    private GameObject SoundOn_Object;
    [SerializeField]
    private GameObject SoundOff_Object;

    [Header("Win Popup")]
    [SerializeField]
    private Sprite BigWin_Sprite;
    [SerializeField]
    private Sprite HugeWin_Sprite;
    [SerializeField]
    private Sprite MegaWin_Sprite;
    [SerializeField]
    private Sprite Jackpot_Sprite;
    [SerializeField]
    private Image Win_Image;
    [SerializeField]
    private GameObject WinPopup_Object;
    [SerializeField]
    private TMP_Text Win_Text;

    [Header("FreeSpins Popup")]
    [SerializeField]
    private GameObject FreeSpinPopup_Object;
    [SerializeField]
    private TMP_Text Free_Text;
    [SerializeField]
    private Button FreeSpin_Button;

    [Header("Splash Screen")]
    [SerializeField]
    private GameObject Loading_Object;
    [SerializeField]
    private Image Loading_Image;
    [SerializeField]
    private TMP_Text Loading_Text;
    [SerializeField]
    private TMP_Text LoadPercent_Text;
    [SerializeField]
    private Button QuitSplash_button;

    [Header("Disconnection Popup")]
    [SerializeField]
    private Button CloseDisconnect_Button;
    [SerializeField]
    private GameObject DisconnectPopup_Object;

    [Header("AnotherDevice Popup")]
    [SerializeField]
    private Button CloseAD_Button;
    [SerializeField]
    private GameObject ADPopup_Object;

    [Header("Reconnection Popup")]
    [SerializeField]
    private TMP_Text reconnect_Text;
    [SerializeField]
    private GameObject ReconnectPopup_Object;

    [Header("LowBalance Popup")]
    [SerializeField]
    private Button LBExit_Button;
    [SerializeField]
    private GameObject LBPopup_Object;

    [Header("Turbo Speed Buttons")]
    [SerializeField]
    private Button m_Turtle_Speed_Button;
    [SerializeField]
    private Button m_Rabbit_Speed_Button;
    [SerializeField]
    private Button m_Cheetah_Speed_Button;

    [Header("Value Paytable")]
    [SerializeField]
    private TMP_Text m_Moon_Value;
    [SerializeField]
    private TMP_Text m_Grand_Value;
    [SerializeField]
    private TMP_Text m_Major_Value;
    [SerializeField]
    private TMP_Text m_Minor_Value;
    [SerializeField]
    private TMP_Text m_Mini_Value;
    [SerializeField]
    private TMP_Text m_Paytable_Mini_Value;
    [SerializeField]
    private TMP_Text m_Paytable_Minor_Value;
    [SerializeField]
    private TMP_Text m_Paytable_Major_Value;
    [SerializeField]
    private TMP_Text m_Paytable_Respins_Desc;

    [Header("Paytable Navigation")]
    [SerializeField]
    private Button m_NextPage_Button;
    [SerializeField]
    private Button m_PrevPage_Button;
    [SerializeField]
    private Toggle[] m_Page_Toggle;
    [SerializeField]
    private GameObject[] m_Page_Reference;
    [SerializeField]
    private int m_CurrentPageCount = 0;
    [SerializeField]
    private TMP_Text m_Paytable_P5_D1;
    [SerializeField]
    private TMP_Text m_Paytable_P5_D2;

    [Header("Quit Popup")]
    [SerializeField]
    private GameObject QuitPopup_Object;
    [SerializeField]
    private Button YesQuit_Button;
    [SerializeField]
    private Button NoQuit_Button;
    [SerializeField]
    private Button CrossQuit_Button;

    [SerializeField]
    private AudioController audioController;
    [SerializeField]
    private Button m_AwakeGameButton;

    [SerializeField]
    private Button GameExit_Button;

    [SerializeField]
    private SlotBehaviour slotManager;

    [SerializeField]
    private SocketIOManager socketManager;

    [SerializeField]
    internal List<Button> m_BetButtons;
    [SerializeField]
    internal List<TMP_Text> m_DeactivatedBetButtons;
    [SerializeField]
    internal TMP_Text m_DeactivatedMaxBetButton;

    private bool isMusic = true;
    private bool isSound = true;
    private bool isExit = false;

    internal int FreeSpins;

    [SerializeField]
    private Button SkipWinAnimation;

    private Tween WinPopupTextTween;
    private Tween ClosePopupTween;

    private int bet_data_counter = 0;
    private int bet_selected = 0;

    [SerializeField] private Button skipFreespinButton;


    //private void Awake()
    //{
    //    //if (Loading_Object) Loading_Object.SetActive(true);
    //    //StartCoroutine(LoadingRoutine());
    //    SimulateClickByDefault();
    //}

    private IEnumerator LoadingRoutine()
    {
        StartCoroutine(LoadingTextAnimate());
        float imageFill = 0f;
        DOTween.To(() => imageFill, (val) => imageFill = val, 0.7f, 2f).OnUpdate(() =>
        {
            if (Loading_Image) Loading_Image.fillAmount = imageFill;
            if (LoadPercent_Text) LoadPercent_Text.text = (100 * imageFill).ToString("f0") + "%";
        });
        yield return new WaitForSecondsRealtime(2);
        yield return new WaitUntil(() => socketManager.isLoaded);
        DOTween.To(() => imageFill, (val) => imageFill = val, 1, 1f).OnUpdate(() =>
        {
            if (Loading_Image) Loading_Image.fillAmount = imageFill;
            if (LoadPercent_Text) LoadPercent_Text.text = (100 * imageFill).ToString("f0") + "%";
        });
        yield return new WaitForSecondsRealtime(1f);
        if (Loading_Object) Loading_Object.SetActive(false);
        StopCoroutine(LoadingTextAnimate());
    }

    private IEnumerator LoadingTextAnimate()
    {
        while (true)
        {
            if (Loading_Text) Loading_Text.text = "Loading.";
            yield return new WaitForSeconds(1f);
            if (Loading_Text) Loading_Text.text = "Loading..";
            yield return new WaitForSeconds(1f);
            if (Loading_Text) Loading_Text.text = "Loading...";
            yield return new WaitForSeconds(1f);
        }
    }

    private void Start()
    {
        //if (Menu_Button) Menu_Button.onClick.RemoveAllListeners();
        //if (Menu_Button) Menu_Button.onClick.AddListener(OpenMenu);

        //if (Exit_Button) Exit_Button.onClick.RemoveAllListeners();
        //if (Exit_Button) Exit_Button.onClick.AddListener(CloseMenu);

        //if (About_Button) About_Button.onClick.RemoveAllListeners();
        //if (About_Button) About_Button.onClick.AddListener(delegate { OpenPopup(AboutPopup_Object); });

        if (Info_Button) Info_Button.onClick.RemoveAllListeners();
        if (Info_Button) Info_Button.onClick.AddListener(delegate { audioController.PlayNormalButton(); OpenPopup(Info_Object); });

        if (Info_Exit) Info_Exit.onClick.RemoveAllListeners();
        if (Info_Exit) Info_Exit.onClick.AddListener(delegate { audioController.PlayNormalButton(); ClosePopup(Info_Object); });

        if (AboutExit_Button) AboutExit_Button.onClick.RemoveAllListeners();
        if (AboutExit_Button) AboutExit_Button.onClick.AddListener(delegate { ClosePopup(AboutPopup_Object); });

        if (Paytable_Button) Paytable_Button.onClick.RemoveAllListeners();
        if (Paytable_Button) Paytable_Button.onClick.AddListener(delegate { audioController.PlayNormalButton(); if (!PaytablePopup_Object.activeSelf) { m_CurrentPageCount = -1; }; NextPrevPaytable(true, false); });

        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

        if (Settings_Button) Settings_Button.onClick.RemoveAllListeners();
        if (Settings_Button) Settings_Button.onClick.AddListener(delegate { OpenPopup(SettingsPopup_Object); });

        if (SettingsExit_Button) SettingsExit_Button.onClick.RemoveAllListeners();
        if (SettingsExit_Button) SettingsExit_Button.onClick.AddListener(delegate { ClosePopup(SettingsPopup_Object); });

        if (skipFreespinButton) skipFreespinButton.onClick.RemoveAllListeners();
        if (skipFreespinButton) skipFreespinButton.onClick.AddListener(delegate { skipFreeSpinPopup(); });

        if (MusicOn_Object) MusicOn_Object.SetActive(true);
        if (MusicOff_Object) MusicOff_Object.SetActive(false);

        if (SoundOn_Object) SoundOn_Object.SetActive(true);
        if (SoundOff_Object) SoundOff_Object.SetActive(false);

        if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
        if (GameExit_Button) GameExit_Button.onClick.AddListener(delegate
        {
            OpenPopup(QuitPopup_Object);
            Debug.Log("Quit event: pressed Big_X button");

        });

        if (NoQuit_Button) NoQuit_Button.onClick.RemoveAllListeners();
        if (NoQuit_Button) NoQuit_Button.onClick.AddListener(delegate
        {
            if (!isExit)
            {
                ClosePopup(QuitPopup_Object);
                Debug.Log("quit event: pressed NO Button ");
            }
        });

        if (CrossQuit_Button) CrossQuit_Button.onClick.RemoveAllListeners();
        if (CrossQuit_Button) CrossQuit_Button.onClick.AddListener(delegate
        {
            if (!isExit)
            {
                ClosePopup(QuitPopup_Object);
                Debug.Log("quit event: pressed Small_X Button ");

            }
        });

        if (LBExit_Button) LBExit_Button.onClick.RemoveAllListeners();
        if (LBExit_Button) LBExit_Button.onClick.AddListener(delegate { ClosePopup(LBPopup_Object); });

        if (YesQuit_Button) YesQuit_Button.onClick.RemoveAllListeners();
        if (YesQuit_Button) YesQuit_Button.onClick.AddListener(delegate
        {
            CallOnExitFunction();
            Debug.Log("quit event: pressed YES Button ");

        });

        if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.RemoveAllListeners();
        if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.AddListener(delegate { CallOnExitFunction(); socketManager.closeSocketCallReactnative(); });

        if (CloseAD_Button) CloseAD_Button.onClick.RemoveAllListeners();
        if (CloseAD_Button) CloseAD_Button.onClick.AddListener(delegate { CallOnExitFunction(); socketManager.closeSocketCallReactnative(); });

        if (FreeSpin_Button) FreeSpin_Button.onClick.RemoveAllListeners();
        if (FreeSpin_Button) FreeSpin_Button.onClick.AddListener(delegate { StartFreeSpins(FreeSpins); });

        if (QuitSplash_button) QuitSplash_button.onClick.RemoveAllListeners();
        if (QuitSplash_button) QuitSplash_button.onClick.AddListener(delegate { OpenPopup(QuitPopup_Object); });

        //if (audioController) audioController.ToggleMute(false);

        isMusic = true;
        isSound = true;

        if (SoundOn_Button) SoundOn_Button.onClick.RemoveAllListeners();
        if (SoundOn_Button) SoundOn_Button.onClick.AddListener(delegate { ToggleSound(true); });

        if (SoundOff_Button) SoundOff_Button.onClick.RemoveAllListeners();
        if (SoundOff_Button) SoundOff_Button.onClick.AddListener(delegate { ToggleSound(false); });

        //if (Music_Button) Music_Button.onClick.RemoveAllListeners();
        //if (Music_Button) Music_Button.onClick.AddListener(ToggleMusic);

        if (SkipWinAnimation) SkipWinAnimation.onClick.RemoveAllListeners();
        if (SkipWinAnimation) SkipWinAnimation.onClick.AddListener(SkipWin);

        if (m_NextPage_Button) m_NextPage_Button.onClick.AddListener(() => { NextPrevPaytable(true, true); });
        if (m_PrevPage_Button) m_PrevPage_Button.onClick.AddListener(() => { NextPrevPaytable(false, true); });

        if (m_Turtle_Speed_Button) m_Turtle_Speed_Button.onClick.RemoveAllListeners();
        if (m_Turtle_Speed_Button) m_Turtle_Speed_Button.onClick.AddListener(() =>
        {
            audioController.PlayNormalButton();
            m_Turtle_Speed_Button.gameObject.SetActive(false);
            m_Rabbit_Speed_Button.gameObject.SetActive(true);
            m_Cheetah_Speed_Button.gameObject.SetActive(false);
            slotManager.m_Speed = 0.2f;//Rabbit's Speed
            slotManager.m_Is_Turtle = false;
            slotManager.m_Is_Rabbit = true;
            slotManager.m_Is_Cheetah = false;
        });

        if (m_Rabbit_Speed_Button) m_Rabbit_Speed_Button.onClick.RemoveAllListeners();
        if (m_Rabbit_Speed_Button) m_Rabbit_Speed_Button.onClick.AddListener(() =>
        {
            audioController.PlayNormalButton();
            m_Turtle_Speed_Button.gameObject.SetActive(false);
            m_Rabbit_Speed_Button.gameObject.SetActive(false);
            m_Cheetah_Speed_Button.gameObject.SetActive(true);
            slotManager.m_Speed = 0.1f;//Cheetah's Speed
            slotManager.m_Is_Turtle = false;
            slotManager.m_Is_Rabbit = false;
            slotManager.m_Is_Cheetah = true;
        });

        if (m_Cheetah_Speed_Button) m_Cheetah_Speed_Button.onClick.RemoveAllListeners();
        if (m_Cheetah_Speed_Button) m_Cheetah_Speed_Button.onClick.AddListener(() =>
        {
            audioController.PlayNormalButton();
            m_Turtle_Speed_Button.gameObject.SetActive(true);
            m_Rabbit_Speed_Button.gameObject.SetActive(false);
            m_Cheetah_Speed_Button.gameObject.SetActive(false);
            slotManager.m_Speed = 0.4f;//Turtle's Speed
            slotManager.m_Is_Turtle = true;
            slotManager.m_Is_Rabbit = false;
            slotManager.m_Is_Cheetah = false;
        });

        TogglePaytable(0);
        DirectClickButton();
    }

    void SkipWin()
    {
        Debug.Log("Skip win called");
        if (ClosePopupTween != null)
        {
            ClosePopupTween.Kill();
            ClosePopupTween = null;
        }
        if (WinPopupTextTween != null)
        {
            WinPopupTextTween.Kill();
            WinPopupTextTween = null;
        }
        ClosePopup(WinPopup_Object);
        slotManager.CheckPopups = false;
    }

    #region PAYTABLE NAVIGATION

    private void TogglePaytable(int index)
    {
        m_CurrentPageCount = index;
        m_Page_Reference[m_CurrentPageCount].SetActive(true);

        // Disable events temporarily
        foreach (var toggle in m_Page_Toggle)
            toggle.onValueChanged.RemoveAllListeners();

        m_Page_Toggle[m_CurrentPageCount].isOn = true;

        for (int i = 0; i < m_Page_Reference.Length; i++)
        {
            if (i != index)
            {
                m_Page_Reference[i].SetActive(false);
                //m_Page_Toggle[i].isOn = false;
            }
        }

        // Reattach listeners

        for (int i = 0; i < m_Page_Toggle.Length; i++)
        {
            Toggle currentToggler = m_Page_Toggle[i];
            m_Page_Toggle[i].onValueChanged.AddListener((b) => { if (b) TogglePaytable(Array.IndexOf(m_Page_Toggle, currentToggler)); });
        }

        // switch (m_CurrentPageCount)
        // {
        //     case 2:
        //         m_PaytableMajor.StartAnimation();
        //         m_PaytableMinor.StartAnimation();
        //         m_PaytableMini.StartAnimation();
        //         break;
        // }
    }
    public bool IsFreeSpinPopupActive()
    {
        return FreeSpinPopup_Object != null && FreeSpinPopup_Object.activeSelf;
    }
    public void skipFreeSpinPopup()
    {
        ClosePopup(FreeSpinPopup_Object);
    }

    private void NextPrevPaytable(bool next_prev, bool m_navigationMode)
    {
        audioController.PlayNormalButton();
        if (m_CurrentPageCount == -1) PaytablePopup_Object.SetActive(true);
        if (next_prev)
        {
            m_CurrentPageCount++;
            if (m_CurrentPageCount == m_Page_Reference.Length)
            {
                if (!m_navigationMode)
                {
                    PaytablePopup_Object.SetActive(false);
                }
                m_CurrentPageCount = 0;
            }
            ;

            TogglePaytable(m_CurrentPageCount);
        }
        else
        {
            m_CurrentPageCount--;
            if (m_CurrentPageCount == -1)
            {
                m_CurrentPageCount = m_Page_Reference.Length - 1;
            }

            TogglePaytable(m_CurrentPageCount);
        }
    }

    #endregion

    internal void UpdateExternalPaytableValue()
    {
        m_Moon_Value.text = ((double)(socketManager.initialData.bets[slotManager.BetCounter] * socketManager.fullInitData.features.moonMultiplier)).ToString("F2");
        m_Grand_Value.text = ((double)(socketManager.initialData.bets[slotManager.BetCounter] * socketManager.fullInitData.features.grandMultiplier)).ToString("F2");
        m_Major_Value.text = ((double)(socketManager.initialData.bets[slotManager.BetCounter] * socketManager.fullInitData.features.majorMultiplier)).ToString("F2");
        m_Minor_Value.text = ((double)(socketManager.initialData.bets[slotManager.BetCounter] * socketManager.fullInitData.features.minorMultiplier)).ToString("F2");
        m_Mini_Value.text = ((double)(socketManager.initialData.bets[slotManager.BetCounter] * socketManager.fullInitData.features.miniMultiplier)).ToString("F2");

        string grand_jackpot_text = $"Collecting all 16 Bonus symbols of any type awards the GRAND Jackpot of {(socketManager.fullInitData.features.grandMultiplier)}x player's bet, which is added to the total win!";
        string moon_jackpot_text = $"The Moon symbol can be drawn on the Moon Mystery symbol and awards the MOON Jackpot of {(socketManager.fullInitData.features.moonMultiplier)}x player's bet! The MOON Jackpot is the maximum prize in the game and Bonus symbol values are not added to it.";

        m_Paytable_P5_D1.text = grand_jackpot_text;//Grand Jackpot
        m_Paytable_P5_D2.text = moon_jackpot_text;//Moon Jackpot
    }

    #region [[===BET BUTTONS HANDLING===]]

    internal void ChangeBets(int transact, bool incDec)
    {
        //HACK: The below code may be work accurately with out using bet_data_counter
        int temp_counter = slotManager.BetCounter;
        for (int i = 0; i < m_BetButtons.Count; i++)
        {
            Button tempButton = m_BetButtons[i];
            TMP_Text tempText = m_DeactivatedBetButtons[i];
            if (incDec)
            {
                if (slotManager.BetCounter < socketManager.initialData.bets.Count)
                {
                    int index = transact != 0 ? temp_counter - ((m_BetButtons.Count - 1) - i) : temp_counter;
                    double counter = socketManager.initialData.bets[index];
                    Button curr_button = tempButton;

                    tempButton.transform.GetChild(0).GetComponent<TMP_Text>().text = counter.ToString();
                    tempText.text = counter.ToString();

                    //tempButton.onClick.AddListener(() =>
                    //{
                    //    Debug.Log("Button Clicked...");
                    //    //DirectClickButton(index);
                    //});

                    if (transact == 0)
                    {
                        temp_counter++;
                    }
                }
            }
            else
            {
                if (slotManager.BetCounter >= 0)
                {
                    int index = transact != 0 ? temp_counter : temp_counter - ((m_BetButtons.Count - 1) - i);
                    double counter = socketManager.initialData.bets[index];
                    Button curr_button = tempButton;

                    //tempButton.onClick.AddListener(() =>
                    //{
                    //    Debug.Log("Button Clicked...");
                    //    //DirectClickButton(index);
                    //});

                    tempButton.transform.GetChild(0).GetComponent<TMP_Text>().text = counter.ToString();
                    tempText.text = counter.ToString();
                    if (transact != 0)
                    {
                        temp_counter++;
                    }
                }
            }
        }
    }

    private void DirectClickButton()
    {
        foreach (Button b in m_BetButtons)
        {
            b.onClick.AddListener((UnityEngine.Events.UnityAction)(() =>
            {
                double value = double.Parse(b.transform.GetChild(0).GetComponent<TMP_Text>().text);
                int index = socketManager.initialData.bets.IndexOf(value);
                int btn_index = m_BetButtons.IndexOf(b);
                Debug.Log(index + " " + value + " " + btn_index);

                DirectSelectButton(index, btn_index);
            }));
        }
    }

    private void DirectSelectButton(int bet_index, int btn_index)
    {
        slotManager.BetCounter = bet_index;
        bet_selected = btn_index;
        bet_data_counter = btn_index;
        if (bet_selected < m_BetButtons.Count)
        {
            ChangeBetToggle(bet_selected);
        }
        UpdateExternalPaytableValue();
    }

    //HACK: bet_data_counter could be removed and the approach could be more optimized.
    internal void SelectBetButton(bool incdec)
    {
        if (incdec)
        {
            if (bet_selected < m_BetButtons.Count - 1)
            {
                bet_selected++;
                bet_data_counter++;
                ChangeBetToggle(bet_selected);
            }
            else
            {
                //NextBets(1);
                ChangeBets(1, incdec);
            }
        }
        else
        {
            if (bet_selected > 0)
            {
                bet_selected--;
                bet_data_counter--;
                ChangeBetToggle(bet_selected);
            }
            else
            {
                //PrevBets(1);
                ChangeBets(1, incdec);
            }
        }
        UpdateExternalPaytableValue();
        //Debug.Log(string.Concat("<color=red><b>", "Bet Counter: " + slotManager.BetCounter + " Bet Data Counter: " + bet_data_counter, "</b></color>"));
    }

    private void ChangeBetToggle(int index)
    {
        for (int i = 0; i < m_BetButtons.Count; i++)
        {
            m_BetButtons[i].interactable = true;
        }

        m_BetButtons[index].interactable = index < m_BetButtons.Count ? false : true;
    }

    internal void StartBetCounter()
    {
        bet_selected = 0;
        bet_data_counter = 0;
        //NextBets(0);
        ChangeBets(0, true);
        ChangeBetToggle(bet_selected);
        UpdateExternalPaytableValue();
    }

    internal void LastBetCounter()
    {
        bet_selected = 3;
        bet_data_counter = socketManager.initialData.bets.Count - 1;
        //PrevBets(0);
        ChangeBets(0, false);
        ChangeBetToggle(bet_selected);
        UpdateExternalPaytableValue();
    }

    private int GetBetCounter(Button m_Click_Button)
    {
        for (int _ = 0; _ < m_BetButtons.Count; _++)
        {
            if (m_BetButtons[_] == m_Click_Button)
            {
                return _;
            }
        }
        return 0;
    }
    #endregion

    internal void LowBalPopup()
    {
        OpenPopup(LBPopup_Object);
    }

    internal void DisconnectionPopup()
    {
        if (!isExit)
        {
            OpenPopup(DisconnectPopup_Object);
        }
    }

    internal void ReconnectionPopup()
    {
        OpenPopup(ReconnectPopup_Object);
    }

    internal void CheckAndClosePopups()
    {
        if (ReconnectPopup_Object.activeInHierarchy)
        {
            ClosePopup(ReconnectPopup_Object);
        }
        if (DisconnectPopup_Object.activeInHierarchy)
        {
            ClosePopup(DisconnectPopup_Object);
        }
    }

    internal void PopulateWin(int value, double amount)
    {
        //switch(value)
        //{
        //    case 1:
        //        if (Win_Image) Win_Image.sprite = BigWin_Sprite;
        //        break;
        //    case 2:
        //        if (Win_Image) Win_Image.sprite = HugeWin_Sprite;
        //        break;
        //    case 3:
        //        if (Win_Image) Win_Image.sprite = MegaWin_Sprite;
        //        break;
        //    case 4:
        //        if (Win_Image) Win_Image.sprite = Jackpot_Sprite;
        //        break;
        //}

        StartPopupAnim(amount);
    }

    internal void EnableDisableSpeed(bool m_toggle)
    {
        m_Turtle_Speed_Button.interactable = m_toggle;
        m_Rabbit_Speed_Button.interactable = m_toggle;
        m_Cheetah_Speed_Button.interactable = m_toggle;
    }

    private void StartFreeSpins(int spins)
    {
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(false);
        FreeSpin_Button.GetComponent<ImageAnimation>().StartAnimation();
        slotManager.FreeSpin(spins);
    }

    internal void FreeSpinProcess(int spins)
    {
        int ExtraSpins = spins - FreeSpins;
        FreeSpins = spins;
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(true);
        if (Free_Text) Free_Text.text = spins.ToString() + " Free spins awarded.";
        if (Free_Text) Free_Text.text = "Received \n" + spins.ToString() + " Free Spins";
        if (Free_Text) Free_Text.text = "Free Spin Set To 3";
        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOVirtual.DelayedCall(1.2f, () =>
        {
            StartFreeSpins(spins);
        });
    }

    private void StartPopupAnim(double amount)
    {
        double initAmount = 0;
        if (WinPopup_Object) WinPopup_Object.SetActive(true);
        WinPopup_Object.transform.GetChild(0).GetComponent<ImageAnimation>().StartAnimation();

        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        WinPopupTextTween = DOTween.To(() => initAmount, (val) => initAmount = val, (double)amount, 0.5f).OnUpdate(() =>
        {
            if (Win_Text) Win_Text.text = initAmount.ToString("F3");
        });

        ClosePopupTween = DOVirtual.DelayedCall(1.2f, () =>
        {
            ClosePopup(WinPopup_Object);
            slotManager.CheckPopups = false;
        });
    }

    internal void ADfunction()
    {
        OpenPopup(ADPopup_Object);
    }

    internal void InitialiseUIData(Paylines symbolsText)
    {
        // if (Support_Button) Support_Button.onClick.RemoveAllListeners();
        // if (Support_Button) Support_Button.onClick.AddListener(delegate { UrlButtons(SupportUrl); });

        // if (Terms_Button) Terms_Button.onClick.RemoveAllListeners();
        // if (Terms_Button) Terms_Button.onClick.AddListener(delegate { UrlButtons(TermsUrl); });

        // if (Privacy_Button) Privacy_Button.onClick.RemoveAllListeners();
        // if (Privacy_Button) Privacy_Button.onClick.AddListener(delegate { UrlButtons(PrivacyUrl); });

        // //m_DeactivatedMaxBetButton.text = socketManager.initialData.Bets[socketManager.initialData.Bets.Count - 1].ToString();

        // StartCoroutine(DownloadImage(AbtImgUrl));
        PopulateSymbolsPayout(symbolsText);
    }

    private void PopulateSymbolsPayout(Paylines paylines)
    {
        for (int i = 0; i < SymbolsText.Length; i++)
        {
            string text = null;
            if (paylines.symbols[i].multiplier[0] != 0)
            {
                text += "<color=orange><b>16x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[0] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[1] != 0)
            {
                text += "<color=orange><b>15x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[1] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[2] != 0)
            {
                text += "<color=orange><b>14x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[2] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[3] != 0)
            {
                text += "<color=orange><b>13x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[3] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[4] != 0)
            {
                text += "<color=orange><b>12x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[4] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[5] != 0)
            {
                text += "<color=orange><b>11x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[5] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[6] != 0)
            {
                text += "<color=orange><b>10x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[6] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[7] != 0)
            {
                text += "<color=orange><b>9x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[7] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[8] != 0)
            {
                text += "<color=orange><b>8x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[8] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[9] != 0)
            {
                text += "<color=orange><b>7x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[9] + "x</b></color> \n";
            }
            if (paylines.symbols[i].multiplier[10] != 0)
            {
                text += "<color=orange><b>6x - </b></color>" + "<color=yellow><b>" + paylines.symbols[i].multiplier[10] + "x</b></color>";
            }
            if (SymbolsText[i]) SymbolsText[i].text = text;
        }

        for (int i = 0; i < paylines.symbols.Count; i++)
        {
            if (paylines.symbols[i].name.ToUpper() == "BONUS")
            {
                if (Bonus_Text) Bonus_Text.text = paylines.symbols[i].description.ToString();
            }
            if (paylines.symbols[i].name.ToUpper() == "STICKYBONUS")
            {
                if (StickyBonus_Text) StickyBonus_Text.text = paylines.symbols[i].description.ToString();
            }
            if (paylines.symbols[i].name.ToUpper() == "MYSTERY")
            {
                if (Mystery_Text) Mystery_Text.text = paylines.symbols[i].description.ToString();
            }
            if (paylines.symbols[i].name.ToUpper() == "MINI")
            {
                if (Mini_Text) Mini_Text.text = paylines.symbols[i].description.ToString();
                if (m_Paytable_Mini_Value && socketManager.fullInitData.features.miniMultiplier != 0) m_Paytable_Mini_Value.text = socketManager.fullInitData.features.miniMultiplier + "X";
            }
            if (paylines.symbols[i].name.ToUpper() == "MINOR")
            {
                if (m_Paytable_Minor_Value && socketManager.fullInitData.features.minorMultiplier != 0) m_Paytable_Minor_Value.text = socketManager.fullInitData.features.minorMultiplier + "X";
            }
            if (paylines.symbols[i].name.ToUpper() == "MAJOR")
            {
                if (m_Paytable_Major_Value && socketManager.fullInitData.features.majorMultiplier != 0) m_Paytable_Major_Value.text = socketManager.fullInitData.features.majorMultiplier + "X";
            }
            if (paylines.symbols[i].name.ToUpper() == "WILD")
            {
                if (Wild_Text) Wild_Text.text = paylines.symbols[i].description.ToString();
            }
            //This is the bonus symbol description
            // if(paylines.symbols[i].Name.ToUpper() == "PLACEHOLDER")
            // {
            //     if (Bonus_Text) Bonus_Text.text = paylines.symbols[i].description.ToString();
            // }
        }
    }

    private void CallOnExitFunction()
    {
        isExit = true;
        audioController.PlayNormalButton();
        slotManager.CallCloseSocket();
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayNormalButton();
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (audioController) audioController.PlayNormalButton();
        if (Popup) Popup.SetActive(false);
        if (!DisconnectPopup_Object.activeSelf)
        {
            if (MainPopup_Object) MainPopup_Object.SetActive(false);
        }
    }

    //private void ToggleMusic()
    //{
    //    isMusic = !isMusic;
    //    if (isMusic)
    //    {
    //        if (MusicOn_Object) MusicOn_Object.SetActive(true);
    //        if (MusicOff_Object) MusicOff_Object.SetActive(false);
    //        audioController.ToggleMute(false, "bg");
    //    }
    //    else
    //    {
    //        if (MusicOn_Object) MusicOn_Object.SetActive(false);
    //        if (MusicOff_Object) MusicOff_Object.SetActive(true);
    //        audioController.ToggleMute(true, "bg");
    //    }
    //}

    private void UrlButtons(string url)
    {
        Application.OpenURL(url);
    }

    private void ToggleSound(bool m_isSound)
    {
        //isSound = !isSound;
        Debug.Log(m_isSound);
        if (!m_isSound)
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(true);
            if (SoundOff_Object) SoundOff_Object.SetActive(false);
            //audioController.MuteUnmute(Sound.All, false, true);
            audioController.MuteUnmute(Sound.Sound, false, true);
            audioController.MuteUnmute(Sound.Music, false, true);
            //if (audioController) audioController.ToggleMute(false, "button");
            //if (audioController) audioController.ToggleMute(false, "wl");
        }
        else
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(false);
            if (SoundOff_Object) SoundOff_Object.SetActive(true);
            //audioController.MuteUnmute(Sound.All, true, true);
            audioController.MuteUnmute(Sound.Sound, true, true);
            audioController.MuteUnmute(Sound.Music, true, true);
            //if (audioController) audioController.ToggleMute(true, "button");
            //if (audioController) audioController.ToggleMute(true, "wl");
        }
    }

    private IEnumerator DownloadImage(string url)
    {
        // Create a UnityWebRequest object to download the image
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        // Wait for the download to complete
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Apply the sprite to the target image
            AboutLogo_Image.sprite = sprite;
        }
        else
        {
            Debug.LogError("Error downloading image: " + request.error);
        }
    }
}
