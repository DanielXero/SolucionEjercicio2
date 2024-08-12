
// Declaracion de una matriz para almacenar las temeraturas diarias


int[,] temperaturasDiariasMes = new int[5, 7];
// Declaracion de una lista para almacenar los promediod de temperaturas semanales
List<double> temperaturasPromedioSemanales = new List<double>();
// Declaracion de una lista para alamacenar temeraturas por encima de cierto umbral
List<int> temperaturasAltas = new List<int>();

int numOpcion;

bool isFinished = false;


Console.WriteLine("\t\t***** Weather Forecast Mejorado *****\n\n");

Console.WriteLine("Ingrese temperatura para un mes completo (31 días)");
do
{
    numOpcion = GetIntegerDataFromUser("1. Ingresar temperatura manualmente\n2. Ingresar temperatura aleatoriamente\nOpcion: ");

} while (numOpcion < 1 || numOpcion > 2);

InicializarTemperaturasDiarias(temperaturasDiariasMes, numOpcion);

MostrarTemperaturasMes(temperaturasDiariasMes);

Console.Write("\n\nPresione cualquier tecla para continuar...");
Console.ReadLine();

Console.Clear();


do
{
    Console.WriteLine("\t\t***** Weather Forecast Mejorado *****\n\n");

    Console.WriteLine("1. Generar temperaturas diarias para un mes completo");
    Console.WriteLine("2. Visualizar temperatura segun un día especifico");
    Console.WriteLine("3. Visualizar promedio temperatura semanales");
    Console.WriteLine("4. Buscar temperaturas por encima de cierto umbral");
    Console.WriteLine("5. Mostrar temperatura promedio mes");
    Console.WriteLine("6. Mostrar temperatura más alta y la más baja del mes");
    do
    {
        numOpcion = GetIntegerDataFromUser("Elige una opcion (1-3): ");

    } while (numOpcion < 0 || numOpcion > 7);



    switch (numOpcion)
    {
        case 1:
            do
            {
                numOpcion = GetIntegerDataFromUser("1. Ingresar temperatura manualmente\n2. Ingresar temperatura aleatoriamente\nOpcion: ");

            } while (numOpcion < 1 || numOpcion > 2);
            InicializarTemperaturasDiarias(temperaturasDiariasMes, numOpcion);
            MostrarTemperaturasMes(temperaturasDiariasMes);
            break;
        case 2:

            MostrarTemperaturasMes(temperaturasDiariasMes);
            int diaMes = GetIntegerDataFromUser("Ingrese el día del mes que desea ver su temperatura(1-31): ");
            int temperatura = ObtenerTemperaturaEspecifica(diaMes, temperaturasDiariasMes);
            string datosTemp = ObtenerDetalleTemperatura(temperatura);
            Console.WriteLine($"El día {diaMes} su temperatura fue {temperatura}°C {datosTemp}");
            break;
        case 3:

            temperaturasPromedioSemanales = CalcularTemperaturaPromedio(temperaturasDiariasMes);
            Console.WriteLine("Promedio temperatura semanal");
            for (int i = 0; i < temperaturasPromedioSemanales.Count; i++)
            {
                Console.WriteLine($"Semana {(i + 1)}: {temperaturasPromedioSemanales[i]}°C");
            }
            break;
        case 4:
            int buscar = GetIntegerDataFromUser("Ingrese el umbral de temperatura que desea buscar: ");
            temperaturasAltas = ObtenerTemperaturas(temperaturasDiariasMes, buscar);
            Console.Write($"Listado de temperatura por encima de {buscar}°C: ");


            foreach (var temp in temperaturasAltas)
            {
                Console.Write($"{temp} ");
            }
            Console.WriteLine();
            break;
        case 5:
            MostrarTemperaturasMes(temperaturasDiariasMes);
            Console.WriteLine($"\n\nEl promedio de temperatura del mes es: {CalcularPromedioMensual(temperaturasDiariasMes)}");
            break;
        case 6:
            MostrarTemperaturasMes(temperaturasDiariasMes);
            Dictionary<string, int> maxMinTemps = ObtenerTemperaturasExtremas(temperaturasDiariasMes);
            Console.WriteLine("\n\n");
            foreach (var item in maxMinTemps)
            {
                Console.WriteLine($"Temperatura {item.Key} -> {item.Value}°C");
            }
            break;
        default:
            break;

    }


    do
    {
        numOpcion = GetIntegerDataFromUser("\n\nDesea continuar con la ejecucion del programa (1-> 'Si' | 2-> 'No')?: ");

    } while (numOpcion < 1 || numOpcion > 2);


    if (numOpcion == 2)
    {
        Console.WriteLine("\nEl programa \"Weather Forecast Mejorado\" ha finalizado. Gracias por su participación.");
        isFinished = true;
    }
    else
    {
        Console.Clear();
    }


} while (!isFinished);







void InicializarTemperaturasDiarias(int[,] matriz, int numOpcion)
{
    int auxCount = 1;
    Random random = new Random();
    double numeroAleatorio = Math.Round(random.NextDouble() * (random.Next(-15, 45)), 1);

    if (numOpcion == 1)
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                if (auxCount <= 31)
                {
                    
                    matriz[i, j] = GetIntegerDataFromUser($"Ingrese temperatura día {auxCount}: ");

                }
                auxCount++;

            }
        }
    } else if (numOpcion == 2) 
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                if (auxCount <= 31)
                {
                   
                    matriz[i, j] = random.Next(-20, 40);

                }
                auxCount++;

            }
        }

    }


}


