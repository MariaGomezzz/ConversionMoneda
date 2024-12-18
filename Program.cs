using System;

namespace ConversionMoneda
{
    class Program
    {
        static void Main(string[] args)
        {
            //Definir tasas de cambio
            const decimal TASA_USD = 4000; // 1 USD = 4000
            const decimal TASA_EUR = 4500; // 1 EUR = 4500

            decimal montoCop = SolicitarMonto();

            // Realizar las conversiones
            decimal montoUsd = montoCop / TASA_USD;
            decimal montoEur = montoCop / TASA_EUR;

            // Mostrar los resultados
            Console.WriteLine($"El valor en USD es: {montoUsd:F4}");
            Console.WriteLine($"El valor en EUR es: {montoEur:F4}");

            Console.ReadLine();
        }

        //Método para solicitar el monto en pesos colombianos (COP) al usuario
        static decimal SolicitarMonto()
        {
            decimal montoCop = 0;
            string montoIngresado = "";

            Console.WriteLine("Ingrese el monto en pesos colombianos (COP): ");

            // Bucle que se ejecuta hasta que el usuario ingrese un monto válido
            do
            {
                montoIngresado = Console.ReadLine() ?? "";

                var validacion = ValidarMonto(montoIngresado);

                if (validacion.Item1)
                {
                    montoCop = Decimal.Parse(montoIngresado);
                    break;
                }
                else
                {
                    Console.WriteLine(validacion.Item2);
                }
            }
            while (true);

            return montoCop;
        }

        // Método que valida que el monto ingresado sea un número válido dentro de un rango permitido
        // El monto no puede contener puntos ni comas y debe estar entre 1 y 1.000.000.000 COP
        static (bool, string) ValidarMonto(string monto)
        {
            //Definir rango permitido para el monto ingresado
            const decimal MONTO_MINIMO = 1m;
            const decimal MONTO_MAXIMO = 1000000000m;

            decimal montoCop = 0;
            bool montoValido = false;
            string mensajeError = "";

            if (String.IsNullOrWhiteSpace(monto))
            {
                mensajeError = "Ingrese un monto válido en números";
            }
            else
            {
                if (monto.Contains(".") || monto.Contains(","))
                {
                    mensajeError = "No se permite el uso de puntos(.) o comas (,). Ingrese solo números.";
                }
                else
                {
                    if (Decimal.TryParse(monto, out montoCop))
                    {
                        if (montoCop >= MONTO_MINIMO && montoCop <= MONTO_MAXIMO)
                        {
                            montoValido = true;
                        }
                        else
                        {
                            mensajeError = $"Ingrese un valor entre {MONTO_MINIMO} y {MONTO_MAXIMO}";
                        }
                    }
                    else
                    {
                        mensajeError = $"Ingrese un monto válido en números";
                    }
                }
            }
            return (montoValido, mensajeError);
        }
    }
}