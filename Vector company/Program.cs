using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

public class Worker
{
    private int _rang;
    private bool _isBoss;
    private double _salary;
    private double _coffee;
    private int _pages;


    public int Rang { get { return _rang; } set { _rang = value; } }

    public bool IsBoss { get { return _isBoss; } set { _isBoss = value; } }

    public double Salary
    {
        get
        {
            return _salary;
        }
        set
        {
            _salary = Math.Round(Math.Round(value * (1 + 0.25 * (Rang - 1)), MidpointRounding.AwayFromZero) * (1 + 0.5 * (IsBoss ? 1 : 0)), MidpointRounding.AwayFromZero);
        }
    }

    public double Coffee
    {
        get
        {
            return _coffee;
        }
        set
        {
            _coffee = Math.Round(value * (1 + (IsBoss ? 1 : 0)), MidpointRounding.AwayFromZero);
        }
    }

    public int Pages
    {
        get
        {
            return _pages;
        }
        set
        {
            _pages = (int)Math.Round((decimal)value * (IsBoss ? 0 : 1), MidpointRounding.AwayFromZero);
        }
    }
}

public class Manager : Worker
{
    public Manager(int rang, bool isBoss)
    {
        Rang = rang;
        IsBoss = isBoss;
        Salary = 50000;
        Coffee = 20;
        Pages = 200;
    }
}

public class Marketer : Worker
{
    public Marketer(int rang, bool isBoss)
    {
        Rang = rang;
        IsBoss = isBoss;
        Salary = 40000;
        Coffee = 15;
        Pages = 150;
    }
}

public class Ingineer : Worker
{
    public Ingineer(int rang, bool isBoss)
    {
        Rang = rang;
        IsBoss = isBoss;
        Salary = 20000;
        Coffee = 5;
        Pages = 50;
    }
}

public class Analyst : Worker
{
    public Analyst(int rang, bool isBoss)
    {
        Rang = rang;
        IsBoss = isBoss;
        Salary = 80000;
        Coffee = 50;
        Pages = 5;
    }
}

public class Department
{
    private string _name = "Всего";
    private List<Worker> _workers = new List<Worker>();
    private List<int> _quantity = new List<int>();

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }
    }
    public List<Worker> Workers { get { return _workers; } }
    public List<int> Quantity { get { return _quantity; } }

    public Department(string name)
    {
        Name = name;
    }

    public void Add(Worker worker_t, int n)
    {
        _workers.Add(worker_t);
        _quantity.Add(n);
    }

    public int GetWorkerQuantity()
    {
        return _quantity.Sum();
    }

    public double GetSalary()
    {
        double sum = 0;
        int i = 0;
        foreach (Worker worker in _workers)
        {
            sum += worker.Salary * _quantity[i++];
        }
        return sum;
    }

    public double GetCoffee()
    {
        double sum = 0;
        for(int i = 0; i < _workers.Count(); ++i)
        {
            sum += _workers[i].Coffee * _quantity[i];
        }
        return sum;
    }

    public double GetPages()
    {
        double sum = 0;
        int i = 0;
        foreach (Worker worker in _workers)
        {
            sum += worker.Pages * _quantity[i++];
        }
        return sum;
    }

    public double GetPageCost()
    {
        return Math.Round(GetSalary() / GetPages(), 2, MidpointRounding.AwayFromZero);
    }
}

class Program
{
    static void Main()
    {
        string[] titles = { "Департамент", "Сотрудников", "Тугрики", "Кофе", "Страницы", "Тугр./стр." };
        var sb = new StringBuilder();
        sb.AppendJoin("     ",titles);
        sb.Append('\n');
        sb.Append('-', 76);
        sb.Append('\n');

        

        List<Department> departmentList = new List<Department>();

        while(true)
        {
            var text = Console.ReadLine();
            if (String.IsNullOrEmpty(text))
                break;
            Department dep = GetDepartment(text);
            departmentList.Add(dep);
            sb.Append($"{dep.Name,-16}{dep.GetWorkerQuantity(),-16}{dep.GetSalary(),-12}{Math.Round(dep.GetCoffee(), MidpointRounding.AwayFromZero),-9}{Math.Round(dep.GetPages(), MidpointRounding.AwayFromZero),-13}{dep.GetPageCost()}");
            sb.Append('\n');
        }

        double salarySum = 0, totalCoffee = 0, totalPages = 0;
        int totalWorkers = 0;
        foreach(var department in departmentList) 
        {
            totalWorkers += department.GetWorkerQuantity();
            salarySum += department.GetSalary();
            totalCoffee += department.GetCoffee();
            totalPages += department.GetPages();
        }
        sb.Append('-', 76);
        sb.Append('\n');
        sb.Append($"{"Всего",-16}{totalWorkers,-16}{Math.Round(salarySum, MidpointRounding.AwayFromZero),-12}{Math.Round(totalCoffee, MidpointRounding.AwayFromZero),-9}{totalPages,-13}{Math.Round(salarySum/totalPages, 2, MidpointRounding.AwayFromZero)}");
        Console.WriteLine(sb.ToString());
    }

    static Department GetDepartment(string info)
    {
        var dep = new Department(info.Split(": ")[0].Split(" ")[1]);
        foreach (var str in info.Split(": ")[1].Split(" + руководитель департамента ")[0].Split(", "))
        {
            var arr = str.Split("*");
            int n = int.Parse(arr[0]);
            int rang = (arr[1])[arr[1].Length - 1] - '0';
            string workerType = arr[1].Remove(arr[1].Length - 1);
            switch (workerType)
            {
                case "manager":
                    dep.Add(new Manager(rang, false), n);
                    break;
                case "marketer":
                    dep.Add(new Marketer(rang, false), n);
                    break;
                case "engineer":
                    dep.Add(new Ingineer(rang, false), n);
                    break;
                case "analyst":
                    dep.Add(new Analyst(rang, false), n);
                    break;
            }

        }
        string boss = info.Split(" + руководитель департамента ")[1];
        int brang = int.Parse((boss[boss.Length - 1]).ToString());
        boss = boss.Remove(boss.Length - 1);
        switch (boss)
        {
            case "manager":
                dep.Add(new Manager(brang, true), 1);
                break;
            case "marketer":
                dep.Add(new Marketer(brang, true), 1);
                break;
            case "engineer":
                dep.Add(new Ingineer(brang, true), 1);
                break;
            case "analyst":
                dep.Add(new Analyst(brang, true), 1);
                break;
        }
        return dep;
    }
}