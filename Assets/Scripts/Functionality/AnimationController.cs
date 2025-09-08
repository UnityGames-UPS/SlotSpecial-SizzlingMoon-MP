using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    internal GameObject m_CoverPanel;
    [SerializeField]
    private SlotBehaviour m_SlotManager;
    [SerializeField]
    private SocketIOManager SocketManager;
    [SerializeField]
    private UIManager m_UIManager;
    [SerializeField]
    internal List<SlotImage> m_ShowTempImages;
    private List<string> m_SymbolsToEmit = new List<string>();

    internal void StartAnimation()
    {
        int row = 0;
        int col = 0;
        m_SymbolsToEmit = SocketManager.fullResultData.features.winningSymbols;
        m_CoverPanel.SetActive(true);
        if(m_SymbolsToEmit.Count > 0)
        {
            for (int i = 0; i < SocketManager.fullResultData.features.winningSymbols.Count; i++)
            {
                row = int.Parse(SocketManager.fullResultData.features.winningSymbols[i].Split(',')[0]);
                col = int.Parse(SocketManager.fullResultData.features.winningSymbols[i].Split(',')[1]);
                //PopulateAnimationSprites(m_ShowTempImages[col]
                //    .slotImages[row].transform.GetChild(2).GetComponent<ImageAnimation>(),
                //    GetValueFromMatrix(row, col)
                //    );
                GameObject obj = m_ShowTempImages[col].slotImages[row].gameObject;
                obj.SetActive(true);
                obj.transform.GetChild(1).GetComponent<ImageAnimation>().StartAnimation();
                obj.transform.GetChild(2).GetComponent<Image>().sprite = m_SlotManager.myImages[int.Parse(SocketManager.fullResultData.matrix[row][col])];

            }
        }
    }

    internal void ResetAnimation()
    {
        m_CoverPanel.SetActive(false);
        for(int i = 0; i < m_ShowTempImages.Count; i++)
        {
            for (int j = 0; j < m_ShowTempImages[i].slotImages.Count; j++)
            {
                m_ShowTempImages[i].slotImages[j].gameObject.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public struct Cord
{
    public int i;
    public int j;
}

[System.Serializable]
public struct AnimCords
{
    public List<Cord> m_Cords;
}
