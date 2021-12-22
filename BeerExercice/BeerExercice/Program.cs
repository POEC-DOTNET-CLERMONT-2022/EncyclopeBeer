// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//int input;

//if (int.TryParse(Console.ReadLine(), out input)) // Un null, un espace ou une tab ne passe pas ce test 
//{
//    Console.WriteLine(input); 
//}
//else
//{
//    Console.WriteLine("La valeur entrée n'est pas un int. Réessayez.");
//}

//if (int.TryParse(null, out input))
//{
//    Console.WriteLine("Un int peut etre null");
//}
//else
//{
//    Console.WriteLine("La valeur entrée n'est pas un int. Réessayez.");
//}

int ReadIntFromUser()
{
    Console.WriteLine("Choisissez un int : ");

    int index;
    if (int.TryParse(Console.ReadLine(), out index))
    {
        Console.WriteLine("On return!");
        return index;
    }
    else
    {
        Console.WriteLine("La valeur entrée n'est pas un int. Réessayez.");
        return ReadIntFromUser();
    }
}
Console.WriteLine(ReadIntFromUser());
