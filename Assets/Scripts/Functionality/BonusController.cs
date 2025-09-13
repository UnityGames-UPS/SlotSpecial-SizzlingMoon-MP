using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BonusController : MonoBehaviour
{
    [SerializeField]
    private SocketIOManager m_SocketManager;
    [SerializeField]
    private UIManager m_UIManager;
    [SerializeField]
    private SlotBehaviour m_SlotBehaviour;
    [SerializeField]
    private ImageAnimation m_FreeSpinInitAnimation;
    [SerializeField]
    private ImageAnimation m_FreeSpinExitAnimation;
    [SerializeField]
    private List<GameObject> m_ListOfMystery = new List<GameObject>();

    private bool isFreezeRunning = false;
    internal bool isMysteryRunning = false;
    [SerializeField] private AudioController audioController;

    #region STICKY BONUS
    internal void StartStickyBonus()
    {
        int row = 0;
        int col = 0;
        if (m_SocketManager.fullResultData.features.isStickyBonus || m_SlotBehaviour.IsFreeSpin)
        {
            for (int i = 0; i < m_SocketManager.fullResultData.features.stickyBonusValue.Count; i++)
            {
                row = m_SocketManager.fullResultData.features.stickyBonusValue[i].position[1];
                col = m_SocketManager.fullResultData.features.stickyBonusValue[i].position[0];

                if (!CheckSticky(m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform))
                    if (m_SocketManager.fullResultData.features.stickyBonusValue[i].value > 0)
                        m_SlotBehaviour.m_Sticky.Add(new Sticky
                        {
                            m_Transform = m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform,
                            m_Count = m_SocketManager.fullResultData.features.stickyBonusValue[i].value
                        });
                //   m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = m_SocketManager.fullResultData.features.stickyBonusValue[i].value.ToString();
                //  Debug.Log($"#### Sticky Symbol: {m_SocketManager.fullResultData.features.stickyBonusValue[i].symbol} at Row: {row}, Col: {col} with Count: {m_SocketManager.fullResultData.features.stickyBonusValue[i].value} ######");
                //HACK: Note that I prefer using class instead for accessing the gameobjects.
                //NOTE: The below code line is to enable the sticky bonus internal hole
                m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(3).gameObject.SetActive
                    (
                        m_SocketManager.fullResultData.features.stickyBonusValue[i].symbol == 11 ? m_SocketManager.fullResultData.features.stickyBonusValue[i].value > 0 ? true : false : false
                    );
                //NOTE: The below code line is to enable the score text
                bool shouldShowPrize = m_SocketManager.fullResultData.features.stickyBonusValue[i].value > 0;
                m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).gameObject.SetActive(shouldShowPrize);
                // m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).gameObject.SetActive(true);

                // m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = m_SocketManager.fullResultData.features.stickyBonusValue[i].value.ToString();
                // m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).GetComponent<TMP_Text>().text = string.Concat(m_SocketManager.fullResultData.features.stickyBonusValue[i].prizeValue, "x").ToString();

                // Update texts safely
                var countText = m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
                var prizeText = m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).GetComponent<TMP_Text>();

                if (shouldShowPrize)
                {
                    countText.text = m_SocketManager.fullResultData.features.stickyBonusValue[i].value.ToString();
                    prizeText.text = $"{m_SocketManager.fullResultData.features.stickyBonusValue[i].prizeValue}x";
                }
                else
                {
                    countText.text = "";   // clear instead of showing 0 or 1
                    prizeText.text = "";
                }
            }
        }
    }

    private bool CheckSticky(Transform m_Transform)
    {
        for (int i = 0; i < m_SlotBehaviour.m_Sticky.Count; i++)
        {
            if (m_SlotBehaviour.m_Sticky[i].m_Transform == m_Transform)
            {
                return true;
            }
        }
        return false;
    }

    internal bool GetSticky(Transform m_transform, bool start_stop)
    {
        for (int i = 0; i < m_SlotBehaviour.m_Sticky.Count; i++)
        {
            if (m_SlotBehaviour.m_Sticky[i].m_Transform == m_transform)
            {
                if (start_stop && m_SlotBehaviour.m_Sticky[i].m_Count >= 0)
                {
                    Sticky sticky = m_SlotBehaviour.m_Sticky[i];
                    if (!m_SlotBehaviour.IsFreeSpin)
                    {
                        sticky.m_Count--;
                    }
                    m_SlotBehaviour.m_Sticky[i] = sticky;
                    if (m_SlotBehaviour.m_Sticky[i].m_Count == -1)
                    {
                        m_SlotBehaviour.m_Sticky[i].m_Transform.GetChild(3).gameObject.SetActive(false);
                        m_SlotBehaviour.m_Sticky.Remove(m_SlotBehaviour.m_Sticky[i]);
                        m_SlotBehaviour.m_Sticky.TrimExcess();
                        return false;
                    }
                    return true;
                }
                else
                {
                    if (!start_stop)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    #endregion

    #region FREEZE BONUS
    internal void StartFreezeBonus()
    {
        int row = 0;
        int col = 0;
        //PopulateFreeSpinResult();
        if (m_SocketManager.fullResultData.features.freeSpin.useFreeSpin || m_SlotBehaviour.IsFreeSpin)
        {
            if (!isFreezeRunning)
            {
                ResetStickyBonus();
                ResetBonus();
                isFreezeRunning = true;
                //Debug.Log("Executed...");
            }

            //if (m_SocketManager.resultData.BonusResultReel.Count > 0)
            //    PopulateFreeSpinResult();

            for (int i = 0; i < m_SocketManager.fullResultData.features.bonus.bonusSymbolValue.Count; i++)
            {
                row = m_SocketManager.fullResultData.features.bonus.bonusSymbolValue[i].position[1];
                col = m_SocketManager.fullResultData.features.bonus.bonusSymbolValue[i].position[0];

                if (!CheckFreeze(m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform))
                    m_SlotBehaviour.m_Sticky.Add(new Sticky
                    {
                        m_Transform = m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform,
                        m_Count = m_SocketManager.fullResultData.features.bonus.bonusSymbolValue[i].prizeValue
                    });
                //if(m_SocketManager.resultData.frozenIndices[i].symbol > 11 && m_SocketManager.resultData.frozenIndices[i].symbol < 18)
                if (int.Parse(m_SocketManager.fullResultData.features.bonus.bonusSymbolValue[i].symbol) < 12)
                {
                    m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).gameObject.SetActive(true);
                    m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).GetComponent<TMP_Text>().text = string.Concat(m_SocketManager.fullResultData.features.bonus.bonusSymbolValue[i].prizeValue, "x").ToString();
                }
            }

            // for (int i = 0; i < m_SocketManager.resultData.frozenIndices.Count; i++)
            // {
            //     row = m_SocketManager.resultData.frozenIndices[i].position[0];
            //     col = m_SocketManager.resultData.frozenIndices[i].position[1];

            //     if (!CheckFreeze(m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform))
            //         m_SlotBehaviour.m_Sticky.Add(new Sticky
            //         {
            //             m_Transform = m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform,
            //             m_Count = m_SocketManager.resultData.frozenIndices[i].prizeValue
            //         });
            //     //if(m_SocketManager.resultData.frozenIndices[i].symbol > 11 && m_SocketManager.resultData.frozenIndices[i].symbol < 18)
            //     if(m_SocketManager.resultData.frozenIndices[i].symbol < 12)
            //     {
            //         m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).gameObject.SetActive(true);
            //         m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform.GetChild(4).GetComponent<TMP_Text>().text = string.Concat(m_SocketManager.resultData.frozenIndices[i].prizeValue, "x").ToString();
            //     }
            // }
        }
    }

    //HACK: May be used in future with backend
    internal void PopulateFreeSpinResult() //###########commented  by pk 13 sept
    {
        // for (int i = 0; i < m_SlotBehaviour.Tempimages.Count; i++)
        // {
        //     for (int j = 0; j < m_SlotBehaviour.Tempimages[i].slotImages.Count; j++)
        //     {
        //         m_SlotBehaviour.m_ShowTempImages[i].slotImages[j].transform.GetChild(2).GetComponent<Image>().sprite = m_SlotBehaviour.myImages[int.Parse(m_SocketManager.fullResultData.bonusMatrix[i][j])];
        //     }
        // }
    }

    private bool CheckFreeze(Transform m_Transform)
    {
        for (int i = 0; i < m_SlotBehaviour.m_Sticky.Count; i++)
        {
            if (m_SlotBehaviour.m_Sticky[i].m_Transform == m_Transform)
            {
                return true;
            }
        }
        return false;
    }

    internal bool GetFreezed(Transform m_transform, bool start_stop)
    {
        for (int i = 0; i < m_SlotBehaviour.m_Sticky.Count; i++)
        {
            if (m_SlotBehaviour.m_Sticky[i].m_Transform == m_transform)
            {
                if (start_stop && m_SlotBehaviour.m_Sticky[i].m_Count >= 0)
                {
                    Sticky sticky = m_SlotBehaviour.m_Sticky[i];
                    //sticky.m_Count--;
                    m_SlotBehaviour.m_Sticky[i] = sticky;
                    if (m_SlotBehaviour.m_Sticky[i].m_Count == 0)
                    {
                        m_SlotBehaviour.m_Sticky.Remove(m_SlotBehaviour.m_Sticky[i]);
                        m_SlotBehaviour.m_Sticky.TrimExcess();
                    }
                    return true;
                }
                else
                {
                    if (!start_stop)
                    {
                        return true;
                    }
                }
            }
            else
            {
                // Debug.Log("Not found In list: "+ m_transform.name + " Parent: " + m_transform.parent.name);
            }
        }
        return false;
    }
    #endregion
    internal IEnumerator StartMoonMysteryAndMystery()
    {
        if (m_SocketManager.fullResultData.features.freeSpin.freeSpinCount == 0)
        {
            if (m_SocketManager.fullResultData.features.bonus.mysteryData.Count > 0)
            {
                yield return StartCoroutine(NewMystery());
            }
        }
    }

    // internal void StartMoonMysteryAndMystery()
    // {
    //     if (m_SocketManager.fullResultData.features.freeSpin.freeSpinCount == 0)
    //     {
    //         if (m_SocketManager.resultData.moonMysteryData.Count > 0)
    //         {
    //             StartCoroutine(Mystery());
    //         }
    //     }
    // }

    private IEnumerator NewMystery()
    {
        
        // Wait until traversal is finished
        yield return new WaitUntil(() => m_SlotBehaviour.m_CheckEndTraversal);
        yield return new WaitForSeconds(2f);

        Debug.Log("<color=red>Continuing The Traversal And Now Started Moon Mystery (New)</color>");

        isMysteryRunning = true;
        m_SlotBehaviour.InitBonusTween();

        // Get strongly typed data
        List<MysteryDatum> data = m_SocketManager.fullResultData.features.bonus.mysteryData;

        // Setup mystery items
        for (int i = 0; i < data.Count; i++)
        {
            var datum = data[i];
            int col = datum.position[1];
            int row = datum.position[0];

            // Update prize value text
            m_SlotBehaviour.m_ShowTempImages[col].slotImages[row]
                .transform.GetChild(4).GetComponent<TMP_Text>().text = (datum.prizeValue + "x");

            // Keep track of the mystery slot
            m_ListOfMystery.Add(m_SlotBehaviour.m_ShowTempImages[col].slotImages[row].gameObject);

            Debug.Log($"Mystery => Col:{col}, Row:{row}, Prize:{datum.prizeValue}, Symbol:{datum.symbol}");
        }

        // Hide elements first
        foreach (var obj in m_ListOfMystery)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.transform.GetChild(2).gameObject.SetActive(false);
        }
   //audioController.PlaySpinAudio(true);
        yield return new WaitForSeconds(1f);

        // Assign mystery symbols
        for (int i = 0; i < data.Count; i++)
        {
            var datum = data[i];
            int col = datum.position[1];
            int row = datum.position[0];

            int spriteIndex;
            if (!int.TryParse(datum.symbol, out spriteIndex))
            {
                Debug.LogWarning($"Mystery symbol '{datum.symbol}' is not numeric, defaulting to 0.");
                spriteIndex = 0;
            }

            m_SlotBehaviour.m_ShowTempImages[col].slotImages[row]
                .transform.GetChild(2).GetComponent<Image>().sprite = m_SlotBehaviour.myImages[spriteIndex];
            Debug.Log($"Mystery Symbol Applied => Col:{col}, Row:{row}, SpriteIndex:{spriteIndex}, Prize:{datum.prizeValue}");

        }

        yield return new WaitForSeconds(2f);

        // Show back with animations
        foreach (var obj in m_ListOfMystery)
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.transform.GetChild(1).gameObject.SetActive(true);
            obj.transform.GetChild(2).gameObject.SetActive(true);

            obj.transform.GetChild(1).GetComponent<ImageAnimation>().StartAnimation();
            m_SlotBehaviour.InitializeShowTweening(obj.transform.GetChild(2));
        }

        // Show multiplier text only for certain symbols
        for (int i = 0; i < data.Count; i++)
        {
            var datum = data[i];
            int col = datum.position[1];
            int row = datum.position[0];

            int spriteIndex = 0;
            int.TryParse(datum.symbol, out spriteIndex);

            if (spriteIndex > 9 && spriteIndex < 12)
            {
                // m_SlotBehaviour.m_ShowTempImages[col].slotImages[row]
                //     .transform.GetChild(4).gameObject.SetActive(true);
                var prizeText = m_SlotBehaviour.m_ShowTempImages[col].slotImages[row]
        .transform.GetChild(4).GetComponent<TMP_Text>();

                prizeText.gameObject.SetActive(true);
                prizeText.text = $"{datum.prizeValue}x";
            }
        }

        yield return new WaitForSeconds(0.8f);

        // Stop tween and clean up
        yield return m_SlotBehaviour.StopBonusTween();

        m_ListOfMystery.Clear();
        m_ListOfMystery.TrimExcess();
        m_SlotBehaviour.BalanceUpdate();
        isMysteryRunning = false;
    }

    //HACK: This Mystery Method Needs To Be Called When We Are Triggering Mystery This Is The Core Method
    private IEnumerator Mystery()
    {
        yield return new WaitUntil(() => m_SlotBehaviour.m_CheckEndTraversal);
        yield return new WaitForSeconds(2f);

        Debug.Log("<color=red>Continuing The Traversal And Now Started Moon Mystery and Mystery</color>");

        isMysteryRunning = true;
        m_SlotBehaviour.InitBonusTween();

        List<List<int>> data = m_SocketManager.resultData.moonMysteryData;

        for (int i = 0; i < m_SocketManager.resultData.moonMysteryData.Count; i++)
        {

            m_SlotBehaviour.m_ShowTempImages[data[i][1]].slotImages[data[i][2]].transform.GetChild(4).GetComponent<TMP_Text>().text = (data[i][3] + "x").ToString();
            m_ListOfMystery.Add(m_SlotBehaviour.m_ShowTempImages[data[i][1]].slotImages[data[i][2]].gameObject);
            Debug.Log(data[i][1] + " " + data[i][2] + " " + data[i][4]);
        }
        foreach (var i in m_ListOfMystery)
        {
            i.transform.GetChild(0).gameObject.SetActive(false);
            i.transform.GetChild(1).gameObject.SetActive(false);
            i.transform.GetChild(2).gameObject.SetActive(false);
            //RunTween(i.transform.GetChild(2));
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < m_SocketManager.resultData.moonMysteryData.Count; i++)
        {
            m_SlotBehaviour.m_ShowTempImages[data[i][1]].slotImages[data[i][2]].transform.GetChild(2).GetComponent<Image>().sprite = m_SlotBehaviour.myImages[data[i][4]];

        }

        yield return new WaitForSeconds(2f);

        foreach (var i in m_ListOfMystery)
        {
            i.transform.GetChild(0).gameObject.SetActive(true);
            i.transform.GetChild(1).gameObject.SetActive(true);
            i.transform.GetChild(2).gameObject.SetActive(true);

            //i.transform.GetChild(4).gameObject.SetActive(true);

            i.transform.GetChild(1).GetComponent<ImageAnimation>().StartAnimation();
            m_SlotBehaviour.InitializeShowTweening(i.transform.GetChild(2));

            //yield return new WaitForSeconds(0.6f);
        }

        for (int i = 0; i < m_SocketManager.resultData.moonMysteryData.Count; i++)
        {
            if (data[i][4] > 9 && data[i][4] < 12)
            {
                m_SlotBehaviour.m_ShowTempImages[data[i][1]].slotImages[data[i][2]].transform.GetChild(4).gameObject.SetActive(true);
            }
        }

        yield return new WaitForSeconds(0.8f);

        yield return m_SlotBehaviour.StopBonusTween();

        m_ListOfMystery.Clear();
        m_ListOfMystery.TrimExcess();
        m_SlotBehaviour.BalanceUpdate();
        isMysteryRunning = false;
    }

    internal void FreeSpinInitAnimation(bool startStop)
    {
        if (startStop)
        {
            m_FreeSpinInitAnimation.gameObject.SetActive(true);
            m_FreeSpinInitAnimation.StartAnimation();
        }
        else
        {
            m_FreeSpinInitAnimation.gameObject.SetActive(false);
            m_FreeSpinInitAnimation.StopAnimation();
        }
    }

    internal void FreeSpinExitAnimation(bool startStop)
    {
        if (startStop)
        {
            m_FreeSpinExitAnimation.gameObject.SetActive(true);
            m_FreeSpinExitAnimation.StartAnimation();
            m_SlotBehaviour.BalanceUpdate();
        }
        else
        {
            m_FreeSpinExitAnimation.gameObject.SetActive(false);
            m_FreeSpinExitAnimation.StopAnimation();
        }
    }

    internal IEnumerator FreeSpinInitAnimRoutine()
    {
        // Debug.Log("Starting Free Spin...");
        audioController.PlayWin(Sound.BigWin);
        FreeSpinInitAnimation(true);

        yield return new WaitForSeconds(2f);
        m_SlotBehaviour.InitializeBonusSlot();

        m_UIManager.FreeSpinProcess((int)m_SocketManager.fullResultData.features.freeSpin.freeSpinCount);

        FreeSpinInitAnimation(false);
    }

    internal IEnumerator FreeSpinExitAnimRoutine(string m_winningtype)
    {
        FreeSpinExitAnimation(true);

        // Debug.Log(m_SocketManager.resultData.isGrandPrize);
        // Debug.Log(m_SocketManager.resultData.isMoonJackpot);
        if ((!m_SocketManager.fullResultData.features.bonus.isGrandPrize && !m_SocketManager.fullResultData.features.bonus.isMoonJackpot && m_SocketManager.fullResultData.features.bonus.isMoonMystery))
        {
            Debug.Log($"##### Free Spins completed 354 ######");
            
            m_SocketManager.AccumulateResult(m_SlotBehaviour.BetCounter);
            Debug.Log($"##### Free Spins completed 355 ######");
            yield return new WaitUntil(() => m_SocketManager.isResultdone);
            yield return StartCoroutine(StartMoonMysteryAndMystery());
        }
      //  audioController.PlaySpinAudio(false);

        yield return new WaitForSeconds(1f);

        m_FreeSpinExitAnimation.transform.GetChild(0).gameObject.SetActive(true);
        DOTweenUIManager.Instance.FadeIn(m_FreeSpinExitAnimation.transform.GetChild(0).GetComponent<CanvasGroup>(), 1f);
        audioController.PlayGold_Enc();
        m_FreeSpinExitAnimation.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = m_winningtype;
        m_FreeSpinExitAnimation.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = ((double)m_SocketManager.fullResultData.payload.currentWinning).ToString("f3");

        yield return new WaitForSeconds(4f);

        m_FreeSpinExitAnimation.transform.GetChild(0).gameObject.SetActive(false);
        FreeSpinExitAnimation(false);
    }

    internal void ResetBonus()
    {
        isFreezeRunning = false;
        m_SlotBehaviour.m_Sticky.Clear();
        m_SlotBehaviour.m_Sticky.TrimExcess();
        Debug.Log($" resetting bonus and sticky count: {m_SlotBehaviour.m_Sticky.Count}");
    }

    internal void ResetStickyBonus()
    {
        // Clear sticky tracking list
        m_SlotBehaviour.m_Sticky.Clear();
        m_SlotBehaviour.m_Sticky.TrimExcess();

        // Loop through all slots and reset sticky visuals
        for (int row = 0; row < m_SlotBehaviour.m_ShowTempImages.Count; row++)
        {
            for (int col = 0; col < m_SlotBehaviour.m_ShowTempImages[row].slotImages.Count; col++)
            {
                var slot = m_SlotBehaviour.m_ShowTempImages[row].slotImages[col].transform;

                // Reset sticky hole (child 3)
                var stickyObj = slot.GetChild(3).gameObject;
                stickyObj.SetActive(false);

                // Reset count text (child 3 -> child 0)
                var countText = slot.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
                countText.text = string.Empty;

                // Reset prize text (child 4)
                var prizeObj = slot.GetChild(4).gameObject;
                prizeObj.SetActive(false);

                var prizeText = slot.GetChild(4).GetComponent<TMP_Text>();
                prizeText.text = string.Empty;
            }
        }

        Debug.Log("Sticky Bonus reset successfully.");
    }
}
