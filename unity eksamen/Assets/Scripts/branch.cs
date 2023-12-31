using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class branch : MonoBehaviour
{
    public List<Sprite> buttonImages;
    public gameManager gameManager;
    public GameObject buttonPrefab;
    public List<skill> skills = new List<skill>();
    
    public characterManager charMan;
    public killZone orbScript;

    public List<GameObject> buttons = new List<GameObject>();

    public void fakeStart(){
        gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        for (int i = 0; i < skills.Count; i++)
        {   
            int index = i;
            GameObject newButton =Instantiate(buttonPrefab);
            buttons.Add(newButton);
            newButton.transform.SetParent(gameObject.transform);
            newButton.GetComponent<buttonScript>().setSkillAndBranch(skills[index],this,index);
            skills[index].setCharMan(charMan);   
            skills[index].setOrb(orbScript);   
            }
            createBtnOne();
    }
    public void activate_skill(int skillToAdd)
    {
        skill skillToActivate = skills[skillToAdd];
        if (skillToActivate.price <= gameManager.levelSys.getTokens())
        {
            if (skillToActivate.activate())
            {
                gameManager.levelSys.removeTokens(skillToActivate.price);
            };
        }

    }
    private void createBtnOne(){
        Button tempBtn =skills[0].button;
        buttonScript tempScript=skills[0].button.GetComponentInParent<buttonScript>();
        tempBtn.interactable=true;
        tempScript.changeImg(1);
    }
}
