using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ArbolBinario
{
    class Nodo_Arbol
    {
        public int info;                //Dato a almacenar en el nodo
        public Nodo_Arbol Izquierdo;    //Nodo izquierdo del arbol
        public Nodo_Arbol Derecho;      //Nodo derecho del arbol
        public Nodo_Arbol Padre;        //Nodo ariz del arbol
        public int altura;
        public int nivel;
        public Rectangle nodo;          //Para dibujar el nodo del arbol

        private ArbolBinario arbol;     //Declarando un objeto del tipo ArbolBinario

        public Nodo_Arbol()             //Constructor por defecto
        {

        }

        public ArbolBinario Arbol       //Constructor por defecto para el objeto de tipo Arbol
        {
            get{ return arbol;}
            set { arbol = value;}
        }

        //Constructor con parametros
        public Nodo_Arbol(int nueva_info, Nodo_Arbol izquierdo, Nodo_Arbol derecho, Nodo_Arbol padre)
        {
            info = nueva_info;
            Izquierdo = izquierdo;
            Derecho = derecho;
            Padre = padre;
            altura = 0;
        }

        //Funcion para insertar un nodo en el ArbolBinario
        public Nodo_Arbol Insertar(int x, Nodo_Arbol t, int Level)
        {
            if (t == null)
            {
                t = new Nodo_Arbol(x, null, null, null);
                t.nivel = Level;
            }

            else if (x < t.info)
            {
                Level++;
                t.Izquierdo = Insertar(x, t.Izquierdo, Level);
            }

            else if (x > t.info)
            {
                Level++;
                t.Derecho = Insertar(x, t.Derecho, Level);
            }
            
            else
            {
                MessageBox.Show("Dato Existente en el Árbol", "Error de ingreso.");
            }

            return t;
        }

        //Funcion para eliminar un nodo del ArbolBinario
        public void Eliminar(int x, ref Nodo_Arbol t)
        {
            if (t != null) //Si la raiz es distinta de null
            {
                if(x< t.info)            //Si el valor a eliminar es menor que la raiz
                {
                    Eliminar(x, ref t.Izquierdo);
                }
                else
                {
                    if (x > t.info)       //Si el valor a eliminar es mayor que la raiz
                    {
                        Eliminar(x, ref t.Derecho);
                    }

                    else
                    {
                        Nodo_Arbol NodoEliminar = t;      //Ya se ubico el nodo que se desea eliminar
                        if (NodoEliminar.Derecho == null) //Se verifica si tiene hijo derecho
                        {
                            t = NodoEliminar.Izquierdo;
                        }
                        else
                        {
                            if (NodoEliminar.Izquierdo == null) //Se verifica si tiene hijo izquierdo
                            {
                                t = NodoEliminar.Derecho;
                            }
                            else
                            {
                                if (Alturas(t.Izquierdo) - Alturas(t.Derecho) > 0)
                                {
                                    //Para verificar que hijo pasa a ser nueva raiz del subarbol
                                    Nodo_Arbol AuxiliarNodo = null;
                                    Nodo_Arbol Auxiliar = t.Izquierdo;
                                    bool Bandera = false;

                                    while (Auxiliar.Derecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.Derecho;
                                        Bandera = true;
                                    }

                                    t.info = Auxiliar.info; //Se crea un nodo temporal
                                    NodoEliminar = Auxiliar;

                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.Derecho = Auxiliar.Izquierdo;
                                    }
                                    else
                                    {
                                        t.Izquierdo = Auxiliar.Izquierdo;
                                    }
                                }
                                else
                                {
                                    if (Alturas(t.Derecho) - Alturas(t.Izquierdo) > 0)
                                    {
                                        Nodo_Arbol AuxiliarNodo = null;
                                        Nodo_Arbol Auxiliar = t.Derecho;
                                        bool Bandera = false;

                                        while (Auxiliar.Izquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.Izquierdo;
                                            Bandera = true;
                                        }

                                        t.info = Auxiliar.info;
                                        NodoEliminar = Auxiliar;

                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.Izquierdo = Auxiliar.Derecho;
                                        }

                                        else
                                        {
                                            t.Derecho = Auxiliar.Derecho;
                                        }
                                    }
                                    else
                                    {
                                        if (Alturas(t.Derecho) - Alturas(t.Izquierdo) == 0)
                                        {
                                            Nodo_Arbol AuxiliarNodo = null;
                                            Nodo_Arbol Auxiliar = t.Izquierdo;
                                            bool Bandera = false;

                                            while (Auxiliar.Derecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.Derecho;
                                                Bandera = true;
                                            }

                                            t.info = Auxiliar.info;
                                            NodoEliminar = Auxiliar;

                                            if (Bandera == true)
                                            {
                                                AuxiliarNodo.Derecho = Auxiliar.Izquierdo;
                                            }
                                            else
                                            {
                                                t.Izquierdo = Auxiliar.Izquierdo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nodo No Existente en el Árbol", "Error de eliminación.");
            }
        }
        //Final de la funcion eliminar

        //Funcion para recorrer el Arbol Binario (Busqueda de un nodo)
        public void Buscar(int x, Nodo_Arbol t)
        {
            if (t != null)
            {
                if(x == t.info)
                {
                    MessageBox.Show("Nodo Encontrado en la posición X: "+ t.CoordenadaX + ", Y: " + t.CoordenadaY);

                    encontrado(t);
                }
                else
                {
                    if(x < t.info)
                    {
                        Buscar(x, t.Izquierdo);
                    }
                    else
                    {
                        if(x > t.info)
                        {
                            Buscar(x, t.Derecho);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nodo No Encontrado en el Árbol", "Error de Búsqueda");
            }
        }

        //Funcion para el dibujo de los nodos del Arvol Binario en el formulario


        //Variables que define el tamaño de los circulos que representaran los nodos del arbol
        private const int Radio = 30; 
        private const int DistanciaH = 80;  //Variable para manejar distancia horizontal
        private const int DistanciaV = 10;  //Variable para manejar distancia vertical

        private int CoordenadaX;   //Variable para manejar posición Eje X
        private int CoordenadaY;   //Variable para manejar posición Eje Y


        //Funcion para encontrar la posición donde se creará el nodo
        public void PosicionNodo (ref int xmin, int ymin)
        {
            int aux1, aux2;
            CoordenadaY = (int)(ymin + Radio / 2);

            if (Izquierdo != null)
            {
                Izquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }

            if((Izquierdo!=null)&&(Derecho != null))
            {
                xmin += DistanciaH;
            }


            //Si existe el nodo derecho y el nodo izquierdo deja un espacio entre ellos 
            if (Derecho != null)
            {
                Derecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }

            if(Izquierdo!=null&& Derecho != null)
            {
                CoordenadaX = (int)((Izquierdo.CoordenadaX + Derecho.CoordenadaX) / 2);
                
            }
            else if (Izquierdo != null)
            {
                aux1 = Izquierdo.CoordenadaX;
                Izquierdo.CoordenadaX = CoordenadaX - 80;
                CoordenadaX = aux1;
            }
            else if (Derecho != null)
            {
                aux2 = Derecho.CoordenadaX;
                //No hay nodo izquierdo, centrar el nodo a la derecha
                Derecho.CoordenadaX = CoordenadaX + 80;
                CoordenadaX = aux2;
            }
            else
            {
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }

        //Funcion para dibujar las ramas de los nodos izquiero y derecho
        public void DibujarRamas (Graphics grafo, Pen Lapiz)
        {
            if (Izquierdo != null) //Dibuja rama izquierda
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, Izquierdo.CoordenadaX, Izquierdo.CoordenadaY);
                Izquierdo.DibujarRamas(grafo, Lapiz);
            }

            if(Derecho!= null) //Dibuja rama derecha
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, Derecho.CoordenadaX, Derecho.CoordenadaY);
                Derecho.DibujarRamas(grafo, Lapiz);
            }
        }


        //Funcion para dibujar el nodo en la posición especifica
        public void DibujarNodo(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Brush encuentro)
        {
            //Dibuja contorno del nodo
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);

            this.nodo = rect;
            grafo.FillEllipse(encuentro, rect);
            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);

            //Para dibujar el nombre del nodo, o sea, el contenido.
            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(info.ToString(), fuente, RellenoFuente, CoordenadaX, CoordenadaY, formato);

            //Dibuja los nodos hijos derecho e izquierdo
            if(Izquierdo!= null)
            {
                Izquierdo.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
            }

            if (Derecho != null)
            {
                Derecho.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
            }
        }
        //Funcion para dar color al nodo del Arbol Binario
        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz)
        {   
            //Dibuja el contorno del nodo
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);

            //Dibuja el contenido del nodo
            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(info.ToString(), fuente, RellenoFuente, CoordenadaX, CoordenadaY, formato);

        }

        //Funcion para calcular la altura de un nodo en el Arbol Binario
        private static int Alturas(Nodo_Arbol t)
        {
            return t == null ? -1 : t.altura;
        }

        //Da las coordenadas de donde se encuentra un nodo al buscarlo.
        public void encontrado(Nodo_Arbol t)
        {
            Rectangle rec = new Rectangle(t.CoordenadaX, t.CoordenadaY, 40, 40);
        }

    }
}
