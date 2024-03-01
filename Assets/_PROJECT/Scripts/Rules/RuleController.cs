using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

public class RuleController : MonoBehaviour
{
    public enum RULES
    {
        VIP,
        ESCAPE,
        DESTROY,
    }
    [SerializeField] RULES rule;
    [SerializeField, ShowIf("rule", RULES.VIP)] Rule_VIP ruleVip;
    [SerializeField, ShowIf("rule", RULES.DESTROY)] Rule_Destroy ruleDestroy;
    [SerializeField, ShowIf("rule", RULES.ESCAPE)] Rule_Escape ruleEscape;
    [SerializeField, ShowIf(EConditionOperator.Or, "renforts", "isEscape")] Renforts ruleRenforts;
    [SerializeField, HideIf("rule", RULES.ESCAPE)] bool renforts = false;
    bool isEscape = false;

    public Rule_Escape RuleEscape { get => ruleEscape; set => ruleEscape = value; }

    private void OnValidate()
    {
        if (rule == RULES.ESCAPE)
        {
            isEscape = true;
            renforts = false;
        }
        else
            isEscape = false;
        if((isEscape || renforts) && ruleRenforts == null)
        {
            this.AddComponent<Renforts>();
            ruleRenforts = GetComponent<Renforts>();
        }
        else
        {
            if(ruleRenforts != null && renforts == false)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(ruleRenforts);
                };
            }

        }
    }


    private void Awake()
    {
        ruleDestroy.UpdateList();
        if(rule == RULES.VIP)
        {
            Eat.OnEat += ruleVip.UpdateBlackList;
        }
        else if(rule == RULES.DESTROY)
        {
            Eat.OnEat += ruleDestroy.UpdateList;
        }
    }
    private void OnDisable()
    {
        if (rule == RULES.VIP)
        {
            Eat.OnEat -= ruleVip.UpdateBlackList;
        }
        else if (rule == RULES.DESTROY)
        {
            Eat.OnEat -= ruleDestroy.UpdateList;
        }
    }

    public IRules GetCurrentRule() 
    {
        switch (rule)
        {
            case RULES.ESCAPE:
                return ruleEscape;
            default:
            case RULES.DESTROY:
                return ruleDestroy;
            case RULES.VIP:
                return ruleVip;
        }
    }
}
