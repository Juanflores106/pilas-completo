using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pilas_completo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string infija = "";
            while (true)
            {
                Console.WriteLine("1. Cargar Expresión");
                Console.WriteLine("2. Convertir a Posfija");
                Console.WriteLine("3. Convertir a Prefija");
                Console.WriteLine("4. Evaluar");
                Console.WriteLine("5. Salir");
                Console.Write("Elige una opción: ");
                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Introduce la expresión infija: ");
                        infija = Console.ReadLine();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Expresión posfija: " + InfijaAPosfija(infija));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Expresión prefija: " + InfijaAPrefija(infija));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            string posfija = InfijaAPosfija(infija);
                            Console.WriteLine("Resultado: " + EvaluarPosfija(posfija));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        break;
                    case 5:
                        return;
                }
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static string InfijaAPosfija(string infija)
        {
            pilas.Pilas<char> stack = new pilas.Pilas<char>(30);
            string posfija = "";
            foreach (char c in infija)
            {
                if (Char.IsDigit(c))
                {
                    if (c < '0' || c > '9')
                    {
                        throw new Exception("Número fuera de rango: " + c);
                    }
                    posfija += c;
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        posfija += stack.Pop();
                    }
                    if (stack.Count == 0 || stack.Peek() != '(')
                    {
                        throw new Exception("Paréntesis desequilibrados");
                    }
                    stack.Pop(); // Eliminar el paréntesis de apertura
                }
                else
                {
                    while (stack.Count > 0 && Precedencia(c) <= Precedencia(stack.Peek()))
                    {
                        posfija += stack.Pop();
                    }
                    stack.Push(c);
                }
            }
            while (stack.Count > 0)
            {
                if (stack.Peek() == '(')
                {
                    throw new Exception("Paréntesis desequilibrados");
                }
                posfija += stack.Pop();
            }
            return posfija;
        }

        static string InfijaAPrefija(string infija)
        {
            string reversa = new string(infija.Reverse().ToArray());
            string posfija = InfijaAPosfija(reversa);
            string prefija = new string(posfija.Reverse().ToArray());
            return prefija;
        }

        static int EvaluarPosfija(string posfija)
        {
            pilas.Pilas<int> stack = new pilas.Pilas<int>(30);
            foreach (char c in posfija)
            {
                if (Char.IsDigit(c))
                {
                    if (c < '0' || c > '9')
                    {
                        throw new Exception("Número fuera de rango: " + c);
                    }
                    stack.Push(c - '0');
                }
                else
                {
                    if (stack.Count < 2)
                    {
                        throw new Exception("Expresión posfija mal formada");
                    }
                    int a = stack.Pop();
                    int b = stack.Pop();
                    switch (c)
                    {
                        case '+':
                            stack.Push(a + b);
                            break;
                        case '-':
                            stack.Push(b - a);
                            break;
                        case '*':
                            stack.Push(a * b);
                            break;
                        case '/':
                            if (a == 0)
                            {
                                throw new Exception("División por cero");
                            }
                            stack.Push(b / a);
                            break;
                        case '^':
                            stack.Push((int)Math.Pow(b, a));
                            break;
                        default:
                            throw new Exception("Operador desconocido: " + c);
                    }
                }
            }
            if (stack.Count != 1)
            {
                throw new Exception("Expresión posfija mal formada");
            }
            return stack.Pop();
        }

        static int Precedencia(char operador)
        {
            switch (operador)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default:
                    return -1;
            }
        }
    }
}
