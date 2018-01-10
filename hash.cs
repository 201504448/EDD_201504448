using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for hash
/// </summary>
public class hash
{

    public int tamanioInicial { get; set; }
    public int porMin { get; set; }
    public int porMax { get; set; }
    public nodoAbb [] arreglo { get; set; }

    public hash()
    {
        tamanioInicial = 43;
        arreglo = new nodoAbb[tamanioInicial];
        for(int i = 0; i<tamanioInicial; i++)
        {//lenar nulo el arreglo
            arreglo[i] = null;
        }
        porMax = 50;
        porMin = 30;
    }

    public string graficar()
    {
        string g = "";
        if(arreglo != null)
        {
            g = "digraph g {\n rankdir=UD; \n node[shape = box]; \n ";
            for(int i = 0; i < arreglo.Length; i++)
            {
                if(arreglo[i] != null)
                {
                    nodoAbb a = arreglo[i];
                    g += "nodo" + i + "[label=\"Usuario: "+a.nickName+" \n Correo: "+a.email+" \n Password: "+a.password+"\"]; \n";
                }
                else
                {
                    g += "nodo" + i + "[label=\" Posicion vacia "+(i+1)+" \"]; \n";
                }
            }
            for (int i = 0; i < arreglo.Length; i++)
            {
                if(i < (arreglo.Length - 1)){
                    g += "nodo" + i + " -> nodo" + (i + 1)+";\n";
                    i = i + 1;//nos pasamos si ibamos en el 0 se pasa al 2
                }
            }
        }
        g += "}\n";
        return g;
    }

    public int funcionHash(string nick)
    {//funcion hash me va a devolver la posicion en el vector
        //funcion de plegamiento
        int pos = 0;
        char[] array = nick.ToCharArray();
        for(int i = 0; i<array.Length; i++)
        {
            pos += (int)array[i];
        }

        pos = pos % tamanioInicial;
        return pos;

    }

    public int rehashing(int posActual)
    {
        int i = 1;
        while(arreglo[posActual] != null)
        {
            i = i * i;
            posActual = posActual + i;
            i++;
        }
        return posActual;
    }

    public int densidad()
    {
        int ocupados = 0;
        if(arreglo != null)
        {
            for(int i = 0; i < arreglo.Length; i++)
            {
                if(arreglo[i] != null)
                {
                    ocupados++;
                }
            }
        }
        int densidad = (ocupados / tamanioInicial) * 100 ;
        return densidad;
    }

    public int ocupados()
    {
        int ocupados = 0;
        if(arreglo != null)
        {
            for(int i = 0; i<arreglo.Length; i++)
            {
                if(arreglo[i] != null)
                {
                    ocupados++;
                }
            }
        }
        return ocupados;
    }


    public void insertar(nodoAbb nodo,nodoAbb [] arreglo)
    {
        if (nodo != null)
        {
            int pos = funcionHash(nodo.nickName);
            if (arreglo[pos] == null)
            {
                arreglo[pos] = nodo;//se ha insertado el nodo en la posicion
                //tengo que ver el porcentaje de ocupacion;
                if(densidad() < porMin || densidad() > porMax)
                {
                    //tengo que hacer mas pequenia la hash
                    int nt = nuevoTamanio(ocupados());
                    //creo un nuevo array
                    nodoAbb[] nuevoArray = new nodoAbb[nt];
                    //tengo que empezar a llenar el nuevo array con los valores que no sean nulos del antiguo array
                    for(int i = 0; i < arreglo.Length; i++)
                    {
                        if(arreglo[i] != null)
                        {
                            //lo empiezo a insertar en el nuevo array
                            insertar(arreglo[i], nuevoArray);
                        }

                    }
                    //ahora tengo que asignar el arreglo al nuevo 
                    this.arreglo = nuevoArray;
                    this.tamanioInicial = nt;
                }

            }
            else
            {
                //tengo que hacer rehashing()
                int nuevaPos = rehashing(pos);
                arreglo[nuevaPos] = nodo;
            }
        }
    }

    public int nuevoTamanio(int ocupados)
    {
        return (ocupados / 35) * 100 ;//siempre que tenga una factor de 35 %
    }
}