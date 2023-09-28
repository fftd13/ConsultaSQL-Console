using ConexaoBancoDeDados;

string connectionstring = "Server=localhost;Database=master;Trusted_Connection=True;";
string tablename = "Clientes";

SqlController sqlctrl = new SqlController(connectionstring, tablename);

string? opc = "";
while (opc != "6")
{
    //Console.Clear();
    Console.WriteLine("_________________________________________________");
    Console.WriteLine("Select = 1 | Update = 2 | Fields = 3 | Insert = 4");
    Console.WriteLine("Delete = 5 | Sair = 6");
    Console.Write("opç: ");
    opc = Console.ReadLine();

    Console.WriteLine("___________________________________________");

    if (opc == "1")
    {
        List<string> table = sqlctrl.SelectAll();

        foreach (var item in table)
        {
            Console.WriteLine(item);
        }
        
    }
    else if (opc == "2")
    {
        Console.Write("Qual campo deseja alterar ?: ");
        string? campo = Console.ReadLine();

        Console.Write("Digite o novo valor: ");
        string? valor = Console.ReadLine();        

        Console.Write("Digite a condição: ");
        string? cond = Console.ReadLine();

        sqlctrl.Update(valor, campo, cond);
    }
    else if (opc == "3")
    {
        Console.WriteLine("Todos os Campos:");
        List<string> campos = sqlctrl.Fields();
        foreach (var item in campos)
        {
            Console.WriteLine(item);
        }
    }
    else if (opc == "4")
    {
        List<string?> valores = new List<string?>();
        Console.WriteLine("Digite os dados do novo cliente abaixo");
        for (int i = 0; i < sqlctrl.Fields().Count; i++)
        {
            Console.Write($"{sqlctrl.Fields()[i]}: ");
            string? resp = Console.ReadLine();
            valores.Add(resp);
        }
        sqlctrl.Insert(valores);
    }
    else if(opc == "5")
    {
        Console.Write("Digite a condiçao: ");
        string? cond = Console.ReadLine();
        sqlctrl.Delete(cond);

    }

    
}

