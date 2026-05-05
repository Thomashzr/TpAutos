namespace TP_2_Autos.Modelos
{
    // ════════════════════════════════════════════════════════════════════════════
    // Nodo<T> — elemento base de las estructuras enlazadas
    // ════════════════════════════════════════════════════════════════════════════
    internal class Nodo<T>
    {
        public T      Dato      { get; set; }
        public Nodo<T>? Siguiente { get; set; }

        public Nodo(T dato)
        {
            Dato      = dato;
            Siguiente = null;
        }
    }

    // ════════════════════════════════════════════════════════════════════════════
    // Pila<T> — estructura LIFO (Last In, First Out)
    // Usos en este TP:
    //   - Historial de operaciones del menú (Program.historial)
    //   - Validación de paréntesis en textos
    //   - Listados en orden inverso
    // ════════════════════════════════════════════════════════════════════════════
    internal class Pila<T>
    {
        private Nodo<T>? _cima;
        public  int      Cantidad { get; private set; }

        public Pila()
        {
            _cima    = null;
            Cantidad = 0;
        }

        /// <summary>Apila un elemento en la cima.</summary>
        public void Push(T dato)
        {
            var nuevo = new Nodo<T>(dato);
            nuevo.Siguiente = _cima;
            _cima    = nuevo;
            Cantidad++;
        }

        /// <summary>Desapila y devuelve el elemento de la cima. Lanza excepción si está vacía.</summary>
        public T Pop()
        {
            if (EstaVacia()) throw new InvalidOperationException("La pila está vacía.");
            T dato = _cima!.Dato;
            _cima  = _cima.Siguiente;
            Cantidad--;
            return dato;
        }

        /// <summary>Devuelve el elemento de la cima sin desapilarlo.</summary>
        public T Peek()
        {
            if (EstaVacia()) throw new InvalidOperationException("La pila está vacía.");
            return _cima!.Dato;
        }

        public bool EstaVacia() => _cima == null;
    }

    // ════════════════════════════════════════════════════════════════════════════
    // Cola<T> — estructura FIFO (First In, First Out)
    // Usos en este TP:
    //   - Cola de vehículos disponibles para una fecha determinada
    //   - Procesamiento ordenado de reservas
    // ════════════════════════════════════════════════════════════════════════════
    internal class Cola<T>
    {
        private Nodo<T>? _frente;
        private Nodo<T>? _fondo;
        public  int      Cantidad { get; private set; }

        public Cola()
        {
            _frente  = null;
            _fondo   = null;
            Cantidad = 0;
        }

        /// <summary>Encola un elemento al fondo.</summary>
        public void Enqueue(T dato)
        {
            var nuevo = new Nodo<T>(dato);
            if (_fondo != null) _fondo.Siguiente = nuevo;
            _fondo = nuevo;
            if (_frente == null) _frente = nuevo;
            Cantidad++;
        }

        /// <summary>Desencola y devuelve el elemento del frente. Lanza excepción si está vacía.</summary>
        public T Dequeue()
        {
            if (EstaVacia()) throw new InvalidOperationException("La cola está vacía.");
            T dato  = _frente!.Dato;
            _frente = _frente.Siguiente;
            if (_frente == null) _fondo = null;
            Cantidad--;
            return dato;
        }

        /// <summary>Devuelve el elemento del frente sin desencolarlo.</summary>
        public T Peek()
        {
            if (EstaVacia()) throw new InvalidOperationException("La cola está vacía.");
            return _frente!.Dato;
        }

        public bool EstaVacia() => _frente == null;
    }
}
