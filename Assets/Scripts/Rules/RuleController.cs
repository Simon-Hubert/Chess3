using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
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

    private void Awake()
    {
        ruleDestroy.SetRule();
    }
}
