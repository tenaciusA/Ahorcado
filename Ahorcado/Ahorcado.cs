using System;
using System.IO;
using System.Threading;

namespace Ahorcado
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cabeza [2,4] Tronco [3,4] Brazo Izq [3,3] Brazo Dch [3,5] Pierna Izq [4,3] Pierna Dch [4,4]            
            // Horca ternimada -> string[,] horca = {{"_","_","_","_","_"," "," "},{"|","/"," "," ","|"," "," "},{"|"," "," "," ","O"," "," "},{"|"," "," ","/","|","|"," "},{"|"," "," ","/","|"," "," "},{"|","_","_","_","_","_","_"},{"|","|","|","|","|","|","|"}};

            // Inicio del juego
            // Pide la palabra |  /   |  / /_  ____  ______________ _____/ /___| 
            string palabra="";
            inicio();
            Console.WriteLine("\n\nJuguemos al ¡¡¡¡AHORCADO!!!!\n");
            Console.WriteLine("\t"+"   <O> ¡Noooo!  ");
            Console.WriteLine("\t" + "    |    ");
            Console.WriteLine("\t"+@"   / \   " + "\n\n");

            Console.WriteLine("¿Quieres jugar solo, o hay alguien tan aburrido como tú?");
            Console.WriteLine("\nElige S para Solo, o A para Acompañado");
            string opcion;
            do
            {
                opcion = Console.ReadLine().ToUpper();
                if (opcion == "S")
                {
                    Random random = new Random();
                    FileStream diccionario = new FileStream("diccionario.txt", FileMode.Open, FileAccess.Read);
                    StreamReader palrand = new StreamReader(diccionario);
                    int numrand = random.Next(0, 5);

                    for (int i = 0; i <= numrand; i++)
                    {
                        palabra = palrand.ReadLine().ToUpper();
                    }
                    palrand.Close();
                    diccionario.Close();
                }
                else if (opcion == "A")
                {
                    Console.Clear();
                    inicio();
                    Console.WriteLine("\n\nEscribe la palabra que quieres que adivine el otro. ¡Cuidado que no mire que hay much@ tramposill@!\n");

                    palabra = Console.ReadLine().ToUpper();
                }
                else
                {
                    Console.WriteLine("\n\nEmpezamos mal si de dos letras no sabes elegir una...");
                    Console.WriteLine("\nPrueba otra vez anda");
                                       
                }
                
            } while (opcion != "S" && opcion != "A");



            // Crea un array con guiones igual de largo que la palabra

            char[] palabraOculta = new char[palabra.Length];
            ocultarPalabra(palabra, palabraOculta);

            // Muestra la horca y los guiones    

            string[,] horca = { { "_", "_", "_", "_", "_", " ", " " }, { "|", "/", " ", " ", "|", " ", " " }, { "|", " ", " ", " ", " ", " ", " " }, { "|", " ", " ", " ", " ", " ", " " }, { "|", " ", " ", " ", " ", " ", " " }, { "|", "_", "_", "_", "_", "_", "_" }, { "|", "|", "|", "|", "|", "|", "|" } };

            // Pide la letra 
            Console.Clear();
            inicio();
            mostrarHorca(horca);
            mostrarPalabra(palabraOculta);
            int cont = 0;
            bool terminada = false;
            while (cont <= 5)
            {




                Console.WriteLine("\nOk, escribe una letra que crees que pueda estar en la palabra secreta\n");
                bool entLetra;
                bool repetida;
                char letra;

                // Validacion y no repeticion de dato
                do
                {
                    repetida = false;
                    entLetra = char.TryParse(Console.ReadLine().ToUpper(), out letra);
                    if (!entLetra)
                    {
                        Console.WriteLine("Escribe sólo una letra\n");
                    }
                    for (int i = 0; i < palabra.Length; i++)
                    {

                        if (letra == palabraOculta[i])
                        {
                            Console.WriteLine("Esa ya la has puesto, hay que leer las cosas hombre. Venga ahora en serio\n");
                            repetida = true;
                            break;

                        }

                    }
                } while (repetida || !entLetra);

                // Comprueba si esta en la palabra

                bool encontrada = false;
                for (int i = 0; i < palabra.Length; i++)
                {

                    if (letra == palabra[i])
                    {

                        encontrada = true;

                        palabraOculta[i] = Convert.ToChar(palabra[i]);
                        Console.Clear();
                        inicio();
                        mostrarHorca(horca);
                        mostrarPalabra(palabraOculta);
                        Console.WriteLine("Vaaale vamos por buen camino\n");

                    }

                }





                //  Comprueba si se han puesto todas las letras

                for (int i = 0; i < palabraOculta.Length; i++)
                {

                    if (palabraOculta[i] == '-')
                    {
                        terminada = false;
                        break;

                    }
                    terminada = true;
                }


                if (!encontrada)
                { // Si no está dibuja una parte del muñeco y muestra la horca
                    cont++;
                    Console.Clear();
                    inicio();
                    dibujarMuñeco(cont, ref horca);
                    Console.WriteLine();
                    mostrarHorca(horca);
                    mostrarPalabra(palabraOculta);

                    Thread.Sleep(1500);
                }

                // Comprueba si conoce la solución y la verifica                
                else if (terminada)
                {
                    Console.Clear();
                    inicio();
                    Console.WriteLine("\nLa palabra secreta era ¡¡¡{0}!!!", palabra.ToUpper());
                    fin();

                }
                else
                {

                    Console.WriteLine("\nDime, por el bien del pobre monigote que sabes qué palabra es. ¡Venga escríbela!");
                    Console.WriteLine("\nSi aún no la sabes pulsa INTRO\n");

                    string solucion = Console.ReadLine().ToUpper();
                    if (solucion == palabra)
                    {
                        Console.Clear();
                        inicio();
                        Console.WriteLine("\nLa palabra secreta era ¡¡¡{0}!!!", palabra.ToUpper());
                        fin();
                        break;
                    }
                    else if (solucion == "")
                    {
                        Console.Clear();
                        inicio();
                        mostrarHorca(horca);
                        mostrarPalabra(palabraOculta);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\nNoooo, vamos piensa venga");
                        Thread.Sleep(1500);
                        Console.Clear();
                        inicio();
                        mostrarHorca(horca);
                        mostrarPalabra(palabraOculta);
                    }

                }

            }
                       
 
        }

        static void inicio()
        {
            
            Console.WriteLine("\t"+"    ___    __                              __    ");
            Console.WriteLine("\t" + "   /   |  / /_  ____  ______________ _____/ /___ ");
            Console.WriteLine("\t" + @"  / /| | / __ \/ __ \/ ___/ ___/ __ `/ __  / __ \");
            Console.WriteLine("\t" + " / ___ |/ / / / /_/ / /  / /__/ /_/ / /_/ / /_/ /");
            Console.WriteLine("\t" + @"/_/  |_/_/ /_/\____/_/   \___/\__,_/\__,_/\____/ ");
            Console.WriteLine();



        }
        static void fin()
        {
            
            Console.WriteLine("\n¡¡Siii!! Has salvado al monigote de un mal rato. Dice que gracias por cierto\n");
            Console.Write("\t"+"   O/  \n");
            Console.Write("\t" + "  /|   \n");
            Console.Write("\t" + @"  / \   "+"\n");
            Console.WriteLine("\nPresiona una tecla para salir");
            Console.ReadKey();
        }

        static void ocultarPalabra (string palabra, char[] guiones){
            for ( int i = 0; i < palabra.Length; i++){
                guiones[i] = '-';
            }
        }


        static void mostrarHorca (string[,] array1){   // Mostrar horca
            for (int i = 0; i < array1.GetLength(0); i++){
                Console.Write("\t");
                for (int j = 0; j < array1.GetLength(1); j++){
                    Console.Write(array1[i,j]);
                }
                Console.WriteLine();
            }

        }

        static void mostrarPalabra (char[] palabraOculta){
            Console.WriteLine("\n");
            Console.Write("\t");
            for ( int i = 0; i < palabraOculta.Length; i++){
                Console.Write(palabraOculta[i]);
            }
            Console.WriteLine("\n\n");
        }

        static void dibujarMuñeco (int contador, ref string[,] horca){
            Console.WriteLine();
            
            switch (contador){
                case 1:
                    horca[2,4]= "O"; 
                    Console.WriteLine("Tranquilo, un fallo lo tiene cualquiera.");              
                    break;
                case 2:
                    horca[3,4]= "|";  
                    Console.WriteLine("Bueno, tampoco es el fin del mundo.");          
                    break;
                case 3:
                    horca[3,3]= "/";
                    Console.WriteLine("Vale, la cosa se pone tensa, cuidadito.");               
                    break;
                case 4:
                    horca[3,5]= "|";
                    Console.WriteLine("Mmmm, oye, sin presiones eh, pero ¡solo te quedan dos fallos!.");              
                    break;
                case 5:
                    horca[4,3]= "/"; 
                    Console.WriteLine("La última oportunidad. ¡No metas la pata!, literalmente...");              
                    break;
                case 6:
                    horca[4,5]= @"\";
                    Console.WriteLine("Ok, genial, perfecto, acaban de ahorcar a un monigote por tu ineptitud. ¿¡Estarás contento!?");              
                    break;

            }

        }

    
    }

}
