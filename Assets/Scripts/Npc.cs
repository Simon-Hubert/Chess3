using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;
    [SerializeField] private float _interactDist;

    private Transform _playerTransform;


    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsWithinInteractDistance()){

            Interact();
        }

        if (!_interactSprite.gameObject.activeSelf && IsWithinInteractDistance())//si le sprite est desac mais player assez proche
        {
            _interactSprite.gameObject.SetActive(true);// active le sprite
        }
        else if(_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            _interactSprite.gameObject.SetActive(false);
        }
        Debug.Log("Distance pllayer, npc : " + _interactDist);
    }


    public abstract void Interact();
    
    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(transform.position, _playerTransform.position) < _interactDist)
        {
            return true;
        }
        return false;
    }

}
