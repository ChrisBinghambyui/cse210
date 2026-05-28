using System;
using System.Collections.Generic;

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = GetWords(text);
        _random = new Random();
    }

    public void Display()
    {
        Console.WriteLine(_reference.GetDisplayText());
        Console.WriteLine(GetDisplayText());
    }

    public void HideRandomWords(int count)
    {
        List<int> open = new List<int>();

        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                open.Add(i);
            }
        }

        int total = count;
        if (total > open.Count)
        {
            total = open.Count;
        }

        for (int i = 0; i < total; i++)
        {
            int index = _random.Next(open.Count);
            int wordIndex = open[index];
            _words[wordIndex].Hide();
            open.RemoveAt(index);
        }
    }

    public bool IsCompletelyHidden()
    {
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                return false;
            }
        }

        return true;
    }

    private string GetDisplayText()
    {
        string text = "";

        for (int i = 0; i < _words.Count; i++)
        {
            text += _words[i].GetDisplayText();

            if (i < _words.Count - 1)
            {
                text += " ";
            }
        }

        return text;
    }

    private List<Word> GetWords(string text)
    {
        List<Word> words = new List<Word>();
        string[] parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < parts.Length; i++)
        {
            words.Add(new Word(parts[i]));
        }

        return words;
    }
}