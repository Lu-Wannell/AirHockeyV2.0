using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public int CountdownTime;

    public Text TimerTxt;
    public GameObject PlayerHitter;
    public GameObject AiHitter;

    public void Timer()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        TimerTxt.gameObject.SetActive(true);
        CountdownTime = 3;
        while ( CountdownTime > 0)
        {
            TimerTxt.text = CountdownTime.ToString();
            PlayerHitter.GetComponent<PlayerMovement>().enabled = false;
            AiHitter.GetComponent<AIScript>().enabled = false;

            yield return new WaitForSeconds(1f);

            CountdownTime--;
        }

        TimerTxt.text = "GO!";
        PlayerHitter.GetComponent<PlayerMovement>().enabled = true;
        AiHitter.GetComponent<AIScript>().enabled = true;


        yield return new WaitForSeconds(1f);

        TimerTxt.gameObject.SetActive(false);
    }
}
