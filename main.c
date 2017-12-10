#include <stdio.h>

#include <malloc.h>

typedef struct Nodo {
    int dato;
    struct Nodo *sigte;
    struct Nodo *anterior;
} nodo;

nodo *primero = NULL;
nodo *ultimo = NULL;

void insertar(int dato);

void mostrar();

void eliminar(int dato);

int main()
{
    inicio();
    return 0;

}

void inicio(){
    int opcion = 0;
    int valor = 0;
    while((opcion = menu()) < 4){
        switch (opcion) {
        case 1:
            printf("Ingrese un numero ENTERO: \n ");
            scanf("%d",&valor);
            insertar(valor);
            break;
        case 2:
            printf("Datos en la lista: \n ");
            mostrar();
            break;
        case 3:
            printf("Ingrese un dato a eliminar: \n ");
            scanf("%d",&valor);
            eliminar(valor);
            break;

        default:
            break;
        }
    }

}

int menu(){
    int opcion = 0;
    printf("Bienvenido, seleccione una opcion: \n");
    printf("1. Insertar.\n2. Mostrar Datos.\n3. Eliminar Dato.\n");
    scanf("%d",&opcion);
    return opcion;

}

void insertar(int dato){
    nodo *nuevo = (nodo*)malloc(sizeof(nodo));
    nuevo->dato = dato;
    if(primero == NULL){
        primero = nuevo;
        ultimo = nuevo;
        ultimo->sigte = NULL;

    }else{
        ultimo->sigte = nuevo;
        nuevo->anterior = ultimo;
        ultimo = nuevo;
        ultimo->sigte = NULL;
    }

}

void mostrar(){
    if(primero != NULL){
        nodo *aux = primero;
        while(aux != NULL){
            printf("Dato: %d\n",aux->dato);
            aux = aux->sigte;
        }
    }else{
        printf("Lista vacía. \n");
    }
}




void eliminar(int dato){
    if(primero != NULL){
        nodo *aux  = NULL;
        if(primero->sigte == NULL){
            if(primero->dato == dato){
                primero = NULL;
                ultimo = NULL;
                free(primero);
                free(ultimo);
            }
        }
        else if(ultimo->dato == dato){
            aux = ultimo;
            ultimo->anterior->sigte = NULL;
            free(ultimo);
            ultimo = aux->anterior;
            ultimo->sigte = NULL;

        }
        else if(primero->dato == dato){
            if(primero->sigte != NULL){
                aux = primero;
                primero->sigte->anterior = NULL;
                free(primero);
                primero = aux->sigte;
                primero->anterior = NULL;
            }else{
                primero = NULL;
                ultimo = NULL;
                free(primero);
                free(ultimo);
            }
        }
        else
        {
            aux = primero;
            nodo *anterior  = NULL;
            nodo *sigte = NULL;
            int encontrado = 0;
            while(aux != NULL){
                if(aux->dato == dato){
                    anterior = aux->anterior;
                    sigte = aux->sigte;
                    encontrado = 1;
                    break;
                }
                aux = aux->sigte;
            }
            if(encontrado == 1){
                if(anterior != NULL && sigte != NULL)
                {
                    anterior->sigte = sigte;
                    sigte->anterior = anterior;
                    aux = NULL;
                    free(aux);
                }
            }


        }
    }
}
