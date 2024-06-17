using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaCotizacion
{
    class Program
    {
        static void Main(string[] args)
        {
            // Datos de la cotización
            Console.WriteLine("Ingrese el número de cotización:");
            string numeroCotizacion = Console.ReadLine();

            Console.WriteLine("Ingrese el nombre del cliente:");
            string nombreCliente = Console.ReadLine();

            Console.WriteLine("Ingrese la dirección del cliente:");
            string direccionCliente = Console.ReadLine();

            DateTime fecha = DateTime.Now;
            Console.WriteLine($"Fecha de la cotización: {fecha}");

            List<Producto> productos = new List<Producto>();
            while (true)
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Eliminar producto");
                Console.WriteLine("3. Finalizar cotización");
                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    // Agregar producto
                    Console.WriteLine("Ingrese el concepto del producto:");
                    string concepto = Console.ReadLine();

                    Console.WriteLine("Ingrese la cantidad:");
                    int cantidad = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el descuento (%):");
                    decimal descuento = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el valor unitario:");
                    decimal valorUnitario = decimal.Parse(Console.ReadLine());

                    productos.Add(new Producto(concepto, cantidad, descuento, valorUnitario));
                }
                else if (opcion == "2")
                {
                    // Eliminar producto
                    Console.WriteLine("Ingrese el índice del producto a eliminar (empezando desde 0):");
                    int indice = int.Parse(Console.ReadLine());
                    if (indice >= 0 && indice < productos.Count)
                    {
                        productos.RemoveAt(indice);
                        Console.WriteLine("Producto eliminado.");
                    }
                    else
                    {
                        Console.WriteLine("Índice inválido.");
                    }
                }
                else if (opcion == "3")
                {
                    // Finalizar cotización
                    break;
                }
                else
                {
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                }
            }

            decimal subTotal = productos.Sum(p => p.Total);
            decimal itbis = subTotal * 0.18m;
            decimal total = subTotal + itbis;

            // Crear el ticket y agregar información
            Ticket ticket = new Ticket();

            ticket.TextoCentro("CG Technology Security");
            ticket.TextoCentro("**********************************");
            ticket.TextoIzquierda("Dirección: c/Juan Rosario #13");
            ticket.TextoIzquierda("San Luis, STO. DGO.");
            ticket.TextoIzquierda("Tel: 829-640'0301");
            ticket.TextoIzquierda("Correo: christophergom.8@gmail.com");
            ticket.TextoIzquierda("RNC: --");
            ticket.TextoIzquierda("");
            ticket.TextoCentro("Factura de Venta");
            ticket.TextoIzquierda($"No Fac: {numeroCotizacion}");
            ticket.TextoIzquierda($"Fecha: {fecha.ToShortDateString()} Hora: {fecha.ToShortTimeString()}");
            ticket.TextoIzquierda($"Cliente: {nombreCliente}");
            ticket.TextoIzquierda($"Dirección cliente: {direccionCliente}");
            ticket.TextoIzquierda("Le Atendió: Christopher Julio");
            ticket.TextoIzquierda("");
            ticket.LineasGuion();
            ticket.EncabezadoVenta();
            ticket.LineasGuion();

            foreach (var producto in productos)
            {
                ticket.AgregaArticulo(producto.Concepto, (double)producto.ValorUnitario, producto.Cantidad, (double)producto.Total);
            }

            ticket.LineasGuion();
            ticket.AgregaTotales("Sub-Total", (double)subTotal);
            ticket.AgregaTotales("Descuento", (double)productos.Sum(p => p.Descuento * p.Cantidad * p.ValorUnitario / 100));
            ticket.AgregaTotales("ITBIS", (double)itbis);
            ticket.TextoIzquierda(" ");
            ticket.AgregaTotales("Total", (double)total);
            ticket.TextoIzquierda(" ");
            ticket.AgregaTotales("Efectivo Entregado:", 0); // Puedes ajustar según la entrada del usuario
            ticket.AgregaTotales("Efectivo Devuelto:", 0);  // Puedes ajustar según la entrada del usuario

            ticket.TextoIzquierda(" ");
            ticket.TextoCentro("**********************************");
            ticket.TextoCentro("*     Gracias por preferirnos    *");
            ticket.TextoCentro("**********************************");
            ticket.TextoIzquierda(" ");

            // Guardar el ticket en un archivo de texto en el escritorio
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = Path.Combine(desktopPath, $"Cotizacion_{numeroCotizacion}.txt");
            ticket.ImprimirTicket(filePath);

            Console.WriteLine($"La cotización se ha guardado en: {filePath}");
            Console.ReadKey();
        }
    }
}