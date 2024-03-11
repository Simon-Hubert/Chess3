using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;
    [SerializeField] private float _interactDist = 10f;

    private Transform _playerTransform;


    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsWithinInteractDistance()){

            Interact();// supposed to talk
        }

        if (!_interactSprite.gameObject.activeSelf && IsWithinInteractDistance())//si le sprite est desac mais player assez proche
        {
            _interactSprite.gameObject.SetActive(true);// active le sprite
        }

        else if(_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            _interactSprite.gameObject.SetActive(false);
        }
    }


    public abstract void Interact();
    
    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(_playerTransform.position, transform.position) < _interactDist)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

}
