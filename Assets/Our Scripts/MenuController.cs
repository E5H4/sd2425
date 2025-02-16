using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void BulletWoundBtn()
    {
        Debug.Log("Button Clicked!");
        SceneManager.LoadScene("BulletWound");
    }

    public void HeartAttackBtn()
    {
        SceneManager.LoadScene("HeartAttack");
    }

    public void HypoShockBtn()
    {
        SceneManager.LoadScene("HypoglycemicShock");
    }

    public void StrokeBtn()
    {
        SceneManager.LoadScene("Stroke");
    }
}
