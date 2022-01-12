using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CRUD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mostrar_Datos();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities entidades = new BDTIENDAEntities();
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
                    p.PRECIO = int.Parse(TextoPrecio.Text);
                    entidades.PRODUCTO.Add(p);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Formato de precio no válido");
                }
                try
                {
                    entidades.SaveChanges();
                    Mostrar_Datos();
                    MessageBox.Show("Producto Añadido");
                    limpiar_textbox();
                }
                catch
                {
                    MessageBox.Show("Se ha producido algún tipo de error, probablemente con la Base de Datos");
                }
            }
        }

        private void Mostrar_Datos()
        {
            BDTIENDAEntities entidades = new BDTIENDAEntities();
            List<PRODUCTO> productos = entidades.PRODUCTO.ToList<PRODUCTO>();
            Listado.ItemsSource = productos;
            Listado.Items.Refresh();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities entidades = new BDTIENDAEntities();
            try
            {
                PRODUCTO producto = entidades.PRODUCTO.ToList<PRODUCTO>().Where(c => c.ID == int.Parse(TextoID.Text)).FirstOrDefault<PRODUCTO>();

                if (producto == null)
                {
                    MessageBox.Show("No se encontró el producto");
                }
                else
                {
                    producto.NOMBRE = TextoProducto.Text;
                    producto.PRECIO = int.Parse(TextoPrecio.Text);
                    entidades.SaveChanges();
                    Mostrar_Datos();
                    limpiar_textbox();
                    MessageBox.Show("Cambios realizados correctamente");

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("No has introducido el ID correctamente");
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities entidades = new BDTIENDAEntities();
            PRODUCTO producto = entidades.PRODUCTO.ToList<PRODUCTO>().Where(c => c.ID == int.Parse(TextoID.Text)).FirstOrDefault<PRODUCTO>();
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
                    limpiar_textbox();
                    MessageBox.Show("Producto Eliminado");
                }
                catch
                {
                    MessageBox.Show("Hubo algún problema");
                }
            }
        }

        private void limpiar_textbox()
        {
            TextoProducto.Clear();
            TextoID.Clear();
            TextoPrecio.Clear();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BDTIENDAEntities entidades = new BDTIENDAEntities();
            try
            {
                PRODUCTO producto = entidades.PRODUCTO.ToList<PRODUCTO>().Where(c => c.ID == int.Parse(TextoID.Text)).FirstOrDefault<PRODUCTO>();

                if (producto == null)
                {
                    MessageBox.Show("No se encontró el producto");
                }
                else
                {
                    TextoProducto.Text = producto.NOMBRE;
                    TextoPrecio.Text = Convert.ToString(producto.PRECIO);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Has introducido incorrectamente el ID");
            }
        }
    }
}
