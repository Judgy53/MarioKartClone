using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMgr : MonoSingleton<UIMgr> {

    [SerializeField]
    private Canvas PauseMenu = null;
    [SerializeField]
    private Canvas InGameInterface = null;
    [SerializeField]
    private Canvas RaceCountDown = null;


	// Use this for initialization
	void Start () {
        if (PauseMenu == null)
            Debug.Log("Please bind a pause menu to the UI manager.");

        if (InGameInterface == null)
            Debug.Log("Please bind an in-game interface to the UI manager.");

        Resume();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Pause()
    {
        InGameInterface.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenu.gameObject.SetActive(false);
        InGameInterface.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public IEnumerator RaceCountDownDisplay()
    {
        RaceCountDown.gameObject.SetActive(true);

        Text[] numbers = RaceCountDown.GetComponentsInChildren<Text>();

        Text[] orderedNums = new Text[3];

        for (int ite = 0; ite < 3; ++ite)
            orderedNums[int.Parse(numbers[ite].text)-1] = numbers[ite];

        for (int step = 0; step < 3; ++step)
        {
            int num = 3 - step - 1; // 0 = 1...

            for (int frame = 0; frame < 5; ++frame) {
                orderedNums[num].rectTransform.anchoredPosition += new Vector2(-10f, 10f);

                /*Shadow newShadow = orderedNums[num].gameObject.AddComponent<Shadow>();
                newShadow.effectDistance = new Vector2(20f, -20f);
                Color shadowColor = newShadow.effectColor;
                shadowColor.a = 1f;
                newShadow.effectColor = shadowColor;*/

                yield return new WaitForSeconds(0.2f);
            }

            orderedNums[num].color = new Color(0.8f, 0.1f, 0.1f);
        }

        yield return new WaitForSeconds(1f);

        for (int num = 0; num < 3; ++num)
            orderedNums[num].color = new Color(0.3f, 1f, 0.3f);

        yield return new WaitForSeconds(1f);

        RaceCountDown.gameObject.SetActive(false);
    }

    public void EndOfLapDisplay()
    {
        InGameInterface.BroadcastMessage("EndOfLap");
    }

    public void NoUI()
    {
        PauseMenu.gameObject.SetActive(false);
        InGameInterface.gameObject.SetActive(false);
        RaceCountDown.gameObject.SetActive(false);
    }
}
