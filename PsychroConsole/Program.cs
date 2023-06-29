using System;
using System.Runtime.CompilerServices;

double tempDry, tempWet, pres, humidity, speed, alfa, pd, pw, dw, dd;
ConsoleKeyInfo key;
do
{
    tempDry = Input("Введiть показник сухого термометра в градусах Цельсiя:");
    tempWet = Input("Введiть показник вологого термометра в градусах Цельсiя:");
    pres = Input("Введiть показник барометра (атмосферного тиску) в мм.рт.ст :");
    speed = Input("Введiть швидкiсть вiтру (штиль = 0.13, буревiй або психрометр Ассмана = 5) :");
    alfa = 1e-6 * (593.1 + (135.1 / Math.Sqrt(speed)) + (48 / speed)); //Залежнiсть психрометричного коефiцiєнта вiд швидкостi руху повiтря на м/с, формула Зворикiна
    pd = 4.5845 * Math.Exp((tempDry * (18.678 - (tempDry / 234.5))) / (257.14 + tempDry)); //парцiальний тиск водяної пари при температурi сухого термометра 
    pw = 4.5845 * Math.Exp((tempWet * (18.678 - (tempWet / 234.5))) / (257.14 + tempWet)); //парцiальний тиск водяної пари при температурi вологого термометра
    dd = (288.97 * pd) / (273.15 + tempDry); //густина насиченої водяної пари при температурi сухого термометра, г/м³
    dw = (288.97 * pw) / (273.15 + tempWet); //густина насиченої водяної пари при температурi вологого термометра, г/м³
    humidity = 100 * ((dw / dd) - alfa * pres * (tempDry - tempWet) / dd); //обчислення вiдносної вологостi повiтря
    Console.WriteLine($"Вiдносна вологiсть повiтря:{humidity:f2}% ");

    Console.Write("Натиснiть Esc для виходу, або будь-яку клавiшу для розрахункiв за iншими даними...");
    key = Console.ReadKey();
    Console.WriteLine();
}
while (key.Key != ConsoleKey.Escape);


double Input(string s)
{
    double x;bool b;
    do
    {
        Console.Write(s);
        if (b = !double.TryParse(Console.ReadLine(), out x))
        {
            Console.WriteLine("Введенi данi мають бути числом. Повторiть введеня");
        }
    }
    while (b);
    return x;
}

