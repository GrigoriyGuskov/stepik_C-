using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;


public class Character
{
    private string _name;
    private int _power;
    private int _dexterity;
    private int _survivability;
    private int _intellect;
    private int _healthPoints;
    private int _magicEnergyPoints;
    private int _armor;
    private int _magicArmor;

    public string Name { get { return _name; } set { _name = value; } }
    public int Power { get { return _power; } set { _power = value; } }
    public int Dexterity { get { return _dexterity; } set { _dexterity = value; } }
    public int Survivability { get { return _survivability; } set { _survivability = value; } }
    public int Intellect { get { return _intellect; } set { _intellect = value; } }
    public int HealthPoints { get { return _healthPoints; } set { _healthPoints = value; } }
    public int MagicEnergyPoints { get { return _magicEnergyPoints; } set { _magicEnergyPoints = value; } }
    public int Armor { get { return _armor; } set { _armor = value; } }
    public int MagicArmor { get { return _magicArmor; } set { _magicArmor = value; } }


    public virtual string GetName()
    {
        return Name;
    }

    public virtual int Attack(Character target)
    {
        return target.Defense(0, 0);
    }

    public int Defense(int damage, int magicDamage)
    {
        damage -= Armor + Dexterity;
        if (damage < 0)
            damage = 0;
        magicDamage -= MagicArmor + Intellect;
        if (magicDamage < 0)
            magicDamage = 0;
        HealthPoints -= damage + magicDamage;
        if (HealthPoints < 0)
        {
            HealthPoints = 0;
        }
        return damage + magicDamage;
        
    }

    public int GetHp()
    {
        return HealthPoints + Armor;
    }
}

public class Knight : Character
{
    public Knight(int pwr, int dxtr, int srv, int intel, string name) 
    {
        Name = name;
        Power = pwr+2;
        Dexterity = dxtr;
        Survivability = srv;
        Intellect = intel;
        HealthPoints = Survivability*4 + 15;
        MagicEnergyPoints = Intellect * 4;
        Armor = Dexterity / 2 + 2;
        MagicArmor = Intellect / 2;
    }

    public override int Attack(Character enemy)
    {
        Console.WriteLine(GetName() + " attacking " + enemy.GetName() + " with sword.");
        int damage = 5 + Power;
        return enemy.Defense(damage, 0);
    }
    public override string GetName()
    {
        return "Knight " + Name;
    }
}

public class Thief : Character
{
    public Thief(int pwr, int dxtr, int srv, int intel, string name)
    {
        Name = name;
        Power = pwr;
        Dexterity = dxtr+3;
        Survivability = srv;
        Intellect = intel;
        HealthPoints = Survivability * 4;
        MagicEnergyPoints = Intellect * 4;
        Armor = Dexterity / 2;
        MagicArmor = Intellect / 2;
    }
    public override int Attack(Character enemy)
    {
        Console.WriteLine(GetName() + " attacking " + enemy.GetName() + " with dagger.");
        int damage = 4 + Dexterity;
        return enemy.Defense(damage, 0);
    }
    public override string GetName()
    {
        return "Thief " + Name;
    }
}

public class Mage : Character
{
    public Mage(int pwr, int dxtr, int srv, int intel, string name)
    {
        Name = name;
        Power = pwr;
        Dexterity = dxtr;
        Survivability = srv;
        Intellect = intel + 5;
        HealthPoints = Survivability * 4;
        MagicEnergyPoints = Intellect * 4 + 25;
        Armor = Dexterity / 2;
        MagicArmor = Intellect / 2 + 2;
    }

    public bool IsEnoughMagicEnergy()
    {
        return MagicEnergyPoints >= 40;
    }
    public override int Attack(Character enemy)
    {
        int damage = 0, magicDamage = 0;
        if (IsEnoughMagicEnergy())
        {
            Console.WriteLine(GetName() + " attacking " + enemy.GetName() + " with chain lightning.");
            magicDamage = Intellect + 10;
        }
        else
        {
            Console.WriteLine(GetName() + " attacking " + enemy.GetName() + " with staff.");
            damage = 15 + Power;
        }
        return enemy.Defense(damage,magicDamage);
    }
    public override string GetName()
    {
        return "Mage " + Name;
    }
}


