using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pilas_completo
{
    internal class pilas
    {
        public class Pilas<T> : Stack<T>
        {
            private int maxSize;
            public Pilas(int maxSize)
            {
                this.maxSize = maxSize;
            }

            public new void Push(T item)
            {
                if (this.Count >= maxSize)
                {
                    throw new Exception("Pila llena");
                }
                base.Push(item);
            }
        }
    }
}
