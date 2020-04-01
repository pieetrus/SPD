
namespace Carlier
{
    /// <summary>
    /// Klasa przechowująca dane na temat zadania
    /// </summary>
    public class Task
    {
        public int  nr { get; set; } //numer zadania
        public int r { get; set; } // termin dostępności
        public int p { get; set; } // czas wykonywania zadania
        public int q { get; set; } // czas dostarczenia
        public int s { get; set; } // termin rozpoczęcia zadania

        public int c => s + p;// termin zakończenia zadania
        public int d => c + q;// termin dostarczenia zadania
    }
}