class Program
{
    static void Main()
    {
        string text = Console.ReadLine();
        List<Character> heroes = new List<Character>();
        List<Character> enemies = new List<Character>();
        bool plfl = false;
        while(text != "end")
        {
            if(text == "hero")
            {
                for(text = Console.ReadLine(); text != "end" && text != "hero" && text != "enemy"; text = Console.ReadLine())
                {
                    var array = text.Split(" ");
                    int pwr = int.Parse(array[1]);
                    int dxt = int.Parse(array[2]);
                    int srv = int.Parse(array[3]);
                    int intel = int.Parse(array[4]);
                    string name = array[5];
                    for (int i = 6; i < array.Length; ++i)
                        name += " " + array[i];
                    switch(array[0]) 
                    {
                        case "knight":
                            heroes.Add(new Knight(pwr, dxt, srv, intel, name));
                            break;
                        case "thief":
                            heroes.Add(new Thief(pwr, dxt, srv, intel, name));
                            break;
                        case "mage":
                            heroes.Add(new Mage(pwr, dxt, srv, intel, name));
                            break;
                    }
                }
            }
            if (text == "enemy")
            {
                for (text = Console.ReadLine(); text != "end" && text != "hero" && text != "enemy"; text = Console.ReadLine())
                {
                    var array = text.Split(" ");
                    int pwr = int.Parse(array[1]);
                    int dxt = int.Parse(array[2]);
                    int srv = int.Parse(array[3]);
                    int intel = int.Parse(array[4]);
                    string name = array[5];
                    for (int i = 6; i < array.Length; ++i)
                        name += " " + array[i];
                    switch (array[0])
                    {
                        case "knight":
                            enemies.Add(new Knight(pwr, dxt, srv, intel, name));
                            break;
                        case "thief":
                            enemies.Add(new Thief(pwr, dxt, srv, intel, name));
                            break;
                        case "mage":
                            enemies.Add(new Mage(pwr, dxt, srv, intel, name));
                            break;
                    }
                }
            }
        }

        Console.WriteLine("Stay a while and listen, and I will tell you a story. A story of Dungeons and Dragons, of Orcs and Goblins, of Ghouls and Ghosts, of Kings and Quests, but most importantly -- of Heroes and NoobCo -- Well... A story of Heroes.");

        string description = "So here starts the journey of our hero";
        if (heroes.Count > 1)
        {
            plfl = true;
            description += "es:";
        }
        description += " " + heroes[0].GetName();
        for (int i = 1; i < heroes.Count; ++i)
            description += ", " + heroes[i].GetName();
        description += " got order to eliminate the local";
        if(enemies.Count > 1)
        {
            description += " gang consists of well known bandits: " + enemies[0].GetName();
            for (int i = 1; i < enemies.Count; ++i)
                description += ", " + enemies[i].GetName();
        }
        else
        {
            description += " bandit known as " + enemies[0].GetName();
        }
        description += ".";
        Console.WriteLine(description);
        if (plfl)
        {
            description = "";
            description += heroes[0].GetName();
            for (int i = 1; i < heroes.Count; ++i)
                description += ", " + heroes[i].GetName();
            description += " engaged the ";
            description += enemies[0].GetName();
            for (int i = 1; i < enemies.Count; ++i)
                description += ", " + enemies[i].GetName();
            description += ".";
            Console.WriteLine(description);
        }
        //battle
        int turn = 0;
        while (heroes.Count > 0 && enemies.Count > 0)
        {
            int target;
            if(turn % 2 == 0)
            {
                for (int i = 0; i < heroes.Count && heroes.Count > 0 && enemies.Count > 0; ++i)
                {
                    if (heroes[i] is Mage && ((Mage)heroes[i]).IsEnoughMagicEnergy())
                    {
                        for(int j = 0; j < enemies.Count; ++j)
                        {
                            Console.WriteLine($"{enemies[j].GetName()} get hit for {heroes[i].Attack(enemies[j])} hp and have {enemies[j].HealthPoints} hp left!");
                            if (enemies[j].HealthPoints == 0)
                                Console.WriteLine(enemies[j].GetName() + " is defeated!");
                        }
                        heroes[i].MagicEnergyPoints -= 40;
                    }
                    else
                    {
                        target = GetTarget(enemies);
                        Console.WriteLine($"{enemies[target].GetName()} get hit for {heroes[i].Attack(enemies[target])} hp and have {enemies[target].HealthPoints} hp left!");
                        if (enemies[target].HealthPoints == 0)
                            Console.WriteLine(enemies[target].GetName() + " is defeated!");
                    }
                    for (int k = enemies.Count - 1; k >= 0; --k)
                    {
                        if (enemies[k].HealthPoints == 0)
                            enemies.RemoveAt(k);
                    }
                }
                
            }
            else
            {
                for (int i = 0; i < enemies.Count && heroes.Count > 0 && enemies.Count > 0; ++i)
                {
                    if (enemies[i] is Mage && ((Mage)enemies[i]).IsEnoughMagicEnergy())
                    {
                        for (int j = 0; j < heroes.Count; ++j)
                        {
                            Console.WriteLine($"{heroes[j].GetName()} get hit for {enemies[i].Attack(heroes[j])} hp and have {heroes[j].HealthPoints} hp left!");
                            if (heroes[j].HealthPoints == 0)
                                Console.WriteLine(heroes[j].GetName() + " is defeated!");
                        }
                        enemies[i].MagicEnergyPoints -= 40;
                    }
                    else
                    {
                        target = GetTarget(heroes);
                        Console.WriteLine($"{heroes[target].GetName()} get hit for {enemies[i].Attack(heroes[target])} hp and have {heroes[target].HealthPoints} hp left!");
                        if (heroes[target].HealthPoints == 0)
                            Console.WriteLine(heroes[target].GetName() + " is defeated!");
                    }
                    for (int k = heroes.Count - 1; k >= 0; --k)
                    {
                        if (heroes[k].HealthPoints == 0)
                            heroes.RemoveAt(k);
                    }

                }
                
            }
            ++turn;
        }

        if (enemies.Count == 0)
            Console.WriteLine("Congratulations!");
        else if (plfl)
            Console.WriteLine("Unfortunately our heroes were brave, yet not enough skilled, or just lack of luck.");
        else
            Console.WriteLine("Unfortunately our hero was brave, yet not enough skilled, or just lack of luck.");
            

    }

    public static int GetTarget(List<Character> chars)
    {
        int min = int.MaxValue, index = -1;
        for (int i = 0; i < chars.Count; ++i) 
        {
            if (chars[i].GetHp() < min)
            {
                min = chars[i].GetHp();
                index = i;
            }

        }
        return index;
    }
}