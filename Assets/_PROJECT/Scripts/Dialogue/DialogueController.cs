using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _npcNameText;
    [SerializeField] private TextMeshProUGUI _npcConvoText;

    [SerializeField][Range(0, 50)] private float _typeSpeed = 20f;
    private const float MAX_TYPE_TIME = 0.1f;

    [SerializeField] private bool _convoEnded;
    private bool _isTyping;

    [SerializeField] private string _currentParagraph;

    [SerializeField] private Queue<string> _paragraphsQueue = new Queue<string>();

    private Coroutine TypeDialogueRoutine;
    

    private void OnValidate()
    {
        if (_typeSpeed == 0)
        {
            Debug.LogWarning("La vitesse d'écrit ne peut pas etre 0.");
            _typeSpeed = 10;
            
        }
    }

    public void DisplayNextParagraph(DialoguePrompt prompt)
    {
        //if there is nothing in the queue
        if (_paragraphsQueue.Count == 0)
        {
            if (!_convoEnded)
            {
                StartConvo(prompt);
            }
            else if (_convoEnded && !_isTyping)
            {
                EndConvo(prompt);
                return;
            }
        }

        //something in the queue
        if(!_isTyping)
        {
            _currentParagraph = _paragraphsQueue.Dequeue();//supprime et renvoie l’élément en tête de la file d’attente.
            TypeDialogueRoutine = StartCoroutine(TypeDialogueText(_currentParagraph));
        }

        //convo is still being typed
        else
        {
            FinishParagraphEarly();
        }


        if (_paragraphsQueue.Count == 0)// ran through all paragraphs
        {
            _convoEnded = true;
        }
    }

    private void StartConvo(DialoguePrompt prompt)
    {
        //activate ui
        if (!gameObject.activeSelf) {

            gameObject.SetActive(true);

        }
        //update speaker name
        _npcNameText.text = prompt._speakerName;

        //put paragraph in queue
        for (int i = 0; i < prompt._paragraphs.Length; i++) {

            _paragraphsQueue.Enqueue(prompt._paragraphs[i]);

        }
    }

    private void EndConvo(DialoguePrompt prompt)
    {
        //clear the queue
        _paragraphsQueue.Clear();

        _convoEnded = false;

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogueText(string p)
    {
        _isTyping = true;

        int maxVisibleChars = 0;

        _npcConvoText.text = p;
        _npcConvoText.maxVisibleCharacters = maxVisibleChars;

        foreach (char c in p.ToCharArray())
        {

            maxVisibleChars++;
            _npcConvoText.maxVisibleCharacters = maxVisibleChars;

            yield return new WaitForSeconds(MAX_TYPE_TIME / _typeSpeed);
        }

        _isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        StopCoroutine(TypeDialogueRoutine);
        _npcConvoText.maxVisibleCharacters = _currentParagraph.Length;
        _isTyping = false;
    }


}