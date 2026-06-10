using System;
using System.Collections.Generic;

class Program
{
    private bool _continue;
    private Random _random;

    public Program()
    {
        _continue = true;
        _random = new Random();
    }

    public void Run()
    {
        List<Reference> refs = new List<Reference>
        {
            new Reference("Joshua", 1, 9),
            new Reference("Article of Faith", 13, 1),
            new Reference("Doctrine and Covenants", 115, 5),
            new Reference("Doctrine and Covenants", 87, 8),
            new Reference("Moroni", 10, 32),
            new Reference("Doctrine and Covenants", 4, 2),
            new Reference("2 Nephi", 31, 20),
            new Reference("James", 1, 5),
            new Reference("Doctrine and Covenants", 19, 23),
            new Reference("John", 14, 15),
            new Reference("1 Nephi", 3, 7),
            new Reference("Doctrine and Covenants", 64, 33, 34),
            new Reference("Proverbs", 3, 5, 6),
            new Reference("Philippians", 4, 13),
            new Reference("3 Nephi", 5, 13),
            new Reference("Doctrine and Covenants", 6, 36),
            new Reference("Moses", 6, 34)
        };

        List<string> texts = new List<string>
        {
            "Be strong and of a good courage; be not afraid, neither be thou dismayed: for the Lord thy God is with thee whithersoever thou goest.",
            "We believe in being honest, true, chaste, benevolent, virtuous, and in doing good to all men.",
            "Arise and shine forth, that thy light may be a standard for the nations.",
            "Stand ye in holy places, and be not moved, until the day of the Lord come.",
            "Come unto Christ, and be perfected in him, and deny yourselves of all ungodliness.",
            "Therefore, O ye that embark in the service of God, see that ye serve him with all your heart, might, mind and strength.",
            "Wherefore, ye must press forward with a steadfastness in Christ, having a perfect brightness of hope, and a love of God and of all men.",
            "If any of you lack wisdom, let him ask of God, that giveth to all men liberally, and upbraideth not; and it shall be given him.",
            "Learn of me, and listen to my words; walk in the meekness of my Spirit, and you shall have peace in me.",
            "If ye love me, keep my commandments.",
            "And by the power of the Holy Ghost ye may know the truth of all things.",
            "And out of small things proceedeth that which is great. Behold, the Lord requireth the heart and a willing mind.",
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.",
            "I can do all things through Christ which strengtheneth me.",
            "And now, I would that ye should be humble, and submissive and gentle; easy to be entreated; full of patience and long-suffering.",
            "Look unto me in every thought; doubt not, fear not.",
            "And thou shalt abide in me, and I in you; therefore walk with me.",
            "For behold, this is my work and my glory—to bring to pass the immortality and eternal life of man."
        };

        int i = _random.Next(refs.Count);
        Scripture scr = new Scripture(refs[i], texts[i]);

        while (_continue)
        {
            ClearScreen();
            DisplayScripture(scr);

            if (scr.IsCompletelyHidden())
            {
                Console.WriteLine();
                Console.Write("1 = go again, 2 = quit: ");
                string input = Console.ReadLine();

                bool again = input != null && input.Trim() == "1";

                if (again)
                {
                    int j = _random.Next(refs.Count);
                    scr = new Scripture(refs[j], texts[j]);
                }
                else
                {
                    _continue = false;
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write("Press enter to continue or type quit: ");
                string input = Console.ReadLine();

                if (input != null && input.Trim().ToLower() == "quit")
                {
                    _continue = false;
                }
                else
                {
                    scr.HideRandomWords(3);
                }
            }
        }
    }

    private void ClearScreen()
    {
        Console.Clear();
    }

    private void DisplayScripture(Scripture scr)
    {
        scr.Display();
    }

    static void Main(string[] args)
    {
        Program p = new Program();
        p.Run();
    }
}