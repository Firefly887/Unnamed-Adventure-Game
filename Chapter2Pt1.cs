using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AdvGameLib
{
    public class Chapter2Pt1 : IActions
    {
        ArrayList inventory = new ArrayList();
        bool chapterComplete = false;
        public string location = "bridge";
        int gateCount = 0;
        int talkCountGate = 0;
        bool maskEquipped = false;
        byte nounByte;
        byte verbByte;
        Verb verb = new Verb();
        NounCh2 noun = new NounCh2();
        public void Shoot(byte noun)
        {
            switch (noun)
            {
                case 1:
                    if (location == "bridge")
                        WriteLine("I shoot finger guns at the bridge...\nBut no-one is around to see how cool i am :(");
                    else
                        WriteLine("I can't shoot the bridge from here!");
                    break;
                case 2:
                    if (location == "gate")
                        WriteLine("I shoot finger guns at the gate...\nThe guard shakes his head at me");
                    else
                        WriteLine("I can't shoot the gate from here!");
                    break;
                case 3:
                    if (location == "gate")
                        WriteLine("I shoot finger guns at the guard...\nHe shoots them back! *SO COOL!*");
                    else
                        WriteLine("I can't shoot the guard from here!");
                    break;
                case 5:
                    if (location == "bridge")
                        WriteLine("I shoot finger guns into the void...\nThe void does nothing :(");
                    else
                        WriteLine("The portal is too far away for that!");
                    break;
                default:
                    WriteLine("I shoot finger guns...\nAnd miss! :(");
                    break;
            }
        }
        public void CheckInventory()
        {
            Clear();
            if (inventory.Count == 0)
                WriteLine("I have nothing in my inventory!");
            else
            {
                WriteLine("In my inventory I currently have:");
                foreach (string item in inventory)
                {
                    WriteLine($"{item}\n");
                }
            }
        }
        public void Talk(byte noun)
        {
            switch (noun)
            {
                case 3:
                    if (location == "gate")
                    {
                        GateTalk();
                    }
                    else
                    {
                        WriteLine("I can't talk to him here...");
                    }
                    break;
                default:
                    WriteLine("I have no-one to talk to (so lonely *pew pew*)");
                    break;
            }
        }
        public void Use(byte noun, byte verb)
        {
            if (location == "bridge")
            {
                switch (noun)
                {
                    case 5:
                        WriteLine("Why would I bother going back?\nI've done all i can there...");
                        break;
                    case 4:
                        WriteLine("I'm already wearing the mask...");
                        break;
                    case 6:
                        CheckInventory();
                        break;
                    default:
                        WriteLine("I can't use that this way");
                        break;
                }
            }
            else if (location == "gate")
            {
                switch (noun)
                {
                    case 5:
                        WriteLine("I can't get to the portal from here.");
                        break;
                    case 4:
                        WriteLine("I'm already wearing the mask...");
                        break;
                    default:
                        WriteLine("I can't use that this way");
                        break;
                }
            }
        }
        public void Search(byte noun)
        {
            switch (noun)
            {
                case 1:
                case 4:
                    if (location == "bridge")
                    {
                        WriteLine("I search the bridge for a mask...\nAfter some time I finally found one...\nIt's a little dirty...but it should do!");
                        inventory.Add("Dirty Mask");
                        maskEquipped = true;
                        WriteLine("I put on the Dirty Mask\nIt smells a little...Funky!");
                    }
                    else if (location == "gate")
                    {
                        if (noun == 4)
                            WriteLine("I try searching for a mask at the gate...\nBut the place is pretty barren;\nand the guard is looking at me in a funny way.\nMaybe i should look on the bridge?");
                        else if (noun == 1)
                            WriteLine("I can't search the bridge from here!");
                    }
                    break;
                case 3:
                    if (location == "bridge")
                    {
                        WriteLine("There are no guards on the bridge");
                    }
                    else if (location == "gate")
                    {
                        WriteLine("I can't search the guard!\nI'll get arrested!");
                    }
                    break;
                default:
                    WriteLine("I can't search that!");
                    break;
            }
        }
        public void Go(byte noun)
        {
            switch (noun)
            {
                case 1:
                    if (location == "bridge")
                    {
                        Clear();
                        WriteLine("I am already on the bridge...");
                    }
                    else
                    {
                        Clear();
                        WriteLine("I head to the bridge...");
                        location = "bridge";
                        BridgeTravel();
                    }
                    break;
                case 2:
                    if (location == "gate")
                    {
                        Clear();
                        WriteLine("I am already at the gate...");
                    }
                    else
                    {
                        Clear();
                        WriteLine("I head toward the gate...");
                        location = "gate";
                        GateTravel();
                    }
                    break;
                case 5:
                    Clear();
                    if (location == "bridge")
                        WriteLine("There is nothing for me there...");
                    break;
                case 7:
                    Clear();
                    if (location == "gate")
                    {
                        if (maskEquipped == true)
                        {
                            WriteLine("I head through the checkpoint...\nOnce I am through I see...\n========================");
                            chapterComplete = true;
                        }
                        else
                        {
                            WriteLine("I can't get through the checkpoint without a mask!");
                        }
                    }
                    else
                    {
                        WriteLine("I can't go through the checkpoint if i am not at  the gate");
                    }
                    break;
                default:
                    WriteLine("I have no idea.");
                    break;
            }
        }
        public void MainLogic()
        {
            Clear();
            WriteLine("Somewhere bright! (my eyes they burn!)\nOnce my eyes adjust I see I am stood on a bridge;\nWith a guard by the gate on the other side...");
            while (chapterComplete == false)
            {
                string command = ReadLine();
                if (command.ToLower() == "quit")
                    goto EndOfLogic;
                string[] instruction = command.Split(' ');
                if (Enum.TryParse(instruction[0], true, out verb))
                {
                    if (Enum.IsDefined(typeof(Verb), verb))
                    {
                        if (Enum.TryParse(instruction[instruction.Count() - 1], true, out noun))
                        {
                            if (Enum.IsDefined(typeof(NounCh2), noun))
                            {
                                noun = (NounCh2)Enum.Parse(typeof(NounCh2), instruction[instruction.Count() - 1], true);
                                verb = (Verb)Enum.Parse(typeof(Verb), instruction[0], true);
                                verbByte = (byte)verb;
                                nounByte = (byte)noun;
                                switch (verbByte)
                                {
                                    case 1:
                                        Go(nounByte);
                                        break;
                                    case 2:
                                        Use(nounByte, verbByte);
                                        break;
                                    case 3:
                                        Talk(nounByte);
                                        break;
                                    case 9:
                                    case 7:
                                        Search(nounByte);
                                        break;
                                    case 6:
                                        if (nounByte == 6)
                                            CheckInventory();
                                        else
                                            WriteLine("I have nothing i can check.");
                                        break;
                                    case 5:
                                    case 4:
                                        Shoot(nounByte);
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
        EndOfLogic:
            ReadKey();
        }
        public void BridgeTravel()
        {
            WriteLine("The bridge is quiet...\nNobody seems to be around...");
        }
        public void GateTravel()
        {           
            if (maskEquipped == false)
            {
                if (gateCount == 0)
                {
                    WriteLine("The guard eyes me suspicously...\nBefore he utters:");
                    WriteLine("\"Keep ya distance lad! T'is an outbreak of plague we be dealin' wi'\nYou'll be needin' a face covrin' if yer wanna pass...\"");
                    gateCount++;
                    WriteLine("I guess I'll have to go back and look for a mask then...");
                }
                else if (gateCount == 5)
                {
                    WriteLine("The guard looks sympathetically...\n\"I still cannae give you entry wi'out a face covrin'\"");
                    gateCount++;
                }
                else
                {
                    WriteLine("I still can't let ye in wi'out a face covrin' sonny!");
                    gateCount++;
                }
            }
            else
            {
                WriteLine("The guard says happily \"On your way lad\"\nBehind him is a checkpoint");
                WriteLine("The guard looks like he wants to ask me something...\nMaybe I should talk to him?");
            }
        }
        public void GateTalk()
        {
            switch (talkCountGate)
            {
                case 0:
                    WriteLine("The guard asks...\n\"I don't s'pose yer could do me a favour?...\nSee I'm mighty parched and could do wi' sommat to wet me w'istle\"");
                    WriteLine("I tell the guard I shall do what I can.");
                    talkCountGate++;
                    break;
                case 10:
                    WriteLine("The guard bursts out with \n\"We have to stop meeting like this squire\"");
                    talkCountGate++;
                    break;
                default:
                    WriteLine("I don't suppose you have managed to source that drink have you?");
                    talkCountGate++;
                    break;
            }
        }
    }
}
