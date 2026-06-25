using System;

class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public Word(string text, bool isHidden)
    {
        _text = text;
        _isHidden = isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return GetBlankText();
        }

        return _text;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    private string GetBlankText()
    {
        string blank = "";

        for (int i = 0; i < _text.Length; i++)
        {
            blank += "_";
        }

        return blank;
    }
}