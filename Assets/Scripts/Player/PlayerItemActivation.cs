using System.Collections;
using System.Collections.Generic;
using Items;
using scripts;
using UnityEngine;

public class PlayerItemActivation : MonoBehaviour
{
    public static bool isPlayerHasItem;

    private void Update()
    {
        ItemCheck();

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(DataItems.shoe) //Item Shoe
            {
                StartCoroutine("SpeedActive");
            }

            else if(DataItems.oil)
            {
                StartCoroutine("OilOn");
            }
        }
    }

    void ItemCheck()
    {
        if (DataItems.oil || DataItems.obstacle || DataItems.shield || DataItems.shoe || DataItems.waterBall > 0)
        {
            isPlayerHasItem = true;
            //Debug.Log("player got item");
        }
        else
        {
            isPlayerHasItem = false;
            //Debug.Log("player doesn't got item");
        }    

        if(DataItems.shoe)
        {
            Debug.Log("Sepatu_Dapet");
        }
        else if(DataItems.oil)
        {
            Debug.Log("Oli_Dapet");
        }
        
        if(GameObject.Find("CurrentItem") != null)
        {
            //Debug.Log("Ada");
        }
    }

    public IEnumerator SpeedActive()
    {
        PlayerMovement.speed.Value = 10;
        yield return new WaitForSeconds(5);
        PlayerMovement.speed.Value = 5;
        DataItems.shoe = false;
    }

    public IEnumerator OilOn()
    {
        PlayerMovement.speed.Value = 3;
        yield return new WaitForSeconds(5);
        DataItems.oil = false;
        PlayerMovement.speed.Value = 5;
    }
}
