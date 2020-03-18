using System;
using System.Collections.Generic;
using System.Text;

namespace lab2_Schrage
{
    /// <summary>
    /// Klasa przechowująca dane na temat zadania
    /// </summary>
    public class Task
    {
        public int r { get; set; } // termin dostępności
        public int p { get; set; } // czas wykonywania zadania
        public int q { get; set; } // czas dostarczenia

        //public int c { get; set; } // termin zakończenia zadania
        //public int s { get; set; } // termin rozpoczęcia zadania
        //public int d { get; set; } // termin dostarczenia zadania
    }
}
