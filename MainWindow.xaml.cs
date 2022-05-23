using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CRUD
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mostrar_Datos();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities1 entidades = new BDTIENDAEntities1();
            PRODUCTO p = new PRODUCTO();
            if (TextoProducto.Text == "")
            {
                MessageBox.Show("Tiene que introducir el nombre del producto");
            }
            else
            {
                p.NOMBRE = TextoProducto.Text;
                try
                {
                    p.PRECIO = TextoPrecio.Text;
                    p.EXISTENCIAS = TextoExistencias.Text;
                    
                    entidades.PRODUCTO.Add(p);
                    entidades.SaveChanges();
                    
                    MessageBox.Show("Producto Añadido");
                    limpiar_textbox();
                    Mostrar_Datos();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Formato de precio no válido");
                }
            }
        }
        private void limpiar_textbox()
        {
            TextoID.Clear();
            TextoProducto.Clear();
            TextoExistencias.Clear();
            TextoPrecio.Clear();
        }




        private void Mostrar_Datos()
        {
            BDTIENDAEntities1 entidades = new BDTIENDAEntities1();
            List<PRODUCTO> productos = entidades.PRODUCTO.ToList<PRODUCTO>();
            for (int i = 0; i < productos.Count; i++) { 
            Lista.ItemsSource= productos;
            Lista.Items.Refresh();

            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities1 entidades = new BDTIENDAEntities1();
            try
            {
                PRODUCTO producto = entidades.PRODUCTO.ToList<PRODUCTO>().Where(c => c.ID == int.Parse(TextoID.Text.ToString())).FirstOrDefault<PRODUCTO>();

                if (producto == null)
                {
                    MessageBox.Show("No se encontró el producto");
                }
                else
                {
                    producto.NOMBRE = TextoProducto.Text;
                    producto.PRECIO = TextoPrecio.Text;
                    producto.EXISTENCIAS = TextoExistencias.Text;
                    entidades.SaveChanges();

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("No has introducido el ID correctamente");
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities1 entidades = new BDTIENDAEntities1();
            PRODUCTO producto = entidades.PRODUCTO.ToList<PRODUCTO>().Where(c => c.ID == int.Parse(TextoID.Text.ToString())).FirstOrDefault<PRODUCTO>();
            if (producto == null)
            {
                MessageBox.Show("No se encontró el producto");
            }
            else
            {
                try
                {
                    entidades.PRODUCTO.Remove(producto);
                    entidades.SaveChanges();
                    Mostrar_Datos();
                    MessageBox.Show("Producto Borrado");

                }
                catch
                {
                    MessageBox.Show("Hubo algún problema");
                }
            }
        }


        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities1 entidades = new BDTIENDAEntities1();
            try
            {
                PRODUCTO producto = entidades.PRODUCTO.ToList<PRODUCTO>().Where(c => c.ID == int.Parse(TextoID.Text.ToString())).FirstOrDefault<PRODUCTO>();

                if (producto == null)
                {
                    MessageBox.Show("No se encontró el producto");
                }
                else
                {

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Has introducido incorrectamente el ID");
            }
        }
        private void mostrarproducto() {


            

        }
    
    
   
    }
}