int ObtenerTemperaturaEspecifica(int dia, int[,] matriz)
{
    int contDias = 0;
    int temp = 0;

    for (int i = 0; i < matriz.GetLength(0); i++)
    {
        for (int j = 0; j < matriz.GetLength(1); j++)
        {
            if (contDias == (dia-1))
            {
                temp = matriz[i, j];

               
            }
            contDias++;
        }
    }

    


    return temp;
}





string ObtenerDetalleTemperatura(int temp)
{
    string msg = "";
    switch (temp)
    {
        case < 0:
            msg += "- Hizo mucho frío.";
            break;
        case >= 0 and <= 20:
            msg += "- El clima estaba fresco.";
            break;
        case > 20:
            msg += "- Hizo calor afuera.";
            break;
                   
    }

    return msg;
}


void MostrarTemperaturasMes(int[,] matriz)
{

    Console.WriteLine("\n\n\t***** Calendadario de temperatura del mes *****\n\n");
    int contDias = 1;
    Console.WriteLine("DO\tLU\tMA\tMI\tJU\tVI\tSA");
    for (int i = 0; i < matriz.GetLength(0); i++)
    {
        for (int j = 0; j < matriz.GetLength(1); j++)
        {
            if (contDias <= 31)
            {
                Console.Write($"{matriz[i, j]}°C\t");

            }
            contDias++;
            

        }
        Console.WriteLine();
    }
}



/// Opción para calcular y ver temperaturas promedios de cada semana. Aquí debes usar otra colección para el almacenamiento.

List<double> CalcularTemperaturaPromedio(int[,] matriz)
{

    List<double> listTemperaturasPromedio = new List<double>();
    int acumTemperaturas;
    double promedioSemanal = 0.0;
    int contDias = 1, auxDias = 0;

    for (int i = 0; i < matriz.GetLength(0); i++)
    {
        acumTemperaturas = 0;
        auxDias = 0;
        for (int j = 0; j < matriz.GetLength(1); j++)
        {
            if(contDias <= 31)
            {
                acumTemperaturas += matriz[i, j];
                auxDias++;
            }
            
            

            contDias++;


        }

        promedioSemanal = Math.Round(acumTemperaturas / (double) auxDias, 2);
        listTemperaturasPromedio.Add(promedioSemanal);

    }


    return listTemperaturasPromedio;
}


/// Implementar una función para encontrar las temperaturas por encima de un umbral (20°) y almacenarlas en una colección.




List<int> ObtenerTemperaturas(int[,] matriz, int umbral)
{
    List<int> listTemperaturas = new List<int>();
    int contDias = 1;
    for (int i = 0; i < matriz.GetLength(0); i++)
    {

        for (int j = 0; j < matriz.GetLength(1); j++)
        {

            if (matriz[i, j] > umbral && contDias <= 31)
            {
                listTemperaturas.Add(matriz[i, j]);
            }
            contDias++;

        }
    }

    return listTemperaturas;


}

// Implementar una función para calcular la temperatura promedio del mes.

double CalcularPromedioMensual(int[,] matriz)
{
    int acumTemperaturas = 0;
    double promedioMes = 0.0;
    int contDias = 1;
    for (int i = 0; i < matriz.GetLength(0); i++)
    {

        for (int j = 0; j < matriz.GetLength(1); j++)
        {

            if (contDias <= 31)
            {
                acumTemperaturas += matriz[i, j];
            }
            contDias++;

        }
    }
    promedioMes = Math.Round(acumTemperaturas / 31.0, 2);

    return promedioMes;


}






/// Implementar una función para encontrar la temperatura más alta y la más baja.

Dictionary<string, int> ObtenerTemperaturasExtremas(int[,] matriz)
{
    Dictionary<string, int> tempsMaxMin = new Dictionary<string, int>() { { "max", 0 }, { "min", 0 } };
    int max = matriz[0, 0], min = matriz[0,0];

    int contDias = 1;
    for (int i = 0; i < matriz.GetLength(0); i++)
    {

        for (int j = 0; j < matriz.GetLength(1); j++)
        {

            if (contDias <= 31 && max < matriz[i, j])
            {
               max = matriz[i, j];
            } else if ((contDias <= 31 && min > matriz[i, j])) {
                min = matriz[i, j];
            }
            contDias++;

        }
    }

    tempsMaxMin["max"] = max;
    tempsMaxMin["min"] = min;

    return tempsMaxMin;

}


int GetIntegerDataFromUser(string message)
{
    string? userData;
    int data = 0;
    bool isDataValid = false;


    while (!isDataValid)
    {
        Console.Write(message);
        userData = Console.ReadLine();

        if (!Int32.TryParse(userData, out data))
        {
            Console.WriteLine("El dato que ingresaste no es valido. Vuelve a intentarlo");
        }
        else
        {
            isDataValid = true;
        }


    }

    return data;
}

