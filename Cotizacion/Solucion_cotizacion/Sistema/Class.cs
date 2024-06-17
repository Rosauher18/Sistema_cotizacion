using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Ticket
{
    private StringBuilder sb;

    public Ticket()
    {
        sb = new StringBuilder();
    }

    public void TextoCentro(string texto)
    {
        sb.AppendLine(texto.PadLeft((40 + texto.Length) / 2).PadRight(40));
    }

    public void TextoIzquierda(string texto)
    {
        sb.AppendLine(texto);
    }

    public void LineasGuion()
    {
        sb.AppendLine(new string('-', 40));
    }

    public void EncabezadoVenta()
    {
        sb.AppendLine("Articulo           Precio   Cantidad  Subtotal");
    }

    public void AgregaArticulo(string articulo, double precio, int cantidad, double subtotal)
    {
        sb.AppendLine($"{articulo.PadRight(20).Substring(0, 20)}{precio.ToString("F2").PadLeft(8)}{cantidad.ToString().PadLeft(8)}{subtotal.ToString("F2").PadLeft(10)}");
    }

    public void AgregaTotales(string titulo, double total)
    {
        sb.AppendLine($"{titulo.PadRight(30)}{total.ToString("F2").PadLeft(10)}");
    }

    public void ImprimirTicket(string filePath)
    {
        File.WriteAllText(filePath, sb.ToString());
    }
}
class Producto
{
    public string Concepto { get; set; }
    public int Cantidad { get; set; }
    public decimal Descuento { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Total
    {
        get
        {
            decimal totalBruto = Cantidad * ValorUnitario;
            decimal descuentoAplicado = totalBruto * (Descuento / 100);
            return totalBruto - descuentoAplicado;
        }
    }

    public Producto(string concepto, int cantidad, decimal descuento, decimal valorUnitario)
    {
        Concepto = concepto;
        Cantidad = cantidad;
        Descuento = descuento;
        ValorUnitario = valorUnitario;
    }
}