using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace AdvGameLib
{
    public class Chapter1 : IActions
    {
        public string inventory = "";
        public string location = "";
        public bool isHole = false;
        public string command;
        bool chapterComplete = false;
        Verb verb = new Verb();
        NounCh1 noun = new NounCh1();
        byte verbByte;
        byte nounByte;

        public void Talk(byte noun)
        {
        }
        public void Shoot(byte noun)
        {
            switch (noun)
            {
                case 1:
                    if (location == "mirror")
                    {
                        WriteLine("I shoot finger guns at the mirror...\nThe guy in the mirror copies me...\nHow rude!");
                    }
                    else
                    {
                        WriteLine("I can't shoot the mirror from here!");
                    }
                    break;
                case 2:
                    if (location == "table")
                    {
                        WriteLine("I shoot finger guns at the table...\nIt plays dead.");
                    }
                    else
                    {
                        WriteLine("I can't shoot the table from here!");
                    }
                    break;
                case 4:
                    if (isHole == true)
                        WriteLine("I shoot finger guns into the hole...\nNothing happens.");
                    else
                        WriteLine("What hole?");
                    break;
                default:
                    WriteLine("I shoot finger guns...\nAnd miss! :(");
                    break;
            }
        }
        public void Use(byte noun, byte verb)
        {
            if (inventory == "")
            {
                if (location == "mirror")
                {
                    if (verb == 2 || verb == 7 || verb == 6)
                    {
                        switch (noun)
                        {
                            case 1:
                                WriteLine("I looked in the mirror to see what i saw...\nAcquired a Saw.");
                                inventory = "Saw";
                                break;
                            case 2:
                                WriteLine("The table is too far away for me to look at properly...Maybe if I got closer?");
                                break;
                            default:
                                WriteLine("I'm not sure what to do here.");
                                break;
                        }
                    }
                    else
                        WriteLine("I can't do that here");
                }
                else if (location == "table")
                {
                    if (isHole == false)
                    {
                        WriteLine("This table looks really sturdy...\nI'm not sure what to do with it though...");
                    }
                    else
                    {
                        WriteLine("There is already a hole\nMaybe i can travel through it?");
                    }
                }
                else if (location == "")
                {
                    if (verb == 2 || verb == 6 || verb == 7)
                    {
                        switch (noun)
                        {
                            case 1:
                                WriteLine("The mirror is too far away for me to see properly...Maybe if I got closer?");
                                break;
                            case 2:
                                WriteLine("The table is too far away for me to get a proper look...Maybe I should get closer?");
                                break;
                            default:
                                WriteLine("I'm not sure what to do here.");
                                break;
                        }
                    }
                    else
                        WriteLine("I'm can't do that here.");
                }
            }
            else
            {
                if (location == "mirror")
                {
                    switch (noun)
                    {
                        case 1:
                        case 3:
                            if (verb == 7)
                                WriteLine($"I hold the {inventory} in front of the mirror...\nI look really handy!");
                            else
                                WriteLine("I'm not sure what to do here.");
                            break;
                        case 2:
                            WriteLine("The table is too far away for me to see properly...Maybe if I was closer?");
                            break;
                        default:
                            WriteLine("I'm not sure what to do here...");
                            break;
                    }
                }
                else if (location == "table")
                {
                    switch (noun)
                    {
                        case 3:
                        case 2:
                            if (verb == 2 || verb == 8 || verb == 10)
                            {
                                if (isHole == false)
                                {
                                    WriteLine($"I use the {inventory} to cut a hole in the table...\nIt seems i can travel through here...");
                                    WriteLine("The saw disappeared in my hands...\nStrange that!");
                                    isHole = true;
                                    inventory = "";
                                }
                                else
                                {
                                    WriteLine("There is already a hole!...\nMaybe i can travel through it?");
                                }
                            }
                            else
                                WriteLine("I don't know what I am meant to be doing");
                            break;
                    }
                }
            }
        }
        public void Go(byte noun)
        {
            switch (noun)
            {
                case 1:
                    location = "mirror";
                    WriteLine("I approach the mirror...\nIt is a rather large mirror.");
                    break;
                case 2:
                    location = "table";
                    if (isHole == false)
                    {
                        WriteLine("I approach the table...\nIt seems sturdy");
                        break;
                    }
                    else
                    {
                        WriteLine("I climb through the hole...\nI wonder where it leads??");
                        chapterComplete = true;
                        break;
                    }
                case 4:
                    if (location == "table")
                    {
                        if (isHole == true)
                        {
                            WriteLine("I climb through the hole...\nI wonder where it leads??");
                            chapterComplete = true;
                            break;
                        }
                        else
                        {
                            WriteLine("What hole?");
                            break;
                        }
                    }
                    else
                    {
                        WriteLine("What hole?");
                        break;
                    }
                default:
                    WriteLine("I can't do that...");
                    break;
            }
        }
        public void MainLogic()
        {
            WriteLine("I woke up in a concrete room with no windows or doors...");
            WriteLine("All that was in this room is a mirror and a table to keep me company...\nHow did I get out??");
            while (chapterComplete == false)
            {
                WriteLine("What should i do?");
                command = ReadLine();
                if (command.ToLower() == "quit")
                    goto EndOfLogic;
                string[] instruction = command.Split(' ');
                if (Enum.TryParse(instruction[0], true, out verb))
                {
                    if (Enum.IsDefined(typeof(Verb), verb))
                    {
                        if (Enum.TryParse(instruction[instruction.Count() - 1], true, out noun))
                        {
                            if (Enum.IsDefined(typeof(NounCh1), noun))
                            {
                                noun = (NounCh1)Enum.Parse(typeof(NounCh1), instruction[instruction.Count() - 1], true);
                                verb = (Verb)Enum.Parse(typeof(Verb), instruction[0], true);
                                verbByte = (byte)verb;
                                nounByte = (byte)noun;
                                switch (verbByte)
                                {
                                    case 1:
                                        Go(nounByte);
                                        break;
                                    case 2:
                                    case 7:
                                    case 9:
                                    case 10:
                                        Use(nounByte, verbByte);
                                        break;
                                    case 4:
                                    case 5:
                                        Shoot(nounByte);
                                        break;
                                    case 3:
                                        WriteLine("I have no one to talk to...*sad noises*");
                                        break;
                                    default:
                                        WriteLine("I'm not sure what I am doing here...");
                                        break;
                                }
                            }
                        }
                        else
                            WriteLine("I don't know what you want me to do\nHINT!: pay attention to your surroundings to find out what you might have gotten wrong.");
                    }

                }
                else
                    WriteLine("I didn't quite hear what you said at first...\nTry again.\nHINT!: check the tutorial document for accepted verb parameters.");
            }
            Chapter2Pt1 ch2P1 = new Chapter2Pt1();
            WriteLine("Inside the hole in the table was a portal...Fancy that!");
            WriteLine("Travelling through the portal leads to...");
            WriteLine("......................................................");
            ch2P1.MainLogic();
        EndOfLogic:
            ReadKey();
        }
    }
}