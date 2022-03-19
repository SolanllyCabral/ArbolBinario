using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArbolBinario
{
    public partial class FormularioArbol : Form
    {
        public FormularioArbol()
        {
            InitializeComponent();
        }

        //Desabilitadores y habilitadores
        protected void habilitarButtons()
        {
            btnPreorden.Enabled = true;
            btnPostorden.Enabled = true;
            btnInorden.Enabled = true;
        }

        protected void deshabilitarButtons()
        {
            btnPreorden.Enabled = false;
            btnPostorden.Enabled = false;
            btnInorden.Enabled = false;
        }

        //Declaracion de variables a utilizar
        int Dato = 0;
        int cont = 0;

        ArbolBinario mi_Arbol = new ArbolBinario(null);  //Creacion del objeto Arbol
        Graphics g;     // Definion del objeto grafico


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Evento del formulario que permitirá dibujar el Arbol binario
        private void FormularioArbol_Paint(object sender, PaintEventArgs en)
        {
            en.Graphics.Clear(this.BackColor);
            en.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            en.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = en.Graphics;

            mi_Arbol.DibujarArbol(g, this.Font, Brushes.Salmon, Brushes.Black, Pens.Black, Brushes.White);
        }

        private void btnInsertar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDato.Text))
            {
                MessageBox.Show("Por favor ingrese un dato.");
                txtDato.Focus();
                return;

            }
            else
            {
                Dato = int.Parse(txtDato.Text);
                mi_Arbol.Insertar(Dato);

                txtDato.Clear();
                txtDato.Focus();

                cont++;

                //lblAltura.Text = "Altura: " + mi_Arbol.calcularAltura(mi_Arbol.Raiz);
                //lblAltura.Visible = true;
            }

            habilitarButtons();
            Refresh();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDato.Text))
            {
                MessageBox.Show("No existe un dato que eliminar");
                txtDato.Focus();
                return;

            }
            else
            {
                Dato = int.Parse(txtDato.Text);
                mi_Arbol.Eliminar(Dato);

                txtDato.Clear();
                txtDato.Focus();

                cont--;

                if (cont == 0)
                {
                    deshabilitarButtons();
                    lblAltura.Visible = false;
                }
            }
            Refresh();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDato.Text))
            {
                MessageBox.Show("Por favor agregue el dato que desea buscar.");
                txtDato.Focus();
                return;

            }
            else
            {
                Dato = int.Parse(txtDato.Text);

                mi_Arbol.Buscar(Dato);

                txtDato.Clear();
                txtDato.Focus();

            }

            Refresh();
        }

        private void btnPreorden_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDato.Text))
            {
                MessageBox.Show("No hay datos en el Arbol");
                txtDato.Focus();
                return;

            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                Refresh();
                txtDato.Clear();
                deshabilitarButtons();
                g = this.CreateGraphics();
                mi_Arbol.colorear(g, this.Font, Brushes.Blue, Brushes.White, Pens.Black, mi_Arbol.Raiz, false, false, true);
                habilitarButtons();
                this.Cursor = Cursors.Default;
            }
        }

        private void btnInorden_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDato.Text))
            {
                MessageBox.Show("No hay datos en el Arbol");
                txtDato.Focus();
                return;

            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                Refresh();
                txtDato.Clear();
                deshabilitarButtons();
                g = this.CreateGraphics();
                mi_Arbol.colorear(g, this.Font, Brushes.Blue, Brushes.White, Pens.Black, mi_Arbol.Raiz, false, true, false);
                habilitarButtons();
                this.Cursor = Cursors.Default;
            }
        }

        private void btnPostorden_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDato.Text))
            {
                MessageBox.Show("No hay datos en el Arbol");
                txtDato.Focus();
                return;

            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                Refresh();
                txtDato.Clear();
                deshabilitarButtons();
                g = this.CreateGraphics();
                mi_Arbol.colorear(g, this.Font, Brushes.Blue, Brushes.White, Pens.Black, mi_Arbol.Raiz, true, false, false);
                habilitarButtons();
                this.Cursor = Cursors.Default;
            }
        }

        private void FormularioArbol_MouseClick(object sender, MouseEventArgs e)
        {
            int valor = 100;
            Refresh();
            txtDato.Clear();
            g = this.CreateGraphics();
            valor = mi_Arbol.seleccionar(g, this.Font, Brushes.White, Pens.Black, mi_Arbol.Raiz, e.Location);
            if (valor != 100)
            {
                txtDato.Text = valor.ToString();
            }
        }

        private void txtDato_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '-'))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtDato_TextChanged(object sender, EventArgs e)
        {
            Refresh();
            if (txtDato.Text != String.Empty && txtDato.Text != "-")
            {
                try
                {
                    int num;
                    num = Convert.ToInt32(txtDato.Text);
                    if (num > 99 || num < -99)
                    {
                        txtDato.BackColor = Color.Orange;
                        btnInsertar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnBuscar.Enabled = false;
                        MessageBox.Show("No se admiten numeros tan grandes", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (mi_Arbol.existe(num, mi_Arbol.Raiz))
                        {
                            g = this.CreateGraphics();
                            btnInsertar.Enabled = false;
                            btnEliminar.Enabled = true;
                            btnBuscar.Enabled = false;
                            mi_Arbol.resaltar(g, this.Font, Brushes.Blue, Brushes.White, Pens.Black, mi_Arbol.Raiz, num);
                            txtDato.BackColor = Color.Red;
                        }
                        else
                        {
                            btnInsertar.Enabled = true;
                            btnEliminar.Enabled = false;
                            btnBuscar.Enabled = true;
                            txtDato.BackColor = Color.Green;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("No se admiten numeros tan grandes 2", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDato.BackColor = Color.Orange;
                }
            }
            else
            {
                txtDato.BackColor = Color.Red;
                btnInsertar.Enabled = false;
                btnEliminar.Enabled = false;
                btnBuscar.Enabled = false;
            }
        }
    }
}

